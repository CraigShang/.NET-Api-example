using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Models.Enum
{
    /// <summary>
    /// api return code enum
    /// </summary>
    public enum ReturnCodeEnum
    {
        InnerError = 0,
        Success = 100,
        ParamError = 200,
        ExpiredToken = 300,
        InvalidToken = 400
    }
}
