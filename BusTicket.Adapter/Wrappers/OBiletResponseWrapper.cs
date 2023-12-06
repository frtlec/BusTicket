using BusTicket.Adapter.Dtos;
using BusTicket.Shared.Extensions;
using System.ComponentModel.Design;
using System.Drawing;
using System;
using System.Net;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.CodeAnalysis;

namespace BusTicket.Adapter.Wrappers
{

    /// <summary>
    /// This wrapper class centralizes control over the responses from the OBilet API.Other operations using the OBiletIntegration class will obtain clearer results through this intermediary class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OBiletResponseWrapper<T> 
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public OBiletCommonResponseDto<T> Response { get; set; }
        /// <summary>
        ///  The method interpreting the response would return either success or failure from that method.
        /// </summary>
        /// <returns>
        /// Fail or Success
        /// </returns>
        public static OBiletResponseWrapper<T> Generate(bool isSuccessFul, string responseBody, string errorMessage)
        {
            //If there's an error in the HTTP request, we return directly.
            if (isSuccessFul == false)
                return Fail(errorMessage);
            OBiletCommonResponseDto<T> response = null;
            try
            {
                response=JsonSerializer.Deserialize<OBiletCommonResponseDto<T>>(responseBody);
            }
            catch (Exception ex)
            {

                throw;
            }

            //hThe HTTP request might not be faulty, but there could be an error within the response body.
            (bool, string) hasError = CheckError(response);
            if (hasError.Item1 == false)
            {
                return Success(response);
            }
            else
            {
                return Fail(hasError.Item2);
            }
        }
        protected static (bool, string) CheckError(OBiletCommonResponseDto<T> response)
        {
            if (response.Status.ToLower() == "success")
                return (false, string.Empty);

            string errorMessage = string.IsNullOrEmpty(response.UserMessage) ? $"{response.Status}": response.UserMessage;
            return (true, errorMessage);

        }
        protected static OBiletResponseWrapper<T> Success(OBiletCommonResponseDto<T> data)
        {
            return new()
            {
                Response = data,
                IsSuccessful = true
            };
        }
        protected static OBiletResponseWrapper<T> Fail(string errorMessage)
        {
            return new()
            {
                IsSuccessful = false,
                Message = errorMessage
            };
        }

    }
}