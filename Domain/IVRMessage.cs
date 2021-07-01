using System;

namespace Domain
{
    public class IVRMessage
    {
        public string AccountName { get; set; } // could use Account name to send specific messages to client to dfferent subscription if needed
        public string EmployeeId { get; set; }
        // probably should use enum here but for quick demo
        public string MessageType { get; set; } // calloff or Reporting Late if you really want to use topic filters to send messages to seperte subscription

        public int SendEmail { get; set; }
    }
}
