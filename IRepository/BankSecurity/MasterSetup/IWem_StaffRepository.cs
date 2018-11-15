using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.BankSecurity.MasterSetup {
    public interface IWem_StaffRepository {
        Dictionary<string, object> CreateRow(Wem_Staff bank);

        Dictionary<string, object> UpdateRow(Wem_Staff bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<Wem_Staff> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<Wem_Staff> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
