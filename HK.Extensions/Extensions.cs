using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace HK.Extensions {
    public static class Extensions {
        public static DataTable ToDataTable<T>(this IList<T> list, string tableName = null) {
            DataTable table;
            PropertyDescriptorCollection props =
                TypeDescriptor.GetProperties(typeof(T));
            Type[] nested = typeof(T).GetNestedTypes();
            if (!string.IsNullOrEmpty(tableName))
                table = new DataTable(tableName);
            else
                table = new DataTable(typeof(T).Name.ToString());
            int j = 0;
            for (int i = 0; i < props.Count; i++) {
                PropertyDescriptor prop = props[i];

                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    j++;
                }
                else {
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }
            }
            object[] values = new object[props.Count - j];
            foreach (T item in list) {
                for (int i = 0; i < values.Length; i++) {
                    values[i] = props[i].GetValue(item);
                }
                table.Rows.Add(values);
            }
            return table;
        }

        public static DataTable ObjectToData(object o)
        {
            DataTable dt = new DataTable("OutputData");

            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);

            o.GetType().GetProperties().ToList().ForEach(f =>
            {
                try
                {
                    f.GetValue(o, null);
                    dt.Columns.Add(f.Name, f.PropertyType);
                    dt.Rows[0][f.Name] = f.GetValue(o, null);
                }
                catch { }
            });
            return dt;
        }

        public static List<T> ConvertToList<T>(this DataTable table) where T : class, new() {
            try {
                var list = new List<T>();

                foreach (var row in table.AsEnumerable()) {
                    T obj = new T();

                    foreach (var prop in obj.GetType().GetProperties()) {
                        try {
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                           // propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex) {
                            continue;
                        }
                    }

                    list.Add(obj);
                }
                return list;
            }
            catch (Exception ex) {
                return null;
            }
        }

        public static T ConvertToObject<T>(this DataTable table) where T : class, new()
        {
            try
            {
                T obj = new T();
                foreach (var row in table.AsEnumerable())
                {
                    foreach (var prop in obj.GetType().GetProperties())
                    {
                        try
                        {
                            var propertyInfo = obj.GetType().GetProperty(prop.Name);
                            // propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
                            propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType), null);
                        }
                        catch (Exception ex)
                        {
                            continue;
                        }
                    }
                }
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder ConvertToString(this DataTable table) {
            StringBuilder str = new StringBuilder();
             
            foreach (DataRow row in table.Rows) {
                str.Append(row.Field<string>(0));
            }

            return str;
        }
    }
}
