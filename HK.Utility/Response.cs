using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HK.Utility
{
    public class Response
    {
        public static Dictionary<string, object> SuccessResponse<T>(string status, string statusCode, string statusDescription, T Response) where T : class
        {
            Dictionary<string, object> SuccessResponse = new Dictionary<string, object>();
            SuccessResponse.Add("Status", status);
            SuccessResponse.Add("StatusCode", statusCode);
            SuccessResponse.Add("StatusDescription", statusDescription);
            SuccessResponse.Add("Response", Response);
            return SuccessResponse;
        }
        public static Dictionary<string, object> SuccessResponse<T>(string status, string statusCode, string statusDescription)
        {
            Dictionary<string, object> SuccessResponse = new Dictionary<string, object>();
            SuccessResponse.Add("Status", status);
            SuccessResponse.Add("StatusCode", statusCode);
            SuccessResponse.Add("StatusDescription", statusDescription);
            return SuccessResponse;
        }

        public static Dictionary<string, object> ErrorResponse(string status, string statusCode, string statusDescription)
        {
            Dictionary<string, object> ErrorResponse = new Dictionary<string, object>();
            ErrorResponse.Add("Status", status);
            ErrorResponse.Add("StatusCode", statusCode);
            ErrorResponse.Add("StatusDescription", statusDescription);
            return ErrorResponse;
        }
    }

    public static class Connection {
        ///// <summary>
        ///// Cofiguration Database Connection
        ///// </summary>
        ///// <returns></returns>
        //public static string GetOperationConnection() {
        //    return ConfigurationManager.ConnectionStrings["CofigurationConnection"].ToString();
        //}

        /// <summary>
        /// Operation Database Connection
        /// </summary>
        /// <returns></returns>
        public static string GetOperationConnection() {
            return ConfigurationManager.ConnectionStrings["OperationConnection"].ToString();
        }

        /// <summary>
        /// Operation Database Connection
        /// </summary>
        /// <returns></returns>
        public static string GetVMConnection() {
            return ConfigurationManager.ConnectionStrings["VMConnection"].ToString();
        }


    }
}
