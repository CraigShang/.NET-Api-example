using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RefactorThis.Models;
using RefactorThis.Models.Enum;
using RefactorThis.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Filters
{
    /// <summary>
    /// AuthorizationMiddleware
    /// </summary>
    public class AuthorizationMiddleware : ActionFilterAttribute
    {
        /// <summary>
        /// 重写OnActionExecutionAsync，在Action请求之前进行token认证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public override async Task OnActionExecutionAsync(ActionExecutingContext actionContext, ActionExecutionDelegate next)
        {
            if (!TokenCom.SkipTokenAuthorization(actionContext))
            {
                ReturnValueBase<string> returnValue = new ReturnValueBase<string>();
                // 获取token
                string token = TokenCom.GetTokenFromRequest(actionContext.ActionArguments);
                if (string.IsNullOrEmpty(token))
                {
                    returnValue.code = (int)ReturnCodeEnum.InvalidToken;
                    actionContext.Result = new ObjectResult(returnValue);
                }
                else
                {
                    //验证token是否过期
                    if (TokenCom.CheckToken(token))
                    {
                        returnValue.code = (int)ReturnCodeEnum.ExpiredToken;
                        actionContext.Result = new ObjectResult(returnValue);
                    }
                    else
                        await base.OnActionExecutionAsync(actionContext, next);
                }
            }
            else
            {
                await base.OnActionExecutionAsync(actionContext, next);
            }
        }

    }
}
