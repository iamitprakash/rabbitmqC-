using RabbitMQ.Client;
using System.Text;

namespace Publisher.App
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    Console.WriteLine("Sending 'hello' messages to RabbitMQ. Press Ctrl+C to exit.");


                    int count = 0;
                    while (true)
                    {
                        string message = "hello -->" + count;
                        var body = Encoding.UTF8.GetBytes(message);

                        channel.BasicPublish(exchange: "",
                                             routingKey: "hello",
                                             basicProperties: null,
                                             body: body);

                        Console.WriteLine($" [X] Sent 'hello' (#{++count})");
                        //count++;
                        Thread.Sleep(1000); // Wait for 1 second before sending the next message
                    }
                }
            }
        }
    }
}
