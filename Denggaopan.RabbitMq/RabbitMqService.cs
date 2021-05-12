using Denggaopan.RabbitMq.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Denggaopan.RabbitMq
{
    /// <summary>
    /// RabbitMQ消息队列通用服务
    /// </summary>
    public class RabbitMqService : IRabbitMqService
    {
        private readonly RabbitMqConfig _config;
        private IConnection _connection;//单例连接（多线程共享一个连接）
        private IList<string> _hostnames;
        private ConnectionFactory _factory;
        private IList<string> _queuenamels;


        public RabbitMqService(RabbitMqConfig config)
        {
            _config = config;

            _factory = new ConnectionFactory()
            {
                AutomaticRecoveryEnabled = true,
                UserName = _config.UserName,
                Password = _config.Password,
                VirtualHost = _config.Vhost
            };
            if (!string.IsNullOrEmpty(_config.QueueNames))
            {
                _queuenamels = _config.QueueNames.Split('|');
            }
            _hostnames = _config.ServerIps.Split('|');
        }

        #region 私有函数

        /// <summary>
        /// 建立RabbitMQ TCP连接
        /// </summary>
        private void InitConnection()
        {
            if (_connection == null || !_connection.IsOpen)
            {
                _connection = _factory.CreateConnection(_hostnames, _config.ClientProvidedName);
            }
        }

        /// <summary>
        /// 使用指定信道发送消息到队列
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="queueName"></param>
        /// <param name="messageBody"></param>
        /// <param name="expiration"></param>
        private void Send(IModel channel, string queueName, string messageBody, string expiration = "3600000")
        {

            //声明队列
            channel.QueueDeclare(queue: queueName,
                         durable: true,//标记为持久性
                         exclusive: false,
                         autoDelete: false,
                         arguments: null);

            // 设置消息属性
            var properties = channel.CreateBasicProperties();
            properties.Persistent = true;//持久消息
            properties.Expiration = expiration;//过期时间

            //发送数据
            var body = Encoding.UTF8.GetBytes(messageBody);
            channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: properties,
                                 body: body);

        }
        #endregion


        #region 公开方法
        /// <summary>
        /// 发送点对点消息
        /// 共享TCP连接，独立信道
        /// </summary>
        /// <param name="queueName">队列名称 如：ha.q1</param>
        /// <param name="messageBody">消息内容</param>
        /// <param name="expiration">消息过期时间（毫秒），默认1小时[可选]</param>
        /// <param name="exchangeName">指定交换机名称,如:king.order[可选]</param>
        /// <returns></returns>
        public bool SendMessage(string queueName, string messageBody, string correlationId = "", string expiration = "3600000", string exchangeName = "")
        {
            if (!_config.Enable)
            {
                return false;
            }

            InitConnection();
            using (var channel = _connection.CreateModel())
            {
                //指定Exchange,否则默认Exchange
                if (exchangeName != "")
                    channel.ExchangeDeclare(exchange: exchangeName, type: ExchangeType.Direct, durable: true); //声明交换机


                //声明队列
                channel.QueueDeclare(queue: queueName,
                             durable: true,//标记为持久性
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

                // 设置消息属性
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;//持久消息
                properties.Expiration = expiration;//过期时间
                if (!string.IsNullOrEmpty(correlationId))
                    properties.CorrelationId = correlationId;

                //发送数据
                channel.ConfirmSelect();
                var body = Encoding.UTF8.GetBytes(messageBody);
                channel.BasicPublish(exchange: exchangeName,
                                     routingKey: queueName,
                                     basicProperties: properties,
                                     body: body);
                return channel.WaitForConfirms();
            }
        }


        /// <summary>
        /// 批量发送点对点消息
        /// 共享TCP连接，多个消息共享一个信道
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        public bool SendMessageBatch(IList<MessageModel> messageList)
        {
            if (!_config.Enable)
            {
                return false;
            }

            InitConnection();

            using (var channel = _connection.CreateModel())
            {
                channel.ConfirmSelect();
                foreach (var item in messageList)
                {

                    string queueName = item.queueName;
                    string messageBody = item.messageBody;
                    string expiration = string.IsNullOrWhiteSpace(item.expiration) ? "3600000" : item.expiration;

                    Send(channel, queueName, messageBody, expiration);

                }
                return channel.WaitForConfirms();
            }
        }


        /// <summary>
        /// 批量发送点对点消息
        /// 共享TCP连接，多个消息共享一个信道
        /// </summary>
        /// <param name="messageList"></param>
        /// <returns></returns>
        public bool SendMsgBatchFromConfig(string messageBody, string expiration = "3600000")
        {
            if (!_config.Enable)
            {
                return false;
            }
            if (_queuenamels == null || _queuenamels.Count == 0)
            {
                return false;
            }
            if (string.IsNullOrEmpty(messageBody))
            {
                throw new ArgumentException("messageBody is null");
            }
            InitConnection();
            using (var channel = _connection.CreateModel())
            {
                channel.ConfirmSelect();
                foreach (var item in _queuenamels)
                {
                    string queueName = item;
                    Send(channel, queueName, messageBody, expiration);
                }
                return channel.WaitForConfirms();
            }
        }


        /// <summary>
        /// 广播消息
        /// </summary>
        /// <param name="exchangeName"></param>
        /// <param name="messageBody"></param>
        /// <returns></returns>
        public bool PulishMessage(string exchangeName, string messageBody)
        {
            if (!_config.Enable)
            {
                return false;
            }

            InitConnection();
            using (var channel = _connection.CreateModel())
            {
                channel.ExchangeDeclare(exchangeName, ExchangeType.Fanout); //声明交换机

                //发送数据
                channel.ConfirmSelect();
                var body = Encoding.UTF8.GetBytes(messageBody);
                channel.BasicPublish(exchangeName, "", null, body);
                return channel.WaitForConfirms();
            }
        }

        /// <summary>
        /// 消费者
        /// </summary>
        /// <param name="queueName">队列名称</param>
        /// <param name="handler">处理器</param>
        public IModel ConsumeHandler(string queueName, Func<string, string, bool> handler)
        {
            InitConnection();
            var channel = _connection.CreateModel();
            {
                channel.QueueDeclare(queue: queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);//设置一次接收1条消息
                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, ea) =>
                {
                    var currId = ea.BasicProperties.CorrelationId;
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var rtnR = false;
                    if (handler != null)
                        rtnR = handler.Invoke(message, currId);
                    if (rtnR)
                        channel.BasicAck(ea.DeliveryTag, false);
                };
                channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                //channel.BasicConsume(queue: "hello", autoAck: true, consumer: consumer);
            }
            return channel;
        }
        #endregion
    }


}
