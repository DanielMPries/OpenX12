using openx12.documents.attributes;

namespace openx12.documents.models
{
    [EdiSegment(Name="INS", Loop="2000")]
    public class InsuredBenefit
    {
        [EdiValue("X(1)", Path = "1", Description = "INS01 - Yes/No Condition or Response Code")]
        public string ResponseCode { get; set; }

        [EdiValue("X(2)", Path = "2", Description = "INS02 - Individual Relationship Code")]
        public string IndividualRelationshipCode { get; set; }

        [EdiValue("X(3)", Path = "3", Description = "INS03 - Maintenance Type Code")]
        public string MaintenanceTypeCode { get; set; }

        [EdiValue("X(3)", Path = "4", Description = "INS04 - Maintenance Reason Code")]
        public string MaintenanceReasonCode { get; set; }

        [EdiValue("X(1)", Path = "5", Description = "INS05 - Benefit Status Code")]
        public string BenefitStatusCode { get; set; }

        [EdiValue("X(1)", Path = "6", Description = "INS06 - Medicare Status Code")]
        public string MedicareStatusCode { get; set; }

        [EdiValue("X(2)", Path = "7", Description = "INS07 - COBRA Qualifying Event Code")]
        public string COBRAQualifyingEventCode { get; set; }

        [EdiValue("X(2)", Path = "8", Description = "INS08 - Employment Status Code")]
        public string EmploymentStatusCode { get; set; }

        [EdiValue("X(1)", Path = "9", Description = "INS09 - Student Status Code")]
        public string StudentStatusCode { get; set; }
        //This is supposed to be a handicap indicator but some carriers bootstrap it to convey other Y/N responses.
        [EdiValue("X(1)", Path = "10", Description = "INS10 - Response Code (Handicap Indicator)")]
        public string HandicapIndicator { get; set; }

        [EdiValue("X(3)", Path = "11", Description = "INS11 - Date Time Period Format Qualifier")]
        public string DateTimePeriodFormatQualifier { get; set; }

        [EdiValue("X(35)", Path = "12", Description = "INS12 - Date Time Period")]
        public string DateTimePeriod { get; set; }

        [EdiValue("X(35)", Path = "13", Description = "INS13 - Confidentiality Code")]
        public string ConfidentialityCode { get; set; }
        //These are usually not used but still possible...
        [EdiValue("X(30)", Path = "14", Description = "INS14 - City Name", Mandatory = false)]
        public string CityName { get; set; }

        [EdiValue("X(30)", Path = "15", Description = "INS15 - State or Province Code", Mandatory = false)]
        public string StateProvinceCode { get; set; }

        [EdiValue("X(3)", Path = "16", Description = "INS16 - Country Code", Mandatory = false)]
        public string CountryCode { get; set; }

        [EdiValue("X(9)", Path = "17", Description = "INS17 - Number", Mandatory = false)]
        public string Number { get; set; }
    }
}