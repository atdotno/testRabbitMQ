using RabbitMQ.Client;
using System.Text;



var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();


await channel.QueueDeclareAsync(queue: "helloqueue", durable: false, exclusive: false, autoDelete: false,
    arguments: null);


const string message = "Hello woooorld!";
var body = Encoding.UTF8.GetBytes(message);


await channel.BasicPublishAsync(exchange: string.Empty, routingKey: "helloqueue", body: body);
Console.WriteLine($" You just sent : '{message}' to the message queue.");



Console.WriteLine("Press enter to exit");
Console.ReadLine();