using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.BankSecurity.MasterSetup {
    public interface ICMC_Pending_TypeRepository {
        Dictionary<string, object> CreateRow(CMC_Pending_Type bank);

        Dictionary<string, object> UpdateRow(CMC_Pending_Type bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<CMC_Pending_Type> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<CMC_Pending_Type> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
