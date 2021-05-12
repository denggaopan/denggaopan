using Denggaopan.RabbitMq.Models;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.RabbitMq
{
    public interface IRabbitMqService
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="messageBody"></param>
        /// <param name="correlationId"></param>
        /// <param name="expiration"></param>
        /// <param name="exchangeName"></param>
        /// <returns></returns>
        bool SendMessage(string queueName, string messageBody, string correlationId = "", string expiration = "3600000", string exchangeName = "");

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="messageBody"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        bool SendMsgBatchFromConfig(string messageBody, string expiration = "3600000");
        bool SendMessageBatch(IList<MessageModel> messageList);

        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        bool PulishMessage(string exchangeName, string messageBody);

        /// <summary>
        /// 消费处理程序（返回true正常消费，false消费异常）
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="handler">arg1:body,arg2:correlationId</param>
        /// <returns>返回false表示处理失败</returns>
        IModel ConsumeHandler(string queueName, Func<string, string, bool> handler);
    }
}
