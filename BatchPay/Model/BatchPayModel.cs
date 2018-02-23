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
        public decimal InvoiceID { get; set; }
        public byte TransactionType { get; set; }
        public decimal GrossAmount { get; set; }
        public string Reference { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public string AuthorizationNumber { get; set; }

        //Individual Card
        public string IndividualCardHolder { get; set; }
        public string IndividualCCMask
        {
            get
            {
                if (IndividualCardNo != null && IndividualCardNo != "")
                {
                    string temp = "";

                    for (int x = 0; x < IndividualCardNo.Length - 4; x++)
                    {
                        temp += "X";
                    }

                    return temp + IndividualCardNo.Substring(IndividualCardNo.Length - 4, 4);
                }
                else
                    return "";
            }
        }

        public DateTime? _IndividualExpirationDate { get; set; }
        public string IndividualExpirationDate
        {
            get
            {
                if (_IndividualExpirationDate != null)
                {
                    DateTime temp = DateTime.Parse(_IndividualExpirationDate.ToString());

                    return temp.Month.ToString() + "/" + temp.Year.ToString();
                }
                else
                    return "";
            }
        }
        public string IndividualCardNo { get; set; }

        //Corporate Card
        public DateTime? _CorpExpirationDate { get; set; }
        public string CorpExpirationDate
        {
            get
            {
                if (_CorpExpirationDate != null)
                {
                    DateTime temp = DateTime.Parse(_CorpExpirationDate.ToString());

                    return temp.Month.ToString() + "/" + temp.Year.ToString();
                }
                else
                    return "";
            }
        }
        public string CorpCardHolder { get; set; }
        public string CorpCCMask
        {
            get
            {
                if (CorpCardNo != null && CorpCardNo != "")
                {
                    string temp = "";

                    for (int x = 0; x < CorpCardNo.Length - 4; x++)
                    {
                        temp += "X";
                    }

                    return temp + CorpCardNo.Substring(CorpCardNo.Length - 4, 4);
                }
                else
                    return "";
            }
        }
        public string CorpCardNo { get; set; }
        
    }
}
