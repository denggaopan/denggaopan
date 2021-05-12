using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.RabbitMq.Models
{
    public class RabbitMqConfig
    {
        /// <summary>
        /// 服务IP列表（以“|”分割）
        /// </summary>
        public string ServerIps { get; set; }

        /// <summary>
        /// 客户端提供名称
        /// </summary>
        public string ClientProvidedName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// vhost
        /// </summary>
        public string Vhost { get; set; }

        /// <summary>
        /// 是否开启
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 指定消息队列（|分割）
        /// </summary>
        public string QueueNames { get; set; }
    }
}
