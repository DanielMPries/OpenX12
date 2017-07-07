using openx12.Attributes;

namespace openx12.Models.Primatives.Codes
{
    /// <summary>
    /// Error codes used in the AK304 data element of the AK3 segment (Data Segment Note)
    /// </summary>
    public enum DataSegmentErrorCodes
    {
        /// <summary>
        /// Unrecognized segment ID
        /// </summary>
        [Code("1", "Unrecognized segment ID")]
        UnrecognizedSegmentId,

        /// <summary>
        /// Unexpected segment
        /// </summary>
        [Code("2", "Unexpected segment")]
        UnexpectedSegment,

        /// <summary>
        /// Mandatory segment missing
        /// </summary>
        [Code("3", "Mandatory segment missing")]
        MandatorySegmentMissing,

        /// <summary>
        /// Loop occurs over maximum times
        /// </summary>
        [Code("4", "Loop occurs over maximum times")]
        LoopOccursOverMaximumTimes,

        /// <summary>
        /// Segment exceeds maximum use
        /// </summary>
        [Code("5", "Segment exceeds maximum use")]
        SegmentExceedsMaximumUse,

        /// <summary>
        /// Segment not in defined transaction set
        /// </summary>
        [Code("6", "Segment not in defined transaction set")]
        SegmentNotInDefinedTransactionSet,

        /// <summary>
        /// Segment not in proper sequence 
        /// </summary>
        [Code("7", "Segment not in proper sequence")]
        SegmentNotInProperSequence,

        /// <summary>
        /// Segment has data element errors
        /// </summary>
        [Code("8", "Segment has data element errors")]
        SegmentHasDataElementErrors
    }
}
