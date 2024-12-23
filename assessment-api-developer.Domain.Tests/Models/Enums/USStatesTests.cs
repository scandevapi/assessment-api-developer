using assessment_api_developer.Domain.Models;

namespace assessment_api_developer.Domain.Tests.Models
{
    public class USStatesTests
    {
        [Fact]
        public void USStatesTests_ShouldHaveCorrectValues()
        {
            Assert.Equal(0, (int)USStates.Alabama);
            Assert.Equal(1, (int)USStates.Alaska);
        }
    }
}
