using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using LNTSlipPortal_Repository.DataServices;
using System.Data;
using LNTSlipPortal_Repository.DTO;

namespace LNTSlipPortal.Models
{
    public class AutoCompleteData
    {
        public string Mode { get; set; }
        AutoCompleteRequest obj = new AutoCompleteRequest();
        public AutoCompleteData(string mode, string keyword, string relatedTo = "")
        {
            this.Mode = mode;
            obj.Type = "List";
            obj.KeyWord = keyword; 
            if (mode == "RoleEmployee")
            {
                obj.TableName = "dbo.RoleMaster";
                obj.TextColumnName = "RoleName";
                obj.ValueColumnName = "RoleId";
                obj.WhereClause = "RoleId>1";
            }
           else if (mode == "ProjectRAWS")
            {
                obj.TableName = "dbo.Project";
                obj.TextColumnName = "ProjectName";
                obj.ValueColumnName = "ProjectId";
            }
            else if (mode == "ProductCategoryRAWS")
            {
                obj.TableName = "dbo.ProductCategory";
                obj.TextColumnName = "ProductCatNo";
                obj.ValueColumnName = "ProductCatId";
            }
            else if (mode == "ScopeRAWS")
            {
                obj.TableName = "dbo.Scope";
                obj.TextColumnName = "ScopeName";
                obj.ValueColumnName = "ScopeId";
            }

        }
        public List<AutoCompleteResponse> GetAutocompleteData()
        {
            SqlParameter[] para = new SqlParameter[6];
            para[0] = new SqlParameter().CreateParameter("@Keyword", obj.KeyWord, 100);
            para[1] = new SqlParameter().CreateParameter("@TableName", obj.TableName, -1);
            para[2] = new SqlParameter().CreateParameter("@DisplayColumnName", obj.TextColumnName, 100);
            para[3] = new SqlParameter().CreateParameter("@ValueColumnName", obj.ValueColumnName, 100);
            para[4] = new SqlParameter().CreateParameter("@WhereClause", obj.WhereClause, 500);
            para[5] = new SqlParameter().CreateParameter("@Type", obj.Type.ToUpper(), 50);
            DataTable dt = new dalc().GetDataTableByPara("dbo.GetAutoCompleteData", para);
            List<AutoCompleteResponse> lst = new List<AutoCompleteResponse>();
            foreach (DataRow dr in dt.Rows)
                lst.Add(new AutoCompleteResponse(Convert.ToString(dr["ID"]), Convert.ToString(dr["Name"])));
            return lst;
        }
    }

    public class AutoCompleteRequest
    {
        public string TextColumnName { get; set; }
        public string ValueColumnName { get; set; }
        public string TableName { get; set; }
        public string WhereClause { get; set; }
        public string KeyWord { get; set; }
        public string Type { get; set; }
    }
}