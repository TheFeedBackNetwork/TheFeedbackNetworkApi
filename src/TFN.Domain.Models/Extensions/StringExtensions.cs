using System;
using System.Net.Mail;

namespace TFN.Domain.Models.Extensions
{
    public static class StringExtensions
    {
        public static bool IsUrl(this string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            bool isValidUrl;

            try
            {
                Uri uriResult;
                isValidUrl = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out uriResult) &&
                             (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }
            catch (Exception)
            {
                return false;
            }

                

            if (!isValidUrl)
            {
                return false;
            }

            return true;
        }

        public static bool IsEmail(this string email)
        {
            try
            {
                var addr = new MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
