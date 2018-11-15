using System.Collections.Generic;
using Model.BankSecurity.MasterSetup;
using Model.Common;
using HK.Utility;

namespace IRepository.BankSecurity.MasterSetup {
    public interface ICMC_ComBankRepository {
        Dictionary<string, object> CreateRow(CMC_ComBank data);

        Dictionary<string, object> UpdateRow(CMC_ComBank data);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray); 

        List<CMC_ComBank> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<CMC_ComBank> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
