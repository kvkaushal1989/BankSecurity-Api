using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CMC_Sources {
        public int SOURCE_ID { get; set; }

        public string FLDESC { get; set; }

        public long? SEQNO { get; set; }

        public string CLOSE_ { get; set; }


        public string LoginID { get; set; }

        public string UTCOffset { get; set; }
    }
}
