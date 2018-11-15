using Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.AuthorizationProvider {
    public interface IUserAuthorizationProviders {
        bool UserAuthorizationStatus(AuthorizationInput authorizationInput);
        //TimezoneInfoDetails UserAuthorizationStatusWithTimeZone(AuthorizationInput authorizationInput);
    }
}
