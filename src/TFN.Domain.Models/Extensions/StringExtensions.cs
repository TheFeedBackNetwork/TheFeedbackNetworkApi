﻿using System;
using System.Net.Mail;

namespace TFN.Domain.Models.Extensions
{
    internal static class StringExtensions
    {
        public static bool IsUrl(this string url)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            Uri uriResult;
            var isValidUrl = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp;

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
