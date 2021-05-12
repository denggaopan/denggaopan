using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.RabbitMq.Models
{
    public class MessageModel
    {
        /// <summary>
        /// 队列名称
        /// </summary>
        public string queueName { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string messageBody { get; set; }

        /// <summary>
        /// 过期时间（单位毫秒）[可选参数,默认1小时]
        /// </summary>
        public string expiration { get; set; }
    }
}
