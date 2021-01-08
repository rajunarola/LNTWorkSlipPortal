using LNTSlipPortal.Models;
using LNTSlipPortal_Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static LNTSlipPortal.Models.Common;

namespace LNTSlipPortal.Models
{
    public class GridData
    {
        public string TableName { get; set; }
        public string ColumnsName { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public int PageNumber { get; set; }
        public int RecordPerPage { get; set; }
        public string WhereClause { get; set; }
        public string JsonData { get; set; }
        public string ExportedColumns { get; set; }
        public string ExportedFileName { get; set; }

        public GridData(string type,Boolean IsExport = false, int UserRole=0)
        {
            if (type == "EmployeeMaster")
            {
                this.ColumnsName = "EmployeeId,EmployeeName,R.RoleId,PSNumber,CreatedAt,R.RoleName,IsActive,IsDelete";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "EmployeeId";
                this.SortOrder = "desc";
                this.TableName = @"dbo.EmployeeMaster AS E
                                INNER JOIN dbo.RoleMaster AS R ON R.RoleId=E.RoleId";
                this.WhereClause = "IsDelete != 1";
                this.ExportedFileName = "Employee";
                this.ExportedColumns = "EmployeeId[Hidden],EmployeeName,R.RoleId,CreatedAt,R.RoleName";
                GridFunctions oGrid = new GridFunctions();
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<EmployeeMaster>(this);
                else
                    oGrid.Export(this);
            }

            else if (type == "RAWS")
            {
                this.ColumnsName = "RAWSID,RAWSNO,P.ProjectName,P.ProjectId,PC.ProductCatId,PC.ProductCatNo,U.RoleId,U.UserName,IsActive,ProductDescription,CreatedBy,Status,ST.StatusName,S.ScopeName,CreatedAt,ActualManHours";
                this.PageNumber = 1;
                this.RecordPerPage = 10;
                this.SortColumn = "RAWSID";
                this.SortOrder = "desc";
                this.TableName = @"dbo.RAWS AS R
                                INNER JOIN dbo.Project AS P ON P.ProjectId=R.ProjectId
                                INNER JOIN dbo.Scope AS S ON S.ScopeId=R.ScopeId
                                INNER JOIN dbo.Status AS ST ON R.Status = ST.StatusId
                                INNER JOIN dbo.UserMaster AS U ON U.UserId = R.CreatedBy
                                INNER JOIN dbo.ProductCategory AS PC ON PC.ProductCatId=R.ProductCatId";

                var forPMG1_SCG1 = UserRole == (int)Role.PMG1 || UserRole == (int)Role.SCG1 ? " and Status >= " + (int)RAWSStatus.Draft : "";
                var forPMG2_SCG2 = UserRole == (int)Role.PMG2 || UserRole == (int)Role.SCG2 ? " and Status >= " + (int)RAWSStatus.Initiated : "";
                var forQC = UserRole == (int)Role.QC  ? "and Status >= " + (int)RAWSStatus.Approved_by_PMG_SCG : "";
                var forPlanner = UserRole == (int)Role.Planner ? "and Status >= " + (int)RAWSStatus.Accepted_by_Shop : "";
                var forShop = UserRole == (int)Role.Shop ? "and Status >= " + (int)RAWSStatus.Acknowledged_by_QC : "";
                var forRolePMG1 = UserRole == (int)Role.PMG1 ? " and  RoleId !=" + (int)Role.SCG1 : "";
                var forRoleSCG1 = UserRole == (int)Role.SCG1 ? " and  RoleId !=" + (int)Role.PMG1 : "";
                var forRoleSCG2 = UserRole == (int)Role.SCG2 ? " and  RoleId !=" + (int)Role.PMG1 : "";
                var forRolePMG2 = UserRole == (int)Role.PMG2 ? " and  RoleId !=" + (int)Role.SCG1 : "";


                this.WhereClause = "ISNULL(IsActive,1)=1 " + forPMG1_SCG1 + forPMG2_SCG2 + forQC + forPlanner + forShop + forRolePMG1 + forRolePMG2 + forRoleSCG1 + forRoleSCG2; 
                this.ExportedFileName = "RAWS";
                this.ExportedColumns = "RAWSID[Hidden],P.ProjectName,PC.ProductCatNo,CreatedAt,S.ScopeName";
                GridFunctions oGrid = new GridFunctions();
                if (!IsExport)
                    this.JsonData = oGrid.GetJson<RAW>(this);
                else
                    oGrid.Export(this);
            }
        }
    }
}
