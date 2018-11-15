using System.Collections.Generic;
using Model.BankSecurity.MasterSetup;
using Model.Common;
using HK.Utility;

namespace IRepository.BankSecurity.MasterSetup {
    public interface ICMC_ExpensesRepository {
        Dictionary<string, object> CreateRow(CMC_Expenses bank);

        Dictionary<string, object> UpdateRow(CMC_Expenses bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray); 

        List<CMC_Expenses> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<CMC_Expenses> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
