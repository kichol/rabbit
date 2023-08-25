using RabbitMQ.Client;
using System.Text;

namespace ProducerRabbitMQ
{
    public class Sender
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            var connection = factory.CreateConnection();
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("BasicTest",false,false,false,null);
                string message = "Hello RabbitMQ to ja Ksysio";
                var  body = Encoding.UTF8.GetBytes(message);
                channel.BasicPublish("", "BasicTest", false,null, body);
                Console.WriteLine($"sent message: {message}");

            }
        }
    }
}