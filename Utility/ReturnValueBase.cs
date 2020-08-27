using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Models
{
    /// <summary>
    /// API返回值基础模型
    /// </summary>
    public class ReturnValueBase<T>
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool success { get; set; }
        /// <summary>
        /// 错误码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 返回值
        /// </summary>
        public T data { get; set; }
    }
}
