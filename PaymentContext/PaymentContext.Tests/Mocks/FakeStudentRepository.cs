using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.Mocks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubsctiption(Student student)
        {
            throw new NotImplementedException();
        }

        public bool DocumentExists(string document)
        {
            if (document == "99999999999")
                return true;

            return false;
        }

        public bool EmailExists(string email)
        {
            if (email == "carlos@gmail.com")
                return true;

            return false;
        }
    }
}
