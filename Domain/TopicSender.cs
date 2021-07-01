using Domain;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IVRClient
{

    public class TopicSender
    {
        private TopicClient m_TopicClient;

        public TopicSender(string connectionString, string topicPath)
        {
            m_TopicClient = new TopicClient(connectionString, topicPath);

        }

        public async Task SendOrderMessage(IVRMessage order)
        {

            Console.WriteLine($"{ order.ToString() }");

            // Serialize the order to JSON
            var orderJson = JsonConvert.SerializeObject(order);

            // Create a message containing the serialized order Json
            var message = new Message(Encoding.UTF8.GetBytes(orderJson));

            // Promote properties that you want to use as FILTERS for different subscriptions...       
            message.UserProperties.Add("messagetype", order.MessageType);
            message.UserProperties.Add("accountname", order.AccountName);
            message.UserProperties.Add("sendemail", order.SendEmail);


            // Send the message
            await m_TopicClient.SendAsync(message);
        }

        public async Task Close()
        {
            await m_TopicClient.CloseAsync();
        }




    }
}
