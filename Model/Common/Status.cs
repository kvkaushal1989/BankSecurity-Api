using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common {
    public class Status {
        public int ID { get; set; }
        /// <summary>
        /// Code (For Success starts with 'FCS'+4 digit and for Error 'FCE'+4 digit)
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Success/Error Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Success/Error
        /// </summary>
        public bool? IsSuccess { get; set; }

        /// <summary>
        /// Created On
        /// </summary>
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Modified On
        /// </summary>
        public DateTime? ModifiedOn { get; set; }

        /// <summary>
        /// Success/Failure
        /// </summary>
        public string Type { get; set; }
    }
}
