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

        #region check 1

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

        #endregion

        #region check 2

        //#LR Iseries/DALS
        public IEnumerable<LRIseriesDto> Check2_Query1()
        {
            const string sqlQuery = "SELECT '#LR Iseries/DALS' QueryName,at.Attachment FileName,At.AttachmentID,po.ProductOrderID,Ds.DecisionID,At.CreatedDate,po.OrganizationID,po.ProductID,po.productOrder,at.PathURL,ds.Status" +
                " from productOrderDetails pd with (nolock) inner join productOrder po with(nolock) on po.ProductOrderID = pd.ProductOrderID" +
                " where pd.status in ('Queued for Valuation','Valuation In process') and po.productID=23 and po.organizationid in (1592589)" +
                " group by po.organizationid,pd.status";

            return _dbConnection.Query<LRIseriesDto>(sqlQuery);
        }


        //#Non-CLEAR
        public IEnumerable<NonCLEARDto> Check2_Query2()
        {
            const string sqlQuery = "select '#Non-CLEAR' QueryName,po.ProductOrderID,po.ProductOrder,at.AttachmentID,at.Attachment,ds.DecisionID,ds.ActualStart,ds.status,ds.ActualCompletion,po.CreatedDate, org.Organization,p.ProductName,po.OrganizationID,po.productid,po.productOrderType,po.status ,pod.PODCount,at.FileRowCount,ds.* " +
                " from productorder po with (nolock) inner join organization org with(nolock) on po.OrganizationID = org.OrganizationID inner join product p with(nolock) on po.productID = p.ProductID" +
                " OUTER APPLY (SELECT pd.productORderID,count(pd.loanid) PODCount FROM productORderDetails pd with(nolock) WHERE po.productORderID = pd.ProductOrderID group by pd.productORderID)POD" +
                " OUTER APPLY (SELECT att.AttachmentID,att.Attachment,att.FileRowCount FROM Attachment att with(nolock) WHERE po.AttachmentID = att.AttachmentID AND(ATT.Attachment NOT LIKE '%ISERIES%' AND ATT.Attachment NOT LIKE '%DALSINPUT%'))at" +
                " OUTER APPLY (select d.DecisionID,dt.DecisionTemplateID,dt.DecisionTemplate,dt.Object,dt.ReportID,d.ActualStart,d.status,d.ActualCompletion from product p with(nolock) inner join DecisionTemplate dt with(nolock) on dt.DecisionTemplateID = p.DecisionID AND p.ProductID = po.ProductID inner join Decision d with(nolock) on d.DecisionTemplateID = dt.DecisionTemplateID WHERE d.Object = 'ProductOrder' AND d.ObjectID = po.ProductOrderID)ds" +
                "  where po.createddate >= getdate()-@d AND po.AttachmentID = at.AttachmentID and po.productID != 23 order by po.CreatedDate";

            return _dbConnection.Query<NonCLEARDto>(sqlQuery);
        }

        //#CLEAR
        public IEnumerable<CLEARDto> Check2_Query3()
        {
            const string sqlQuery = "select '#CLEAR', po.ProductOrderID,po.ProductOrder,at.AttachmentID,at.Attachment, poSum.TotalCountInFile,poSum.SuccessCount,poSum.CLRRCount,poSum.InputErrorCount,poSum.PublicDataErrorCount,poSum.TotalCountVerfication,poSum.SuccessCountVerfication,poSum.FileType,po.CreatedDate,org.Organization,p.ProductName,po.OrganizationID,po.productid,po.productOrderType,po.status ,pod.PODCount,at.FileRowCount" +
                " from productorder po with (nolock) inner join organization org with(nolock) on po.OrganizationID = org.OrganizationID inner join product p with(nolock) on po.productID = p.ProductID" +
                " OUTER APPLY (SELECT pd.productORderID,count(pd.loanid) PODCount FROM productORderDetails pd with(nolock) WHERE po.productORderID = pd.ProductOrderID group by pd.productORderID)POD" +
                " OUTER APPLY (SELECT att.AttachmentID,att.Attachment,att.FileRowCount FROM Attachment att with(nolock) WHERE po.AttachmentID = att.AttachmentID)at" +
                " OUTER APPLY (SELECT FileType,TotalCountInFile,SuccessCount,CLRRCount,ValidationErrorDupCount,ValidationErrorCount,InputErrorCount,PublicDataErrorCount," +
                " CASE when ISNULL(TotalCountInFile, 0) = (ISNULL(InputErrorCount,0) + ISNULL(SuccessCount,0)) then ISNULL(TotalCountInFile, 0) Else(ISNULL(InputErrorCount, 0) + ISNULL(SuccessCount, 0)) - ISNULL(TotalCountInFile, 0) end as TotalCountVerfication," +
                " CASE when ISNULL(SuccessCount, 0) = (ISNULL(PublicDataErrorCount,0) + ISNULL(CLRRCount,0)) then ISNULL(SuccessCount, 0) Else(ISNULL(PublicDataErrorCount, 0) + ISNULL(CLRRCount, 0)) - ISNULL(SuccessCount, 0) end as SuccessCountVerfication" +
                " FROM VproductOrderSummary VpoSum with (nolock) WHERE po.AttachmentID = VpoSum.AttachmentID AND po.ProductOrderID = VpoSum.ProductOrderID)poSum" +
                " where po.createddate >= getdate()-5 and po.productID = 23 order by po.CreatedDate";



            return _dbConnection.Query<CLEARDto>(sqlQuery);
        }

        #endregion


        #region check 3

        const int d = 5;
        public IEnumerable<ScheduleOpenDto> Check3_Query1()
        {
            const string sqlQuery = "SELECT 'Schedule open' QueryName ,GETDATE() DateNow,e.Event,e.active, e.EventService, e.MessageQueue, eq.*" +
                " FROM   EventQueue eq (nolock) INNER JOIN Event e(nolock) ON e.EventID = eq.EventID" +
                " where eq.Object ='Schedule' AND eq.FailureDate IS NULL AND eq.FailureMessage IS NULL AND eq.successDate IS  NULL AND eq.SuccessMessage IS NULL AND eq.MSMQBatchID IS NOT NULL AND eq.QueuedDate <= GETDATE() - @d ORDER BY eq.QueuedDate DESC";

            var parameters = new { d = d };
            return _dbConnection.Query<ScheduleOpenDto>(sqlQuery, parameters);
        }

        public IEnumerable<ScheduleOpenDto> Check3_Query2()
        {
            const string sqlQuery = "SELECT 'Schedule open1' QueryName ,e.Event,e.active, e.EventService, e.MessageQueue, eq.*" +
                " FROM   EventQueue eq (nolock) INNER JOIN Event e(nolock) ON e.EventID = eq.EventID where eq.Object = 'Schedule'" +
                " AND eq.FailureMessage IS not NULL AND eq.successDate IS  NULL AND eq.QueuedDate >= GETDATE()-10 ORDER BY eq.QueuedDate DESC";

            return _dbConnection.Query<ScheduleOpenDto>(sqlQuery);
        }

        public IEnumerable<DecisionStepOnScheduleOpenDto> Check3_Query3()
        {
            const string sqlQuery = "SELECT top 100 d.*,e.Event, e.EventService, e.MessageQueue, eq.*" +
                " FROM   EventQueue eq (nolock) INNER JOIN Event e(nolock) ON e.EventID = eq.EventID" +
                " outer apply (select ds.DecisionStepID,ds.DecisionID from DecisionStep ds with(nolock) where ds.DecisionStepID = CAST(SUBSTRING(eq.QueuedUser, CHARINDEX(' ', eq.QueuedUser, 1), 15) AS INT))d" +
                "where e.EventID NOT IN (666,161,752,759) AND eq.Object != 'Schedule' AND e.excludeFromMonitoring != 0 AND eq.FailureMessage IS NOT NULL AND eq.successDate IS  NULL AND eq.SuccessMessage IS NULL AND eq.QueuedDate >= GETDATE() - @d ORDER BY eq.QueuedDate DESC";

            return _dbConnection.Query<DecisionStepOnScheduleOpenDto>(sqlQuery);
        }


        public IEnumerable<ScheduleOpenDto> Check3_Query4()
        {
            const string sqlQuery = "SELECT 'Schedule EQs' QueryName,e.Event, e.EventService, e.MessageQueue, eq.*" +
                " FROM   EventQueue eq (nolock) INNER JOIN Event e(nolock) ON e.EventID = eq.EventID" +
                " where eq.Object ='Schedule' AND eq.FailureMessage IS NOT NULL AND eq.successDate IS  NULL AND eq.SuccessMessage IS NULL AND eq.QueuedDate >= GETDATE() - @d ORDER BY eq.QueuedDate DESC";

            var parameters = new { d = d };
            return _dbConnection.Query<ScheduleOpenDto>(sqlQuery, parameters);
        }

        public IEnumerable<ScheduleOpenDto> Check3_Query5()
        {
            const string sqlQuery = "select top 100 'PollinEQs' QueryName,d.*,et.event, eq.*" +
                " from eventqueue eq inner join Event et on et.EventID = eq.EventID" +
                " outer apply (select ds.DecisionStepID,ds.DecisionID from DecisionStep ds with(nolock) where ds.DecisionStepID = CAST(SUBSTRING(eq.QueuedUser, CHARINDEX(' ', eq.QueuedUser, 1), 15) AS INT))d" +
                " where et.excludeFromMonitoring is null and(eq.FailureMessage is not null and eq.SuccessMessage is NULL) AND eq.QueuedDate >= GETDATE() - @d order by eq.Queueddate desc";

            var parameters = new { d = d };
            return _dbConnection.Query<ScheduleOpenDto>(sqlQuery, parameters);
        }

        #endregion
    }
}
