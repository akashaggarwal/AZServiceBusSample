using Config;
using Domain;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WriteDBSubscriber
{
    class Program
    {
        static SubscriptionClient receiver;

        static async Task Main(string[] args)
        {
            var manager = new Manager(Settings.ConnectionString);

            receiver = new SubscriptionClient(Settings.ConnectionString, Settings.TopicName, Settings.DBSubscription);

            await ReceiveOrdersFromEmailSubscription();

          
            
        }

        static async Task ReceiveOrdersFromEmailSubscription()
        {

            var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
            {
                MaxConcurrentCalls = Settings.threads,
                AutoComplete = false
            };

            receiver.RegisterMessageHandler(ProcessOrderMessageAsync, messageHandlerOptions);

            Console.WriteLine("Press enter to quit...");
            Console.ReadLine();

            await receiver.CloseAsync();
        }

        private static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
        {
            Console.WriteLine($"Message handler encountered an exception {exceptionReceivedEventArgs.Exception}.");
            return Task.CompletedTask;
        }
        private static async Task ProcessOrderMessageAsync(Message message, CancellationToken token)
        {
            try
            {

                // Process the order message
                var orderJson = Encoding.UTF8.GetString(message.Body);
                var order = JsonConvert.DeserializeObject<IVRMessage>(orderJson);

                InspectMessage.Show(message);

                // any validations on message if needed maybe something that could cause DB insert to fail so you dont want that message
                // to keep reappearing even though after messagecount of 10 it will automatically go in deadletter
                //TODO
                // might not be needed since message after 10 time failure will land in deadletter but you might not know what happened

                // Send email or whatever else you want to do
                //TODO


                // Complete the message
                await receiver.CompleteAsync(message.SystemProperties.LockToken);

            }
            catch (Exception ex)
            {
                await receiver.DeadLetterAsync(message.SystemProperties.LockToken, ex.Message, ex.ToString());

            }




        }








    }
}
