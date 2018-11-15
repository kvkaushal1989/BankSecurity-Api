using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Common {
    public class Property {
        public int ID { get; set; }
    }

    /// <summary>
    /// Pagination Model
    /// </summary>
    public class Pagination {
        /// <summary>
        /// PageSize
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// PageNumber
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Filter
        /// </summary>
        public string Filter { get; set; }

        /// <summary>
        /// SearchText
        /// </summary>
        public string SearchText { get; set; }
    }
}
