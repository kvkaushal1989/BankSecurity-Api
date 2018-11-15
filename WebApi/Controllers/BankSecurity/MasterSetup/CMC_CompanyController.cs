using HK.Utility;
using IRepository.BankSecurity.MasterSetup;
using Model.BankSecurity.MasterSetup;
using Model.Common;
using Repository.BankSecurity.MasterSetup;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers.BankSecurity.MasterSetup {
    [RoutePrefix("v1/bank-security/CompanySetup")]
    public class CompanyController : ApiController {
        ICMC_CompanyRepository repository = new CMC_CompanyRepository();

        #region Create Row
        /// <summary>
        /// Create Row
        /// </summary>
        /// <remarks>
        /// {
        ///     "COMPANY_ID":0,
        ///     "FLDESC":"BCA",
        ///     "CLOSE":"Y",
        ///     "UTCOffset":"+05:30"
        /// }
        /// </remarks>
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateRow(CMC_Company postData) {
            return Ok(repository.CreateRow(postData));
        }
        #endregion

        #region Update Row
        /// <summary>
        /// Update Row
        /// </summary>
        /// <remarks>
        /// {
        ///     "COMPANY_ID":0,
        ///     "FLDESC":"BCA",
        ///     "CLOSE":"Y",
        ///     "UTCOffset":"+05:30"
        /// }
        /// </remarks>
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("")]
        [HttpPut]
        public IHttpActionResult UpdateRow(CMC_Company postData) {
            return Ok(repository.UpdateRow(postData));
        }
        #endregion

        #region View Row
        /// <summary>
        /// View Row By ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult ViewRow(long id) {
            return Ok(repository.ViewRow(id));
        }
        #endregion

        #region Delete Row
        /// <summary>
        /// Delete Row
        /// </summary>
        /// <remarks>
        /// [1]
        /// </remarks>
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("")]
        [HttpDelete]
        public IHttpActionResult DeleteRow(List<int> idArray) {
            return Ok(repository.DeleteRow(idArray));
        }
        #endregion

        #region Pagination
        /// <summary>
        /// Update Row
        /// </summary>
        /// <remarks>
        /// {
        ///     "PageSize":10,
        ///     "PageNumber":1,
        ///     "Filter":"Active",
        ///     "SearchText":""
        /// }
        /// </remarks>
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("pagination", Name = "CompanySetupPaginationList")]
        [HttpPost]
        public IHttpActionResult PaginationList(Pagination pagination) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            try {
                if (!ModelState.IsValid) {
                    return Ok(Response.ErrorResponse("Error", "FCE00006", "Invalid JSON"));
                }
                else {
                    PageLinkBuilder linkBuilder = null;
                    Int32 totalRecordCount = 0, totalPages = 0, skipRecords = 0;
                    List<CMC_Company> paginatedRowArray = null;
                    try {
                        paginatedRowArray = repository.PaginationSettings(pagination.Filter, pagination.SearchText, pagination.PageSize, pagination.PageNumber,
                            ref totalRecordCount, ref totalPages, ref skipRecords);

                        linkBuilder = new PageLinkBuilder(Url, "CompanySetupPaginationList", null, pagination.PageNumber, pagination.PageSize, totalRecordCount, pagination.Filter, pagination.SearchText);

                        return Ok(repository.PaginationMessage(paginatedRowArray, linkBuilder, totalRecordCount));
                    }
                    catch (Exception ex) {
                        return InternalServerError();
                    }
                }
            }
            catch (Exception ex) {
                return InternalServerError();
            }
        }
        #endregion

        #region Get DropDown List
        /// <summary>
        ///  GetDropDownList
        /// </summary> 
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("getDropDownList")]
        [HttpGet]
        public IHttpActionResult GetDropDownList() {
            return Ok(repository.GetDropDownList());
        }
        #endregion
    }
}
