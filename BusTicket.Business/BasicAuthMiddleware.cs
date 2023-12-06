using BusTicket.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusTicket.Business
{
    public class CustomAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.Value.Contains("/Home/Error"))
            {
                await _next(context);
            }
            var sessionStore = context.RequestServices.GetRequiredService<ISessionStore>();
            string session = context.Request.Cookies["BusTicket_Session"];

            if (string.IsNullOrEmpty(session))
            {
                ResponseWrapper<string> responseWrapper = await sessionStore.CreateSessionToken();

                if (responseWrapper.IsSuccessful == false)
                {
                    context.Response.Redirect("/Home/Error");
                    return;
                }
                session = responseWrapper.Data;

                context.Response.Cookies.Append("BusTicket_Session", session); ;
            }

            (string, string) decodedSesionBase64 = sessionStore.Decode(session);
            string sessionID = decodedSesionBase64.Item1;
            string deviceID = decodedSesionBase64.Item2;
            var claims = new[]
             {
                new Claim("SessionID", sessionID),
                new Claim("DeviceID", deviceID)
            };
            var identity = new ClaimsIdentity(claims, "custom");
            var principal = new ClaimsPrincipal(identity);

            context.User = principal;

            await _next(context);

        }
    }
}
