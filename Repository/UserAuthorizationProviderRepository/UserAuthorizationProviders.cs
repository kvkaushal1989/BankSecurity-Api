using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using HK.Utility;
using HK.Extensions;
using IRepository.AuthorizationProvider;
using HK.DBManager;
using Model.Common;

namespace WebApi.Repository {
    public class UserAuthorizationProviders: IUserAuthorizationProviders {

        public Boolean UserAuthorizationStatus(AuthorizationInput authorizationInput) {
            DataSet DataSet = new DataSet();
            var InputParameters = new[] { new { LoginID = authorizationInput.UserName, Password = authorizationInput.Password } }.ToList();
            using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
                DataSet = DBManager.Post("BS_UserAuthorizationStatusGET_PROC", InputParameters);
            }

            Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];
            if (Status.IsSuccess == true) {
                return true;
            }
            return false;
        }



        //public TimezoneInfoDetails UserAuthorizationStatusWithTimeZone(AuthorizationInput authorizationInput) {
        //    DataSet DataSet = new DataSet();
        //    TimezoneInfoDetails TimeZoneInfo = null;
        //    var InputParameters = new[] { new { LoginID = authorizationInput.UserName, Password = authorizationInput.Password } }.ToList();
        //    using (var DBManager = new DBManager(Connection.GetOperationConnection())) {
        //        DataSet = DBManager.Post("OP_UserAuthorizationStatusGET", InputParameters);
        //    }

        //    Status Status = (DataSet.Tables[0]).ConvertToList<Status>()[0];
        //    if (Status.IsSuccess == true) {
        //        TimeZoneInfo = (DataSet.Tables[1]).ConvertToList<TimezoneInfoDetails>()[0];
        //        TimeZoneInfo.PropertyTimeOffsetInMinutes = TimeZoneInfo.GetOffsetIntoMinutes(TimeZoneInfo.PropertyOffSet);
        //        TimeZoneInfo.ServerTimeOffsetInMinutes = TimeZoneInfo.GetOffsetIntoMinutes(TimeZoneInfo.ServerOffSet);
        //        TimeZoneInfo.TimeZoneDiffrenceInMinutes = TimeZoneInfo.PropertyTimeOffsetInMinutes - TimeZoneInfo.ServerTimeOffsetInMinutes;

        //        return TimeZoneInfo;
        //    }
        //    return TimeZoneInfo;
        //}
    }
}
