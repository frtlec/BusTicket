using BusTicket.Adapter.ConfigModels;
using BusTicket.Adapter.Dtos;
using BusTicket.Adapter.Wrappers;
using Microsoft.Extensions.Options;
using RestSharp;
using System.Text.Json;

namespace BusTicket.Adapter.ExternalServices
{
    public interface IOBiletIntegration
    {
        public Task<OBiletResponseWrapper<GetSessionResponseDto>> GetSessionsAsync(GetSessionRequestDto requestData);
    }
    public class OBiletIntegration : IOBiletIntegration
    {
        private readonly OBiletIntegrationSetting _oBiletIntegrationSetting;
        private readonly RestClient _restClient;
        private const string BasicToken = "JEcYcEMyantZV095WVc3G2JtVjNZbWx1";
        public OBiletIntegration(IOptions<OBiletIntegrationSetting> option)
        {
            _oBiletIntegrationSetting = option.Value;


            var options = new RestClientOptions(_oBiletIntegrationSetting.BaseUrl)
            {
                MaxTimeout = -1,
            };
            _restClient=new RestClient(options);
        }
        public async Task<OBiletResponseWrapper<GetSessionResponseDto>> GetSessionsAsync(GetSessionRequestDto requestData)
        {
            OBiletResponseWrapper<GetSessionResponseDto> oBiletResponseWrapper = new OBiletResponseWrapper<GetSessionResponseDto>();

            var request = new RestRequest(_oBiletIntegrationSetting.GetSettionUrl, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic JEcYcEMyantZV095WVc3G2JtVjNZbWx1");
            request.AddJsonBody(requestData);
            RestResponse  response= await _restClient.ExecuteAsync(request);
            oBiletResponseWrapper.Generate(response.IsSuccessful,response.StatusCode,response.Content,response.ErrorMessage);

            return oBiletResponseWrapper;
        }
    }
}
