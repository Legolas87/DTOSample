using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class PublicDataImportDetailsDto
    {
        public string FileName { get; set; }
        public bool RecordsImported { get; set; }
        public int ImportedRecordsCount { get; set; }
        public int SkippedRecords { get; set; }
        public int PublicDataImportDetailsID { get; set; }
        public int ParentAttachmentID { get; set; }
        public int CreatedPersonID { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedPersonID { get; set; }
        public DateTime? UpdatedDate { get; set; }      
    }
}
