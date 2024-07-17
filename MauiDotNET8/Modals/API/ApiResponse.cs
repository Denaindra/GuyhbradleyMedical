using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiDotNET8.Modals.API
{
    public class ApiResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
    }
    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }
    }
}
