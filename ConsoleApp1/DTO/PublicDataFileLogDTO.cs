using System;

namespace ConsoleApp1.Models
{
    public class PublicDataFileLogDTO
    {
        public int PublicDataFileLogID { get; set; }
        public string FileName { get; set; }
        public int FileCount { get; set; }
        public int DataBaseCount { get; set; }
        public string Status { get; set; }  //enum?
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int CreatedPersonID { get; set; }
        public int UpdatedPersonID { get; set; }
    }
}
