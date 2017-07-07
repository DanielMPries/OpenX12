using openx12.Attributes;

namespace openx12.Models.Interchange.Qualifiers
{
    /// <summary>
    /// Interchange Usage Indicator
    /// Code indicating whether data enclosed by this interchange envelope is test,
    /// production or information
    /// </summary>
    public enum InterchangeUsageIndicatorType
    {
        /// <summary>Production Data</summary>
        [Code("P", "Production Data")]
        ProductionData,

        /// <summary>Test Data</summary>
        [Code("T", "Test Data")]
        TestData
    }
}
