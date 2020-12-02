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

        #region CMAS request for Active CLEAR Monitroing Loans

        //To check whether we requested CMAS or not ,it will log in PublicDataFileLog table if there is +(ADD) or -(Drop) in the Active CLAER monitioing count
        public IEnumerable<PublicDataFileLogDto> Check1_Query1()
        {
            const string sqlQuery = "SELECT TOP 5 *" +
                " FROM PublicDataFileLog with (nolock) order by PublicDataFileLogID desc";

            return _dbConnection.Query<PublicDataFileLogDto>(sqlQuery);
        }

        //--810 CMAS requestEVent will be triggered by 896 Schduled event will trigger daily at 1pm PST only if any +Add or -Drop in the total active CLEAR monitoring count
        public IEnumerable<EventQueueDto> Check1_Query2()
        {
            const string sqlQuery = "SELECT TOP 5 *" +
                " FROM EventQueue where EventID=896 order by QueuedDate DESC";

            return _dbConnection.Query<EventQueueDto>(sqlQuery);
        }

        //--810 CMAS requestEVent will be triggered by 896 Schduled event will trigger daily at 1pm PST only if any +Add or -Drop in the total active CLEAR monitoring count
        public IEnumerable<EventQueueDto> Check1_Query3()
        {
            const string sqlQuery = "SELECT TOP 5 *" +
                " FROM EventQueue where EventID=810 order by QueuedDate DESC";

            return _dbConnection.Query<EventQueueDto>(sqlQuery);
        }
        #endregion

        #region CMAS Response file import monitoring

        //--To check the recent cmas resp zip file from the attachment table
        public IEnumerable<EventDto> Check1_Query4()
        {
            const string sqlQuery = "SELECT active,MessageQueue,*" +
                " FROM Event where Event in ('Import LER Public data Monitoring Results','Queue Loan for Client to Public Match')";

            return _dbConnection.Query<EventDto>(sqlQuery);
        }



        public IEnumerable<EventQueueExtendedDto> Check1_Query5()
        {
            const string sqlQuery = "SELECT TOP 5 getdate() DateNow,*" +
                " FROM Eventqueue with (nolock) where EventID=816  ORDER BY QueuedDate DESC";

            return _dbConnection.Query<EventQueueExtendedDto>(sqlQuery);
        }

        public IEnumerable<AttachmentDto> Check1_Query6()
        {
            const string sqlQuery = "select top 5 createddate,updateddate,*" +
                " from attachment with (nolock) where attachmenttype ='CLEAR LienWatch zip' order by attachmentid desc";

            return _dbConnection.Query<AttachmentDto>(sqlQuery);
        }

        #endregion

        #region IMPORT PUBLIC TABLES VERIFICATION

        const int attid = 58767304;
        public IEnumerable<PublicDataImportDetailsDto> Check1_Query7()
        {
            const string sqlQuery = "select FileName,RecordsImported,ImportedRecordsCount,SkippedRecords,*" +
                " FROM PublicDataImportDetails with (nolock) where ParentAttachmentID=@attid";

            var parameters = new { attid = attid };

            return _dbConnection.Query<PublicDataImportDetailsDto>(sqlQuery, parameters);
        }

        public IEnumerable<TblCnt> Check1_Query8()
        {
            const string sqlQuery =
                "SELECT 'BSE--PublicBaseData'    tbl,COUNT(*) cnt FROM PublicBaseData    WITH (NOLOCK) WHERE ParentAttachmentID = @attid UNION" +
                "SELECT 'HOA--PublicHOA'         tbl,COUNT(*) cnt FROM PublicHOA         WITH (NOLOCK) WHERE ParentAttachmentID = @attid UNION" +
                "SELECT 'LNS--PublicBKInv'       tbl,COUNT(*) cnt FROM PublicBKInv       WITH (NOLOCK) WHERE ParentAttachmentID = @attid UNION" +
                "SELECT 'MRT--PublicMortgage'    tbl,COUNT(*) cnt FROM PublicMortgage    WITH (NOLOCK) WHERE ParentAttachmentID = @attid UNION" +
                "SELECT 'PRP--PublicProperty'    tbl,COUNT(*) cnt FROM PublicProperty    WITH (NOLOCK) WHERE ParentAttachmentID = @attid UNION" +
                "SELECT 'TRN--PublicTransaction' tbl,COUNT(*) cnt FROM PublicTransaction WITH (NOLOCK) WHERE ParentAttachmentID = @attid";

            var parameters = new { attid = attid };

            return _dbConnection.Query<TblCnt>(sqlQuery, parameters);
        }



        //--810 CMAS requestEVent will be triggered by 896 Schduled event will trigger daily at 1pm PST only if any +Add or -Drop in the total active CLEAR monitoring count
        public IEnumerable<EventQueueDto> Check1_Query9()
        {
            const string sqlQuery = "SELECT TOP 5 getdate() 'DateNow',*" +
                " FROM Eventqueue with (nolock) where EventID=816 AND ObjectID=@attid ORDER BY QueuedDate DESC";

            var parameters = new { attid = attid };
            return _dbConnection.Query<EventQueueDto>(sqlQuery, parameters);
        }

        //--810 CMAS requestEVent will be triggered by 896 Schduled event will trigger daily at 1pm PST only if any +Add or -Drop in the total active CLEAR monitoring count
        public IEnumerable<EventQueueDto> Check1_Query10()
        {
            const string sqlQuery = "SELECT TOP 5 getdate() 'DateNow',*" +
                " FROM Eventqueue with (nolock) where EventID=811 AND ObjectID=@attid ORDER BY QueuedDate DESC";

            var parameters = new { attid = attid };
            return _dbConnection.Query<EventQueueDto>(sqlQuery, parameters);
        }
        #endregion

        #region TO VERIFY THE clear DECISIONS ,Decisions will create as part of 811 EQ process---

        //To see the list CLEAR decision whcih are not completed from last 3 days
        public IEnumerable<DecisionONOrganizationDto> Check1_Query11()
        {
            const string sqlQuery = "dn.CreatedDate,dn.UpdatedDate,org.Organization,dn.Status,dn.ActualStart,dn.ActualCompletion,dn.*" +
                " from decision dn with (nolock)" +
                " inner join organization org with(nolock)" +
                " on org.OrganizationID = dn.ObjectID and dn.Object = 'Organization'" +
                " where decisionTemplateID = 686-- - 653 is old clear tempalteID" +
                " and cast(dn.CreatedDate as date) >= CAST(GETDATE() - 3 AS date)" +
                " and dn.Status is NULL   --you can comment this line to verify the completed decisions" +
                " order by dn.DecisionID desc";

            return _dbConnection.Query<DecisionONOrganizationDto>(sqlQuery);
        }

        const int decisionID = 7417656;
        //To see the details of the decision WITH DecisionID
        public IEnumerable<DecisionDto> Check1_Query12()
        {
            const string sqlQuery = "DecisionID,Decision,Object,ObjectID,DecisionTemplateID,Status,ActualCompletion,CreatedPersonID,CreatedDate,UpdatedPersonID,UpdatedDate,CanceledDate,*" + //'*AllResult'tblResult,
                " FROM Decision (NOLOCK) WHERE DecisionID = @DecisionID";

            var parameters = new { DecisionID = decisionID };
            return _dbConnection.Query<DecisionDto>(sqlQuery, parameters);
        }

        public IEnumerable<EventOnEventQueueDto> Check1_Query13()
        {
            const string sqlQuery = "SELECT et.Event,eq.*,et.*" +
                " FROM EventQueue eq WITH(NOLOCK)" +
                " INNER JOIN DecisionStep ds WITH(NOLOCK) ON eq.EventQueueID = ds.ObjectID AND ds.Object = 'EventQueue'" +
                " INNER JOIN Event et WITH(NOLOCK) ON et.EventID = eq.EventID" +
                " WHERE ds.DecisionID = @DecisionID ORDER BY eq.QueuedUser DESC";

            return _dbConnection.Query<EventOnEventQueueDto>(sqlQuery);
        }

        #endregion

        #region To veirfy the Loan POD status

        public IEnumerable<PODOrganizationDto> Check1_Query14()
        {
            const string sqlQuery = "select po.organizationid,pd.status,count(1)" +
                " from productOrderDetails pd with (nolock) inner join productOrder po with(nolock) on po.ProductOrderID = pd.ProductOrderID" +
                " where pd.status in ('Queued for Valuation','Valuation In process') and po.productID=23 and po.organizationid in (1592589)" +
                " group by po.organizationid,pd.status";

            return _dbConnection.Query<PODOrganizationDto>(sqlQuery);
        }

        public IEnumerable<PODLoanDto> Check1_Query15()
        {
            const string sqlQuery = "select loanid,status,count(1)" +
                " from serviceOrder where createdDate>= cast(GETDATE()-10 as date) and loanid in" +
                " (select pd.loanid from productOrderDetails pd with(nolock) inner join productOrder po with(nolock)" +
                " on po.ProductOrderID = pd.ProductOrderID" +
                " where pd.status in ('Queued for Valuation', 'Valuation In process') and po.productID = 23 and po.organizationid in (1592159)) " +
                " group by loanid, status having loanid> 1";

            return _dbConnection.Query<PODLoanDto>(sqlQuery);
        }

        #endregion
    }
}
