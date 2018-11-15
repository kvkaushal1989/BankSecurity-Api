using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CMC_Bank {

        public long ID { get; set; }

        public string BankCode { get; set; }

        public string BankName { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }
    }
}
