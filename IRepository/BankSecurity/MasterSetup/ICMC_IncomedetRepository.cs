﻿using HK.Utility;
using Model.BankSecurity.MasterSetup;
using System.Collections.Generic;

namespace IRepository.BankSecurity.MasterSetup {
    public interface ICMC_IncomedetRepository {
        Dictionary<string, object> CreateRow(CMC_Incomedet bank);

        Dictionary<string, object> UpdateRow(CMC_Incomedet bank);

        Dictionary<string, object> ViewRow(long id);

        Dictionary<string, object> DeleteRow(List<int> idArray);

        List<CMC_Incomedet> PaginationSettings(string filterBy, string searchText, int pageSize, int pageNo, ref int totalRecordCount, ref int totalPages, ref int skipRecords);
        Dictionary<string, object> PaginationMessage(List<CMC_Incomedet> paginatedRowArray, PageLinkBuilder linkBuilder, int totalRecordCount);

        Dictionary<string, object> GetDropDownList();
    }
}
