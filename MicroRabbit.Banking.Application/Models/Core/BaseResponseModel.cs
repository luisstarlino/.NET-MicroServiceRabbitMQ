using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroRabbit.Banking.Application.Models.Core
{
    public class BaseResponseModel
    {
        public bool Success { get; }
        public object? Data { get; }


        public BaseResponseModel(bool success, object? data)
        {
            Success = success;
            Data = data;
        }

        public BaseResponseModel(bool success) : this(success, null) { }

        public BaseResponseModel() : this(true) { }
    }
}
