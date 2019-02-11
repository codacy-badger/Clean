using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Model
{
    public class QuoteSearch
    {
        public string Email { get; set; }
        public string SearchString { get; set; }
        public string Status { get; set; }
        public int PageNo { get; set; }
        public string List { get; set; }
    }
}
