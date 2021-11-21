## Dotnet AmazonMQ (ActiveMQ engine) Demo
This is a Proof of Concept aiming to demonstrate the integration of AWS AMQ (Pub/Sub) into .Net via [MassTransit](https://masstransit-project.com/).
This repo is part of a series focusing on .NET/C#. This repo is meant to be public.

#### Attention: AWS AMQ is not a free of charge service. It has a free tier though. Mind the charges.

#### Stack and Features:
.NET 6, MassTransit, DI, Async, AWS AMQ (ActiveMQ engine)

#### Overview:
- .Net WenApi contains a single POST endpoint allowing posting messages to AMQ
- MessagingServices containing the logic to send message to AMQ
- Dummy Console App to consume the posted messages

*Note:* To check the Pub/Sub in real time start both projects (WebApi and Console App) together. This can be configured through the Solution properties.

#### Prerequisites:
- AWS Account
- [AWS AMQ broker](https://docs.aws.amazon.com/amazon-mq/latest/developer-guide/amazon-mq-creating-configuring-broker.html)

*Note:* To avoid significant charges, ensure that the smaller instance type is selected on broker creation (possibly mq.t2.micro). Also, terminate the broker once finished.

##### Troubleshooting:
- For any web console connectivity issues check the inbound rules in the Sequrity Group (Custom TCP type).
- For any broker related connectivity issues check the inbound rules in the Sequrity Group, ensure you are using the correct endpoint for the selected protocol, ensure that the latest MassTransit stable version is installed, ensure that the correct AWS region is declared at the broker connection string (in the application).  

