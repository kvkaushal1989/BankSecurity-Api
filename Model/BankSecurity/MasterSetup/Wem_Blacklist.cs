using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wem_Blacklist {
        public long ID { get; set; }

        public string USER_ID { get; set; }

        public string REMARK { get; set; }

        public string ISSUEDBY { get; set; }

        public DateTime ISSUEDDT { get; set; }

        public string PRODUCT_ID { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
