using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatchPay.Model
{
    public class BDOFormatModel
    {
        private char padding = ' ';

        private string _merchantNumber;
        public string MerchantNumber
        {
            get
            {
                return _merchantNumber.PadRight(11, padding);
            }
            set
            {
                _merchantNumber = "12345678910";
            }
        }

        private string _cardNumber;
        public string CardNumber
        {
            get
            {
                return _cardNumber.PadRight(16, padding);
            }
            set
            {
                _cardNumber = value;
            }
        }

        private string _cardHolderName;
        public string CardHolderName
        {
            get
            {
                return _cardHolderName.PadRight(30, padding);
            }
        }

        private string _merchantBillNo;
        public string MerchatBillNo
        {
            get
            {
                return _merchantBillNo.PadRight(7, padding);
            }
            set
            {
                _merchantBillNo = value;
            }
        }

        private string _transactionDate;
        public string TransactionDate
        {
            get
            {
                return _transactionDate.PadRight(8, padding);
            }
            set
            {
                _transactionDate = value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description.PadRight(26, padding);
            }
            set
            {
                _description = value;
            }
        }

        private string _filler1;
        public string Filler1
        {
            get
            {
                return _filler1.PadRight(4, padding);
            }
            set
            {
                _filler1 = value;
            }
        }

        private string _processedDate;
        public string ProcessedDate
        {
            get
            {
                return _processedDate.PadRight(8, padding);
            }
            set
            {
                _processedDate = value;
            }
        }

        private string _processedCode;
        public string ProcessedCode
        {
            get
            {
                return _processedCode.PadRight(1, padding);
            }
            set
            {
                _processedCode = value;
            }
        }

        private string _areaCode;
        public string AreaCode
        {
            get
            {
                return _areaCode.PadRight(2, padding);
            }
            set
            {
                _areaCode = value;
            }
        }

        private string _billCode;
        public string BillCode
        {
            get
            {
                return _billCode.PadRight(1, padding);
            }
            set
            {
                _billCode = value;
            }
        }

        private string _miscCode;
        public string MiscCode
        {
            get
            {
                return _miscCode.PadRight(2, padding);
            }
            set
            {
                _miscCode = value;
            }
        }

        private string _debitCreditIndicator;
        public string DebitCreditIndicator
        {
            get
            {
                return _debitCreditIndicator.PadRight(1, padding);
            }
            set
            {
                _debitCreditIndicator = value;
            }
        }

        private string _RPSType;
        public string RPSType
        {
            get
            {
                return _RPSType.PadRight(2, padding);
            }
            set
            {
                _RPSType = value;
            }
        }

        private string _issuer;
        public string Issuer
        {
            get
            {
                return _issuer.PadRight(1, padding);
            }
            set
            {
                _issuer = value;
            }
        }

        private string _authorizationCode;
        public string AuthorizationCode
        {
            get
            {
                return _authorizationCode.PadRight(6, padding);
            }
            set
            {
                _authorizationCode = value;
            }
        }

        private string _expiryDate;
        public string ExpirayDate
        {
            get
            {
                return _expiryDate.PadRight(6, padding);
            }
            set
            {
                _expiryDate = value;
            }
        }

        private string _sequenceNo;
        public string SequenceNo
        {
            get
            {
                return _sequenceNo.PadRight(3, padding);
            }
            set
            {
                _sequenceNo = value;
            }
        }

        private string _CVC2Value;
        public string CVC2Value
        {
            get
            {
                return _CVC2Value.PadRight(3, padding);
            }
            set
            {
                CVC2Value = value;
            }
        }

        private string _filler2;
        public string Filler2
        {
            get
            {
                return _filler2.PadRight(11, padding);
            }
            set
            {
                _filler2 = value;
            }
        }

        private string _reasonCode;
        public string ReasonCode
        {
            get
            {
                return _reasonCode.PadRight(2, padding);
            }
            set
            {
                _reasonCode = value;
            }
        }
    }
}
