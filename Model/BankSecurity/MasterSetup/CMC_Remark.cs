using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CMC_Remark {
        public long REMARK_ID { get; set; }

        public string TYPE { get; set; }

        public string REMARK_DESC { get; set; }

        public string CLOSE { get; set; }

        public long? SEQNO { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }
}
