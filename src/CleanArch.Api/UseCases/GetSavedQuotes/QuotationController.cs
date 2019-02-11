using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CleanArch.Core.Model;
using CleanArch.Core.Interface.Quotation;

namespace CleanArch.Api.UseCases.GetSavedQuotes
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class QuotationController : ControllerBase
    {
        private readonly IQuotationService _QuotationService;
        public QuotationController(IQuotationService quotationService)
        {
            _QuotationService = quotationService;
        }

        [Route("GetSavedQuoteList")]
        [HttpPost]
        public dynamic GetSavedQuoteList([FromBody] QuoteSearch request)
        {
            try
            {

                ListSavedQuotesEntity lstSavedQuotes = new ListSavedQuotesEntity();

                if (string.IsNullOrEmpty(request.List))
                {
                    request.List = "QuoteList";
                }
                if (string.IsNullOrEmpty(request.Status))
                {
                    request.Status = "ACTIVE";
                }

                request.SearchString = string.IsNullOrEmpty(request.SearchString) ? request.SearchString : Convert.ToString(request.SearchString).ToUpper();
                //if (request.PageNo != 0) { pageno = Convert.ToInt32(request.PageNo); }              

                //lstSavedQuotes.QuotesTabCount = _QuotationService.GetQuoteStatusCount(request.Email, Convert.ToString(request.SearchString).ToUpper(), request.Status);
                lstSavedQuotes.QuotesTabCount = _QuotationService.GetQuoteStatusCount(request);

                lstSavedQuotes.SavedQuotes = _QuotationService.GetOnlySavedQuotesOrTemplates(request);

                return Ok(lstSavedQuotes);
            }
            catch (Exception ex)
            {
                return ex;
                //LogError("api/Quotation", "GetSavedQuoteList", ex.Message.ToString(), ex.StackTrace.ToString());
                //return new HttpResponseMessage()
                //{
                //    Content = new JsonContent(new
                //    {
                //        StatusCode = HttpStatusCode.ExpectationFailed,
                //        Message = ex.Message.ToString()
                //    })
                //};
            }
        }
    }
}