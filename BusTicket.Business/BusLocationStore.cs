using BusTicket.Adapter.Dtos;
using BusTicket.Adapter.ExternalServices;
using BusTicket.Adapter.Wrappers;
using BusTicket.Business.Caching;
using BusTicket.Business.Dtos;
using BusTicket.Shared.Dtos;
using BusTicket.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business
{

    public interface IBusLocationStore
    {
        public Task<ResponseWrapper<List<KeyValuePair<int, string>>>> SearchAsync(string term);
        public Task<ResponseWrapper<GetAllWithCloneResponse>> GetAllWithCloneAsync(int skip = 0, int take = 0);
    }
    public class BusLocationStore : IBusLocationStore
    {
        private readonly IOBiletIntegration _integration;
        private readonly IBusTicketCache _busTicketCache;
        private readonly string SESSION_ID;
        private readonly string DEVICE_ID;

        public BusLocationStore(IOBiletIntegration integration, IBusTicketCache busTicketCache, IHttpContextAccessor httpContext)
        {
            _integration = integration;
            _busTicketCache = busTicketCache;
            var session = httpContext.GetSessionInfos();
            SESSION_ID = session.Item1;
            DEVICE_ID = session.Item2;

        }

        public async Task<ResponseWrapper<List<KeyValuePair<int, string>>>> SearchAsync(string term)
        {
            
            var request = new GetBusLocationsRequestDto()
            {
                DeviceSession = new()
                {
                    SessionId = SESSION_ID,
                    DeviceId = DEVICE_ID
                },
                Date = DateTime.Now,
                Data = term,
                Language = "tr-TR"
            };

            OBiletResponseWrapper<List<GetBusLocationsResponseDto>> response = await _integration.GetBusLocationsAsync(request);
            if (response.IsSuccessful == false)
            {
                return ResponseWrapper<List<KeyValuePair<int, string>>>.Warning(response.Message);
            }

            //dublicate check
            List<KeyValuePair<int, string>> mapped = response.Response.Data.GroupBy(f => f.Id)
                 .Select(f => f.First())
                 .Select(f => new KeyValuePair<int, string>(f.Id, f.Name)).ToList();

            return ResponseWrapper<List<KeyValuePair<int, string>>>.Success(mapped);
        }

        public async Task<ResponseWrapper<GetAllWithCloneResponse>> GetAllWithCloneAsync(int skip = 0, int take = 0)
        {
           
            (bool, List<GetBusLocationsResponseDto>) cacheData = _busTicketCache.Get<List<GetBusLocationsResponseDto>>(IBusTicketCache.BUS_LOCATION_KEY);
            List<GetBusLocationsResponseDto> data = cacheData.Item2;
            if (cacheData.Item1 == false)
            {
                var request = new GetBusLocationsRequestDto()
                {
                    DeviceSession = new()
                    {
                        SessionId =SESSION_ID,
                        DeviceId =DEVICE_ID
                    },
                    Date = DateTime.Now,
                    Data = string.Empty,
                    Language = "tr-TR"
                };
                OBiletResponseWrapper<List<GetBusLocationsResponseDto>> getBusLocations = await _integration.GetBusLocationsAsync(request);
                if (getBusLocations.IsSuccessful == false)
                {
                    return ResponseWrapper<GetAllWithCloneResponse>.Warning(getBusLocations.Message);
                }

                data = getBusLocations.Response.Data;
                _busTicketCache.Set<List<GetBusLocationsResponseDto>>(IBusTicketCache.BUS_LOCATION_KEY, data, IBusTicketCache.BUS_LOCATION_EXPIRE);
            }
            //dublicate check
            IEnumerable<KeyValuePair<int, string>> mapped = data.GroupBy(f => f.Id)
                 .Select(f => f.First())
                 .Select(f => new KeyValuePair<int, string>(f.Id, f.Name));
            if (take > 0)
                mapped = mapped.Skip(skip).Take(take);
            var respData = new GetAllWithCloneResponse(mapped);
            return ResponseWrapper<GetAllWithCloneResponse>.Success(respData);
        }
    }
}
