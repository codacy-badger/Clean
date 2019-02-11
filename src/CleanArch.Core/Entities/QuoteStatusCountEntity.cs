using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities
{
    public class QuoteStatusCountEntity
    {
        public decimal ALLQUOTES { get; set; }
        public decimal ACTIVEQUOTES { get; set; }
        public decimal EXPIREDQUOTES { get; set; }
        public decimal ALLJOBS { get; set; }
        public decimal ACTIVEJOBS { get; set; }
        public decimal EXPIREDJOBS { get; set; }
        public decimal COLLABORATEDJOBS { get; set; }
        public decimal INITIATEDJOBS { get; set; }
        public decimal INPROGRESSJOBS { get; set; }
        public decimal COMPLETEDJOBS { get; set; }
        public decimal NOMINATIONS { get; set; }
        public decimal CANCELLEDJOBS { get; set; }
    }
}
