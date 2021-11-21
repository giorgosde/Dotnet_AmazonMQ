using MassTransit;
using MassTransit.ActiveMqTransport;
using Contracts;
using GreenPipes;

string username = "{broker_username}";
string password = "{broker_password}";
string hostUrl = "activemq://{broker_endpoint}";
string queueName = "{broker_queue_name}";
int noOfRetries = 5;
int delayMilliseconds = 500;

var bus = Bus.Factory.CreateUsingActiveMq(cfg =>
{
    cfg.Host(new Uri(hostUrl), h =>
    {
        h.Username(username);
        h.Password(password);
        h.UseSsl();
    });

    cfg.ReceiveEndpoint(queueName, e =>
    {
        e.UseRetry(r => r.Interval(noOfRetries, TimeSpan.FromMilliseconds(delayMilliseconds)));
        e.Consumer<DemoMessageConsumer>();
    });
});

await bus.StartAsync();
Console.ReadLine();

public class DemoMessageConsumer : IConsumer<DemoMessageContract>
{
    public Task Consume(ConsumeContext<DemoMessageContract> context)
    {
        Console.WriteLine($"Received Message: {context.Message.Message}");
        return Task.CompletedTask;
    }
}