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

        /*CMAS request for Active CLEAR Monitroing Loans */
        public IEnumerable<PublicDataFileLogDto> Check1_Query1()
        {
            const string sqlQuery = "SELECT TOP 5 PublicDataFileLogID, FileName, CreatedDate" +
                " FROM PublicDataFileLog with (nolock) order by PublicDataFileLogID desc";

            return _dbConnection.Query<PublicDataFileLogDto>(sqlQuery);
        }    
    }
}
