using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Odbiorca1
{
    class LabConsumer : DefaultBasicConsumer
    {
        public LabConsumer(IModel model) : base(model) { }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var message = Encoding.UTF8.GetString(body.ToArray());

            if (properties.Headers != null)
            {
                string student = Encoding.UTF8.GetString((byte[])properties.Headers["student"]);
                int index = (int)properties.Headers["index"];
                Console.WriteLine($"{student} {index} {message}");
            }
            else
            {
                Console.WriteLine(message);
            }
            System.Threading.Thread.Sleep(2000);

            if (properties.CorrelationId != null)
            {
                var responseBytes = Encoding.UTF8.GetBytes($"{message} {properties.CorrelationId}");
                var replyProps = Model.CreateBasicProperties();
                replyProps.CorrelationId = properties.CorrelationId;
                Model.BasicPublish("", properties.ReplyTo, replyProps, responseBytes);
            }
            Model.BasicAck(deliveryTag, false);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---odbiorca2---");

            string queueName = "kolejka184631";
            var factory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost",
                VirtualHost = "103057"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);
                channel.BasicQos(0, 1, false);
                var consumer = new LabConsumer(channel);
                channel.BasicConsume(queueName, false, consumer);
                Console.ReadKey();
            }

            Console.WriteLine("---zadanie7---");
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("abc", "topic");
                var queueName6 = channel.QueueDeclare().QueueName;
                channel.QueueBind(queueName6, "abc", "#.xyz");


                var consumer = new LabConsumer(channel);
                channel.BasicConsume(queueName6, false, consumer);

                Console.ReadKey();

            }
        }
    }
}
