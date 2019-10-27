using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Producer.Interfaces;
using Producer.Model;
using RabbitMQ.Client;

namespace Producer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockMarketController : ControllerBase
    {
        private readonly IProducerService _producerService;
        public StockMarketController(IProducerService producerService)
        {
            _producerService = producerService;
        }
        public IActionResult Index()
        {
            return Ok("Api Started!");
        }

        [HttpPost("SymbolRate")]
        public object SymbolRate([FromServices]RabbitMQSettings rabbitMQSettings, [FromBody]Symbols symbols)
        {
            if (symbols != null && symbols.SymbolsToRate.Any())
            {
               var res = _producerService.SendMessageToQueue(rabbitMQSettings, symbols);
                return new
                {
                    Resultado = res
                };
            }
            else
            {
                return new
                {
                    Erro = "SymbolsToRate must not contain elements"
                };
            }
        }
    }
}