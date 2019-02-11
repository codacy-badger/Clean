using CleanArch.Core.Model;
using CleanArch.Core.Entities;
using CleanArch.Core.Repository.Quotation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using CleanArch.Infrastructure.Core;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace CleanArch.Infrastructure.DataRepository.Quotation
{
    public class QuotationDataRepository : DBFactoryCore, IQuotationRepository
    {
        IConfiguration configuration;
        public QuotationDataRepository(IConfiguration _configuration)
        {

            configuration = _configuration;
        }

        public List<QuoteJobListEntity> GetOnlySavedQuotesOrTemplates(string userId, string status, string list, int PageNo, string SearchString, string Order, int Type)
        {
            throw new NotImplementedException();
        }

        public QuoteStatusCountEntity GetQuoteStatusCount(string userId, string SearchString, string status)
        {

            string mSearch = string.Empty;
            string mSearchNomination = string.Empty;
            if (SearchString != null && SearchString != "")
            {
                if (SearchString.ToLowerInvariant().Contains("airport"))
                {
                    SearchString = Regex.Replace(SearchString.ToLowerInvariant(), "air", "", System.Text.RegularExpressions.RegexOptions.IgnoreCase); //SearchString.ToUpperInvariant();
                    SearchString = SearchString.ToUpperInvariant();
                    mSearch = " AND (UPPER(ORIGINPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(ORIGINPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(QUOTATIONNUMBER) = :SearchString OR UPPER(PRODUCTNAME) = 'AIR'  AND UPPER(MOVEMENTTYPENAME) = :SearchString )";
                }
                else if (SearchString.ToLowerInvariant().Contains("port"))
                {
                    SearchString = SearchString.ToUpperInvariant();
                    mSearch = " AND (UPPER(ORIGINPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(ORIGINPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(QUOTATIONNUMBER) = :SearchString OR UPPER(PRODUCTNAME) = 'OCEAN'  AND UPPER(MOVEMENTTYPENAME) = :SearchString )";
                }
                else
                {
                    SearchString = SearchString.ToUpperInvariant();
                    mSearch = " AND (UPPER(ORIGINPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(ORIGINPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(QUOTATIONNUMBER) = :SearchString  OR UPPER(PRODUCTNAME) = :SearchString OR UPPER(MOVEMENTTYPENAME) = :SearchString )";
                    mSearchNomination = " AND (UPPER(ORIGINPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(ORIGINPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPLACENAME) LIKE '%' || :SearchString  || '%' OR UPPER(DESTINATIONPORTNAME) LIKE '%' || :SearchString  || '%' OR UPPER(TEMPLATENAME) = :SearchString OR UPPER(PRODUCTNAME) = :SearchString  OR UPPER(MOVEMENTTYPENAME) = :SearchString  )";
                }
            }
            QuoteStatusCountEntity mEntity = new QuoteStatusCountEntity();
            if (SearchString != null && SearchString != "")
            {
                ReadText(GetConnectionString(configuration, DBConnectionMode.SSP), "SELECT (SELECT COUNT(1) FROM QMQUOTATION WHERE (STATEID <> 'QUOTEDELETED' AND STATEID <> 'COPYQUOTE') AND (LOWER(CREATEDBY) = :CREATEDBY OR LOWER(GUESTEMAIL) = :CREATEDBY) " + mSearch + " AND (TRUNC (DATEOFVALIDITY) >= TRUNC(SYSDATE) OR TRUNC(DATEOFVALIDITY) IS NULL)) AS ACTIVEQUOTES, (SELECT COUNT(1) FROM QMQUOTATION WHERE (STATEID <> 'QUOTEDELETED' AND STATEID <> 'COPYQUOTE') AND (LOWER(CREATEDBY) = :CREATEDBY OR LOWER(GUESTEMAIL) = :CREATEDBY) " + mSearch + " AND TRUNC (DATEOFVALIDITY) < TRUNC(SYSDATE)) AS EXPIREDQUOTES, (SELECT COUNT(1) FROM QMQUOTATIONTEMPLATE WHERE LOWER(CREATEDBY) = :CREATEDBY " + mSearch + " AND REQUESTTYPE IS NULL " + mSearchNomination + " ) AS NOMINATIONS FROM DUAL",
                new OracleParameter[] {
                    new OracleParameter { OracleDbType = OracleDbType.Long, ParameterName = "CREATEDBY", Value = userId.ToLowerInvariant() },
                    new OracleParameter { ParameterName = "SearchString", Value = SearchString.ToUpperInvariant() }
                }
                , delegate (IDataReader r)
                {
                    mEntity = DataReaderMapToObject<QuoteStatusCountEntity>(r);
                });
            }
            else
            {
                ReadText(GetConnectionString(configuration, DBConnectionMode.SSP), "SELECT (SELECT COUNT(1) FROM QMQUOTATION WHERE (STATEID <> 'QUOTEDELETED' AND STATEID <> 'COPYQUOTE') AND (LOWER(CREATEDBY) = :CREATEDBY OR LOWER(GUESTEMAIL) = :CREATEDBY) " + mSearch + " AND (TRUNC (DATEOFVALIDITY) >= TRUNC(SYSDATE) OR TRUNC(DATEOFVALIDITY) IS NULL)) AS ACTIVEQUOTES, (SELECT COUNT(1) FROM QMQUOTATION WHERE (STATEID <> 'QUOTEDELETED' AND STATEID <> 'COPYQUOTE') AND (LOWER(CREATEDBY) = :CREATEDBY OR LOWER(GUESTEMAIL) = :CREATEDBY) " + mSearch + " AND TRUNC (DATEOFVALIDITY) < TRUNC(SYSDATE)) AS EXPIREDQUOTES, (SELECT COUNT(1) FROM QMQUOTATIONTEMPLATE WHERE LOWER(CREATEDBY) = :CREATEDBY AND REQUESTTYPE IS NULL " + mSearchNomination + " ) AS NOMINATIONS FROM DUAL",
                    new OracleParameter[] { new OracleParameter { OracleDbType = OracleDbType.Long, ParameterName = "CREATEDBY", Value = userId.ToLowerInvariant() } }
                , delegate (IDataReader r)
                {
                    mEntity = DataReaderMapToObject<QuoteStatusCountEntity>(r);
                });
            }
            return mEntity;

        }
    }
}
