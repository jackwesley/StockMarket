using Producer.Model;

namespace Producer.Interfaces
{
    public interface IProducerService
    {
        string SendMessageToQueue(RabbitMQSettings rabbitMQSettings, Symbols symbols);

    }
}
