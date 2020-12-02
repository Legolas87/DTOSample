using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{

    public class ScheduleOpenDto : EventQueueDto
    {
        public int QueryName { get; set; }
        public DateTime DateNow { get; set; }


        public string Event { get; set; }
        public bool active { get; set; }
        public string EventService { get; set; }
        public string MessageQueue { get; set; }

    }
}

