using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
            where = where.Replace("Convert", "").Replace("True", "1").Replace("False", "0");

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

        public static List<KeyValuePair<string, string>> GetProperties(object item)
        {
            var result = new List<KeyValuePair<string, string>>();

            if (item != null)
            {
                var type = item.GetType();
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var pi in properties)
                {
                    var selfValue = type.GetProperty(pi.Name).GetValue(item, null);

                    if (selfValue != null)
                    {
                        result.Add(new KeyValuePair<string, string>(pi.Name, selfValue.ToString()));
                    }
                    else
                    {
                        result.Add(new KeyValuePair<string, string>(pi.Name, null));
                    }
                }
            }
            return result;
        }

        public string GetNamePropertyWithDataAnnotationKey(object instance)
        {
            // Just grabbing this to get hold of the type name:
            var type = instance.GetType();

            // Get the PropertyInfo object:
            var properties = type.GetProperties();

            string name = "";
            var property = properties.FirstOrDefault(p => p.GetCustomAttributes(false).Any(a => a.GetType() == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)));
            if (property != null)
            {
                //Nome da primarykey
                name = $"{property.Name}";
            }

            return name;
        }

        private string GenerateInsertValues(object filters)
        {
            string namePK = GetNamePropertyWithDataAnnotationKey(filters) + ",";
            string valuePK = $"@{namePK}";

            var filtersFields = filters.GetType().GetProperties().Select(a => a.Name).ToArray();
            if (!filtersFields?.Any() ?? true) throw new ArgumentException($"The parameter filters isn't valid. This parameter must be a class type", nameof(filters));

            var propertiesNames = filtersFields.Select(a => $"{a},").ToArray();
            var propertiesValue = filtersFields.Select(a => $"{charParam}{a},").ToArray();

            string names = "";
            string values = "";

            foreach (var item in propertiesNames)
            {
                if (item != namePK)
                    names = names + item;
            }

            foreach (var item in propertiesValue)
            {
                if (item != valuePK)
                    values = values + item;
            }

            names = names.Remove(names.Length - 1);
            values = values.Remove(values.Length - 1);

            return $"({names}) values ({values}); select last_insert_rowid();";
        }

        private string GenerateUpdateValues(object filters)
        {
            string namePK = GetNamePropertyWithDataAnnotationKey(filters);

            //Pega o valor da PK
            var valuePK = filters.GetType().GetProperty(namePK).GetValue(filters, null);

            var filtersFields = filters.GetType().GetProperties().Select(a => a.Name).ToArray();
            if (!filtersFields?.Any() ?? true) throw new ArgumentException($"The parameter filters isn't valid. This parameter must be a class type", nameof(filters));

            var propertiesNames = filtersFields.Select(a => $"{a}= {charParam}{a},").ToArray();

            string names = "";
            foreach (var item in propertiesNames)
            {
                names = names + item;
            }

            names = names.Remove(names.Length - 1);

            return $"{names} where {namePK} = {valuePK}";
        }

        public string GenerateDelete(object key)
        {
            var where = GenerateWhere(key);
            return $"DELETE FROM {tableName} {where} ";
        }

        public string GenetareInsert(TEntity parameters)
        {
            var values = GenerateInsertValues(parameters);

            var query = $"insert into {tableName} {values}";
            return query;
        }

        public string GenerateUpdate(object pks)
        {
            var values = GenerateUpdateValues(pks);
            var query = $"update {tableName} set {values}";

            return query;
        }

        private bool IsComplexType(PropertyInfo propertyInfo)
        {
            bool result = (propertyInfo.PropertyType.IsClass && propertyInfo.PropertyType.Name != "String") || propertyInfo.PropertyType.IsInterface;
            return result;
        }
    }
}
