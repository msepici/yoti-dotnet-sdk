using Newtonsoft.Json;
using Xunit;

namespace Yoti.Auth.Tests.DocScan.Session.Create
{
    public class SdkConfigBuilderTests
    {
        [Fact]
        public void WithBrandId_SetsBrandId_Correctly()
        {
            var builder = new SdkConfigBuilder();
            var sdkConfig = builder.WithBrandId("test-brand-id").Build();

            Assert.Equal("test-brand-id", sdkConfig.BrandId);
        }

        [Fact]
        public void BrandId_IsExcluded_WhenNotSet()
        {
            var builder = new SdkConfigBuilder();
            var sdkConfig = builder.Build();

            var json = JsonConvert.SerializeObject(sdkConfig);
            Assert.DoesNotContain("brand_id", json);
        }
    }
}
