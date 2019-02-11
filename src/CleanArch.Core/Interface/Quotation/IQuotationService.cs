using CleanArch.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Interface.Quotation
{
    public interface IQuotationService
    {
        List<QuoteJobList> GetOnlySavedQuotesOrTemplates(QuoteSearch quoteSearch);
        QuoteStatusCount GetQuoteStatusCount(QuoteSearch quoteSearch);
    }
}
