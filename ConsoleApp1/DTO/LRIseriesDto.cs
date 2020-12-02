using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    /// <summary>
    /// ATTACHMENT on productOrder on decision on 
    /// </summary>
    public class LRIseriesDto : ProductOrderDto
    {
        public int QueryName { get; set; }
        public int FileName { get; set; }
        public int AttachmentID { get; set; }
        public int DecisionID { get; set; }
        public string PathURL { get; set; }       

    }
}

