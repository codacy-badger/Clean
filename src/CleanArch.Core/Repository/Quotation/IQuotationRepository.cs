using CleanArch.Core.Entities;
using CleanArch.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Repository.Quotation
{
   public interface IQuotationRepository
    {
        List<QuoteJobListEntity> GetOnlySavedQuotesOrTemplates(string userId, string status, string list, int PageNo, string SearchString, string Order, int Type);
        QuoteStatusCountEntity GetQuoteStatusCount(string userId, string SearchString, string status);
    }
}
