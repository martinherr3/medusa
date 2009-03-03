using System;
using System.Collections.Generic;
using System.Text;
using Kontac.Net.SmartCode.Model.Templates;
using Kontac.Net.SmartCode.Model;

namespace WebTemplates.Edit
{
    public class EditAspx : EntityTemplate
    {
        public EditAspx()
        {
            CreateOutputFile = true;
            Description = "Generates the Aspx Edit page for the entity";
            Name = "EditAspx";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + "Edit.aspx";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();

            if (primaryKeyColumns.Count > 0)
            {
                WriteLine(@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind=""{0}Edit.aspx.cs"" Inherits=""{1}.Web.{0}Edit"" %>", Helper.ClassName(Entity.Code), Helper.PascalCase(Project.Code));

                WriteLine(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">");

                WriteLine(@"<html xmlns=""http://www.w3.org/1999/xhtml"" >");
                WriteLine(@"<head runat=""server"">");
                WriteLine(@"        <title>{0} Edit</title>", Entity.Caption );
                WriteLine(@"        <meta name=""vs_targetSchema"" content=""http://schemas.microsoft.com/intellisense/ie5"">");
                WriteLine(@"        <link rel=""stylesheet"" href=""Style.css"" type=""text/css"">");
                WriteLine(@"</head>");
                WriteLine(@"<body>");
                WriteLine(@"<H2 align=""center"">{0} Edit</H2>", Entity.Caption);
                WriteLine(@"        <table align=""center"">");
                WriteLine(@"            <tr>");
                WriteLine(@"                <td><a href=""Default.html"">Home</a></td>");
                WriteLine(@"            </tr>");
                WriteLine(@"        </table>");
                WriteLine(@"        <form id=""form"" method=""post"" runat=""server"">");
                foreach (ColumnSchema column in Table.Columns)
                {
                    if (column.IsIdentity)
                    {
                        WriteLine(@"             <input type=""hidden"" id=""ui{0}"" runat=""server"" />", column.Code);
                    }
                }
                WriteLine(@"            <table align=""center"">");
                foreach (ColumnSchema column in Table.Columns)
                {
                    if (!column.IsIdentity && column.NetDataType != "System.Byte[]")
                    {
                        WriteLine(@"                <tr>");
                        WriteLine(@"                    <td class=""label"">{0}</td>", column.Caption );
                        if (column.Control is Kontac.Net.SmartCode.Model.Profile.ComboBox)
                        {
                            WriteLine(@"                    <td><asp:DropDownList ID=""ui{0}"" Runat=""server"" Width=""200"" /></td>", column.Code);
                        }
                        else if (column.Control is Kontac.Net.SmartCode.Model.Profile.CheckBox)
                        {
                            WriteLine(@"                    <td><asp:CheckBox ID=""ui{0}"" Runat=""server"" /></td>", column.Code);
                        }
                        else
                        {
                            WriteLine(@"                    <td><asp:TextBox ID=""ui{0}"" Runat=""server"" /></td>", column.Code);
                        }
                        WriteLine(@"                </tr>");
                    }
                }

                WriteLine(@"            </table>");
                WriteLine(@"            <table align=""center"">");
                WriteLine(@"                <tr>");
                WriteLine(@"                    <td>");
                WriteLine(@"                        <asp:LinkButton Runat=""server"" ID=""UpdateButton"" OnClick=""Update"" Text=""Save"" />");
                WriteLine(@"                        <asp:LinkButton Runat=""server"" ID=""DeleteButton"" OnClick=""Delete"" Text=""Delete"" />");
                WriteLine(@"                        <a href=""{0}List.aspx"">View {1} List</a>", Helper.ClassName(Entity.Code), Entity.Caption);
                WriteLine(@"                    </td>");
                WriteLine(@"                </tr>");
                WriteLine(@"                <tr>");
                WriteLine(@"                    <td>");
                WriteLine(@"                        <asp:Label ID=""SaveLabel"" runat=""server"" ForeColor=""Red"" />");
                WriteLine(@"                    </td>");
                WriteLine(@"                </tr>");
                WriteLine(@"                <tr>");
                WriteLine(@"                    <td>");
                foreach (ColumnSchema column in Table.Columns)
                {
                    if (!column.IsIdentity && column.NetDataType != "System.Byte[]" && column.IsRequired && !(column.Control is Kontac.Net.SmartCode.Model.Profile.CheckBox ))
                    {
                        WriteLine(@"                        <asp:RequiredFieldValidator id=""ui{0}Validator"" ControlToValidate=""ui{0}"" ErrorMessage=""{1} is a required field."" runat=""server"" /><br />", column.Code , column.Caption );
                    }
                }
                WriteLine(@"                    </td>");
                WriteLine(@"                </tr>");
                WriteLine(@"                <tr style=""visibility: hidden""><td><input type=""checkbox"" runat=""server"" id=""uiIsNew"" checked  /></td></tr>");
                WriteLine(@"            </table>");
                WriteLine(@"        </form>");
                WriteLine(@"</body>");
                WriteLine(@"</html>");

            }

        }
    }
}
