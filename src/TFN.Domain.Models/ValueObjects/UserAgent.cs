using System.Globalization;

namespace TFN.Domain.Models.ValueObjects
{
    public class UserAgent
    {
        public string IPAddress { get; private set; }
        public string Culture { get; private set; }
        public string LanguageCode { get; private set; }
        public string CountryCode { get; private set; }
        public string CurrencyCode { get; private set; }
        public string NativeCountryName { get; private set; }

        private UserAgent(string IpAddress, string culture, string languageISOCode, string countryISOCode,
            string currencyISOCode, string nativeCountryName)
        {
            IPAddress = IpAddress;
            Culture = culture;
            LanguageCode = languageISOCode;
            CountryCode = countryISOCode;
            CurrencyCode = currencyISOCode;
            NativeCountryName = nativeCountryName;
        }

        public static UserAgent From(string IpAddress, string culture, string languageISOCode, string countryISOCode,
            string currencyISOCode, string nativeCountryName)
        {
            return new UserAgent(IpAddress,culture,languageISOCode,countryISOCode,currencyISOCode, nativeCountryName);
        }
    }
}