using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CMC_Product {
        public long PRODUCT_ID { get; set; }

        public string FLDESC { get; set; }

        public long? SEQNO { get; set; }

        public string DTDESC { get; set; }

        public string CLOSE { get; set; }

        public string DECIMAL_ALLOW { get; set; }

        public decimal? OPENNING { get; set; }

        public DateTime? OPENNINGDT { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
