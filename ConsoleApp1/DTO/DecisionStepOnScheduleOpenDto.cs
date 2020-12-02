using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class DecisionStepOnScheduleOpenDto : ScheduleOpenDto
    {
        public int DecisionStepID { get; set; }
        public int DecisionID { get; set; }
       
    }   
}
