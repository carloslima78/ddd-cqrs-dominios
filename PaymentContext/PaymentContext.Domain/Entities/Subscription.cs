using PaymentContext.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    public class Subscription : Entity
    {
        private IList<Payment> _payments;
        public Subscription(DateTime? expireDate)
        {
            CreateDate = DateTime.Now;
            LastUpdate = DateTime.Now;
            ExpireDate = expireDate;
            Active = true;
            
            _payments = new List<Payment>();
        }

        public DateTime CreateDate {get; private set;}
        public DateTime LastUpdate { get; private set; }
        public DateTime? ExpireDate { get; private set; }
        public bool Active {get; private set; }
        public IReadOnlyCollection<Payment> Payments { get { return _payments.ToArray(); } }

        public void AddPayment(Payment payment)
        {
            // Biblioteca Flunt
            AddNotifications(new Flunt.Validations.Contract()
                .Requires()
                .IsGreaterThan(DateTime.Now, payment.PaidDate, "Subscription.Payments", "A data do pagamento deve ser futura"));

            _payments.Add(payment);
        }

        public void Activate()
        {
            Active = true;
            LastUpdate = DateTime.Now;
        }

        public void Inactivate()
        {
            Active = false;
            LastUpdate = DateTime.Now;
        }
    }
}