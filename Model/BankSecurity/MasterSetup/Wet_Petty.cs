using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wet_Petty {
        public long PETTY_ID { get; set; }

        public DateTime ISSUEDDT { get; set; }

        public string ISSUEDBY { get; set; }

        public string FLDESC { get; set; }

        public decimal? DBAMT { get; set; }

        public decimal? CRAMT { get; set; }

        public string STATUS { get; set; }

        public long? TO_ID { get; set; }

        public long? EXPENSES_PID { get; set; }

        public long? EXPENSES_ID { get; set; }

        public long? INCOME_PID { get; set; }

        public long? INCOME_ID { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
