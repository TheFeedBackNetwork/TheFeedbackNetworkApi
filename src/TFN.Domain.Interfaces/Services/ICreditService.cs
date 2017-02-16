using System.Threading.Tasks;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface ICreditService
    {
        Task AwardCredit(User fromUser, User toUser, int amount);
        Task RemoveCredit(User fromUser, User toUser, int amount);
    }
}