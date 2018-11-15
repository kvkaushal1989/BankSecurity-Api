using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.Common {
    public class Enumerations {
        /// <summary>
        /// Gender
        /// </summary>
        public enum Gender {
            /// <summary>
            /// Male
            /// </summary>
            Male,
            /// <summary>
            /// Female
            /// </summary>
            Female,
            /// <summary>
            /// Others
            /// </summary>
            Other
        };

        /// <summary>
        /// Record Type
        /// </summary>
        public enum RecordType {
            /// <summary>
            /// Userdefined Record
            /// </summary>
            UserDefined,
            /// <summary>
            /// Predefined Record
            /// </summary>
            Predefined
        };

          
        /// <summary>
        /// Title
        /// </summary>
        public enum Title {
            /// <summary>
            /// Mr
            /// </summary>
            Mr,
            /// <summary>
            /// Mrs
            /// </summary>
            Mrs,
            /// <summary>
            /// Ms
            /// </summary>
            Ms,
            /// <summary>
            /// Other
            /// </summary>
            Other
        };

        /// <summary>
        /// Contact Type
        /// </summary>
        public enum ContactType {
            /// <summary>
            /// Present
            /// </summary>
            Present,
            /// <summary>
            /// Permanent
            /// </summary>
            Permanent,
            /// <summary>
            /// Office
            /// </summary>
            Office,
            /// <summary>
            /// Communication
            /// </summary>
            Communication,
            /// <summary>
            /// Billing
            /// </summary>
            Billing
        };

        /// <summary>
        /// Service Error Messages
        /// </summary>
        public enum ErrorCodes {
            /// <summary>
            /// Error Code For General API Testing Through REST Client
            /// </summary>
            BCEAPIT,

            /// <summary>
            /// Internal Server Error 
            /// </summary>
            BCE00000,
            /// <summary>
            /// Duplicate Code
            /// </summary>
            BCE00001,
            /// <summary>
            /// Duplicate Name
            /// </summary>
            BCE00002,
            /// <summary>
            /// Tagged As Default, Cannot be made Inactive
            /// </summary>
            BCE00003,
            /// <summary>
            /// Inactive At Group Level
            /// </summary>
            BCE00004,
            /// <summary>
            /// Card Cannot Be Saved, Try Again Later
            /// </summary>
            BCE00005,
            /// <summary>
            /// Card Cannot Be Updated, Try Again Later
            /// </summary>
            BCE00006,
            /// <summary>
            /// Incorrect Password
            /// </summary>
            BCE00007,
            /// <summary>
            /// Incorrect Username
            /// </summary>
            BCE00008            
        }
    }
}