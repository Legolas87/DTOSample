using Automation.DataAccess.DataAccessObjects.DKP.DTO;
using Dapper;
using System.Collections.Generic;
using System.Data;

namespace Automation.DataAccess.DKP
{
    public class DailyKanbanProcessDAL
    {
        private readonly IDbConnection _dbConnection;

        public DailyKanbanProcessDAL(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        //--To check whether we requested CMAS or not ,it will log in PublicDataFileLog table if there is +(ADD) or -(Drop) in the Active CLAER monitioing count
        public IEnumerable<PublicDataFileLogDto> Check1_Query1()
        {
            const string sqlQuery = "SELECT TOP 5 PublicDataFileLogID, FileName, CreatedDate" +
                " FROM PublicDataFileLog with (nolock) order by PublicDataFileLogID desc";

            return _dbConnection.Query<PublicDataFileLogDto>(sqlQuery);
        }

        //--810 CMAS requestEVent will be triggered by 896 Schduled event will trigger daily at 1pm PST only if any +Add or -Drop in the total active CLEAR monitoring count
        public IEnumerable<EventQueueDto> Check2_Query1()
        {
            const string sqlQuery = "SELECT TOP 5 *" +
                " FROM EventQueue where EventID=896 order by QueuedDate DESC";

            return _dbConnection.Query<EventQueueDto>(sqlQuery);
        }

        //--810 CMAS requestEVent will be triggered by 896 Schduled event will trigger daily at 1pm PST only if any +Add or -Drop in the total active CLEAR monitoring count
        public IEnumerable<EventQueueDto> Check2_Query2()
        {
            const string sqlQuery = "SELECT TOP 5 *" +
                " FROM EventQueue where EventID=810 order by QueuedDate DESC";

            return _dbConnection.Query<EventQueueDto>(sqlQuery);
        }

        //--To check the recent cmas resp zip file from the attachment table
        public IEnumerable<EventDto> Check3_Query1()
        {
            const string sqlQuery = "SELECT active,MessageQueue,*" +
                " FROM Event where Event in ('Import LER Public data Monitoring Results','Queue Loan for Client to Public Match')";

            return _dbConnection.Query<EventDto>(sqlQuery);
        }


        public IEnumerable<EventQueueExtendedDto> Check4_Query1()
        {
            const string sqlQuery = "SELECT TOP 5 getdate() DateNow,*" +
                " FROM Eventqueue with (nolock) where EventID=816  ORDER BY QueuedDate DESC";

            return _dbConnection.Query<EventQueueExtendedDto>(sqlQuery);
        }

        public IEnumerable<AttachmentDto> Check5_Query1()
        {
            const string sqlQuery = "select top 5 createddate,updateddate,*" +
                " from attachment with (nolock) where attachmenttype ='CLEAR LienWatch zip' order by attachmentid desc";

            return _dbConnection.Query<AttachmentDto>(sqlQuery);
        }

        //---iMPORT PUBLIC TABLES VERIFICATION
        public IEnumerable<PublicDataImportDetailsDto> Check6_Query1(int attid)
        {
            const string sqlQuery = "select FileName,RecordsImported,ImportedRecordsCount,SkippedRecords,*" +
                " FROM PublicDataImportDetails with (nolock) where ParentAttachmentID=@attid";

            var parameters = new { attid = attid };

            return _dbConnection.Query<PublicDataImportDetailsDto>(sqlQuery, parameters);
        }
    }
}
