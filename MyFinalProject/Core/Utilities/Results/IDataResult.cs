using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>:IResult
    {

        //Data : Ürün veya ürünler olur
        T Data { get; }

    }
}
