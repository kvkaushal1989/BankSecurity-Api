using HK.Utility;
using Model.BankSecurity.MasterSetup;
using IRepository.BankSecurity.MasterSetup;
using Repository.BankSecurity.MasterSetup;
using Model.Common;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers {
    [RoutePrefix("v1/bank-security/Bank")]
    public class CMC_BankController : ApiController
    {
        ICMC_BankRepository bankRepository = new CMC_BankRepository();

        #region Create Row
        /// <summary>
        /// Create Row
        /// </summary>
        /// <remarks>
        /// {
        ///     "ID":0,
        ///     "BankCode":"PUN",
        ///     "BankName":"Punjab National Bank",
        ///     "UTCOffset":"+05:30"
        /// }
        /// </remarks>
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("")]
        [HttpPost]
        public IHttpActionResult CreateRow(CMC_Bank postData) {
            return Ok(bankRepository.CreateRow(postData));
        }
        #endregion

        #region Update Row
        /// <summary>
        /// Update Row
        /// </summary>
        /// <remarks>
        /// {
        ///     "ID":1,
        ///     "BankCode":"PUN",
        ///     "BankName":"Punjab National Bank",
        ///     "UTCOffset":"+05:30"
        /// }
        /// </remarks>
        /// <param name="postData"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("")]
        [HttpPut]
        public IHttpActionResult UpdateRow(CMC_Bank postData) {
            return Ok(bankRepository.UpdateRow(postData));
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
            return Ok(bankRepository.ViewRow(id));
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
            return Ok(bankRepository.DeleteRow(idArray));
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
        [Route("pagination", Name= "PaginationList")]
        [HttpPost]
        public IHttpActionResult PaginationList(Pagination pagination) {
            Dictionary<string, object> ResponseData = new Dictionary<string, object>();
            try {
                if (!ModelState.IsValid) {
                    return  Ok(Response.ErrorResponse("Error", "FCE00006", "Invalid JSON"));
                }
                else {
                    PageLinkBuilder linkBuilder = null;
                    Int32 totalRecordCount = 0, totalPages = 0, skipRecords = 0;
                    List<CMC_Bank> paginatedRowArray = null;
                    try {
                        paginatedRowArray = bankRepository.PaginationSettings(pagination.Filter, pagination.SearchText, pagination.PageSize, pagination.PageNumber,
                            ref totalRecordCount, ref totalPages, ref skipRecords);

                        linkBuilder = new PageLinkBuilder(Url, "PaginationList", null, pagination.PageNumber, pagination.PageSize, totalRecordCount, pagination.Filter, pagination.SearchText);

                        return Ok(bankRepository.PaginationMessage(paginatedRowArray, linkBuilder, totalRecordCount));
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
        /// 
        /// </summary>  
        /// <returns></returns>
        [AllowAnonymous]
        [Route("getDropDownList")]
        [HttpGet]
        public IHttpActionResult GetBankList() {
            return Ok(bankRepository.GetDropDownList());
        }
        #endregion
    }
}
