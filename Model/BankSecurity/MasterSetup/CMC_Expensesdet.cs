using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CMC_Expensesdet {
        public long TRANC_ID { get; set; }

        public long PID { get; set; }

        public string FLDESC { get; set; }

        public long? SEQNO { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }
    }
}
