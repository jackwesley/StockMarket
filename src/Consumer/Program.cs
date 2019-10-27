using Consumer.Model;
using Consumer.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace Consumer
{
    class Program
    {
        private static IConfiguration _configuration;
        private static RabbitMQSettings _rabbigMQSettings;
        private static HGFinanceSettings _HGFinanceSettings;

        public static void Main(string[] args)
        {
            Startup();

            var factory = new ConnectionFactory()
            {
                HostName = _rabbigMQSettings.HostName,
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _rabbigMQSettings.Queue,
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(queue: _rabbigMQSettings.Queue,
                     autoAck: true,
                     consumer: consumer);

                Console.WriteLine("Aguardando mensagens para processamento");
                Console.WriteLine("Pressione uma tecla para encerrar...");
                Console.ReadKey();
            }

            Console.WriteLine("Processed Ok");
        }


        private static void Consumer_Received(
           object sender, BasicDeliverEventArgs e)
        {
            var message = Encoding.UTF8.GetString(e.Body);
            var symbols = JsonConvert.DeserializeObject<Symbols>(message);
            try
            {
                foreach (var symbol in symbols.SymbolsToRate)
                {
                    GetSymbolRate(symbol);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("Fim das cotações");
        }

        private static void GetSymbolRate(string symbol)
        {
            using (var httpClient = new HttpClient())
            {

                var builder = new UriBuilder(_HGFinanceSettings.BaseAdress);
                builder.Port = -1;
                var query = HttpUtility.ParseQueryString(builder.Query);
                query["key"] = _HGFinanceSettings.Key;
                query["symbol"] = symbol;
                builder.Query = query.ToString();

                HttpResponseMessage response = httpClient.GetAsync(builder.ToString()).GetAwaiter().GetResult();
                string content = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();

                if (response.StatusCode == HttpStatusCode.OK )
                {
                    var sellingRate = JsonConvert.DeserializeObject<SellingRate>(content);
                    Console.WriteLine($"\n----Cotação para {symbol.ToUpper()}-----------------");
                    Console.WriteLine("\n" + sellingRate.results);
                    Console.WriteLine("\n-----------------------------------------------------");
                }
                else
                {
                    Console.WriteLine($"\n----Cotação para {symbol.ToUpper()}-----------------");
                    Console.WriteLine($"\n---Erro: {response.StatusCode}. Tente novamente!!---");
                    Console.WriteLine("\n-----------------------------------------------------");
                }
            }
        }

        #region StartupConfigurations
        private static void Startup()
        {
            StartupConfigurations();
            StartupRabbitMQSettings();
            StartupHGFinanceSettings();
        }

        private static void StartupRabbitMQSettings()
        {
            _rabbigMQSettings = new RabbitMQSettings();
            new ConfigureFromConfigurationOptions<RabbitMQSettings>(
                _configuration.GetSection("RabbitMQSettings"))
                    .Configure(_rabbigMQSettings);
        }

        private static void StartupHGFinanceSettings()
        {
            _HGFinanceSettings = new HGFinanceSettings();
            new ConfigureFromConfigurationOptions<HGFinanceSettings>(
                _configuration.GetSection("HGFinanceSettings"))
                    .Configure(_HGFinanceSettings);
        }

        private static void StartupConfigurations()
        {
            var builder = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile($"appsettings.json");
            _configuration = builder.Build();
        }
        #endregion


    }
}
