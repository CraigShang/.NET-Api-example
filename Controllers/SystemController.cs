using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RefactorThis.Utility;
using RefactorThis.Utility.CustomerAttribute;

namespace RefactorThis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        /// <summary>
        /// 获取token，之后每个接口用url参数传递
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [SkipAuthorization]
        public string Get()
        {
            return TokenCom.GenerateToken();
        }
    }
}
