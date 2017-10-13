using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcNet.DataHelper
{
    internal class QueryHelper<TEntity>
    {
        private static string table;
        private static string select;
        private static string selectPaging;
        private static string update;
        private static string create;
        private static string delete;

        public QueryHelper()
        {
            table = GetType().GenericTypeArguments[0].Name;
        }

        public string GetSelect()
        {
            select = $"SELECT * FROM {table}";
            return select;
        }

        public string GetSelectPaging(object entity)
        {
            return "";
        }
    }
}
