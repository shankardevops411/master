using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduleservice.Services.Common.Response
{
    public static class Response
    {
        public static Response<T> Fail<T>(string message, T data = default) => new Response<T>(data, message, true);
        public static Response<T> Ok<T>(T data, string message) => new Response<T>(data, message, false);

    }
    public class Response<T>
    {
        public Response(T data, string msg, bool error)
        {
            Data = data;
            message = msg;
            Èrror = error;
        }
        public T Data { get; set; }
        public string message { get; set; }
        public bool Èrror { get; set; }
    }
}
