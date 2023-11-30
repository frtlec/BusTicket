using BusTicket.Shared.Extensions;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BusTicket.Adapter.Wrappers
{
    public class OBiletResponseWrapper<T>
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public OBiletResponseWrapper<T> Generate(bool isSuccessFul, HttpStatusCode code, string response, string errorMessage)
        {
            //http isteğinde hata varsa doğrudan dönüyoruz.
            if (isSuccessFul == false)
                return Fail(errorMessage);


            T data = JsonSerializer.Deserialize<T>(response);
            //http isteği hatalı değil ancak response bodyde hata olabilir.
            (bool, string) hasError = CheckError<T>(data);
            if (hasError.Item1 == false)
            {
                return Success(data);
            }
            else
            {
                return Fail(hasError.Item2);
            }
        }
        /// <summary>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected (bool, string) CheckError<T>(T obj)
        {

            string status = obj.FindProperty<string>("status");
            if (status.ToLower() == "success")
                return (false, string.Empty);

            //burada localization işlemi yapılabilir
            string errorMessage = obj.FindProperty<string>("user-message");
            return (true, errorMessage);

        }
        protected OBiletResponseWrapper<T> Success<T>(T data)
        {
            return new()
            {
                Data = data,
                IsSuccessful = true
            };
        }
        protected OBiletResponseWrapper<T> Fail(string errorMessage)
        {
            return new()
            {
                IsSuccessful = false,
                Message = errorMessage
            };
        }

    }
}