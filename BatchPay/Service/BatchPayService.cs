using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BatchPay.Model;
using System.IO;
using System.Data.Entity;

namespace BatchPay.Service
{
    public class BatchPayService
    {
        private static bool CheckIfVisa(string _cardNo)
        {
            if (_cardNo.Substring(0, 1) == "4")
                return true;
            else
                return false;
        }

        private static bool CheckIfMasterCard(string _cardNo)
        {
            switch (_cardNo.Substring(0, 2))
            {
                case "50": return true;
                case "51": return true;
                case "52": return true;
                case "53": return true;
                case "54": return true;
                case "55": return true;
                default: return false;
            }
        }

        private static bool CheckIfAMEX(string _cardNo)
        {
            switch (_cardNo.Substring(0, 2))
            {
                case "34": return true;
                case "37": return true;
                default: return false;
            }
        }

        private static bool CheckIfJCB(string _cardNo)
        {
            switch (_cardNo.Substring(0, 4))
            {
                case "3528": return true;
                case "3529": return true;
                case "3530": return true;
                case "3531": return true;
                case "3532": return true;
                case "3533": return true;
                case "3534": return true;
                case "3535": return true;
                case "3536": return true;
                case "3537": return true;
                case "3538": return true;
                case "3539": return true;
                case "3540": return true;
                case "3541": return true;
                case "3542": return true;
                case "3543": return true;
                case "3544": return true;
                case "3545": return true;
                case "3546": return true;
                case "3547": return true;
                case "3548": return true;
                case "3549": return true;
                case "3550": return true;
                case "3551": return true;
                case "3552": return true;
                case "3553": return true;
                case "3554": return true;
                case "3555": return true;
                case "3556": return true;
                case "3557": return true;
                case "3558": return true;
                case "3559": return true;
                case "3560": return true;
                case "3561": return true;
                case "3562": return true;
                case "3563": return true;
                case "3564": return true;
                case "3565": return true;
                case "3566": return true;
                case "3567": return true;
                case "3568": return true;
                case "3569": return true;
                case "3570": return true;
                case "3571": return true;
                case "3572": return true;
                case "3573": return true;
                case "3574": return true;
                case "3575": return true;
                case "3576": return true;
                case "3577": return true;
                case "3578": return true;
                case "3579": return true;
                case "3580": return true;
                case "3581": return true;
                case "3582": return true;
                case "3583": return true;
                case "3584": return true;
                case "3585": return true;
                case "3586": return true;
                case "3587": return true;
                case "3588": return true;
                case "3589": return true;
                default: return false;
            }
        }
        private static string ConvertPaxName(string _input)
        {
            try
            {
                string returnPaxName = "";

                string[] paxName = _input.Split('/');

                returnPaxName = paxName[1].Substring(0, paxName[1].Length - 2) + " " + paxName[0];

                return returnPaxName;
            }
            catch
            {
                return "";
            }
        }

        public static void Save(string _path, string MID, string rps, List<BatchPayModel> data, out string message)
        {
            try
            {
                message = "";

                using (var dbBatch = new BatchPayEntities1())
                {
                    var tempSSS = dbBatch.ConfigCount.FirstOrDefault(r => r.Description == "SSS");
                    var tempYYY = dbBatch.ConfigCount.FirstOrDefault(r => r.Description == "YYY");

                    string yyy = tempYYY.Settings;
                    int? sss;
                    char padding = ' ';
                    string tempCardno = "";
                    string tempExpDate = "";


                    sss = tempSSS.Count;


                    StreamWriter visaMasterJCD = new StreamWriter(_path + "\\" + "RPV" + rps + sss.ToString().PadLeft(3, '0') + "." + yyy);

                    StreamWriter amex = new StreamWriter(_path + "\\" + "RPA" + rps + sss.ToString().PadLeft(3, '0') + "." + yyy);

                    data.ForEach(item =>
                    {
                        string temp = MID.PadRight(11, padding);

                        if (item.IndividualCardNo != "" || item.IndividualCardNo != null)
                        {
                            temp += item.IndividualCardNo.Replace(" ", "").PadLeft(16, '0');
                            tempCardno = item.IndividualCardNo;
                            tempExpDate = item.IndividualExpirationDate;
                            temp += item.IndividualCardHolder.PadRight(30, padding);
                        }
                        else
                        {
                            temp += item.CorpCardNo.Replace(" ", "").PadLeft(16, '0');
                            temp += item.CorpCardNo.PadRight(30, padding);
                            tempCardno = item.CorpCardNo;
                            tempExpDate = item.CorpExpirationDate;
                        }

                        temp += "".PadLeft(7, '0') + //Merchant Bill No
                     string.Format("{0:yyyyMMdd}", DateTime.Now) + //Transaction Date
                     String.Format("{0:0.00}", item.GrossAmount).Replace(".", "").PadLeft(9, '0') + //Transaction Amount
                     ("Invoice No: " + item.InvoiceNo).PadRight(26, padding) + // Description
                     ("").PadRight(4, padding) + //Filler
                     ("").PadRight(8, '0') + //Processed Date
                     ("").PadRight(1, padding) + //Processed Code
                     ("").PadRight(2, '0') + //Area Code
                     ("").PadRight(1, '0') + //Bill Code
                     ("").PadRight(2, '0') + //Misc Code
                     ("D").PadRight(1, padding) + //D = Debit/Charge C = Credit/Reversal
                     rps.PadRight(2, padding) +  // RPS Type
                     "I" + //I = BDO & = Non-BDO 
                     ("").PadLeft(5, padding) + //Authorization Code
                     string.Format("{0: yyyyMM}", DateTime.Parse(tempExpDate)) +
                     yyy.PadLeft(3, '0') +
                     ("").PadLeft(3, padding) + //Card Verification Code
                     ("").PadLeft(11, padding) + //Filler
                     ("").PadLeft(2, padding); //Reason Code

                    if (CheckIfAMEX(tempCardno))
                            amex.WriteLine(temp);
                        else if (CheckIfJCB(tempCardno) || CheckIfMasterCard(tempCardno) || CheckIfVisa(tempCardno))
                            visaMasterJCD.WriteLine(temp);
                    });

                    visaMasterJCD.Close();

                    amex.Close();

                    tempSSS.Count = tempSSS.Count + 1;

                    dbBatch.Entry(tempSSS).State = EntityState.Modified;

                    dbBatch.SaveChanges();
                }
            }
            catch(Exception error)
            {
                message = error.Message;
            }
        }

        public static List<BatchPayModel> GetBatch(DateTime _startDate, string _currency, out string message)
        {
            try
            {
                message = "";

                using (var travDB = new TravComEntities())
                {
                        List<BatchPayModel> merged = new List<BatchPayModel>();

                        var query = (from batch in travDB.CCBatchUpload
                                    join client in travDB.Profiles on batch.ProfileNumber equals client.ProfileNumber                          
                                    where batch.InvoiceDate == _startDate &&
                                    batch.CurrencyCode == _currency &&
                                    batch.TransactionType == 1 &&  
                                    client.PhoneNumber5 == "Y" &&
                                    client.PhoneType5 == "EMAIL"
                                    orderby batch.InvoiceNumber
                                    select new BatchPayModel
                                    {
                                        TransactionType = batch.TransactionType,
                                        GrossAmount = batch.GrossAmount,
                                        Reference = batch.Reference,
                                        IndividualCardHolder = batch.PassengerName,
                                        AuthorizationNumber = batch.AuthorizationNumber,
                                        _IndividualExpirationDate = batch.ExpirationDate,
                                        IndividualCardNo = batch.IndiCardNo,
                                        CorpCardHolder = batch.CorpCardHolder,
                                        _CorpExpirationDate = batch.CorpCardExp,
                                        CorpCardNo = batch.CorpCardNo,
                                        InvoiceDate = batch.InvoiceDate,
                                        InvoiceNo = batch.InvoiceNumber,
                                        InvoiceID = batch.InvoiceID,
                                    }).ToList();
                                                                                                                                                                                                                                                                                                                                                                                                                          
                        string tempInvoiceNo = "";

                        BatchPayModel tempBatch = new BatchPayModel();

                        int count = 0;

                        var tempList = query;

                        tempList.ForEach(item =>
                        {
                            count++;

                            if (tempInvoiceNo == "" || tempInvoiceNo != item.InvoiceNo)
                            {
                                if (tempInvoiceNo != item.InvoiceNo && tempInvoiceNo != "")
                                {
                                    merged.Add(tempBatch);

                                    tempBatch = new BatchPayModel();
                                }

                                tempInvoiceNo = item.InvoiceNo;

                                tempBatch.AuthorizationNumber = item.AuthorizationNumber;
                                tempBatch.InvoiceNo = item.InvoiceNo;
                                tempBatch.TransactionType = item.TransactionType;
                                tempBatch.GrossAmount = item.GrossAmount;
                                tempBatch.InvoiceDate = item.InvoiceDate;
                                tempBatch.AuthorizationNumber = item.AuthorizationNumber;

                                //Individual Cards
                                tempBatch.IndividualCardHolder = ConvertPaxName(item.IndividualCardHolder);
                                tempBatch.IndividualCardNo = item.IndividualCardNo;
                                tempBatch._IndividualExpirationDate = item._IndividualExpirationDate;

                                //Corporate Cards
                                tempBatch._CorpExpirationDate = item._CorpExpirationDate;
                                tempBatch.CorpCardHolder = item.CorpCardHolder;
                                tempBatch.CorpCardNo = item.CorpCardNo;

                            }
                            else if (tempInvoiceNo == item.InvoiceNo)
                            {
                                tempBatch.GrossAmount = tempBatch.GrossAmount + item.GrossAmount;
                            }

                            if (count >= query.ToList().Count())
                            {
                                merged.Add(tempBatch);
                            }
                        });

                        return merged.OrderBy(r => r.InvoiceDate).ToList();
                    }
            }
            catch(Exception error)
            {
                message = error.Message;

                return null;
            }
        }

        //public static List<BatchPayModel> GetBatchViaSQL(DateTime _startDate, DateTime _endDate, out string message)
        //{
        //    try
        //    {
        //        message = "";

        //        using (var travDB = new TravComEntities())
        //        {
        //            List<BatchPayModel> mergePayments = new List<BatchPayModel>();

        //            var payments = from c in travDB.CCBatchUpload                         
        //                           where (c.InvoiceDate >= _startDate || c.InvoiceDate <= _endDate) && c.CCID != 0
        //                           && c.GrossAmount > 0
        //                           orderby c.InvoiceNumber
        //                           select new BatchPayModel
        //                           {
        //                               //AuthorizationNumber = c.AuthorizationNumber,
        //                               //CardHolder = c.CardHolder,
        //                               //CCID = c.CCID,
        //                               //ExpirationDate = c.ExpirationDate,
        //                               //GrossAmount = c.GrossAmount,
        //                               //InvoiceNo = c.InvoiceNumber,
        //                               //MaskCreditCardNo = c.CCNumber,
        //                               //Reference = c.Reference,
        //                               //TransactionType = c.TransactionType,
        //                               //CCMask = c.CCMask,
        //                               //InvoiceDate = c.InvoiceDate
        //                           };

        //            string tempInvoiceNo = "";
        //            BatchPayModel tempPayment = new BatchPayModel();

        //            var paymentList = payments.ToList();

        //            int count = 0;

        //            payments.ToList().ForEach(item =>
        //            {
        //                count++;

        //                if (tempInvoiceNo == "" || tempInvoiceNo != item.InvoiceNo)
        //                {
        //                    if(tempInvoiceNo != item.InvoiceNo && tempInvoiceNo != "")
        //                    {
        //                        mergePayments.Add(tempPayment);

        //                        tempPayment = new BatchPayModel();
        //                    }

        //                    tempInvoiceNo = item.InvoiceNo;

        //                    tempPayment.AuthorizationNumber = item.AuthorizationNumber;
        //                    tempPayment.CardHolder = item.CardHolder;
        //                    tempPayment.CCID = item.CCID;
        //                    tempPayment.ExpirationDate = item.ExpirationDate;
        //                    tempPayment.GrossAmount = item.GrossAmount;
        //                    tempPayment.InvoiceNo = item.InvoiceNo;
        //                    tempPayment.Reference = item.Reference;
        //                    tempPayment.TransactionType = item.TransactionType;
        //                    tempPayment.MaskCreditCardNo = item.MaskCreditCardNo;
        //                    tempPayment.CCMask = item.CCMask;
        //                }
        //                else if(tempInvoiceNo == item.InvoiceNo)
        //                {
        //                    tempPayment.GrossAmount += item.GrossAmount;
        //                }

        //                if(count + 1 == payments.Count())
        //                {
        //                    mergePayments.Add(tempPayment);
        //                }
        //            });

        //            return mergePayments.ToList();
        //        }
        //    }
        //    catch(Exception error)
        //    {
        //        message = error.Message;

        //        return null;
        //    }
        //}
    }
}
