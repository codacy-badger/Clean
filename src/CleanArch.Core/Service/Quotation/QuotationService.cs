using CleanArch.Core.Interface.Quotation;
using CleanArch.Core.Model;
using CleanArch.Core.Repository.Quotation;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CleanArch.Core.Entities;

namespace CleanArch.Core.Service.Quotation
{
    public class QuotationService : IQuotationService
    {

        private readonly IMapper _mapper;     

        IQuotationRepository _QuotationRepository;
        public QuotationService(IQuotationRepository quotationRepository , IMapper mapper)
        {
            _QuotationRepository = quotationRepository;
            _mapper = mapper;

        }
        public List<QuoteJobList> GetOnlySavedQuotesOrTemplates(QuoteSearch quoteSearch)
        {
            int type = quoteSearch.Status.ToUpperInvariant() == "NOMINATIONS" ? 1 : 0;
            var model= _QuotationRepository.GetOnlySavedQuotesOrTemplates(quoteSearch.Email, quoteSearch.Status, quoteSearch.List, quoteSearch.PageNo, quoteSearch.SearchString, "DESC", type);
            List<QuoteJobList> obj = Mapper.Map<List<QuoteJobListEntity>, List<QuoteJobList>>(model);
            return obj;
        }

        public QuoteStatusCount GetQuoteStatusCount(QuoteSearch quoteSearch)
        {

            var model= _QuotationRepository.GetQuoteStatusCount(quoteSearch.Email, quoteSearch.SearchString, quoteSearch.Status);
            QuoteStatusCount obj = _mapper.Map<QuoteStatusCount>(model);         
            return obj;
        }
    }
}
