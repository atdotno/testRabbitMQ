using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;


var factory = new ConnectionFactory { HostName = "localhost" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();


await channel.QueueDeclareAsync(queue: "helloqueue", durable: false, exclusive: false, autoDelete: false,
    arguments: null);


Console.WriteLine(" Waiting..");


var consumer = new AsyncEventingBasicConsumer(channel);
consumer.ReceivedAsync += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"You just received: '{message}' from the message queue");
    return Task.CompletedTask;
};

await channel.BasicConsumeAsync("helloqueue", autoAck: true, consumer: consumer);


Console.ReadLine();