using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Registration_Login.Models
{
    public class Result
    {
        public Result(bool succeeded, IEnumerable<string> errors, object data)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
            Data = data;
        }
        public bool Succeeded { get; set; }
        public object Data { get; set; }
        public string[] Errors { get; set; }
        public static Result Success()
        {
            return new Result(true, new string[] { }, null);
        }
        public static Result Failure(IEnumerable<string> errors)
        {

            return new Result(false, errors, null);
        }
        public static Result Success(object data)
        {
            return new Result(true, new string[] { }, data);
        }

    }
}

