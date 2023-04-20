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

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            var message = Encoding.UTF8.GetString(body.ToArray());
            Console.WriteLine($"{message}");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("---nadawca---");
            Console.WriteLine("nacisnij aby rozpoczac wysylanie ");
            Console.ReadKey();

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
                string replyQueueName = channel.QueueDeclare().QueueName;
                var consumer = new LabConsumer(channel);
                channel.BasicConsume(replyQueueName, true, consumer);

                for (int i = 0; i < 10; i++)
                {
                    var body = Encoding.UTF8.GetBytes($"message{i + 1}");
                    IBasicProperties properties = channel.CreateBasicProperties();

                    properties.ReplyTo = replyQueueName;
                    var corrId = Guid.NewGuid().ToString();
                    properties.CorrelationId = corrId;

                    properties.Headers = new Dictionary<string, object>();
                    properties.Headers.Add("student", Encoding.UTF8.GetBytes("Michał Kuprianowicz"));
                    properties.Headers.Add("index",184631);
                    
                    channel.BasicPublish("", queueName, properties, body);
                }
                Console.WriteLine("wiadomosci zostaly wyslane");
                Console.WriteLine("napisnij aby zaczac zadanie 7");
                Console.ReadKey();
            }


            // zad 7
            Console.WriteLine("start zadanie 7");
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("abc", "topic");

                for (int i = 0; i < 10; i++)
                {
                    var body = Encoding.UTF8.GetBytes($"message{i + 1} zadania7");

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
