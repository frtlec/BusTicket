
using BusTicket.Adapter.Dtos;
using BusTicket.Adapter.ExternalServices;
using BusTicket.Adapter.Wrappers;
using BusTicket.Business.Caching;
using BusTicket.Shared.Dtos;
using BusTicket.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business
{
    public interface ISessionStore
    {
        public Task<ResponseWrapper<string>> CreateSessionToken();
        public string Encode(string sessionId, string deviceId);
        public (string, string) Decode(string token);
    }
    public class SessionStore : ISessionStore
    {
        private readonly IOBiletIntegration _integration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IBusTicketCache _cache;

        public SessionStore(IOBiletIntegration integration, IHttpContextAccessor httpContextAccessor, IBusTicketCache cache)
        {
            _integration = integration;
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }
        /// <summary>
        /// This method retrieves Session ID and Device ID information from the obilet session API and converts it to a basic token.
        /// </summary>
        public async Task<ResponseWrapper<string>> CreateSessionToken()
        {
            var ipAddress = _httpContextAccessor.GetClientIpAddress();
            var port = _httpContextAccessor.GetClientPort();
            (string, string) browserInfos = _httpContextAccessor.GetBrowserInfos();
            string key = GetKey(ipAddress, port, browserInfos.Item1, browserInfos.Item2);

            (bool, string) cacheData = _cache.Get<string>(key);
            if (cacheData.Item1)
            {
                //Token in the cache.
                return ResponseWrapper<string>.Success(cacheData.Item2);
            }


            GetSessionRequestDto getSessionRequestDto = new GetSessionRequestDto()
            {
                Browser = new GetSessionRequestDto.BrowserModel
                {
                    Name = browserInfos.Item1,
                    Version = browserInfos.Item2,
                },
                Connection = new GetSessionRequestDto.ConnectionModel
                {
                    IpAddress = ipAddress,
                    Port = port
                }
            };
            OBiletResponseWrapper<GetSessionResponseDto> getSession = await _integration.GetSessionsAsync(getSessionRequestDto);
            if (getSession.IsSuccessful == false)
            {
                return ResponseWrapper<string>.Warning(getSession.Message);
            }
            string token = $"{getSession.Response.Data.SessionId}:{getSession.Response.Data.DeviceId}".EncodeBase64();

            _cache.Set(key, token, 60 * 60 * 24);
            return ResponseWrapper<string>.Success(token);
        }
        /// <summary>
        /// It receives basic tokens and returns Session IDs and Device IDs.
        /// Item1 SessionId 
        /// Item2 DeviceId
        /// </summary>
        public (string,string) Decode(string token)
        {
            string decodeBase64 = token.DecodeBase64();
            string[] split = decodeBase64.Split(':');
            return (split[0], split[1]);
        }
        /// <summary>
        /// It creates a base64 encoding with SessionID and DeviceID with a ':' between them.
        /// </summary>
        public string Encode(string sessionId, string deviceId)
        {
            return $"{sessionId}:{deviceId}".EncodeBase64();
        }

        private string GetKey(string ipaddress, string port, string browserName, string version)
        {
            return $"{ipaddress.Replace(".", string.Empty)}_{port}_{browserName}_{version.Replace(".", string.Empty)}";
        }
    }
}
