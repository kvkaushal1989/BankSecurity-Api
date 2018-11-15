using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wem_Promotion {
        public long PROMOTIONCD { get; set; }

        public string FLDESC { get; set; }

        public string DISCRT { get; set; }

        public DateTime EFTDTFRM { get; set; }

        public DateTime EFTDTTO { get; set; }

        public string APRODUCT_ID { get; set; }

        public DateTime ISSUEDDT { get; set; }

        public string ISSUEDBY { get; set; }

        public string CLOSE { get; set; }

        public decimal? LIMIT { get; set; }

        public long? DAILY_QTY { get; set; }

        public long? SEQNO { get; set; }

        public long? MONTHLY_QTY { get; set; }

        public decimal? MIN_TOPUP { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
