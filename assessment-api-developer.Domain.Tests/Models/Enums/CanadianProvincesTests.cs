using assessment_api_developer.Domain.Models;

namespace assessment_api_developer.Domain.Tests.Models
{
    public class CanadianProvincesTests
    {
        [Fact]
        public void CanadianProvinces_ShouldHaveCorrectValues()
        {
            Assert.Equal(0, (int)CanadianProvinces.Alberta);
            Assert.Equal(1, (int)CanadianProvinces.BritishColumbia);
        }
    }
}
