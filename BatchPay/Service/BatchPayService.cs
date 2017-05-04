using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchPay.Model;

namespace BatchPay.Service
{
    public class BatchPayService
    {
        public static List<BatchPayModel> GetBatch(DateTime _startDate, DateTime _endDate, out string message)
        {
            try
            {
                message = "";

                using (var travDB = new TravComEntities())
                {
                    List<BatchPayModel> mergePayments = new List<BatchPayModel>();

                    var payments = from c in travDB.CCBatchUpload                         
                                   where c.InvoiceDate >= _startDate && c.InvoiceDate <= _endDate && c.CCID != 0
                                   select new BatchPayModel
                                   {
                                       AuthorizationNumber = c.AuthorizationNumber,
                                       CardHolder = c.CardHolder,
                                       CCID = c.CCID,
                                       ExpirationDate = c.ExpirationDate,
                                       GrossAmount = c.GrossAmount,
                                       InvoiceNo = c.InvoiceNumber,
                                       MaskCreditCardNo = c.CCNumber,
                                       Reference = c.Reference,
                                       TransactionType = c.TransactionType,
                                       CCMask = c.CCMask
                                   };

                    string tempInvoiceNo = "";
                    BatchPayModel tempPayment = new BatchPayModel();

                    payments.ToList().ForEach(item =>
                    {
                        if (tempInvoiceNo == "" || tempInvoiceNo != item.InvoiceNo)
                        {
                            if(tempInvoiceNo != item.InvoiceNo && tempInvoiceNo != "")
                            {
                                mergePayments.Add(tempPayment);

                                tempPayment = new BatchPayModel();
                            }

                            tempInvoiceNo = item.InvoiceNo;

                            tempPayment.AuthorizationNumber = item.AuthorizationNumber;
                            tempPayment.CardHolder = item.CardHolder;
                            tempPayment.CCID = item.CCID;
                            tempPayment.ExpirationDate = item.ExpirationDate;
                            tempPayment.GrossAmount = item.GrossAmount;
                            tempPayment.InvoiceNo = item.InvoiceNo;
                            tempPayment.Reference = item.Reference;
                            tempPayment.TransactionType = item.TransactionType;
                            tempPayment.MaskCreditCardNo = item.MaskCreditCardNo;
                            tempPayment.CCMask = item.CCMask;
                        }
                        else if(tempInvoiceNo == item.InvoiceNo)
                        {
                            tempPayment.GrossAmount += item.GrossAmount;
                        }
                    });

                    return mergePayments.ToList();
                }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }
    }
}
