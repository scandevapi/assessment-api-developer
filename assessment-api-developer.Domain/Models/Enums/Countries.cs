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


// '---' How to use description attribute in enum '---' //
// ---------------------------------------------------- //
//Countries country = Countries.UnitedStates;
//string description = EnumHelper.GetEnumDescription(country);
//Console.WriteLine(description); // Output: United States