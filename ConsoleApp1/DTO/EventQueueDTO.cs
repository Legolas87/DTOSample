using System;
namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class EventQueueDto
    {
        public int EventQueueID { get; set; }
        public int EventID { get; set; }
        public string Object { get; set; }
        public int ObjectID { get; set; }
        public DateTime QueuedDate { get; set; }
        public string QueuedUser { get; set; }
        public DateTime? ExecuteDate { get; set; }
        public string ExecuteUser { get; set; }
        public DateTime? SuccessDate { get; set; }
        public string SuccessMessage { get; set; }
        public DateTime? FailureDate { get; set; }
        public string FailureMessage { get; set; }
        public int? ExecuteCount { get; set; }
        public DateTime? RequeueDate { get; set; }
        public string RequeueMessage { get; set; }
        public int? QueuedUserID { get; set; }
        public int? Priority { get; set; }
        public string MSMQBatchID { get; set; }
        public int? CreatedPersonID { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? OrderID { get; set; }
        public bool? Async { get; set; } // not sure about the type
        public string PartitionDateKey { get; set; }
        public bool? Archived { get; set; }
    }


    public class EventQueueExtendedDto: EventQueueDto
    {
        public DateTime DateNow { get; set; }
    }
}

