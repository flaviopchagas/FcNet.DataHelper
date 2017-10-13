using FcNet.DataHelper.Common;
using FcNet.DataHelper.Demo.Entity;
using System.Collections.Generic;

namespace FcNet.DataHelper.Demo
{
    class Program
    {
        private static string connString = "Server=localhost;Database=GoOn;User Id=sa;Password=qwe@1234;";

        static void Main(string[] args)
        {
            IDataFactory<erp_persons> person = DataFactory<erp_persons>.GetInstance(connString, FrameworkType.Dapper, ProviderType.SqlClient);

            IEnumerable<erp_persons> all = person.All();
        }
    }
}
