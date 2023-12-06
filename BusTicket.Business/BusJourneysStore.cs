using BusTicket.Adapter.Dtos;
using BusTicket.Adapter.ExternalServices;
using BusTicket.Adapter.Wrappers;
using BusTicket.Business.Caching;
using BusTicket.Business.Dtos;
using BusTicket.Business.ModelValidator;
using BusTicket.Shared.Dtos;
using FluentValidation.Results;
using BusTicket.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BusTicket.Business
{
    public interface IBusJourneysStore
    {
        public Task<ResponseWrapper<GetJourneysResponse>> Get(BusJourneysGetInput input);
    }
    public class BusJourneysStore : IBusJourneysStore
    {
        private readonly IOBiletIntegration _integration;
        private readonly string SESSION_ID;
        private readonly string DEVICE_ID;

        public BusJourneysStore(IOBiletIntegration integration, IHttpContextAccessor httpContext)
        {
            _integration = integration;
            var session = httpContext.GetSessionInfos();
            SESSION_ID = session.Item1;
            DEVICE_ID = session.Item2;
        }
        public async Task<ResponseWrapper<GetJourneysResponse>> Get(BusJourneysGetInput input)
        {
            //Validation
            ValidationResult validationResult = new BusJourneysGetInputValidator().Validate(input);
            if (validationResult.IsValid == false)
            {
                var error = validationResult.Errors.FirstOrDefault();
                return ResponseWrapper<GetJourneysResponse>.Warning(error.ErrorMessage);
            }
            //The request is being prepared.
            var request = new GetBusJourneysRequestDto()
            {
                DeviceSession = new DeviceSession
                {
                    SessionId = SESSION_ID,
                    DeviceId = DEVICE_ID
                },
                Data = new GetBusJourneysRequestDto.DataModel()
                {
                    DepartureDate = input.Date.ToString("yyyy-MM-dd"),
                    DestinationId = input.DestinationId,
                    OriginId = input.OriginId,
                },
                Date = input.Date.ToString("yyyy-MM-dd"),
                Language = "tr-TR"
            };

            OBiletResponseWrapper<List<GetBusJourneys>> response = await _integration.GetBusJourneys(request);
            if (response.IsSuccessful == false)
            {
                return ResponseWrapper<GetJourneysResponse>.Warning(response.Message);
            }

            //Some information is retrieved from within the directory because it's not provided in the header.
            GetBusJourneys first = response.Response.Data.FirstOrDefault(f => f.OriginLocationId == input.OriginId && f.DestinationLocationId == input.DestinationId);

            //Mapping
            GetJourneysResponse mapped = new GetJourneysResponse()
            {
                DestinationLocation = first?.DestinationLocation,
                OriginLocation = first?.OriginLocation,
                JourneysDate = input.Date,
                Items = response.Response.Data.Where(f => f.Journey != null).Select(f => new GetJourneysResponse.Item
                {
                    Origin = f.Journey.Origin,
                    Currency = f.Journey.Currency,
                    Arrival = f.Journey.Arrival.TimeOfDay,
                    Departure = f.Journey.Departure.TimeOfDay,
                    Destination = f.Journey.Destination,
                    Price = f.Journey.OriginalPrice
                }).ToList()

            };
            return ResponseWrapper<GetJourneysResponse>.Success(mapped);
        }
    }
}
