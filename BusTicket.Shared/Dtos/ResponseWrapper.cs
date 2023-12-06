using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BusTicket.Shared.Dtos
{
    public class ResponseWrapper<T>
    {
        private ResponseWrapper()
        {
                
        }
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
        public static ResponseWrapper<T> Success(T data)
        {
            return new()
            {
                Data = data,
                IsSuccessful = true,
                HttpStatusCode = HttpStatusCode.OK
            };
        }
        public static ResponseWrapper<T> Fail(string errorMessage)
        {
            return new()
            {
                IsSuccessful = false,
                Message = errorMessage,
                HttpStatusCode = HttpStatusCode.InternalServerError
            };
        }
        public static ResponseWrapper<T> Warning(string errorMessage)
        {
            return new()
            {
                IsSuccessful = false,
                Message = errorMessage,
                HttpStatusCode = HttpStatusCode.BadRequest
            };
        }

    }
}
