using System;

namespace Config
{
    public class Settings
    {
        //ToDo: Enter a valid Service Bus connection string
        // Copy from ServiceBus->SharedAccesPolicies->RootManageSharedAccesseKey->PrimaryConnectionString unless you created seperate key for 
        //each subscriber
        public static string ConnectionString = "Endpoint=sb://cmsivr.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=kgoAHT7xnYGuZqK28rSIb20rHJ24Nixzfn8QhSSeQrQ=";
        public static string TopicName = "cms";

        public static int threads = 1;

        public static string DBSubscription = "WriteToDB";

        public static string EmailSubscription = "SendEmail";



    }
}
