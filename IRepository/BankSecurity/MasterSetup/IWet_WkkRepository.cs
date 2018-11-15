using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.BankSecurity.MasterSetup {
    public interface IWet_WkkRepository {
        Dictionary<string, object> CreateRow(Wet_Wkk bank);

        Dictionary<string, object> UpdateRow(Wet_Wkk bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<Wet_Wkk> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<Wet_Wkk> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
