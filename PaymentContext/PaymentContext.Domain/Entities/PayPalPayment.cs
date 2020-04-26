
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment
    {
        public PayPalPayment(string transactionCode, DateTime paidDate, DateTime expiredDate, decimal total, decimal totalPaid, Address address, string payer, Document document, Email email) 
            : base (paidDate, expiredDate, total, totalPaid, address, payer, document,  email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }
    }
}
