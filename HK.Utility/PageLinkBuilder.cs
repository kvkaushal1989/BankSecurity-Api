using System;
using System.Web.Http.Routing;

namespace HK.Utility {
    /// <summary>
    /// Generic Class to Build Pagination Link URLs for Data Table Navigation
    /// </summary>
    public class PageLinkBuilder {
        /// <summary>
        /// First Page Link URLs for Data Table Navigation
        /// </summary>
        public Uri FirstPage { get; private set; }
        /// <summary>
        /// Last Page Link URLs for Data Table Navigation
        /// </summary>
        public Uri LastPage { get; private set; }
        /// <summary>
        /// Next Page Link URLs for Data Table Navigation
        /// </summary>
        public Uri NextPage { get; private set; }
        /// <summary>
        /// Previous Page Link URLs for Data Table Navigation
        /// </summary>
        public Uri PreviousPage { get; private set; }
        /// <summary>
        /// Generic Method to Build Pagination Link URLs for Data Table Navigation
        /// </summary>
        public PageLinkBuilder(UrlHelper urlHelper, string routeName, object routeValues, int pageNo, int pageSize, long totalRecordCount, string filterBy, string searchText) {
            // Determine total number of pages
            var pageCount = totalRecordCount > 0
                ? (int) Math.Ceiling(totalRecordCount / (double) pageSize)
                : 0;
            int maxPageSizeLimit = Int32.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaxListPaginationSize"]);
            if (pageSize > maxPageSizeLimit) {
                pageSize = 10;
            }
            // Create them page links
            FirstPage = new Uri(urlHelper.Link(routeName, new { pageNo = 1, pageSize = pageSize, filterBy = filterBy, searchText = searchText }));
            LastPage = new Uri(urlHelper.Link(routeName, new { pageNo = pageCount, pageSize = pageSize, filterBy = filterBy, searchText = searchText }));
            if(pageNo > 1) {
                PreviousPage = new Uri(urlHelper.Link(routeName, new { pageNo = pageNo - 1, pageSize = pageSize, filterBy = filterBy, searchText = searchText } ));
            }
            if(pageNo < pageCount) {
                NextPage = new Uri(urlHelper.Link(routeName, new { pageNo = pageNo + 1, pageSize = pageSize, filterBy = filterBy, searchText = searchText }));
            }
        }
    }
}
