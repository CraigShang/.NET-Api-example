using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RefactorThis.Models.Enum;
using RefactorThis.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Filters
{
    /// <summary>
    /// ResultMiddleware
    /// </summary>
    public class ResultMiddleware : ActionFilterAttribute
    {
        /// <summary>
        ///  对返回的结果进行统一的 格式
        /// </summary>
        /// <param name="context"></param>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!TokenCom.SkipResultAuthorization(context))
            {
                if (context.Result is ObjectResult)
                {
                    var objectResult = context.Result as ObjectResult;
                    if (objectResult.Value == null)
                    {
                        context.Result = new ObjectResult(new { success = false, code = 404, msg = "Resource not found", data = "null" });
                    }
                    else
                    {
                        dynamic returnlValue = objectResult.Value;
                        if (returnlValue.code == (int)ReturnCodeEnum.Success)
                        {
                            returnlValue.success = true;
                        }
                        else
                        {
                            returnlValue.success = false;                            
                        }
                        returnlValue.msg = Enum.GetName(typeof(ReturnCodeEnum), int.Parse(returnlValue.code.ToString()));
                        context.Result = new ObjectResult(returnlValue);
                    }
                }
            }
        }


    }
}
