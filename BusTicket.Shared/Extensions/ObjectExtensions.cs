using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BusTicket.Shared.Extensions
{
    public static class ObjectExtensions
    {
        public static T FindProperty<T>(this object obj, string propName)
        {
            Type t = obj.GetType();
            PropertyInfo statuProp = t.GetProperty(propName);
            if (statuProp == null)
                return default;
            return (T)statuProp.GetValue(obj);
        }
    }
    public static class IHttpContextAccessorExtensions
    {
        public static string GetClientIpAddress(this IHttpContextAccessor httpContextAccessor)
        {
            string ip = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            if (ip == "::1")
            {
                return "127.0.0.1";
            }
            return ip;
        }
        public static string GetClientPort(this IHttpContextAccessor httpContextAccessor)
        {

            return httpContextAccessor.HttpContext.Connection.RemotePort.ToString();
        }
        public static (string, string) GetBrowserInfos(this IHttpContextAccessor httpContextAccessor)
        {
            var userAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();

            string[] browserInfos = userAgent.Split("/");

            string browserName = browserInfos.GetValue(0) != null ? browserInfos.GetValue(0).ToString() : "UNKNOW";
            string browserVersion = browserInfos.GetValue(1) != null ? browserInfos.GetValue(1).ToString() : "UNKNOW";


            return (browserName, browserVersion);

        }
        /// <summary>
        /// Item1 sessionId
        /// Item2 deviceId
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <returns></returns>
        public static (string, string) GetSessionInfos(this IHttpContextAccessor httpContextAccessor)
        {
            try
            {
                var sessionId = httpContextAccessor.HttpContext.User.Claims.First(f => f.Type == "SessionID").Value;
                var deviceId = httpContextAccessor.HttpContext.User.Claims.First(f => f.Type == "DeviceID").Value;
                return (sessionId, deviceId);
            }
            catch
            {

                return (string.Empty, string.Empty);
            }

        }
    }
}
