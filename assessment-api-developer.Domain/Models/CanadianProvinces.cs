﻿using System.ComponentModel;

namespace assessment_api_developer.Domain.Models
{
    public enum CanadianProvinces
    {
        Alberta,
        [Description("British Columbia")]
        BritishColumbia,
        Manitoba,
        NewBrunswick,
        [Description("Newfoundland and Labrador")]
        NewfoundlandAndLabrador,
        [Description("Northwest Territories")]
        NovaScotia,
        Ontario,
        [Description("Prince Edward Island")]
        PrinceEdwardIsland,
        Quebec,
        Saskatchewan,
        [Description("Yukon")]
        NorthwestTerritories,
        Nunavut,
        Yukon
    }
}
