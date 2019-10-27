using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Producer.Interfaces;
using Producer.Model;
using RabbitMQ.Client;
using System;
using System.Text;

namespace Producer.Services
{
    public class ProducerService : IProducerService
    {

        public string SendMessageToQueue(RabbitMQSettings rabbitMQSettings, Symbols symbols)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = rabbitMQSettings.HostName };


                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: rabbitMQSettings.Queue,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    string message = JsonConvert.SerializeObject(symbols.SymbolsToRate);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "",
                                         routingKey: rabbitMQSettings.Queue,
                                         basicProperties: null,
                                         body: body);
                }

                return "Mensage sent";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

        }
    }
}
