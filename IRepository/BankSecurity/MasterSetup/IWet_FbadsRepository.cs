using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System.Collections.Generic;

namespace IRepository.BankSecurity.MasterSetup {
    public interface IWet_FbadsRepository {
        Dictionary<string, object> CreateRow(Wet_Fbads bank);

        Dictionary<string, object> UpdateRow(Wet_Fbads bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<Wet_Fbads> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<Wet_Fbads> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
