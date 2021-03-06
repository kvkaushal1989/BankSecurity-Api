﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model.Common;
using Model.BankSecurity.MasterSetup;
using IRepository.BankSecurity.MasterSetup;
using HK.Utility;
using HK.DBManager;
using HK.Extensions;
using System.Data;
using Repository.BankSecurity.MasterSetup;

namespace Repository.BankSecurity.MasterSetup {
    public class Wem_BlacklistaccRepository :IWem_BlacklistaccRepository {
        #region Create New Row
        public Dictionary<string, object> CreateRow(Wem_Blacklistacc data) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { data }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.Post("BS_Wem_BlacklistaccCreate_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                var Result = DataSet.Tables[1].AsEnumerable().Select(m => new { ID = m.Field<long>("ID"), Code = m.Field<string>("Code") }).ToList().FirstOrDefault();
                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, Result);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion

        #region Update Row
        public Dictionary<string, object> UpdateRow(Wem_Blacklistacc data) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { data }.ToList();

            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.Put("BS_Wem_BlacklistaccUpdate_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                var Result = DataSet.Tables[1].AsEnumerable().Select(m => new { ID = m.Field<long>("ID"), Code = m.Field<string>("Code") }).ToList().FirstOrDefault();
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
                DataSet = DBManager.Post("BS_Wem_BlacklistaccDelete_PROC", InputParameters);
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
                DataSet = DBManager.GetDataSet("BS_Wem_BlacklistaccViewRowByID_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                List<Wem_Blacklistacc> data = (DataSet.Tables[1]).ConvertToList<Wem_Blacklistacc>();

                ResponseData = Response.SuccessResponse<object>(Status.Type, Status.Code, Status.Description, data);
            }
            else {
                ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
            }
            return ResponseData;
        }
        #endregion

        #region Pagination
        public Dictionary<string, object> PaginationMessage(List<Wem_Blacklistacc> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount) {
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
        public List<Wem_Blacklistacc> PaginationSettings(string filter, string searchText, int pageSize, int pageNumber, ref int totalRecordCount, ref int totalPages, ref int skipRecords) {
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

        List<Wem_Blacklistacc> PaginationList(int skipRecords, int pageSize, string filter, string searchText) {
            try {
                DataSet DataSet = new DataSet();
                var InputParameters = new[] { new { PageSize = pageSize, PageNumber = skipRecords, Filter = filter, SearchText = searchText } }.ToList();

                using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                    DataSet = DBManager.GetDataSet("BS_Wem_BlacklistaccPaginationList_PROC", InputParameters);
                }

                Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

                if (Status.IsSuccess == true) {
                    return DataSet.Tables[1].ConvertToList<Wem_Blacklistacc>();
                }
                else {
                    return null;
                }
            }
            catch (Exception ex) {
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
                    DataSet = DBManager.GetDataSet("BS_Wem_BlacklistaccPaginationListTotal_PROC", InputParameters);
                }

                Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

                if (Status.IsSuccess == true) {
                    var Result = DataSet.Tables[1].AsEnumerable().Select(m => new { Total = m.Field<int>("Total") }).FirstOrDefault();

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
                //DataSet = DBManager.GetDataSet("BS_Wem_BlacklistaccDropDownList_PROC", InputParameters);
                DataSet = DBManager.GetDataSet("BS_CMC_COMBANKDropDownList_PROC");
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];

            if (Status.IsSuccess == true) {
                List<Wem_Blacklistacc> data = (DataSet.Tables[1]).ConvertToList<Wem_Blacklistacc>();

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
