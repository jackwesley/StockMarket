# StockMarket
> This is a project to get information form Brazilian Stock Market. In this solution there are 2 pojects.

# Producer
1. API which you can call passing the symbol of a stock from Brazilian Stock Market. This call insert the codes in a RabbitMQ
for a posterior processment.

EndPoint Method Post:
`https://localhost:44354/StockMarket/SymbolRate`

# Payload:
`
{
"symbolsToRate":  ["b3sa3", "bidi4"]
}
`

# Consumer
2. Console Application responsible for read the RabbitMQ and get a price 

from Brazilian Stock Market via api https://hgbrasil.com/
