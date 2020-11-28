using Automation.DataAccess.DKP;
using System;
using System.Data.SqlClient;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            SqlConnection dbConnection = new SqlConnection("Data Source=localhost;Initial Catalog=testDb;Integrated Security=True");

            DailyKanbanProcessDAL d = new DailyKanbanProcessDAL(dbConnection);
            var r =d.Check1_Query1();
        }
    }
}
