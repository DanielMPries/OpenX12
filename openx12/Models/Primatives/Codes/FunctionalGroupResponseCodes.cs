using openx12.Attributes;

namespace openx12.Models.Primatives.Codes
{
    /// <summary>
    /// Error codes used in the AK905 through AK909 data elements of the AK9 segment (Functional Group Response Trailer)
    /// </summary>
    public enum FunctionalGroupResponseCodes
    {
        ///<summary>Functional Group Not Supported</summary>
        [Code("1", "Functional Group Not Supported")]
        FunctionalGroupNotSupported,
    
        ///<summary>Functional Group Version Not Supported</summary>
        [Code("2", "Functional Group Version Not Supported")]
        FunctionalGroupVersionNotSupported,
    
        ///<summary>Functional Group Trailer Missing</summary>
        [Code("3", "Functional Group Trailer Missing")]
        FunctionalGroupTrailerMissing,
    
        ///<summary>Group Control Number In The Functional Group Header And Trailer Do Not Agree</summary>
        [Code("4", "Group Control Number In The Functional Group Header And Trailer Do Not Agree")]
        GroupControlNumberInTheFunctionalGroupHeaderAndTrailerDoNotAgree,
    
        ///<summary>Number Of Included Transaction Sets Does Not Match Actual Count</summary>
        [Code("5", "Number Of Included Transaction Sets Does Not Match Actual Count")]
        NumberOfIncludedTransactionSetsDoesNotMatchActualCount,
    
        ///<summary>Group Control Number Violates Syntax</summary>
        [Code("6", "Group Control Number Violates Syntax")]
        GroupControlNumberViolatesSyntax
    }
}
