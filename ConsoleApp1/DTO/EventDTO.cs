using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class EventDto
    {
        public bool active { get; set; }
        public string MessageQueue { get; set; }
        public int EventID { get; set; }
        public string Event { get; set; }
        public string SubscriberID { get; set; }
        public string Object { get; set; }
        public int? ObjectID { get; set; }
        public string ObjectType { get; set; } //in excel there were null values only
        public string ObjectStatus { get; set; } // in excel there were null values only
        public int EventType { get; set; }
        public int EventTrigger { get; set; }
        public string EventQuery { get; set; }
        public string EventXsltUrl { get; set; }
        public string EventWebservice { get; set; }
        public string EventService { get; set; }
        public string EventXslt { get; set; }
        public int? ReportID { get; set; }
        public string EventSchema { get; set; }
        public bool Active { get; set; }
        public bool Async { get; set; }
        public bool MemberSpecific { get; set; }
        public int ExecuteCount { get; set; } 
        public DateTime CreatedDate { get; set; }
        public int CreatedPersonID { get; set; }
        public bool? EmailSuccess { get; set; }
        public bool? EmailFailure { get; set; }
        public bool? EmailExecute { get; set; }
        public bool? WebServiceSuccess { get; set; }
        public bool? WebServiceFailure { get; set; }
        public bool? WebServiceExecute { get; set; }
        public int? ScheduleID { get; set; }
        public bool? ObjectTypeExclude { get; set; }
        public bool? ObjectStatusExclude { get; set; }
        public int? RequeueScheduleID { get; set; }
        public int? Priority { get; set; }
        public int ImpersonateType { get; set; }
        public int? ImpersonateID { get; set; }
        public int? CredentialID { get; set; }
        public bool ImportResultXml { get; set; }
        public int? ImportResultXsltID { get; set; }
        public bool ExcludeFromMonitoring { get; set; }
    } 
}

