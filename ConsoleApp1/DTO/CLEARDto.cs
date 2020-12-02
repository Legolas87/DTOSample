using System;

namespace Automation.DataAccess.DataAccessObjects.DKP.DTO
{
    public class CLEARDto : ProductOrderDto
    {
        public int QueryName { get; set; }
        
        public int AttachmentID { get; set; }
        public string Attachment { get; set; }
        public int FileRowCount { get; set; }


        public int TotalCountInFile { get; set; }
        public int SuccessCount { get; set; }
        public int CLRRCount { get; set; }
        public int InputErrorCount { get; set; }
        public int PublicDataErrorCount { get; set; }
        public int TotalCountVerfication { get; set; }
        public int SuccessCountVerfication { get; set; }
        public int FileType { get; set; }      

        public DateTime ActualStart { get; set; }
        public DateTime ActualCompletion { get; set; }
        public string Organization { get; set; }
        public string ProductName { get; set; }
        public int PODCount { get; set; }
       
    }
}

