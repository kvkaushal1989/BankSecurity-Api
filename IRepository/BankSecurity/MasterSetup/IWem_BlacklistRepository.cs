using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System.Collections.Generic;

namespace IRepository.BankSecurity.MasterSetup {
    public interface IWem_BlacklistRepository {
        Dictionary<string, object> CreateRow(Wem_Blacklist bank);

        Dictionary<string, object> UpdateRow(Wem_Blacklist bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<Wem_Blacklist> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<Wem_Blacklist> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
