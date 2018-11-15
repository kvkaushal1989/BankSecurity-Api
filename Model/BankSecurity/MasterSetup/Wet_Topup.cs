using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wet_Topup {
        public long TRANC_ID { get; set; }

        public string TELNO { get; set; }

        public decimal AMOUNT { get; set; }

        public decimal? BONUS { get; set; }

        public DateTime? ISSUEDDT { get; set; }

        public string ISSUEDBY { get; set; }

        public string USER_ID { get; set; }

        public string CLAIM { get; set; }

        public string SHIFT { get; set; }

        public DateTime TIME { get; set; }

        public string PRODUCT_ID { get; set; }

        public string TYPE { get; set; }

        public long? COMPANY_BANK { get; set; }

        public string CLAIM_BY { get; set; }

        public DateTime? CLAIMDT { get; set; }

        public string ADJUSTMENT { get; set; }

        public string STATUS { get; set; }

        public string ADJUSTMENT_REMARK { get; set; }

        public long? PROMOTIONCD { get; set; }

        public string REMARK { get; set; }

        public string ADJUSTMENT_TYPE { get; set; }

        public string KIOSK_STATUS { get; set; }

        public string CS_APPROVEBY { get; set; }

        public string BANK_APPROVEBY { get; set; }

        public string RCPDATE { get; set; }

        public long? LATE_TRANC_ID { get; set; }

        public string ADJUSTMENT_CATEGORY { get; set; }

        public string SPECIAL { get; set; }

        public decimal? SPECIAL_AMT1 { get; set; }

        public decimal? SPECIAL_AMT2 { get; set; }

        public string ANGPAU_NAME { get; set; }

        public string DEPOSIT_VIA { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
