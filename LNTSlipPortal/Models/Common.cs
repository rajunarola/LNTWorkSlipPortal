using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LNTSlipPortal.Models;
using System.Text;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

namespace LNTSlipPortal.Models
{
    public class CommonController : Controller
    {
        public static void SetMessage(Controller ctr, string Message, string cls = "success", string BoldText = "")
        {
            ctr.TempData["IsShowMsg"] = true;
            ctr.TempData["G_Msg"] = Message;
            ctr.TempData["G_Cls"] = cls.ToLower();
            ctr.TempData["G_BoldText"] = BoldText + " !";
            //return View();
        }
    }
    public class Common
    {
        //Define the tripple Des Provider
        public TripleDESCryptoServiceProvider m_des = new TripleDESCryptoServiceProvider();

        //Define the string Handler
        public UTF8Encoding m_utf8 = new UTF8Encoding();

        //Define the local Propertirs Array
        public byte[] m_key = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 14, 13, 15, 16, 17, 18, 19, 20, 21, 22, 24, 23 };
        public byte[] m_iv = { 8, 7, 6, 5, 4, 3, 2, 1 };

        public string Encypt(string text)
        {
            byte[] input = m_utf8.GetBytes(text);
            byte[] output = Transform(input, m_des.CreateEncryptor(m_key, m_iv));
            return Convert.ToBase64String(output);
        }
        public string Decrypt(string text)
        {
            byte[] input = Convert.FromBase64String(text);
            byte[] output = Transform(input, m_des.CreateDecryptor(m_key, m_iv));
            return m_utf8.GetString(output);
        }

        private byte[] Transform(byte[] input, ICryptoTransform cryptoTransform)
        {
            //Create the Neccessary streams
            MemoryStream memStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memStream, cryptoTransform, CryptoStreamMode.Write);
            //transform the bytes as requested
            cryptoStream.Write(input, 0, input.Length);
            cryptoStream.FlushFinalBlock();
            //Reasd the memory stream and convert it to byte array
            memStream.Position = 0;
            byte[] result = new byte[Convert.ToInt32(memStream.Length - 1) + 1];
            memStream.Read(result, 0, Convert.ToInt32(result.Length));
            memStream.Close();
            cryptoStream.Close();
            return result;
        }

        public string DataTableToJSON(DataTable table)
        {

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow row in table.Rows)
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();

                foreach (DataColumn col in table.Columns)
                {
                    dict[col.ColumnName] = row[col];
                }
                list.Add(dict);
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return serializer.Serialize(list);
        }

        public string getclass(string color)
        {
            if (color == "Green")
            {
                return "online-status-online";
            }
            else if (color == "Yellow")
            {
                return "online-status-recent";
            }
            else
            {
                return "online-status-offline";
            }

        }

        public enum Role
        {
            Admin = 1,
            PMG1 = 2,
            PMG2 = 3,
            SCG1 = 4,
            SCG2 = 5,
            QC = 6,
            Planner =7,
            Shop =8
        }

        public enum RAWSStatus
        {
            Draft = 1,
            Initiated = 2,
            Approved_by_PMG_SCG = 3,
            Acknowledged_by_QC = 4,
            Accepted_by_Shop = 5,
            Validated_by_Planner = 6,
            Checked_verified = 7,
            Closed = 8
        }

    }



    public static class CommonMethods
    {
        public static int GetProperInt(this string str)
        {
            try
            {
                return Convert.ToInt32(str);
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public static List<T> ConvertToList<T>(this DataTable dt)
        {
            List<T> data = new List<T>();
            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }
            return data;
        }
        public static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                foreach (PropertyInfo pro in temp.GetProperties())
                {
                    if (pro.Name == column.ColumnName)
                    {
                        if (!string.IsNullOrEmpty(Convert.ToString(dr[column.ColumnName])))
                            pro.SetValue(obj, dr[column.ColumnName]);
                    }
                    else
                        continue;
                }
            }
            return obj;
        }





        public static void ExportToExcel(this DataTable dt, string FileName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (string.IsNullOrEmpty(FileName))
                FileName = "Excel";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
             "attachment;filename=" + FileName + ".xls");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //for (int i = 0; i < GridView1.Rows.Count; i++)
            //{
            //    //Apply text style to each Row
            //    GridView1.Rows[i].Attributes.Add("class", "textmode");
            //}
            GridView1.RenderControl(hw);
            //Open a memory stream that you can use to write back to the response
            byte[] byteArray = Encoding.ASCII.GetBytes(sw.ToString());
            MemoryStream s = new MemoryStream(byteArray);
            StreamReader sr = new StreamReader(s, Encoding.ASCII);
            HttpContext.Current.Response.Write(sr.ReadToEnd());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportToWord(this DataTable dt, string FileName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (string.IsNullOrEmpty(FileName))
                FileName = "Word";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + FileName + ".doc");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-word ";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportToPdf(this DataTable dt, string FileName)
        {
            GridView GridView1 = new GridView();
            GridView1.AllowPaging = false;
            //GridView1.HeaderStyle.BackColor = System.Drawing.Color.Black;
            //GridView1.HeaderStyle.ForeColor = System.Drawing.Color.White;
            GridView1.HeaderStyle.Font.Size = 12;
            GridView1.HeaderStyle.Font.Bold = true;
            GridView1.Font.Size = 10;
            GridView1.DataSource = dt;
            GridView1.DataBind();
            if (string.IsNullOrEmpty(FileName))
                FileName = "Pdf";
            HttpContext.Current.Response.ContentType = "application/pdf";
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + FileName + ".pdf");
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            StringReader sr = new StringReader(sw.ToString());
            Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            HttpContext.Current.Response.Write(pdfDoc);
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public static void ExportToCsv(this DataTable dt, string FileName)
        {
            if (string.IsNullOrEmpty(FileName))
                FileName = "csv";
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.AddHeader("content-disposition",
                "attachment;filename=" + FileName + ".csv");
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/text";


            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator
                sb.Append(dt.Columns[k].ColumnName + ',');
            }
            //append new line
            sb.Append("\r\n");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');
                }
                //append new line
                sb.Append("\r\n");
            }
            HttpContext.Current.Response.Output.Write(sb.ToString());
            HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
            HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }



    }
}