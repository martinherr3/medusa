using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;

namespace Medusa.Architecture.Utils
{
    public class BindingHelper
    {
        /// <summary>
        /// Gets the data table
        /// </summary>
        /// <param name="list">The list of business objects</param>
        /// <returns>Data table with a row for each business object in the list</returns>
        public static DataTable GetDataTable(System.Collections.IList list, Type typ)
        {
            DataTable dt = new DataTable();

            PropertyInfo[] pi = typ.GetProperties();

            foreach (PropertyInfo p in pi)
            {
                dt.Columns.Add(new DataColumn(p.Name, p.PropertyType));
            }

            foreach (object obj in list)
            {
                object[] row = new object[pi.Length];

                int i = 0;

                foreach (PropertyInfo p in pi)
                {
                    row[i++] = p.GetValue(obj, null);
                }
                dt.Rows.Add(row);
            }

            return dt;

        }

        //public static DataTable GetDataTable(System.Collections.Generic.IList<T> list)
        //{

        //    Type typ = typeof(T);

        //    DataTable dt = new DataTable();

        //    PropertyInfo[] pi = typ.GetProperties();

        //    foreach (PropertyInfo p in pi)
        //    {
        //        dt.Columns.Add(new DataColumn(p.Name, p.PropertyType));
        //    }

        //    foreach (object obj in list)
        //    {
        //        object[] row = new object[pi.Length];

        //        int i = 0;

        //        foreach (PropertyInfo p in pi)
        //        {
        //            row[i++] = p.GetValue(obj, null);
        //        }
        //        dt.Rows.Add(row);
        //    }

        //    return dt;

        //}
    }
}
