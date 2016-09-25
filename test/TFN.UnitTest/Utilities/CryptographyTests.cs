using System;
using TFN.Domain.Services.Utilities;
using Xunit;
using FluentAssertions;

namespace TFN.UnitTest.Utilities
{
    public class CryptographyTests
    {
        const string Category = "CryptographyUtility";

        [Theory]
        [InlineData(-50)]
        [InlineData(0)]
        [Trait("Category", Category)]
        public void UrlSafeId_CreateUrlSafeUniqueId_ArgumentExceptionThrown(int urllength)
        {
            Action act = () => CryptographyUtility.CreateUrlSafeUniqueId(urllength);

            act.ShouldThrow<ArgumentException>();
        }
    }
}
