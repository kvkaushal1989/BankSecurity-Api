using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.BankSecurity.MasterSetup {
    public class File_Images {
        public string NAME { get; set; }

        public string SUBJECT { get; set; }

        public int ID { get; set; }

        public byte[] BLOB_CONTENT { get; set; }

        public string MIME_TYPE { get; set; }

        public float? DOC_SIZE { get; set; }

        public string USERINFO { get; set; }

        public DateTime? UPLOAD_DATE { get; set; }

        public string TYPE { get; set; }

        public string LoginID { get; set; }

        public string UTCOffset { get; set; }

    }
}
