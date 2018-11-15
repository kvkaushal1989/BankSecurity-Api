using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IRepository.BankSecurity.MasterSetup {
    public interface ICMC_SourcesRepository {
        Dictionary<string, object> CreateRow(CMC_Sources bank);

        Dictionary<string, object> UpdateRow(CMC_Sources bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<CMC_Sources> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<CMC_Sources> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
