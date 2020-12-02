using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class DecisionDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }        
        public string Status { get; set; }
        public DateTime? ActualStart { get; set; }
        public DateTime? ActualCompletion { get; set; }
        public int DecisionID { get; set; }
        public string Decision { get; set; }
        public string Object { get; set; }
        public int ObjectID { get; set; }
        public int DecisionTemplateID { get; set; }
        public int? ParentDecisionStepID { get; set; }
        public int? DecisionType { get; set; }        
        public int CurrentStep { get; set; }
        public DateTime? EstimatedStart { get; set; }
        public DateTime? ProjectedStart { get; set; }
        public DateTime? EstimatedCompletion { get; set; }
        public DateTime? ProjectedCompletion { get; set; }
        public int CreatedPersonID { get; set; }
        public int UpdatedPersonID { get; set; }
        public int? OwnerID { get; set; }
        public int? SubscriberID { get; set; }
        public bool? Delayed { get; set; }
        public int? DelayCount { get; set; }
        public int? DelayDuration { get; set; }
        public int? AssignedOrganizationID { get; set; }
        public int? AssignedPersonID { get; set; }
        public int? ChildDecisionID { get; set; }
        public int? ParentDecisionID { get; set; }
        public DateTime? CanceledDate { get; set; }
        public int? ProductOrderDetailsId { get; set; }
        public int? CollectionMemberId { get; set; }
    }
    public class DecisionONOrganizationDto:DecisionDto
    {
        public string Organization { get; set; }
    }
}
