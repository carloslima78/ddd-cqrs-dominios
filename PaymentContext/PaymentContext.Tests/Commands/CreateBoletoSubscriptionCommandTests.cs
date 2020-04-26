using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Comands;

namespace PaymentContext.Tests.Commands
{
    [TestClass]
    public class CreateBoletoSubscriptionCommandTests
    {
        [TestMethod]
        public void ShouldReturnErrorWhenNameIsInvalid()
        {
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "";
            
            Assert.AreEqual(false, command.Valid);
        }
    }
}
