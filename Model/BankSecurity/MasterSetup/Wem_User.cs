using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wem_User {

        public int ID { get; set; }

        public string SCHEMA { get; set; }

        public string USRCD { get; set; }

        public string FLNAME { get; set; }

        public string EMAIL { get; set; }

        public string ADMINUSR { get; set; }

        public string ROLEID { get; set; }

        public string PASSWORD { get; set; }

        public string CLOSE { get; set; }

        public DateTime? EXPIREDT { get; set; }

        public string STAFFCD { get; set; }

        public short? USRLEVEL { get; set; }

        public int? LAWYER_SNO { get; set; }

        public string SHIFT { get; set; }

        public long DEPTCD { get; set; }

        public string BYPASS_IP { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
