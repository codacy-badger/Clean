using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Core.Entities
{
    public class ReceiptHeaderEntity
    {
        public long JOBNUMBER { get; set; }
        public decimal RECEIPTAMOUNT { get; set; }
        public decimal RECEIPTTAX { get; set; }
        public decimal RECEIPTNETAMOUNT { get; set; }
        public decimal DISCOUNTAMOUNT { get; set; }
    }
}
