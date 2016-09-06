using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TFN.Domain.Interfaces.Services;

namespace TFN.Domain.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool ValidatePassword(string password)
        {
            throw new NotImplementedException();
        }

        public bool VerifyHashedPassword(string hashedPassword, string password)
        {
            throw new NotImplementedException();
        }
    }
}
