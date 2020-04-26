using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Comands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenDocumentExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();

            command.FirstName = "Carlos";
            command.LastName = "Fabiano";
            command.Document = "99999999999";
            command.Email = "carlos@gmail.com";
            command.BarCode = "111";
            command.BoletoNumber = "111";
            command.PaidDate = DateTime.Now;
            command.ExpiredDate = DateTime.Now.AddMonths(1);
            command.Total = 60;
            command.TotalPaid = 60;
            command.Payer = "CFL";
            command.PayerDocument = "12345678911";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "cfl@cfl.com.br";
            command.Street = "aaa";
            command.Number = "aaa";
            command.Neighborhood = "aaa";
            command.City = "Guarulhos";
            command.State = "SP";
            command.Country = "BR";
            command.Zip = "123";

            handler.Hanldle(command);

            Assert.AreEqual(false, handler.Valid);
        }
    }
}
