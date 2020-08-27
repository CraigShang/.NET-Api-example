using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using RefactorThis.Utility.CustomerAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Utility
{
    public class TokenCom
    {
        /// <summary>
        /// 生成token
        /// </summary>
        /// <returns></returns>
        public static string GenerateToken()
        {
            return Utils.GetTimeStamp(DateTime.Now).ToString();
        }
        /// <summary>
        /// 验证token
        /// </summary>
        /// <returns></returns>
        public static bool CheckToken(string token)
        {
            bool isOk = true;
            TimeSpan ts = DateTime.Now - Utils.TimeStampToDate(long.Parse(token));
            //验证token与当前时间差,超过5分钟失效
            if (ts.TotalSeconds > 1500)
            {
                isOk = false;
            }
            return isOk;
        }
        /// <summary>
        /// get token from url query params
        /// </summary>
        /// <param name="actionArguments"></param>
        /// <returns></returns>
        public static string GetTokenFromRequest(IDictionary<string, dynamic> actionArguments)
        {
            string token = string.Empty;
            try
            {
                foreach (var value in actionArguments.Values)
                {
                    string typeName = value.GetType().Name;
                    if (typeName == "token")
                    {
                        token = value;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return token;
        }
        /// <summary>
        /// 是否跳过token验证
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static bool SkipTokenAuthorization(ActionExecutingContext actionContext)
        {
            var isSkip = false;
            try
            {
                var controllerActionDescriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
                if (controllerActionDescriptor != null)
                {
                    isSkip = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                      .Any(a => a.GetType().Equals(typeof(SkipAuthorizationAttribute)));
                }
            }
            catch (Exception ex)
            {
            }
            return isSkip;
        }
        /// <summary>
        /// 是否跳过统一处理返回值
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        public static bool SkipResultAuthorization(ResultExecutingContext actionContext)
        {
            var isSkip = false;
            try
            {
                var controllerActionDescriptor = actionContext.ActionDescriptor as ControllerActionDescriptor;
                if (controllerActionDescriptor != null)
                {
                    isSkip = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true)
                      .Any(a => a.GetType().Equals(typeof(SkipResultAttribute)));
                }
            }
            catch (Exception ex)
            {
            }
            return isSkip;
        }


    }
}
