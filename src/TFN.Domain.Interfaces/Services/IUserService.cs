using System.Threading.Tasks;
using TFN.Domain.Interfaces.Repositories;
using TFN.Domain.Models.Entities;

namespace TFN.Domain.Interfaces.Services
{
    public interface IUserService : IUserRepository
    {
        Task<User> GetAsync(string username, string password);
    }
}
