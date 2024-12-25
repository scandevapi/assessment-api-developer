using assessment_api_developer.Domain.Models;

namespace assessment_api_developer.Domain.Tests.Models
{
    public class CountriesTests
    {


        [Fact]
        public void Countries_ShouldHaveCorrectValues()
        {
            // Act & Assert
            Assert.Equal(0, (int)Countries.Canada);
            Assert.Equal(1, (int)Countries.UnitedStates);
        }
    }
}
