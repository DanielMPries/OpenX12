using System;
using System.Collections.Generic;
using System.Linq;
using openx12.Mappers;
using openx12.Models.Interchange.Qualifiers;

namespace openx12.Models.Interchange {
    /// <summary>
    /// The interchange control header (ISA)
    /// </summary>
    public class InterchangeControlHeader : Segment, IInterchangeControlHeader {
        private ICodeMapper<AuthortizationInformationType> _AuthortizationInformationTypeMapper;
        private ICodeMapper<SecurityInformationType>       _SecurityInformationTypeMapper;
        private ICodeMapper<InterchangeIdType>             _InterchangeIdTypeMapper;
        private ICodeMapper<AcknowledgementRequestedType>  _AcknowledgementRequestedTypeMapper;
        private ICodeMapper<InterchangeUsageIndicatorType> _InterchangeUsageIndicatorTypeMapper;

        /// <summary>Regex pattern of an ISA Header Segment.  The ISA is the only fixed format segment in the x12 specification.</summary>
        /// <remarks>
        /// Regex Rules:
        ///     Starts with "ISA"                               SEGMENT NAME
        ///     Followed by 67 characters of any type           AUTHOR, SECURITY, SENDER, RECIEVER ELEMENTS
        ///     Followed by 6 digits                            INTERCHANGE DATE
        ///     Followed by 1 character of any type             CONTROL CHARACTER
        ///     Followed by 4 digits                            INTERCHANGE TIME
        ///     Followed by 3 characters of any type            CONTROL CHARACTER + INTERCHANGE CONTROL STANDARD ID + CONTROL CHARACTER
        ///     Followed by 5 digits                            INTERCHANGE CONTROL VERSION NUMBER
        ///     Followed by 1 character of any type             CONTROL CHARACTER
        ///     Followed by 9 digits                            INTERCHANGE CONTROL NUMBER
        ///     Followed by 1 character of any type             CONTROL CHARACTER
        ///     Followed by 1 digit that must be 0 or 1         BOOLEAN ACKNOWLEDGEMENT REQUESTED
        ///     Followed by 1 character of any type             CONTROL CHARACTER
        ///     Followed by 1 character that must be a P or T   USAGE INDICATOR
        ///     Followed by 3 characters of any type            CONTROL CHARACTERS
        /// </remarks>
        public const string HeaderRegexPattern = @"(ISA)(.{67})(\d{6})(.{1})(\d{4})(.{3})(\d{5})(.{1})(\d{9})(.{1})([0|1]{1})(.{1})([P|T]{1})(.{3})";


        public const string ElementName = "ISA";
        private const int _ElementCount = 16;
        private const string _CenturyStart = "20";

        /// <summary>
        /// Code identifying the type of information in the Authorization Information (ISA01)
        /// </summary>
        public AuthortizationInformationType AuthortizationInformationType { get; set; }

        /// <summary>
        /// Information used for additional identification or authorization of the interchange <para/>
        /// sender or the data in the interchange; the type of information is set by the <para/>
        /// Authorization Information Qualifier (ISA02)
        /// </summary>
        public string AuthortizationInformation {
            get => Elements[1].Value ?? string.Empty;
            set => Elements[1].Value = value;
        }

        /// <summary>
        /// Code identifying the type of information in the Security Information (ISA03)
        /// </summary>
        public SecurityInformationType SecurityInformationType { get; set; }

        /// <summary>
        /// This is used for identifying the security information about the interchange sender <para/>
        /// or the data in the interchange; the type of information is set by the Security <para/>
        /// Information Qualifier (ISA04)
        /// </summary>
        public string SecurityInformation {
            get => Elements[3].Value ?? string.Empty;
            set => Elements[3].Value = value;
        }


        /// <summary>
        /// Code indicating the system/method of code structure used to designate the <para/>
        /// sender or receiver ID element being qualified (ISA05)
        /// </summary>
        public InterchangeIdType SenderInterchangeIdType { get; set; }

        /// <summary>
        /// Identification code published by the sender for other parties to use as the receiver <para/>
        /// ID to route data to them; the sender always codes this value in the sender ID <para/>
        /// element (ISA06)
        /// </summary>
        public string InterchangeSenderId {
            get => Elements[5].Value ?? string.Empty;
            set => Elements[5].Value = value;
        }


        /// <summary>
        /// Code indicating the system/method of code structure used to designate the <para/>
        /// sender or receiver ID element being qualified (ISA07)
        /// </summary>
        public InterchangeIdType RecieverInterchangeIdType { get; set; }

        /// <summary>
        /// Identification code published by the receiver of the data; When sending, it is used <para/>
        /// by the sender as their sending ID, thus other parties sending to them will use this <para/>
        /// as a receiving ID to route data to them (ISA08)
        /// </summary>
        public string InterchangeReceiverId {
            get => Elements[7].Value ?? string.Empty;
            set => Elements[7].Value = value;
        }

        /// <summary>
        /// Date and Time of the interchange (ISA09 & ISA10)
        /// </summary>
        public DateTime InterchangeDateTime {
            get => _InterchangeDateTime;
            set {
                _InterchangeDateTime = value;
                Elements[8].Value = _InterchangeDateTime.ToString(Constants.DateFormatting.YYMMDD);
                Elements[9].Value = _InterchangeDateTime.ToString(Constants.TimeFormatting.HHMM);
            }
        }

        /// <summary>
        /// Rhe repetition separator is a delimiter and not a data
        /// element; this field provides the delimiter used to separate repeated occurrences <para/>
        /// of a simple data element or a composite data structure; this value must be <para/>
        /// different than the data element separator, component element separator, and the <para/>
        /// segment terminator (ISA11)
        /// </summary>
        public string RepetitionSeparator {
            get => Elements[10].Value ?? string.Empty;
            set => Elements[10].Value = value;
        }

        /// <summary>
        /// Code specifying the version number of the interchange control segments (ISA12)
        /// </summary>
        public string InterchangeControlVersionNumber {
            get => Elements[11].Value ?? string.Empty;
            set => Elements[11].Value = value;
        }

        /// <summary>
        /// A control number assigned by the interchange sender (ISA13)
        /// </summary>
        public string InterchangeControlNumber {
            get => Elements[12].Value ?? string.Empty;
            set => Elements[12].Value = value;
        }

        /// <summary>
        /// Code indicating sender’s request for an interchange acknowledgment (ISA14)
        /// </summary>
        public AcknowledgementRequestedType AcknowledgementRequestedType { get; set; }

        /// <summary>
        /// Code indicating whether data enclosed by this interchange envelope is test, <para/>
        /// production or information (ISA15)
        /// </summary>
        public InterchangeUsageIndicatorType InterchangeUsageIndicatorType { get; set; }

        /// <summary>
        /// The component element separator is a delimiter and not a <para/>
        /// data element; this field provides the delimiter used to separate component data <para/>
        /// elements within a composite data structure; this value must be different than the <para/>
        /// data element separator and the segment terminator (ISA16)
        /// </summary>
        public string ComponentElementSeperator {
            get => Elements[15].Value ?? string.Empty;
            set => Elements[15].Value = value;
        }

        public static FormattingOptions GetFormattingOptionsFromHeading(string value) {
            var elementSeparator = value.Substring(3, 1);
            var elementStrings = value.Substring(0,106).Split(elementSeparator.ToCharArray().First());
            var segmentTerminator = elementStrings[16].Substring(1);
            var componentSeparator = elementStrings[16].Substring(0,1);
            var repetitionSeparator = elementStrings[11];

            return new FormattingOptions()
            {
                SegmentTerminator = segmentTerminator,
                ElementSeparator = elementSeparator,
                ComponentSeparator = componentSeparator,
                RepetitionSeparator = repetitionSeparator
            };
        }

        public InterchangeControlHeader() {
            InitializeDependiencies();
            Name = ElementName;
        }

        public InterchangeControlHeader(string value) : this(value, FormattingOptions.DefaultOptions) { }

        public InterchangeControlHeader(string value, FormattingOptions options) {
            InitializeDependiencies();
            Name = ElementName;
            Parse(value, options);
        }

        private void InitializeDependiencies() {
            _AuthortizationInformationTypeMapper = new CodeMapper<AuthortizationInformationType>();
            _SecurityInformationTypeMapper       = new CodeMapper<SecurityInformationType>();
            _InterchangeIdTypeMapper             = new CodeMapper<InterchangeIdType>();
            _AcknowledgementRequestedTypeMapper  = new CodeMapper<AcknowledgementRequestedType>();
            _InterchangeUsageIndicatorTypeMapper = new CodeMapper<InterchangeUsageIndicatorType>();
        }

        protected override void Parse(string value, FormattingOptions options) {
            FormattingOptions = options;
            Elements = new List<Element>(_ElementCount);
            value = value.Substring(0, 105);
            base.Parse(value, options);

            AuthortizationInformationType = _AuthortizationInformationTypeMapper.Map(Elements[0].Value);
            SecurityInformationType       = _SecurityInformationTypeMapper.Map(Elements[2].Value);
            SenderInterchangeIdType       = _InterchangeIdTypeMapper.Map(Elements[4].Value);
            RecieverInterchangeIdType     = _InterchangeIdTypeMapper.Map(Elements[6].Value);
            AcknowledgementRequestedType  = _AcknowledgementRequestedTypeMapper.Map(Elements[13].Value);
            InterchangeUsageIndicatorType = _InterchangeUsageIndicatorTypeMapper.Map(Elements[14].Value);

            InterchangeDateTime = new DateTime(
                year: int.Parse(_CenturyStart + Elements[8].Value?.Substring(0, 2)),
                month: int.Parse(Elements[8].Value?.Substring(2, 2)),
                day: int.Parse(Elements[8].Value?.Substring(4, 2)),
                hour: int.Parse(Elements[9].Value?.Substring(0, 2)),
                minute: int.Parse(Elements[9].Value?.Substring(2, 2)),
                second: 0
            );
        }

        public override string Value {
            get {
                var dataElements = new []
                {
                    Name,
                    _AuthortizationInformationTypeMapper.Map(AuthortizationInformationType),
                    AuthortizationInformation.PadRight(10),
                    _SecurityInformationTypeMapper.Map(SecurityInformationType),
                    SecurityInformation.PadRight(10),
                    _InterchangeIdTypeMapper.Map(SenderInterchangeIdType),
                    InterchangeSenderId.PadRight(15),
                    _InterchangeIdTypeMapper.Map(RecieverInterchangeIdType),
                    InterchangeReceiverId.PadRight(15),
                    InterchangeDateTime.ToString(Constants.DateFormatting.YYMMDD),
                    InterchangeDateTime.ToString(Constants.TimeFormatting.HHMM),
                    RepetitionSeparator,
                    InterchangeControlVersionNumber.PadRight(5),
                    InterchangeControlNumber.PadLeft(9,'0'),
                    _AcknowledgementRequestedTypeMapper.Map(AcknowledgementRequestedType),
                    _InterchangeUsageIndicatorTypeMapper.Map(InterchangeUsageIndicatorType),
                    ComponentElementSeperator
                };

                return string.Join(FormattingOptions.ElementSeparator, dataElements.ToArray());
            }
        }

        private DateTime _InterchangeDateTime;
        
    }
}
