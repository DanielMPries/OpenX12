namespace openx12.Models.Interchange
{
    /// <summary>
    /// define the end of an interchange of zero or more functional groups 
    /// and interchange-related control segments
    /// </summary>
    public interface IInterchangeControlTrailer
    {
        /// <summary>
        /// Number of Included Functional Groups
        /// A count of the number of functional groups included in an interchange
        /// </summary>
        int IncludedFunctionalGroups { get; }

        /// <summary>
        /// Interchange Control Number
        /// A control number assigned by the interchange sender
        /// </summary>
        string InterchangeControlNumber { get; }
    }
}
