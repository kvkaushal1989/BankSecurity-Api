﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class CMC_Company {
        public long COMPANY_ID { get; set; }

        public string FLDESC { get; set; }

        public string CLOSE { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }
    }
}
