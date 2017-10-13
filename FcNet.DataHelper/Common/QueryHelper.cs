using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FcNet.DataHelper.Common
{
    internal static class QueryHelper<TEntity>
    {
        private static readonly IDictionary<string, string> ColumnNames = new Dictionary<string, string>();

        public static string GetTableName()
        {
            return typeof(TEntity).Name;
        }

        public static string GetSelect()
        {
            string select = $"SELECT * FROM {GetTableName()}";
            return select;
        }

        public static string GetColumnName(PropertyInfo propertyInfo)
        {
            //string columnName, key = $"{propertyInfo.DeclaringType}.{propertyInfo.Name}";

            //if (ColumnNames.TryGetValue(key, out columnName))
            //    return columnName;

            //columnName = _columnNameResolver.ResolveColumnName(propertyInfo);
            //ColumnNames[key] = columnName;

            return "";//columnName;
        }

        //private static string table;

        //public QueryHelper()
        //{
        //    table = GetType().GenericTypeArguments[0].Name;
        //}


        //private static string GetTableName()
        //{
        //    var myActualType = typeof(TEntity);

        //    //table = GetType().GenericTypeArguments[0].Name;


        //    //var type = entity.GetType();
        //    return "";//GetTableName(type);
        //}




        //private string GetSelectPaging(Int32 skip = 1, Int32 pageSize = 10, string orderName = "Name", string sortDirection = "ASC", string searchValue = "")
        //{
        //    string filter = "";
        //    if (searchValue != "") filter = $" WHERE name LIKE '%{searchValue}%'";
        //    int page = skip + pageSize;

        //    string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY " + orderName + " " + sortDirection + ") AS RowNum, * " +
        //        $"FROM {table} {filter}) AS Result " +
        //        $"WHERE RowNum > {skip} AND RowNum <= {page} ORDER BY RowNum";

        //    //    totalRows = sqlConnection.Query<int>("SELECT COUNT(*) FROM erp_persons").FirstOrDefault();
        //    //    totalFilteredRows = sqlConnection.Query<int>("SELECT COUNT(*) FROM erp_persons" + filter).FirstOrDefault();

        //    return sql;
        //}
    }
}
