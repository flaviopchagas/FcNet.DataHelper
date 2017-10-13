using FcNet.DataHelper.Demo.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcNet.DataHelper.Demo
{
    class Program
    {
        private static string connString = "Server=localhost;Database=GoOn;User Id=sa;Password=qwe@1234;";

        static void Main(string[] args)
        {
            IRepository<erp_persons> person = DataFactory<erp_persons>.GetInstance(connString, FrameworkType.Dapper, ProviderType.SqlClient);

            IEnumerable<erp_persons> all = person.All();
        }
    }
}
