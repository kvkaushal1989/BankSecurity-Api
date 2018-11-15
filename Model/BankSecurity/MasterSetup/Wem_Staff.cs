using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class Wem_Staff {
        public long STAFFCD { get; set; }

        public string FLNAME { get; set; }

        public string SEX { get; set; }

        public DateTime? JOINDT { get; set; }

        public string RACE { get; set; }

        public DateTime? DOB { get; set; }

        public DateTime? RESIGNDT { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }

}
