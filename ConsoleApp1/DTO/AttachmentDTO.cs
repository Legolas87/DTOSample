using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class AttachmentDto
    {
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
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
        public string OutboundDue { get; set; }
        public string OutboundReceived { get; set; }
        public string ReturnService { get; set; }
        public string ReturnTrackingID { get; set; }
        public string ReturnDue { get; set; }
        public string ReturnReceived { get; set; }
        public int CreatedPersonID { get; set; }
       // public DateTime? CreatedDate { get; set; }
        public int UpdatedPersonID { get; set; }
       // public DateTime UpdatedDate { get; set; }
        public string MetaXSD { get; set; }
        public string MetaXML { get; set; }
        public int? AttachmentTemplateID { get; set; }
        public string FileContent { get; set; }
        public string AttachmentExtension { get; set; }
        public string FileObjectAssembly { get; set; }
        public int? FileRowCount { get; set; }
        public int FileTypeId { get; set; }
        public string ClientFileName { get; set; }
        public int AttachmentProductId { get; set; }
    }
}
