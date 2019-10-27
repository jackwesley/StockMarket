# StockMarket
This is a project to get information form Brazilian Stock Market.
In this solution there are 2 pojects.
1. (Producer) - API that you can call and pass the symbol of a stock from Brazilian Stock Market. This call insert the codes in a RabbitMQ
for a posterior processment.
2. (Consumer) - Console Application who read the RabbitMQ and get a price from Brazilian Stock Market via api https://hgbrasil.com/
