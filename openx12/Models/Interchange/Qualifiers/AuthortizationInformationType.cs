using openx12.Attributes;

namespace openx12.Models.Interchange.Qualifiers
{
    /// <summary>
    /// Authorization Information Qualifier
    /// Code identifying the type of information in the Authorization Information
    /// </summary>
    public enum AuthortizationInformationType
    {
        /// <summary>
        /// No Authorization Information Present (No Meaningful Information in I02)
        /// </summary>
        [Code("00", "No Authorization Information Present")]
        NoAuthorizationInformationPresent,

        /// <summary>
        /// Additional Data Identification
        /// </summary>
        [Code("03", "Additional Data Identification")]
        AdditionalDataIdentification
    }
}
