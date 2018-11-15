using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wet_Wkk {
        public DateTime TRXDT { get; set; }

        public long PHONE_ID { get; set; }

        public string ISSUEDBY { get; set; }

        public DateTime ISSUEDDT { get; set; }

        public long OPENING { get; set; }

        public long CLOSING { get; set; }

        public string LOCATION { get; set; }

        public long INTEREST { get; set; }

        public string SHIFT { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }
}
