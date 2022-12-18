using Contracts;
using MassTransit;
using MassTransit.ActiveMqTransport;
using Microsoft.Extensions.Configuration;

namespace MessagingServices;

public interface IProducerService
{
    public Task SendMessageAsync(DemoMessageContract demoContract);
}
public class ProducerService : IProducerService
{
    ISendEndpoint sendEndpoint;
    private readonly IConfiguration Configuration;

    public ProducerService(IConfiguration configuration)
    {
        Configuration = configuration;
        string hostUrl = Configuration["AMQ:HostUrl"];

        var bus = Bus.Factory.CreateUsingActiveMq(cfg =>
        {
            cfg.Host(new Uri(hostUrl), h =>
            {
                h.Username(Configuration["AMQ:Username"]);
                h.Password(Configuration["AMQ:Password"]);
                h.UseSsl();
            });
        });

        sendEndpoint = bus.GetSendEndpoint(new Uri($"{hostUrl}/{Configuration["AMQ:QueueName"]}")).Result;
    }
    public async Task SendMessageAsync(DemoMessageContract demoContract)
    {
        await sendEndpoint.Send(demoContract);
    }
}
