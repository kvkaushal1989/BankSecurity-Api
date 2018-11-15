using System.Collections.Generic;
using Model.BankSecurity.MasterSetup;
using Model.Common;
using HK.Utility;

namespace IRepository.BankSecurity.MasterSetup {
    public interface ICMC_BankRepository {
        Dictionary<string, object> CreateRow(CMC_Bank bank);

        Dictionary<string, object> UpdateRow(CMC_Bank bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray); 

        List<CMC_Bank> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<CMC_Bank> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
