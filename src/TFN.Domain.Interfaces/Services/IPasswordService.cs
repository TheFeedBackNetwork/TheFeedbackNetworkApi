using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFN.Domain.Interfaces.Services
{
    public interface IPasswordService
    {
        bool ValidatePassword(string password);
        string HashPassword(string password);
        bool VerifyHashedPassword(string hashedPassword, string password);
    }
}
