using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class ProductOrderDto
    {
        public int ProductOrderID { get; set; }
        public int OrganizationID { get; set; }
        public int ProductID { get; set; }
        public string ProductOrder { get; set; }
        public string Status { get; set; }
        public string ProductOrderType { get; set; }
        public DateTime CreatedDate { get; set; }

    } 
}

