using Config;
using Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IVRClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // not needed if already created but wont harm since emailsubscription uses topic filter which portal doesnt allow
            await CreateTopicsAndSubscriptions();

            await SendOrderMessages();

        }
        static async Task CreateTopicsAndSubscriptions()
        {
            var manager = new Manager(Settings.ConnectionString);

            await manager.CreateTopic(Settings.TopicName);
            await manager.CreateSubscription(Settings.TopicName, "WritetoDB");

            await manager.CreateSubscriptionWithSqlFilter(Settings.TopicName, "SendEmail", "sendemail = 1");

        }

        static async Task SendOrderMessages()
        {
            var orders = CreateTestOrders();

            var sender = new TopicSender(Settings.ConnectionString, Settings.TopicName);

            foreach (var order in orders)
            {
                await sender.SendOrderMessage(order);
            }

            // Always be a good Service Bus citizan...
            await sender.Close();

        }

        
        static List<IVRMessage> CreateTestOrders()
        {
            var orders = new List<IVRMessage>();

            orders.Add(new IVRMessage()
            {
                AccountName ="1234",
                 EmployeeId = "123",
                  MessageType = "Calloff",
                  SendEmail = 1
            });
            orders.Add(new IVRMessage()
            {
                AccountName = "9876",
                EmployeeId = "1211",
                MessageType = "ReportingLate"
                ,
                SendEmail = 0
            });

            // Feel free to add more orders if you like.


            return orders;
        }


    }
}
