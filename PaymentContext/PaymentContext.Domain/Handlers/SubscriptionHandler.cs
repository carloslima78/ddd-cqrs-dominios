using Flunt.Notifications;
using PaymentContext.Domain.Comands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using System;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, 
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPaySubsctiptionCommand>,
        IHandler<CreateCreditCardSubscriptionCommand>
    {
        // Repositório
        private readonly IStudentRepository _repository;

        // Email service
        private readonly IEmailService _emailService;

        // Injeção de dependência do repositório.
        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Hanldle(CreateBoletoSubscriptionCommand command)
        {
            /*
             *  1. Verificar se documento já é cadastrado.
             *  2. Verificar se e-mail já é cadastrado.
             *  3. Gerar Value Objects.
             *  4. Gerar as entidades.
             *  5. Aplicar as validações.
             *  6. Salvar as informações.
             *  8. Enviar e-mail de boas vindas. 
             */

            // Fail Fast Validation
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível realizar a assinatura");
            }

            // Verifica se documento já existe
            if (_repository.DocumentExists(command.Document))
                AddNotification("Document", "Este CPF já está em uso");

            // Verifica se e-mail já existe
            if (_repository.DocumentExists(command.Email))
                AddNotification("Email", "Este e-mail já está em uso");

            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document, EDocumentType.CPF);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.Zip);
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            
            var payment = new BoletoPayment(command.BarCode, command.BoletoNumber, command.PaidDate, 
                command.ExpiredDate, command.Total, command.TotalPaid, address, command.Payer, 
                new Document("41671216032", EDocumentType.CPF), email);

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // Salvar informações.
            _repository.CreateSubsctiption(student);

            // Enviar e-mail de boas vindas.
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem Vindo", "Sua assinatura foi criada com sucesso.");

            return new CommandResult(true, "Sua assinatura foi criada com sucesso");
        }

        public ICommandResult Hanldle(CreatePayPaySubsctiptionCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Hanldle(CreateCreditCardSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
