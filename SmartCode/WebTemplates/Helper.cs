using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Kontac.Net.SmartCode.Model;

namespace WebTemplates
{
    public class Helper
    {
        private static Regex cleanRegEx;
        static Helper()
        {
            cleanRegEx = new Regex(@"\s+|_|-|\.", RegexOptions.Compiled);
        }


        public static string ClassName(string name)
        {
            return MakeSingle(name);
        }

        public static string CleanName(string name)
        {
            return cleanRegEx.Replace(name, "");
        }

        public static string CamelCase(string name)
        {
            string output = CleanName(name);
            return char.ToLower(output[0]) + output.Substring(1);
        }

        public static string PascalCase(string name)
        {
            string output = CleanName(name);
            return char.ToUpper(output[0]) + output.Substring(1);
        }

        public static string MakePlural(string name)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
            Regex plural3 = new Regex("(?<keep>[sxzh])$");
            Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

            if (plural1.IsMatch(name))
                return plural1.Replace(name, "${keep}ies");
            else if (plural2.IsMatch(name))
                return plural2.Replace(name, "${keep}s");
            else if (plural3.IsMatch(name))
                return plural3.Replace(name, "${keep}es");
            else if (plural4.IsMatch(name))
                return plural4.Replace(name, "${keep}s");

            return name;
        }

        public static string MakeSingle(string name)
        {
            Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
            Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
            Regex plural3 = new Regex("(?<keep>[sxzh])es$");
            Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

            if (plural1.IsMatch(name))
                return plural1.Replace(name, "${keep}y");
            else if (plural2.IsMatch(name))
                return plural2.Replace(name, "${keep}");
            else if (plural3.IsMatch(name))
                return plural3.Replace(name, "${keep}");
            else if (plural4.IsMatch(name))
                return plural4.Replace(name, "${keep}");

            return name;
        }

        public static bool IsManyToManyTable(TableSchema table)
        {
            return (table.Columns.Count == 2 && table.HasPrimaryKey() && table.PrimaryKeyColumns().Count == 2 && table.InReferences.Count == 2);
        }

        public static bool IsChildFKColumn(ColumnSchema column, TableSchema table)
        {
            foreach (ReferenceSchema inReference in table.InReferences)
            {
                foreach (ReferenceJoin join in inReference.Joins)
                {
                    //Found the child Column...
                    if (join.ChildColumn.ObjectID == column.ObjectID)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static bool IsNullableType(ColumnSchema column)
        {
            return !column.IsRequired && column.NetDataType != "System.String";
        }

        public static string GetConvertToNETType(ColumnSchema  column, string strExpression)
        {
            switch (column.NetDataType)
            {
                case "System.Int32":
                    return "Convert.ToInt32(" + strExpression + ")";
                case "System.Byte":
                    return "Convert.ToByte(" + strExpression + ")";
                case "System.Boolean":
                    return "Convert.ToBoolean(" + strExpression + ")";
                case "System.Char":
                    return "Convert.ToChar(" + strExpression + ")";
                case "System.DateTime":
                    return "Convert.ToDateTime(" + strExpression + ")";
                case "System.Decimal":
                    return "Convert.ToDecimal(" + strExpression + ")";
                case "System.Double":
                    return "Convert.ToDouble(" + strExpression + ")";
                case "System.Int16":
                    return "Convert.ToInt16(" + strExpression + ")";
                case "System.Int64":
                    return "Convert.ToInt64(" + strExpression + ")";
                case "System.SByte":
                    return "Convert.ToSByte(" + strExpression + ")";
                case "System.Single":
                    return "Convert.ToSingle(" + strExpression + ")";
                case "System.TimeSpan":
                    return "Convert.ToDateTime(" + strExpression + ")";
                case "System.UInt16":
                    return "Convert.ToUInt16(" + strExpression + ")";
                case "System.UInt32":
                    return "Convert.ToUInt32(" + strExpression + ")";
                case "System.UInt64":
                    return "Convert.ToUInt64 (" + strExpression + ")";
                case "System.Guid":
                    return "Convert.ToString(" + strExpression + ")";
                case "System.Byte[]":
                    return "(Byte[])(" + strExpression + ")";
                default:
                    return "Convert.ToString(" + strExpression + ")";
            }

        }

        internal static string GetValueOrText(ColumnSchema column)
        {
            if (column.IsIdentity)
            {
                return "Value";
            }
            else
            {
                if (column.Control is Kontac.Net.SmartCode.Model.Profile.ComboBox)
                {
                    return "SelectedValue";
                }
                else if (column.Control is Kontac.Net.SmartCode.Model.Profile.CheckBox)
                {
                    return "Checked";
                }
                else
                {
                    return "Text";
                }
            }
        }

    }
}
