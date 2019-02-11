using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Model
{
    public class ListSavedQuotesEntity
    {
        public QuoteStatusCount QuotesTabCount { get; set; }
        public IEnumerable<QuoteJobList> SavedQuotes { get; set; }


    }
}
