using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace musicList2.Models
{
    public class ErrorModel
    {
        public int Code;
        public string Message;

        public ErrorModel(int code, string message)
        {
            Code = code;
            Message = message;
        }

        public static ErrorModel BadRequest() => 
            new ErrorModel(400, "bad request");

        public static ErrorModel NotFound() => 
            new ErrorModel(404, "not found");

        public static ErrorModel AlreadyExists() =>
            new ErrorModel(400, "already exists");
    }
}
