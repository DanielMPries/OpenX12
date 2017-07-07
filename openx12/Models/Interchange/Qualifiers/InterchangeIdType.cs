using openx12.Attributes;

namespace openx12.Models.Interchange.Qualifiers
{
    /// <summary>
    /// Interchange ID Qualifier
    /// Code indicating the system/method of code structure used to designate the
    /// sender or receiver ID element being qualified
    /// </summary>
    public enum InterchangeIdType
    {
        /// <summary>
        /// Duns (Dun & Bradstreet)
        /// </summary>
        [Code("01", "Duns (Dun & Bradstreet)")]
        Duns,

        /// <summary>
        /// Duns Plus Suffix
        /// </summary>
        [Code("14", "Duns Plus Suffix")]
        DunsPlusSuffix,

        /// <summary>
        /// Health Industry Number (HIN)
        /// </summary>
        [Code("20", "Health Industry Number")]
        HealthIndustryNumber,

        /// <summary>
        /// Carrier Identification Number as assigned by 
        /// Health Care Financing Administration (HCFA)
        /// </summary>
        [Code("27", "Carrier Identification Number")]
        CarrierIdentificationNumber,

        /// <summary>
        /// Fiscal Intermediary Identification Number as
        /// assigned by Health Care Financing Administration (HCFA)
        /// </summary>
        [Code("28", "Fiscal Intermediary Identification Number")]
        FiscalIntermediaryIdentificationNumber,

        /// <summary>
        /// Medicare Provider and Supplier Identification Number as 
        /// assigned by Health Care Financing Administration (HCFA)
        /// </summary>
        [Code("29", "Medicare Provider and Supplier Identification Number")]
        MedicareProviderAndSupplierIdentificationNumber,

        /// <summary>
        /// U.S. Federal Tax Identification Number
        /// </summary>
        [Code("30", "U.S. Federal Tax Identification Number")]
        USFederalTaxIdentificationNumber,

        /// <summary>
        /// National Association of Insurance Commissioners Company Code (NAIC)
        /// </summary>
        [Code("33", "National Association of Insurance Commissioners Company Code (NAIC)")]
        NationalAssociationOfInsuranceCommissionersCompanyCode,

        /// <summary>
        /// Mutually Defined
        /// </summary>
        [Code("ZZ", "Mutually Defined")]
        MutuallyDefined
    }
}
