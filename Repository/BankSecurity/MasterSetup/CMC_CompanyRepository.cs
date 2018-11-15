﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Common;
using Model.BankSecurity.MasterSetup;
using IRepository.BankSecurity.MasterSetup;
using HK.Utility;
using HK.DBManager;
using System.Data;
using HK.Extensions;

namespace Repository.BankSecurity.MasterSetup {
    public class CMC_CompanyRepository : ICMC_CompanyRepository {

        #region Create New Row
        public Dictionary<string, object> CreateRow(CMC_Company data) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { data }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.Post("BS_CMC_CompanyCreate_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                var Result = DataSet.Tables[1].AsEnumerable().Select(m => new { ID = m.Field<long>("COMPANY_ID"), Code = m.Field<string>("FLDESC") }).ToList().FirstOrDefault();
                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, Result);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion

        #region Update Row
        public Dictionary<string, object> UpdateRow(CMC_Company data) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { data }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.Put("BS_CMC_CompanyUpdate_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                var Result = DataSet.Tables[1].AsEnumerable().Select(m => new { ID = m.Field<long>("COMPANY_ID"), Code = m.Field<string>("FLDESC") }).ToList().FirstOrDefault();
                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, Result);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion

        #region Delete Row
        public Dictionary<string, object> DeleteRow(List<int> idArray) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { new { IDArray = string.Join(",", idArray) } }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.Post("BS_CMC_CompanyDelete_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                var Result = DataSet.Tables[1].AsEnumerable().Select(m => new { Status = m.Field<string>("Status") }).ToList().FirstOrDefault();
                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, Result);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion

        #region View Row By ID
        public Dictionary<string, object> ViewRow(long id) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { new { ID = id } }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.GetDataSet("BS_CMC_CompanyViewRowByID_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                List<CMC_Company> data = (DataSet.Tables[1]).ConvertToList<CMC_Company>();

                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, data);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion

        #region Pagination
        public Dictionary<string, object> PaginationMessage(List<CMC_Company> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount) {
            Dictionary<string, object> jsondata = new Dictionary<string, object>();
            Dictionary<string, object> recordData = new Dictionary<string, object>();
            try {
                recordData.Add("FirstPage", linkBuilder.FirstPage != null ? linkBuilder.FirstPage.ToString() : null);
                recordData.Add("PreviousPage", linkBuilder.PreviousPage != null ? linkBuilder.PreviousPage.ToString() : null);
                recordData.Add("NextPage", linkBuilder.NextPage != null ? linkBuilder.NextPage.ToString() : null);
                recordData.Add("LastPage", linkBuilder.LastPage != null ? linkBuilder.LastPage.ToString() : null);
                recordData.Add("TotalRecords", totalRecordCount.ToString());
                recordData.Add("Records", paginatedRowArray);

                jsondata.Add("Response", "Success");
                jsondata.Add("Code", "FCS00000");
                jsondata.Add("Description", "Success");
                jsondata.Add("Data", recordData);
                return jsondata;
            }
            catch (Exception ex) {
                return Response.ErrorResponse("Error", "BS00000", "Internal Server Error");
            }
        }

        /// <summary>
        /// Pagination Settings
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="searchText"></param>
        /// <param name="pageSize"></param> 
        /// <param name="pageNumber"></param>
        /// <param name="totalRecordCount"></param>
        /// <param name="totalPages"></param>
        /// <param name="skipRecords"></param>
        /// <returns></returns>
        public List<CMC_Company> PaginationSettings(string filter, string searchText, int pageSize, int pageNumber, ref int totalRecordCount, ref int totalPages, ref int skipRecords) {
            try {
                totalRecordCount = GetAllCount(filter, searchText);
                totalPages = totalRecordCount > 0
                    ? (int)Math.Ceiling(totalRecordCount / (double)pageSize)
                    : 0;
                skipRecords = (pageNumber - 1) * pageSize;
                return PaginationList(skipRecords, pageSize, filter, searchText);
            }
            catch {
                return null;
            }
        }

        List<CMC_Company> PaginationList(int skipRecords, int pageSize, string filter, string searchText) {
            try {
                DataSet DataSet = new DataSet();
                var InputParameters = new[] { new { PageSize = pageSize, PageNumber = skipRecords, Filter = filter, SearchText = searchText } }.ToList();

                using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                    DataSet = DBManager.GetDataSet("BS_CMC_CompanyPaginationList_PROC", InputParameters);
                }

                Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

                if (Status.IsSuccess == true) {
                    return DataSet.Tables[1].ConvertToList<CMC_Company>(); 
                }
                else {
                    return null;
                }
            }
            catch(Exception ex) {
                return null;
            }
        }

        /// <summary>
        /// Get All Row Count
        /// </summary> 
        /// <param name="filter"></param>
        /// <param name="searchText"></param>
        /// <returns></returns>
        public int GetAllCount(string filter, string searchText) { 
            try {
                DataSet DataSet = new DataSet();
                var InputParameters = new[] { new { Filter = filter, SearchText = searchText } }.ToList();

                using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                    DataSet = DBManager.GetDataSet("BS_CMC_CompanyPaginationListTotal_PROC", InputParameters);
                }

                Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

                if (Status.IsSuccess == true) {
                    var Result = DataSet.Tables[1].AsEnumerable().Select(m =>  new { Total = m.Field<int>("Total") }).FirstOrDefault();

                    return Result.Total;
                }
                else {
                    return 0;
                }
            }
            catch (Exception ex) {
                return 0;
            }
        }
        #endregion

        #region Get Drop DownList
        public Dictionary<string, object> GetDropDownList() {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            //var InputParameters = new[] { property }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                //DataSet = DBManager.GetDataSet("BS_CMC_CompanyDropDownList_PROC", InputParameters);
                DataSet = DBManager.GetDataSet("BS_CMC_CompanyDropDownList_PROC");
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                List<CMC_Company> data = (DataSet.Tables[1]).ConvertToList<CMC_Company>();

                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, data);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion
    }
}
