using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using System.Collections.Generic;
using System.Linq;

namespace PaymentContext.Domain.Entities
{
    
    public class Student : Entity
    {
        // Cole��o para ser utilizada internamente
        private IList<Subscription> _subscriptions;

        // Por boas pr�ticas, sempre criar um construtor.
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
        // Cole��o do tipo IReadOnlyCollection para impedir a inclus�o de assinaturas pela propriedade e sim utilizar o m�todo AddSubscription desta entidade.
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        /// <summary>
        /// Exemplo de regra de neg�cio a ser aplciada para assinaturas
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
                .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voc� j� tem uma assinatura ativa")
                .AreEquals(0, subscription.Payments.Count, "Student.Subscriptions.Payments", "Esta assinatura n�o possui pagamentos"));
        }
    }
}