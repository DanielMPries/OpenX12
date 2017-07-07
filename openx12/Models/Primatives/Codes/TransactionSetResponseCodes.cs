using openx12.Attributes;

namespace openx12.Models.Primatives.Codes
{
    /// <summary>
    /// Error codes used in the AK501 data element of the AK5 segment (Transaction Set Response Trailer)
    /// </summary>
    public enum TransactionSetResponseCodes
    {
        ///<summary>Accepted</summary>
        [Code("A", "Accepted")]
        Accepted,

        ///<summary>Accepted But Errors Were Noted</summary>
        [Code("E", "Accepted But Errors Were Noted")]
        AcceptedButErrorsWereNoted,

        ///<summary>Rejected, Message Authentication Code (Mac) Failed</summary>
        [Code("M", "Rejected, Message Authentication Code(Mac) Failed")]
        RejectedMessageAuthenticationCodeFailed,

        ///<summary>Partially Accepted, At Least One Transaction Set Was Rejected</summary>
        [Code("P", "Partially Accepted, At Least One Transaction Set Was Rejected")]
        PartiallyAcceptedAtLeastOneTransactionSetWasRejected,

        ///<summary>Rejected</summary>
        [Code("R", "Rejected")]
        Rejected,

        ///<summary>Rejected, Assurance Failed Validity Tests</summary>
        [Code("W", "Rejected, Assurance Failed Validity Tests")]
        RejectedAssuranceFailedValidityTests,

        ///<summary>Rejected, Content After Decryption Could Not Be Analyzed</summary>
        [Code("X", "Rejected, Content After Decryption Could Not Be Analyzed")]
        RejectedContentAfterDecryptionCouldNotBeAnalyzed
    }
}
