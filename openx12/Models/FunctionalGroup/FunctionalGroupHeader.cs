using System;
using System.Collections.Generic;

namespace openx12.Models.FunctionalGroup
{
    /// <summary>
    /// Functional Group level data member which contains a collection of 
    /// data elements in relation to the header level of a functional group
    /// </summary>
    public class FunctionalGroupHeader : Segment
    {
        public const string ElementName = "GS";
        private const int _ElementCount = 8;

        /// <summary>Code identifying a group of application related transaction sets</summary>
        /// <remarks>Functional Identifier Code M ID 2/2</remarks>
        public string FunctionalIdentifierCode
        {
            get => Elements[0].Value ?? string.Empty;
            set => Elements[0].Value = value;
        }

        /// <summary>Code identifying party sending transmission. Codes agreed to by trading partners</summary>
        /// <remarks>Application Sender'message Code M AN 2/15</remarks>
        public string ApplicationSendersCode
        {
            get => Elements[1].Value ?? string.Empty;
            set => Elements[1].Value = value;
        }

        /// <summary>Code identifying party receiving transmission. Codes agreed to by trading partners</summary>
        /// <remarks>Application Receiver'message Code M AN 2/15</remarks>
        public string ApplicationReceiversCode
        {
            get => Elements[2].Value ?? string.Empty;
            set => Elements[2].Value = value;
        }

        /// <summary>Date expressed as CCYYMMDD</summary>
        /// <remarks>Date M DT 8/8</remarks>
        // TODO: should get format from the FormatOptions
        public string DateString => CreationDateTime.ToString(Constants.DateFormatting.CCYYMMDD);

        /// <summary>
        /// Time expressed in 24-hour clock time as follows: HHMM, or HHMMSS, 
        /// or HHMMSSD, or HHMMSSDD, where H = hours (00-23), M = minutes (00-59), 
        /// S =integer seconds (00-59) and DD = decimal seconds; decimal seconds 
        /// are expressed as follows: D = tenths (0-9) and DD = hundredths (00-99)
        /// </summary>
        /// <remarks>Time M TM 4/8</remarks>
        // TODO: should get format from the FormatOptions
        public string TimeString  => CreationDateTime.ToString(Constants.TimeFormatting.HHMM);

        /// <summary>Assigned number originated and maintained by the sender</summary>
        /// <remarks>Group Control Number M N0 1/9</remarks>
        public string GroupControlNumber
        {
            get => Elements[5].Value ?? string.Empty;
            set => Elements[5].Value = value;
        }

        /// <summary>Code used in conjunction with Version-Release-Industry Identifier to identify the issuer of the standard</summary>
        /// <remarks>Responsible Agency Code M ID 1/2</remarks>
        public string ResponsibleAgencyCode
        {
            get => Elements[6].Value ?? string.Empty;
            set => Elements[6].Value = value;
        }

        /// <summary>
        /// Code indicating the version, release, subrelease, and industry identifier of the EDI
        /// standard being used, including the GS and GE segments; if code in DE455 in GS
        /// segment is X, then in DE 480 positions 1-3 are the version number; positions 4-6
        /// are the release and subrelease, level of the version; and positions 7-12 are the
        /// industry or trade association identifiers (optionally assigned by user); if code in
        /// DE455 in GS segment is T, then other formats are allowed
        /// </summary>
        /// <remarks>Version / Release / Industry Identifier Code M AN 1/12</remarks>
        public string VersionReleaseIndustryIdentifier
        {
            get => Elements[7].Value ?? string.Empty;
            set => Elements[7].Value = value;
        }

        /// <summary>Constructor</summary>
        public FunctionalGroupHeader() {
            Name = ElementName;
            Elements = new List<Element>(capacity: _ElementCount);
            for( var i = 0 ; i < _ElementCount; i++)
            {
                Elements.Add(new Element(string.Empty));
            }
        }

        public FunctionalGroupHeader(string value) : this(value, FormattingOptions.DefaultOptions) { }

        public FunctionalGroupHeader(string value, FormattingOptions options) => Parse(value, options);

        protected override void Parse(string value, FormattingOptions options) {
            Elements = new List<Element>(_ElementCount);
            base.Parse(value, options);
            
            // TODO: account for seconds and fractional seconds
            CreationDateTime = new DateTime(
                year: int.Parse(Elements[3].Value?.Substring(0, 4)),
                month: int.Parse(Elements[3].Value?.Substring(4, 2)),
                day: int.Parse(Elements[3].Value?.Substring(6, 2)),
                hour: int.Parse(Elements[4].Value?.Substring(0, 2)),
                minute: int.Parse(Elements[4].Value?.Substring(2, 2)),
                second: 0
            );
        }

        private DateTime _CreationDateTime;

        public DateTime CreationDateTime {
            get => _CreationDateTime;
            set {
                _CreationDateTime = value;
                Elements[3] = new Element(DateString);
                Elements[4] = new Element(TimeString);
            }
        }
    }
}
