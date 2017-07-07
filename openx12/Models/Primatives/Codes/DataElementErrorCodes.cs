using openx12.Attributes;


namespace openx12.Models.Primatives.Codes
{
    /// <summary>
    /// Error codes used in the AK403 data element of the AK4 segment (Data Element Note)
    /// </summary>
    public enum DataElementErrorCodes
    {
        ///<summary>Mandatory Data Element Missing</summary>
        [Code("1", "Mandatory Data Element Missing")]
        MandatoryDataElementMissing,

        ///<summary>Conditional Required Data Element Missing</summary>
        [Code("2", "Conditional Required Data Element Missing")]
        ConditionalRequiredDataElementMissing,
        
        ///<summary>Too Many Data Elements</summary>
        [Code("3", "Too Many Data Elements")]
        TooManyDataElements,
        
        ///<summary>Data Element Is Too Short</summary>
        [Code("4", "Data Element Is Too Short")]
        DataElementIsTooShort,
        
        ///<summary>Data Element Is Too Long</summary>
        [Code("5", "Data Element Is Too Long")]
        DataElementIsTooLong,
        
        ///<summary>Invalid Character In Data Element</summary>
        [Code("6", "Invalid Character In Data Element")]
        InvalidCharacterInDataElement,
        
        ///<summary>Invalid Code Value</summary>
        [Code("7", "Invalid Code Value")]
        InvalidCodeValue,
        
        ///<summary>Invalid Date</summary>
        [Code("8", "Invalid Date")]
        InvalidDate,
        
        ///<summary>Invalid Time</summary>
        [Code("9", "Invalid Time")]
        InvalidTime,
        
        ///<summary>Exclusion Condition Violated</summary>
        [Code("10", "Exclusion Condition Violated")]
        ExclusionConditionViolated

    }
}
