using System;
using System.Collections.Generic;
using System.Text;

namespace AzureTableStorageService
{
    public class ReturnObject
    {
        public ReturnObject(bool success, string message)
        {
            Success = success;
            Message = message;
        }
        public ReturnObject(bool success, string message, object result)
        {
            Success = success;
            Message = message;
            Result = result;
        }
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
