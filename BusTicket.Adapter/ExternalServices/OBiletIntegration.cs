using BusTicket.Adapter.ConfigModels;
using BusTicket.Adapter.Dtos;
using BusTicket.Adapter.Wrappers;
using Microsoft.Extensions.Options;
using RestSharp;

namespace BusTicket.Adapter.ExternalServices
{
    /// <summary>
    /// The integration class for OBilet.
    /// </summary>
    public class OBiletIntegration : IOBiletIntegration
    {
        private readonly OBiletIntegrationSetting _oBiletIntegrationSetting;
        private readonly RestClient _restClient;
        private const string BASIC_TOKEN = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
        public OBiletIntegration(IOptions<OBiletIntegrationSetting> option)
        {
            _oBiletIntegrationSetting = option.Value;


            var options = new RestClientOptions(_oBiletIntegrationSetting.BaseUrl)
            {
                MaxTimeout = -1,
            };
            _restClient = new RestClient(options);
        }
        public async Task<OBiletResponseWrapper<GetSessionResponseDto>> GetSessionsAsync(GetSessionRequestDto requestData)
        {
            OBiletResponseWrapper<GetSessionResponseDto> oBiletResponseWrapper = new OBiletResponseWrapper<GetSessionResponseDto>();

            var request = new RestRequest(_oBiletIntegrationSetting.GetSessionUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {BASIC_TOKEN}");
            request.AddJsonBody(requestData);
            RestResponse response = await _restClient.ExecuteAsync(request);


            return OBiletResponseWrapper<GetSessionResponseDto>.Generate(response.IsSuccessful, response.Content, response.ErrorMessage);
        }

        public async Task<OBiletResponseWrapper<List<GetBusLocationsResponseDto>>> GetBusLocationsAsync(GetBusLocationsRequestDto requestData)
        {

            var request = new RestRequest(_oBiletIntegrationSetting.BusLocationsUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {BASIC_TOKEN}");
            request.AddJsonBody(requestData);
            RestResponse response = await _restClient.ExecuteAsync(request);

            return OBiletResponseWrapper<List<GetBusLocationsResponseDto>>.Generate(response.IsSuccessful, response.Content, response.ErrorMessage);
        }

        public async Task<OBiletResponseWrapper<List<GetBusJourneys>>> GetBusJourneys(GetBusJourneysRequestDto requestData)
        {
            var request = new RestRequest(_oBiletIntegrationSetting.BusJourneysUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Basic {BASIC_TOKEN}");
            request.AddJsonBody(requestData);
            RestResponse response = await _restClient.ExecuteAsync(request);

            return OBiletResponseWrapper<List<GetBusJourneys>>.Generate(response.IsSuccessful, response.Content, response.ErrorMessage);
        }
    }
}
