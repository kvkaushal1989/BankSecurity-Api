using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wet_Withdraw {
        public long TRANC_ID { get; set; }

        public string TELNO { get; set; }

        public decimal AMOUNT { get; set; }

        public decimal? BONUS { get; set; }

        public DateTime ISSUEDDT { get; set; }

        public string ISSUEDBY { get; set; }

        public string USER_ID { get; set; }

        public string SHIFT { get; set; }

        public DateTime TIME { get; set; }

        public string PRODUCT_ID { get; set; }

        public string TYPE { get; set; }

        public string STATUS { get; set; }

        public decimal? TAXAMT { get; set; }

        public long? COMPANY_BANK { get; set; }

        public string REMARK { get; set; }

        public string BANKCD { get; set; }

        public string BANKACC { get; set; }

        public string CS_APPROVE { get; set; }

        public string BANK_APPROVE { get; set; }

        public string HOLDER_NAME { get; set; }

        public string CASHOUT_STATUS { get; set; }

        public string RCPDATE { get; set; }

        public long? LATE_TRANC_ID { get; set; }

        public string D4 { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
