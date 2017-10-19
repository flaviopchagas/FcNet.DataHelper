using FcNet.DataHelper.Common;
using FcNet.DataHelper.Demo.Entity;
using System;
using System.Linq.Expressions;

namespace FcNet.DataHelper.Demo
{
    class Program
    {
        private static string path = Environment.CurrentDirectory;

        private static string sqlServerDB = @"Server=localhost;Database=GoOn;User Id=sa;Password=qwe@1234;";
        private static string sqlLocalDB = @"data source=(LocalDB)\MSSQLLocalDB;attachdbfilename=|DataDirectory|\Database\DemoDB.mdf;integrated security=True;connect timeout=30;MultipleActiveResultSets=True;";
        private static string sqLiteDB = @"Data Source=Database\sqlite.db3;";
        private static string mongoDB = @"mongodb://localhost:27017/Demo";

        private static Expression<Func<Customers, bool>> filter;

        static void Main(string[] args)
        {
            //CreateSQLiteTable();

            var employee = DataFactory<Employees>.GetInstance(sqlLocalDB);
            var allEmployees = employee.All();

            var customer = DataFactory<Customers>.GetInstance(sqlLocalDB);
            filter = (a => a.CompanyName.Contains("an") && a.CompanyName.Contains("b"));
            var getData = customer.GetData(filter);

            Console.ReadLine();
        }

        private static void CreateSQLiteTable()
        {
            SQLiteManager sq = new SQLiteManager(sqLiteDB);
            sq.CreateTable();
        }
    }
}
