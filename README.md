# StockMarket
> This is a project to get information form Brazilian Stock Market. In this solution there are 2 pojects.
1. (Producer) - API which you can call passing the symbol of a stock from Brazilian Stock Market. This call insert the codes in a RabbitMQ
for a posterior processment.
EndPoint Method Post:
```https://localhost:44354/StockMarket/SymbolRate```

Body:
{
  "symbolsToRate": ["b3sa3", "bidi4"] 
}

2. (Consumer) - Console Application responsible for read the RabbitMQ and get a price from Brazilian Stock Market via api https://hgbrasil.com/
