namespace Common;
public class RabbitMqSettings
{
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string Host { get; set; }
    public int Port { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}