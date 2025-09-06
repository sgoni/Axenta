namespace Axenta.Notifications.Consumers;

public class EmailNotificationIntegrationEventConsumer(
    IEmailSender emailSender,
    ILogger<EmailNotificationIntegrationEventConsumer> logger) : IConsumer<EmailNotificationIntegrationEvent>
{
    public async Task Consume(ConsumeContext<EmailNotificationIntegrationEvent> context)
    {
        var @event = context.Message;
        logger.LogInformation("Consuming EmailNotificationIntegrationEvent CorrelationId={CorrelationId}",
            @event.CorrelationId);

        await emailSender.SendEmailAsync(@event.To, @event.Subject, @event.Body);

        logger.LogInformation("Email sent to {To} with subject matter {Subject}", @event.To, @event.Subject);
    }
}