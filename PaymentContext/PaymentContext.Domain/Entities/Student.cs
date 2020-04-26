using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    
    public class Student : Entity
    {
        // Coleção para ser utilizada internamente
        private IList<Subscription> _subscriptions;

        // Por boas práticas, sempre criar um construtor.
        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;

            _subscriptions = new List<Subscription>();

            // Biblioteca Flunt
            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }  
        public Email Email { get; private set; }     
        public Address Address { get; private set; }
        // Coleção do tipo IReadOnlyCollection para impedir a inclusão de assinaturas pela propriedade e sim utilizar o método AddSubscription desta entidade.
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        /// <summary>
        /// Exemplo de regra de negócio a ser aplciada para assinaturas
        /// </summary>
        /// <param name="subscription">Objeto do tipo Subscription</param>
        public void AddSubscription(Subscription subscription)
        {

            var hasSubscriptionActive = false;

            foreach(var sub in Subscriptions)
            {
                if(sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            // Biblioteca Flunt
            AddNotifications(new Flunt.Validations.Contract()
                .Requires()
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "VocÊ já tem uma assinatura ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Esta assinatura não possui pagamentos"));
        }
    }
}