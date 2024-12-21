using System.ComponentModel;

namespace assessment_api_developer.Domain.Models
{
    public enum Countries
    {
        Canada,
        [Description("United States")]
        UnitedStates
    }
}
