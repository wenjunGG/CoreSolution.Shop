using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CoreSolution.RabbitMQ
{
   public class QueueHelper
    {

        private ConnectionFactory _factory;
        ///声明接收委托
        public delegate void ReceiveActionDelegate(object obj);
        public QueueHelper(string rabbitmqHost,int rabbitmqPort,string rabbitUserName,string rabbitpassword,string vhost)
        {
 
          _factory = new ConnectionFactory() { HostName = rabbitmqHost, Port= rabbitmqPort,UserName=rabbitUserName,Password=rabbitpassword,VirtualHost=vhost };
            
        }
        /// <summary>
        /// 创建消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="obj"></param>
        public void CreateQueueMessage(string queueName,object obj)
        {
            using (var connection = _factory.CreateConnection())
                       using (var channel = connection.CreateModel())
                       {
                           channel.QueueDeclare(queue: queueName,
                                                durable: true,
                                                exclusive: false,
                                                autoDelete: false,
                                                arguments: null);
               
                         
                           var body = CommonHelper.ObjectToBytes(obj);
               
                           var properties = channel.CreateBasicProperties();
                           properties.Persistent = true;
               
                           channel.BasicPublish(exchange: "",
                                                routingKey: queueName,
                                                basicProperties: properties,
                                                body: body);
                WriteLogHelper.WriteLogDoc("CreateQueueLog", "队列" + queueName + "加了数据", "rabbitMqLog");


                       }
          
        }
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="delegateHandle"></param>
        public void GetQueueMessage(string queueName, ReceiveActionDelegate delegateHandle) {

          
                using (var connection = _factory.CreateConnection())
                     using (var channel = connection.CreateModel())
                     {
                         channel.QueueDeclare(queue: queueName,
                                              durable: true,
                                              exclusive: false,
                                              autoDelete: false,
                                              arguments: null);
                
                         channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                
                         var consumer = new EventingBasicConsumer(channel);
                         
                         consumer.Received += (model, ea) =>
                         {
                             channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                             var body = ea.Body;
                             object objval;
                             objval = CommonHelper.BytesToObject(body);
                             delegateHandle(objval);
                             WriteLogHelper.WriteLogDoc("GetQueueLog", "队列" + queueName + "获取数据成功", "rabbitMqLog");

                             
                         };
                        channel.BasicConsume(queue: queueName,
                                         autoAck: false,
                                         consumer: consumer);
                 
                 
                }
                       
        }



    }
}
