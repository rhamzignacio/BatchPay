//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BatchPay.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class CCBatchUpload
    {
        public decimal InvoiceDetailID { get; set; }
        public decimal InvoiceID { get; set; }
        public byte TransactionType { get; set; }
        public decimal GrossAmount { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public Nullable<decimal> CCID { get; set; }
        public string Reference { get; set; }
        public string CardHolder { get; set; }
        public string AuthorizationNumber { get; set; }
        public Nullable<System.DateTime> ExpirationDate { get; set; }
        public string InvoiceNumber { get; set; }
        public byte[] CCNumber { get; set; }
        public string CCMask { get; set; }
    }
}
