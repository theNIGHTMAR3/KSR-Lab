using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Nadawca
{
    class LabConsumer : DefaultBasicConsumer
    {
        public LabConsumer(IModel model) : base(model) { }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, byte[] body)
        {
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"{message}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("nadawca");
            Console.WriteLine("nacisnij aby wyslac");
            Console.ReadKey();

            string queueName = "kolejka<index>";
            var factory = new ConnectionFactory()
            {
                UserName = "<index>",
                Password = "ke2pemurtb8a",
                HostName = "localhost",
                VirtualHost = "vh<index>"
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);
                string replyQueueName = channel.QueueDeclare().QueueName;
                var consumer = new LabConsumer(channel);
                channel.BasicConsume(replyQueueName, true, consumer);

                for (int i = 0; i < 10; i++)
                {
                    var body = Encoding.UTF8.GetBytes($"wiadomosc{i + 1}");
                    IBasicProperties properties = channel.CreateBasicProperties();

                    properties.ReplyTo = replyQueueName;
                    var corrId = Guid.NewGuid().ToString();
                    properties.CorrelationId = corrId;

                    properties.Headers = new Dictionary<string, object>();
                    properties.Headers.Add("student", Encoding.UTF8.GetBytes("Imie Nazwisko"));
                    properties.Headers.Add("index", <index>);
                    
                    channel.BasicPublish("", queueName, properties, body);
                }
                Console.WriteLine("wyslano wiadomosci");
                Console.WriteLine("zadanie6 nacisnij aby wyslac");
                Console.ReadKey();
            }

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("abc", "topic");

                for (int i = 0; i < 10; i++)
                {
                    var body = Encoding.UTF8.GetBytes($"message{i + 1} zadania6");

                    if (i % 2 == 0)
                    {
                        channel.BasicPublish("abc", "abc.def", null, body);
                    }
                    else
                    {
                        channel.BasicPublish("abc", "abc.xyz", null, body);
                    }
                }

                Console.ReadKey();
            }
        }
    }
}
