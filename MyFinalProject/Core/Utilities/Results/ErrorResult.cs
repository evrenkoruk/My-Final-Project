using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorResult:Result
    {
        //base() : Result
        public ErrorResult(string message) : base(false, message)
        {


        }

        //mesaj vermek istemediğimizde bunu çalıştırıyoruz
        public ErrorResult() : base(false)
        {

        }
    }
}
