using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace FcNet.DataHelper.Common
{
    public class QueryGenerator<TEntity> where TEntity : class
    {
        private PropertyInfo[] properties;
        private string[] propertiesNames;
        private string tableName;
        private string charParam;
        private string prefix = "";

        public QueryGenerator(char charParameter = '@')
        {
            Type type = typeof(TEntity);

            charParam = charParameter.ToString();
            properties = type.GetProperties();
            propertiesNames = properties.Where(a => !IsComplexType(a)).Select(a => a.Name).ToArray();
            tableName = type.Name;
        }

        public string ToSqlString(Expression<Func<TEntity, bool>> filter = null)
        {
            string select = GenerateSelect();
            string where = "";

            if (filter != null)
            {
                where = GenerateWhere(filter);
                select = select.Replace(tableName, $"{prefix}.{tableName}");
            }

            return $"{select} {where} ";
        }

        public string GenerateSelect()
        {
            return $"SELECT * FROM {tableName}";
        }

        public string GenerateSelect(Expression<Func<TEntity, bool>> filter)
        {
            string select = GenerateSelect();
            string where = GenerateWhere(filter);

            return $"{select} {where}";
        }

        public string GenerateSelect(object fieldsFilter)
        {
            var select = GenerateSelect();
            var where = GenerateWhere(fieldsFilter);

            return $" {select} {where}";
        }

        private string GenerateWhere(Expression<Func<TEntity, bool>> filter)
        {
            if (filter is null)
                return "";

            prefix = filter.Parameters[0].Name;
            string where = filter.Body.ToString();

            where = where.Replace("AndAlso", "AND").Replace("OrElse", "OR").Replace("\"", "'");

            while (where.Contains("Contains"))
            {
                var c_ini = where.Substring(where.IndexOf(".Contains"));
                var c_par = c_ini.Substring(0, c_ini.IndexOf(")") + 1);
                var c_val = c_par.Replace(".Contains('", "").Replace("')", "");

                where = where.Replace(c_par, $" LIKE '%{c_val}%'");
            }

            where = where.Replace($"{prefix}.", "");

            return $" WHERE {where} ";
        }

        private string GenerateWhere(object filtersPKs)
        {
            var filtersPksFields = filtersPKs.GetType().GetProperties().Select(a => a.Name).ToArray();
            if (!filtersPksFields?.Any() ?? true) throw new ArgumentException($"The parameter filtersPks isn't valid. This parameter must be a class type", nameof(filtersPKs));

            var propertiesWhere = filtersPksFields.Select(a => $"{a} = {charParam}{a}").ToArray();
            var strWhere = string.Join(" AND ", propertiesWhere);

            return $" WHERE {strWhere} ";
        }

        public string GenerateDelete(object parameters)
        {
            var where = GenerateWhere(parameters);
            return $"DELETE FROM {tableName} {where} ";
        }

        public string GenerateUpdate(object pks)
        {
            var pksFields = pks.GetType().GetProperties().Select(a => a.Name).ToArray();
            var sb = new StringBuilder($"UPDATE {tableName} SET ");
            var propertiesNamesDef = propertiesNames.Where(a => !pksFields.Contains(a)).ToArray();
            var propertiesSet = propertiesNamesDef.Select(a => $"{a} = {charParam}{a}").ToArray();
            var strSet = string.Join(",", propertiesSet);
            var where = GenerateWhere(pks);

            sb.Append($" {strSet} {where} ");

            return sb.ToString();
        }

        private bool IsComplexType(PropertyInfo propertyInfo)
        {
            bool result = (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String") || propertyInfo.PropertyType.IsInterface;
            return result;
        }
    }
}
