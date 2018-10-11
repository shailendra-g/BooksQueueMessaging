using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;


namespace BooksQueueMessaging
{
    class Program
    {
        // Connection String for the namespace can be obtained from the Azure portal under the 
        // 'Shared Access policies' section.
        const string ServiceBusConnectionString = "Endpoint=sb://servicebusshailst.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YhPoybz4Gs2cfRM7DF9z+QakdJFJ7dk/+8q1xFpr+wE=";
        const string QueueName = "bookqueue";
        static IQueueClient queueClient;

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            const int numberOfMessages = 30;
            queueClient = new QueueClient(ServiceBusConnectionString, QueueName);

            Console.WriteLine("======================================================");
            Console.WriteLine("Press ENTER key to exit after receiving all the messages.");
            Console.WriteLine("======================================================");

            // Send Messages
            await SendMessagesAsync(numberOfMessages);

            Console.ReadKey();

            await queueClient.CloseAsync();
        }

        static async Task SendMessagesAsync(int numberOfMessagesToSend)
        {
            try
            {

                List<Books> books = new List<Books>()
                {
                   new Books(){BookName= "The Bourne Betrayal",
                               AuthorName= "Lustbader",
                               Price = Convert.ToDecimal(45.50),
                               PublishDate = Convert.ToDateTime("2015-08-31") },
                   new Books(){BookName= "The Bourne Sanction",
                               AuthorName= "Lustbader",
                               Price = Convert.ToDecimal(55.50),
                               PublishDate = Convert.ToDateTime("2016-08-31") },
                   new Books(){BookName= "The Bourne Deception",
                               AuthorName= "Lustbader",
                               Price = Convert.ToDecimal(65.50),
                               PublishDate = Convert.ToDateTime("2017-01-31") },
                   new Books(){BookName= "The Bourne Objective",
                               AuthorName= "Lustbader",
                               Price = Convert.ToDecimal(45.50),
                               PublishDate = Convert.ToDateTime("2017-08-31") },
                   new Books(){BookName= "The Bourne Dominion",
                               AuthorName= "Lustbader",
                               Price = Convert.ToDecimal(55.50),
                               PublishDate = Convert.ToDateTime("2017-12-31") },
                   new Books(){BookName= "The Bourne Imperative",
                               AuthorName= "Lustbader",
                               Price = Convert.ToDecimal(65.50),
                               PublishDate = Convert.ToDateTime("2018-08-31") }
                };

                for (var i = 0; i < books.Count; i++)
                {
                    // Create a new message to send to the queue
                    //string messageBody = $"Message {i}";

                    string messageBody = JsonConvert.SerializeObject(books[i]);
                    Message message = new Message(Encoding.UTF8.GetBytes(messageBody));


                    //var message = new Message(Encoding.UTF8.GetBytes(messageBody));

                    // Write the body of the message to the console
                    Console.WriteLine($"Sending message: {messageBody}");

                    // Send the message to the queue
                    await queueClient.SendAsync(message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"{DateTime.Now} :: Exception: {exception.Message}");
            }
        }
    }
}
