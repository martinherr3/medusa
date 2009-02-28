using System;
using System.Collections.Generic;
using System.Text;
using SmartCode.Template;
using SmartCode.Model;

namespace WebTemplates.List
{
    public class ListAspx : TemplateBase
    {
        public ListAspx()
        {
            CreateOutputFile = true;
            Description = "Generates the Aspx List for the entity";
            Name = "ListAspx";
            OutputFolder = "Web";
        }

        public override string OutputFileName()
        {
            return Helper.ClassName(Entity.Code) + "List.aspx";
        }

        public override void ProduceCode()
        {
            IList<ColumnSchema> primaryKeyColumns = Table.PrimaryKeyColumns();

            if (primaryKeyColumns.Count > 0)
            {
                string queryStringOfPK = "";
                foreach (ColumnSchema column in primaryKeyColumns)
                {
                    if (queryStringOfPK != "")
                    {
                        queryStringOfPK += "&";
                    }
                    if (primaryKeyColumns.Count == 1)
                    {
                        queryStringOfPK += String.Format(@"{0}=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, ""ID"" ).ToString() )%>", Helper.PascalCase(column.Code));
                    }
                    else
                    {
                        queryStringOfPK += String.Format(@"{0}=<%# Server.UrlEncode( DataBinder.Eval(Container.DataItem, ""{0}"" ).ToString() )%>", Helper.PascalCase(column.Code));
                    }
                }
                WriteLine(@"<%@ Page Language=""C#"" AutoEventWireup=""true"" CodeBehind=""{0}List.aspx.cs"" Inherits=""{1}.Web.{0}List"" %>", Helper.ClassName(Entity.Code), Helper.PascalCase(Domain.Code));

                WriteLine(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">");

                WriteLine(@"<html xmlns=""http://www.w3.org/1999/xhtml"" >");
                WriteLine(@"<head runat=""server"">");
                WriteLine(@"        <title>View {0}</title>", Helper.ClassName(Entity.Code));
                WriteLine(@"        <meta name=""vs_targetSchema"" content=""http://schemas.microsoft.com/intellisense/ie5"">");
                WriteLine(@"        <link rel=""stylesheet"" href=""Style.css"" type=""text/css"">");
                WriteLine(@"</head>");
                WriteLine(@"<body>");
                WriteLine(@"    <form id=""{0}Form"" method=""post"" runat=""server"">", Helper.ClassName(Entity.Code));
                WriteLine(@"            <H2 align=""center"">{0} List</H2>", Helper.ClassName(Entity.Code));
                WriteLine(@"            <table align=""center"">");
                WriteLine(@"                <tr>");
                WriteLine(@"                    <td><a href=""Default.html"">Home</a></td>");
                WriteLine(@"                </tr>");
                WriteLine(@"            </table>");
                WriteLine(@"            <br />");
                WriteLine(@"            <br />");
                WriteLine(@"            <asp:DataGrid Runat=""server"" ID=""gridData"" AllowPaging=""True"" PageSize=""25""");
                WriteLine(@"                OnPageIndexChanged=""ChangeGridPage"" AutoGenerateColumns=""False"" HorizontalAlign=""Center"">");
                WriteLine(@"                <PagerStyle Position=""Bottom"" NextPageText=""Next"" PrevPageText=""Previous"" Mode=""NextPrev"" BackColor=""#ffffff""></PagerStyle>");
                WriteLine(@"                <headerstyle cssclass=""header"" borderwidth=""0"" />");
                WriteLine(@"                <alternatingitemstyle cssclass=""alternatingitemstyle"" />");
                WriteLine(@"                <Columns>");
                WriteLine(@"                    <asp:TemplateColumn HeaderText=""Edit"">");
                WriteLine(@"                        <ItemTemplate>");
                WriteLine(@"                            <a href='{0}Edit.aspx?{1}'>", Helper.ClassName(Entity.Code), queryStringOfPK);
                WriteLine(@"                                edit</a>");
                WriteLine(@"                        </ItemTemplate>");
                WriteLine(@"                    </asp:TemplateColumn>");
                WriteLine(@"                    <asp:TemplateColumn HeaderText=""View"">");
                WriteLine(@"                        <ItemTemplate>");
                WriteLine(@"                            <a href='{0}View.aspx?{1}'>", Helper.ClassName(Entity.Code), queryStringOfPK);
                WriteLine(@"                                view</a>");
                WriteLine(@"                        </ItemTemplate>");
                WriteLine(@"                    </asp:TemplateColumn>");

                foreach (ColumnSchema column in Table.Columns())
                {
                    if (column.IsPrimaryKey && primaryKeyColumns.Count == 1)
                    {
                        WriteLine(@"                    <asp:boundcolumn datafield=""ID"" headertext=""{0}"" sortexpression=""ID"" visible=""True"" />", column.Caption);
                    }
                    else
                    {
                        WriteLine(@"                    <asp:boundcolumn datafield=""{0}"" headertext=""{1}"" sortexpression=""{0}"" visible=""True"" />", Helper.PascalCase(column.Code), column.Caption);
                    }
                }

                WriteLine(@"                </Columns>");
                WriteLine(@"            </asp:DataGrid>");
                WriteLine(@"            <table align=""center"">");
                WriteLine(@"                <tr>");
                WriteLine(@"                    <td>");
                WriteLine(@"                        <a href='{0}Edit.aspx'>Add</a>", Helper.ClassName(Entity.Code));
                WriteLine(@"                    </td>");
                WriteLine(@"                </tr>");
                WriteLine(@"            </table>");
                WriteLine(@"        </form>");
                WriteLine(@"</body>");
                WriteLine(@"</html>");
            }

        }
    }
}
