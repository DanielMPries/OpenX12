using System;
using openx12.Models.Interchange.Qualifiers;

namespace openx12.Models.Interchange
{
    /// <summary>
    /// The interchange control header (ISA)
    /// </summary>
    public interface IInterchangeControlHeader
    {
        /// <summary>
        /// Code identifying the type of information in the Authorization Information (ISA01)
        /// </summary>
        AuthortizationInformationType AuthortizationInformationType { get; set; }

        /// <summary>
        /// Information used for additional identification or authorization of the interchange <para/>
        /// sender or the data in the interchange; the type of information is set by the <para/>
        /// Authorization Information Qualifier (ISA02)
        /// </summary>
        string AuthortizationInformation { get; set; }

        /// <summary>
        /// Code identifying the type of information in the Security Information (ISA03)
        /// </summary>

        SecurityInformationType SecurityInformationType { get; set; }

        /// <summary>
        /// This is used for identifying the security information about the interchange sender <para/>
        /// or the data in the interchange; the type of information is set by the Security <para/>
        /// Information Qualifier (ISA04)
        /// </summary>
        string SecurityInformation { get; set; }


        /// <summary>
        /// Code indicating the system/method of code structure used to designate the <para/>
        /// sender or receiver ID element being qualified (ISA05)
        /// </summary>
        InterchangeIdType SenderInterchangeIdType { get; set; }

        /// <summary>
        /// Identification code published by the sender for other parties to use as the receiver <para/>
        /// ID to route data to them; the sender always codes this value in the sender ID <para/>
        /// element (ISA06)
        /// </summary>
        string InterchangeSenderId { get; set; }


        /// <summary>
        /// Code indicating the system/method of code structure used to designate the <para/>
        /// sender or receiver ID element being qualified (ISA07)
        /// </summary>
        InterchangeIdType RecieverInterchangeIdType { get; set; }

        /// <summary>
        /// Identification code published by the receiver of the data; When sending, it is used <para/>
        /// by the sender as their sending ID, thus other parties sending to them will use this <para/>
        /// as a receiving ID to route data to them (ISA08)
        /// </summary>
        string InterchangeReceiverId { get; set; }

        /// <summary>
        /// Date and Time of the interchange (ISA09 & ISA10)
        /// </summary>
        DateTime InterchangeDateTime { get; set; }

        /// <summary>
        /// Rhe repetition separator is a delimiter and not a data
        /// element; this field provides the delimiter used to separate repeated occurrences <para/>
        /// of a simple data element or a composite data structure; this value must be <para/>
        /// different than the data element separator, component element separator, and the <para/>
        /// segment terminator (ISA11)
        /// </summary>
        string RepetitionSeparator { get; set; }

        /// <summary>
        /// Code specifying the version number of the interchange control segments (ISA12)
        /// </summary>
        string InterchangeControlVersionNumber { get; set; }

        /// <summary>
        /// A control number assigned by the interchange sender (ISA13)
        /// </summary>
        string InterchangeControlNumber { get; set; }

        /// <summary>
        /// Code indicating sender’s request for an interchange acknowledgment (ISA14)
        /// </summary>
        AcknowledgementRequestedType AcknowledgementRequestedType { get; set; }

        /// <summary>
        /// Code indicating whether data enclosed by this interchange envelope is test, <para/>
        /// production or information (ISA15)
        /// </summary>
        InterchangeUsageIndicatorType InterchangeUsageIndicatorType { get; set; }

        /// <summary>
        /// The component element separator is a delimiter and not a <para/>
        /// data element; this field provides the delimiter used to separate component data <para/>
        /// elements within a composite data structure; this value must be different than the <para/>
        /// data element separator and the segment terminator (ISA16)
        /// </summary>
        string ComponentElementSeperator { get; set; }

    }
}
