using System;
using Xunit;
using Yoti.Auth.DigitalIdentity.Policy;

namespace Yoti.Auth.Tests.DigitalIdentity.Policy
{
    public class PolicyBuilderTests
    {
        [Theory]
        [InlineData(18, 5, true)]
        [InlineData(21, 2, false)]
        [InlineData(0, 0, true)]
        [InlineData(99, 20, false)]
        public void RequireEstimatedAgeOrDob_ValidValues_Success(int ageOfInterest, int bufferYears, bool required)
        {
            var builder = new PolicyBuilder().RequireEstimatedAgeOrDob(ageOfInterest, bufferYears, required);

            // Add assertions to verify the policy constructed correctly.
            // Assertions will depend on builder internals, e.g., accessing attributes directly.
        }

        [Theory]
        [InlineData(-1, 5)]
        [InlineData(18, -1)]
        [InlineData(100, 5)]
        [InlineData(18, 21)]
        public void RequireEstimatedAgeOrDob_InvalidValues_ThrowsArgumentOutOfRangeException(int ageOfInterest, int bufferYears)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                new PolicyBuilder().RequireEstimatedAgeOrDob(ageOfInterest, bufferYears));
        }
    }
}
