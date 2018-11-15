using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CS_Pending {
        public long PENDING_ID { get; set; }

        public string ISSUEDBY { get; set; }

        public DateTime ISSUEDDT { get; set; }

        public string USER_ID { get; set; }

        public string WCWS { get; set; }

        public string STATUS { get; set; }

        public string NICKNAME { get; set; }

        public int SOURCE_ID { get; set; }

        public string REMARK1 { get; set; }

        public string REMARK1_DT { get; set; }

        public string REMARK2 { get; set; }

        public string REMARK2_DT { get; set; }

        public string REMARK3 { get; set; }

        public string REMARK3_DT { get; set; }

        public DateTime? REMINDDT { get; set; }

        public string WECHAT { get; set; }

        public string WHATAPPS { get; set; }

        public string TELNO { get; set; }

        public string APPROVEBY { get; set; }

        public string REMARK4 { get; set; }

        public string REMARK4_DT { get; set; }

        public string REMARK1_DESC { get; set; }

        public string REMARK2_DESC { get; set; }

        public string REMARK3_DESC { get; set; }

        public string REMARK4_DESC { get; set; }

        public string GEN_FROM { get; set; }

        public string ASSIGN_TO { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }
}
