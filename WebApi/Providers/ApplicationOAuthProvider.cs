using IRepository.AuthorizationProvider;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApi.Repository;

namespace WebApi.Providers {
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId) {
            if (publicClientId == null) {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }



        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context) {
            IUserAuthorizationProviders userAuthorizationProviders = new UserAuthorizationProviders();
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            AuthorizationInput authorizationInput = new AuthorizationInput { UserName = context.UserName, Password = context.Password };

           var IsAuthenticated = userAuthorizationProviders.UserAuthorizationStatus(authorizationInput);


            if (!IsAuthenticated) {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }


            //string DeviceID = context.Request.ReadFormAsync().Result["DeviceID"] == null ? "" : context.Request.ReadFormAsync().Result["DeviceID"];
            //string AppVersion = context.Request.ReadFormAsync().Result["AppVersion"] == null ? "" : context.Request.ReadFormAsync().Result["AppVersion"];
            //string OSVersion = context.Request.ReadFormAsync().Result["OSVersion"] == null ? "" : context.Request.ReadFormAsync().Result["OSVersion"];
            //string DeviceModel = context.Request.ReadFormAsync().Result["DeviceModel"] == null ? "" : context.Request.ReadFormAsync().Result["DeviceModel"];
            //string DeviceIP = context.Request.ReadFormAsync().Result["DeviceIP"] == null ? "" : context.Request.ReadFormAsync().Result["DeviceIP"];



            ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(context.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            //var existingClaimDeviceIP = oAuthIdentity.FindFirst("DeviceIP");
            //if (existingClaimDeviceIP != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimDeviceIP);
            //}


            //var existingClaimDeviceID = oAuthIdentity.FindFirst("DeviceID");
            //if (existingClaimDeviceID != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimDeviceID);
            //}

            //var existingClaimAppVersion = oAuthIdentity.FindFirst("AppVersion");
            //if (existingClaimAppVersion != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimAppVersion);
            //}

            //var existingClaimOSVersion = oAuthIdentity.FindFirst("OSVersion");
            //if (existingClaimOSVersion != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimOSVersion);
            //}

            //var existingClaimDeviceModel = oAuthIdentity.FindFirst("DeviceModel");
            //if (existingClaimDeviceModel != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimDeviceModel);
            //}

            //var existingClaimPropertyOffSet = oAuthIdentity.FindFirst("PropertyOffSet");
            //if (existingClaimPropertyOffSet != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimPropertyOffSet);
            //}
            //var existingClaimServerOffSet = oAuthIdentity.FindFirst("ServerOffSet");
            //if (existingClaimServerOffSet != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimServerOffSet);
            //}
            //var existingClaimServerTime = oAuthIdentity.FindFirst("ServerTime");
            //if (existingClaimServerTime != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimServerTime);
            //}
            //var existingClaimTimeZoneDiffrenceInMinutes = oAuthIdentity.FindFirst("TimeZoneDiffrenceInMinutes");
            //if (existingClaimTimeZoneDiffrenceInMinutes != null) {
            //    oAuthIdentity.RemoveClaim(existingClaimTimeZoneDiffrenceInMinutes);
            //}
            //oAuthIdentity.AddClaim(new Claim("DeviceIP", DeviceIP));
            //oAuthIdentity.AddClaim(new Claim("DeviceID", DeviceID));
            //oAuthIdentity.AddClaim(new Claim("AppVersion", AppVersion));
            //oAuthIdentity.AddClaim(new Claim("OSVersion", OSVersion));
            //oAuthIdentity.AddClaim(new Claim("DeviceModel", DeviceModel));
            //oAuthIdentity.AddClaim(new Claim("PropertyOffSet", tz.PropertyTimeOffsetInMinutes.ToString()));
            //oAuthIdentity.AddClaim(new Claim("ServerOffSet", tz.ServerTimeOffsetInMinutes.ToString()));
            //oAuthIdentity.AddClaim(new Claim("ServerTime", DateTime.UtcNow.ToString("hh:mm:ss")));
            //oAuthIdentity.AddClaim(new Claim("TimeZoneDiffrenceInMinutes", tz.TimeZoneDiffrenceInMinutes.ToString()));





            context.Validated(ticket);

            //oAuthIdentity.AddClaim(new Claim("sub", context.UserName));
            //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            //context.Validated(oAuthIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context) {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary) {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context) {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null) {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context) {
            if (context.ClientId == _publicClientId) {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri) {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName) {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
                //,
                //{ "role", role },
                //{ "DeviceIP",DeviceIP}
            };
            return new AuthenticationProperties(data);
        }
    }
}