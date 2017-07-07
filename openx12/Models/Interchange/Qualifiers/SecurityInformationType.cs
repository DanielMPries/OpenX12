using openx12.Attributes;

namespace openx12.Models.Interchange.Qualifiers
{
    /// <summary>
    /// Security Information Qualifier
    /// Code identifying the type of information in the Security Information
    /// </summary>
    public enum SecurityInformationType
    {
        /// <summary>No Security Information Present</summary>
        [Code("00", "No Security Information Present")]
        NoSecurityInformationPresent,

        /// <summary>Password</summary>
        [Code("01","Password")]
        Password
    }
}
