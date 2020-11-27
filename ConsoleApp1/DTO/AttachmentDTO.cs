using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Models
{
    public class AttachmentDTO
    {
        public DateTime createddate { get; set; }
        public DateTime updateddate { get; set; }

        public int AttachmentID { get; set; }
        public string Attachment { get; set; }
        public string AttachmentType { get; set; }
        public string Object { get; set; }

        public int ObjectID { get; set; }
        public string SubscriberID { get; set; }
        public string Status { get; set; }
        public string OwnerID { get; set; }
        public string PathURL { get; set; }
        public string Description { get; set; }
        public DateTime? CheckOutDate { get; set; }
        public int? CheckOutPersonID { get; set; }

        public bool? CheckOutExpiration { get; set; } //in excel there were null values only
        public DateTime? CheckInDate { get; set; }
        public string CheckInComment { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string OutboundService { get; set; }
        public int? OutboundTrackingID { get; set; }
    }
}
