using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class NonCLEARDto : ProductOrderDto
    {
        public int QueryName { get; set; }
        
        public int AttachmentID { get; set; }
        public string Attachment { get; set; }
        public int DecisionID { get; set; }      
              

        public DateTime ActualStart { get; set; }
        public DateTime ActualCompletion { get; set; }
        public string Organization { get; set; }
        public string ProductName { get; set; }
        public int PODCount { get; set; }
    }
}

