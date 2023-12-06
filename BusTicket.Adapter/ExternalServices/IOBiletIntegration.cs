using BusTicket.Adapter.Dtos;
using BusTicket.Adapter.Wrappers;
using System.Text.Json;

namespace BusTicket.Adapter.ExternalServices
{
    public interface IOBiletIntegration
    {
        public Task<OBiletResponseWrapper<GetSessionResponseDto>> GetSessionsAsync(GetSessionRequestDto requestData);
        public Task<OBiletResponseWrapper<List<GetBusLocationsResponseDto>>> GetBusLocationsAsync(GetBusLocationsRequestDto requestData);
        public Task<OBiletResponseWrapper<List<GetBusJourneys>>> GetBusJourneys(GetBusJourneysRequestDto requestData);
    }
}
