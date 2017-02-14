using TFN.Domain.Interfaces.Services;
using TFN.Domain.Services.Utilities;

namespace TFN.Domain.Services
{
    public class KeyService : IKeyService
    {
        public string GenerateUrlSafeUniqueKey()
        {
            return CryptographyUtility.CreateUrlSafeUniqueId(22);
        }
    }
}