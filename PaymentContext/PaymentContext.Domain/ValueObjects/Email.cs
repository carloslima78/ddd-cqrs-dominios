
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;

            // Biblioteca Flunt
            AddNotifications(new Flunt.Validations.Contract()
                .Requires()
                .IsEmail(Address, "Email.Address", "E-mail inválido"));
        }

        public string Address { get; private set; }

    }
}
