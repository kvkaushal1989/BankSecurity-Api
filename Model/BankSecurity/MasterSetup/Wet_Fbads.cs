using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wet_Fbads {
        public long FBADS_ID { get; set; }

        public long FBID { get; set; }

        public decimal? SPENT { get; set; }

        public long? REACH { get; set; }

        public long? RESULT { get; set; }

        public string INSINFO { get; set; }

        public DateTime? ISSUEDDT { get; set; }

        public long? POST_SHARE { get; set; }

        public long? POST_COMMENT { get; set; }

        public long? FOLLOWED { get; set; }

        public long CAMPAIGN_ID { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
