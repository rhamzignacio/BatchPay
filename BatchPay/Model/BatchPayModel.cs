using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BatchPay.Model;


namespace BatchPay.Model
{
    public class BatchPayModel
    {
        public byte TransactionType { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal? CCID { get; set; }
        public string Reference { get; set; }
        public string CardHolder { get; set; }
        public string AuthorizationNumber { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string InvoiceNo { get; set; }
        public byte[] MaskCreditCardNo { get; set; }
        public string CCMask { get; set; }
        public string UnMaskCreditCardNo
        {
            get
            {
                if (MaskCreditCardNo != null)
                {
                    using (var db = new TravComEntities())
                    {
                        if (CCID != null)
                        {
                            var credNo = db.CCBatchUploadHash.FirstOrDefault(r => r.CCID == CCID);

                            if (credNo != null)
                                return credNo.CCNum;
                            else
                                return "";
                        }
                        else
                            return "";
                    }
                }
                else
                    return "";
            }
        }

     
    }
}
