using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public static class InspectMessage
    {

        public static void  Show(Message message)
        {
            Console.WriteLine($"Received message...");

            Console.WriteLine("Properties");
            Console.WriteLine($"    ContentType             - { message.ContentType }");
            Console.WriteLine($"    CorrelationId           - { message.CorrelationId }");
            Console.WriteLine($"    ExpiresAtUtc            - { message.ExpiresAtUtc }");
            Console.WriteLine($"    Label                   - { message.Label }");
            Console.WriteLine($"    MessageId               - { message.MessageId }");
            Console.WriteLine($"    PartitionKey            - { message.PartitionKey }");
            Console.WriteLine($"    ReplyTo                 - { message.ReplyTo }");
            Console.WriteLine($"    ReplyToSessionId        - { message.ReplyToSessionId }");
            Console.WriteLine($"    ScheduledEnqueueTimeUtc - { message.ScheduledEnqueueTimeUtc }");
            Console.WriteLine($"    SessionId               - { message.SessionId }");
            Console.WriteLine($"    Size                    - { message.Size }");
            Console.WriteLine($"    TimeToLive              - { message.TimeToLive }");
            Console.WriteLine($"    To                      - { message.To }");

            Console.WriteLine("SystemProperties");
            Console.WriteLine($"    EnqueuedTimeUtc - { message.SystemProperties.EnqueuedTimeUtc }");
            Console.WriteLine($"    LockedUntilUtc  - { message.SystemProperties.LockedUntilUtc }");
            Console.WriteLine($"    SequenceNumber  - { message.SystemProperties.SequenceNumber }");

            Console.WriteLine("UserProperties");
            foreach (var property in message.UserProperties)
            {
                Console.WriteLine($"    { property.Key } - { property.Value }");
            }

            Console.WriteLine("Body");
            Console.WriteLine($"{ Encoding.UTF8.GetString(message.Body) }");
            Console.WriteLine();
        }

    }
}
