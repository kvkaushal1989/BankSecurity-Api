using HK.DBManager;
using HK.Extensions;
using HK.Utility;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
//using Newtonsoft.Json;



namespace WebApi.Repository
{
    public class AuthRepository
    {
        DBManager dbutility = new DBManager();
        StringBuilder sqlQuery;

        //public async Task<Dictionary<string, object>> FindUser(User user)
        //{
        //    sqlQuery = new StringBuilder();
        //    string loginData = string.Empty;
        //    try
        //    {
        //        DataSet DataSet = new DataSet();
        //        Dictionary<string, object> ResponseData = new Dictionary<string, object>();
        //        using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
        //            DataSet = DBManager.GetDataSet("SSO_UserAuthorize", user);
        //        }

        //        Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];
        //        //sqlQuery.Append("select [LoginID],[DefaultPMSCode] FROM [dbo].[SSO_Users] WHERE IsDeleted = 0 AND [LoginID]=@LoginID and [LoginPassword]=@LoginPassword");
        //        //SqlCommand selectCommand = new SqlCommand(sqlQuery.ToString());
        //        //selectCommand.Parameters.AddWithValue("LoginID", userName);
        //        //selectCommand.Parameters.AddWithValue("LoginPassword", password);

        //        //loginData = dbutility.ExecuteScalarWithReturnValueString(selectCommand);
        //        if (Status.IsSuccess == true) {
        //            var OutputData = JsonConvert.DeserializeObject(HK.Extensions.Extensions.ConvertToString(DataSet.Tables[1]).ToString());
        //            ResponseData = Response.SuccessResponse(Status.Type, Status.Code, Status.Description, OutputData);
        //        }
        //        else {
        //            ResponseData = Response.ErrorResponse(Status.Type, Status.Code, Status.Description);
        //        }
        //        return ResponseData;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Diagnostics.Debug.WriteLine("SQL Exception : " + ex.Message);
        //        return null;
        //    }
        //}
    }
}