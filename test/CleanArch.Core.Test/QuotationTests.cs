using AutoMapper;
using CleanArch.Core.Repository.Quotation;
using CleanArch.Core.Service.Quotation;
using System;
using Xunit;
using Moq;
using CleanArch.Core.Interface.Quotation;
using CleanArch.Core.Model;

namespace CleanArch.Core.Test
{
    public class QuotationTests
    {
        private readonly Mock<IQuotationRepository> _quotationRepository;
        private readonly Mock<IMapper> _mapper;
        private readonly IQuotationService _quotationService;
        public QuotationTests()
        {
            _quotationRepository = new Mock<IQuotationRepository>(MockBehavior.Loose);
            _mapper = new Mock<IMapper>(MockBehavior.Loose);
            _quotationService =new QuotationService(_quotationRepository.Object,_mapper.Object);       

        }
        
        [Fact]
        public void Get_Saved_Quotation_With_Status_Nominations()
        {
            //
            // Arrange
            QuoteSearch request = new QuoteSearch()
            {
                Email = "pannepu@agility.com",
                SearchString = string.Empty,
                Status = "NOMINATIONS",
                PageNo = 1,
                List=string.Empty
            };

            //
            // Act
            var response= _quotationService.GetOnlySavedQuotesOrTemplates(request);

            //
            // Assert            
            Assert.NotNull(response);
        }

        [Fact]
        public void Get_Saved_Quotation_With_Status_Other_Than_Nominations()
        {
            //
            // Arrange
            QuoteSearch request = new QuoteSearch()
            {
                Email = "pannepu@agility.com",
                SearchString = string.Empty,
                Status = "NOMINATIONS",
                PageNo = 1,
                List = string.Empty
            };

            //
            // Act
            var response = _quotationService.GetOnlySavedQuotesOrTemplates(request);

            //
            // Assert            
            Assert.NotNull(response);
        }
    }
}
