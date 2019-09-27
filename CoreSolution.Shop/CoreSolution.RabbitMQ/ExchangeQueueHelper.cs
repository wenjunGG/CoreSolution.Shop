using CoreSolution.Tools;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.RabbitMQ
{
   public   class ExchangeQueueHelper
    {

        private ConnectionFactory _factory;
        ///声明接收委托
        public delegate void ReceiveActionDelegate(object obj);
        public ExchangeQueueHelper(string rabbitmqHost, int rabbitmqPort, string rabbitUserName, string rabbitpassword, string vhost)
        {

            _factory = new ConnectionFactory() { HostName = rabbitmqHost, Port = rabbitmqPort, UserName = rabbitUserName, Password = rabbitpassword, VirtualHost = vhost };

        }
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="obj"></param>
        public void CreateExchangeMessage(string exchangename,string rootingkey, object obj)
        {
            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangename,
                    type: "direct");

                var body = CommonHelper.ObjectToBytes(obj);

                IBasicProperties props = channel.CreateBasicProperties();
                props.ContentType = "text/plain";
                props.DeliveryMode = 2;

                channel.BasicPublish(exchange: exchangename,
                               routingKey: rootingkey,
                               basicProperties: props,
                               body: body);
                WriteLogHelper.WriteLogDoc("CreateExchangeQueueLog", "创建交换机" + exchangename + "加了数据", "rabbitMqLog");


            }

        }
       /// <summary>
       /// 接收消息
       /// </summary>
       /// <param name="exchangeName"></param>
       /// <param name="queueName"></param>
       /// <param name="rootingkey"></param>
       /// <param name="delegateHandle"></param>
        public void GetQueueMessage(string exchangeName, string queueName,string rootingkey, ReceiveActionDelegate delegateHandle)
        {


            using (var connection = _factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: exchangeName,
                                       type: "direct");
                channel.QueueDeclare(queue: queueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                ///绑定交换机队列
                channel.QueueBind(queue: queueName,
                                    exchange: exchangeName,
                                    routingKey: rootingkey);

                var consumer = new EventingBasicConsumer(channel);
                //声明方法
                consumer.Received += (model, ea) =>
                {
                    channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    var body = ea.Body;
                    object objval;
                    objval = CommonHelper.BytesToObject(body);
                    delegateHandle(objval);
                    WriteLogHelper.WriteLogDoc("GetExchangeQueueLog", "交换机："+exchangeName+"绑定队列：" + queueName + "获取数据成功", "rabbitMqLog");


                };
                channel.BasicConsume(queue: queueName,
                                 autoAck: false,
                                 consumer: consumer);


            }

        }


    }
}
