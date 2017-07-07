using System.Collections.Generic;
using openx12.Mappers;
using openx12.Models.Primatives.Codes;

namespace openx12.Models.Primatives {
    /// <summary>
    /// Reports errors in a data segment and identifies the location of the data segment (AK3) <para/>
    /// </summary>
    public class AK3_SegmentAcknowledgementError : Segment {
        private readonly ICodeMapper<DataSegmentErrorCodes> _DataSegmentErrorCodeMapper;

        public const string ElementName = "AK3";

        /// <summary>
        /// Identifies the segment in error with its X12 segment ID, for example, NM1.
        /// </summary>
        public string ErrorSegmentId {
            get => Elements[0].Value ?? string.Empty;
            set => Elements[0].Value = value;
        }

        /// <summary>
        /// The segment count of the segment in error. 
        /// </summary>
        public int SegmentCount {
            get => string.IsNullOrEmpty(Elements[1].Value) ? 0 : int.Parse(Elements[1].Value);
            set => Elements[1].Value = value.ToString();
        }

        /// <summary>
        /// Identifies a bounded loop: a loop surrounded by an LS segment and a LE segment.
        /// </summary>
        public string BoundedLoop {
            get => Elements[2].Value ?? string.Empty;
            set => Elements[2].Value = value;
        }

        /// <summary>
        /// The error code for the error in the data segment
        /// </summary>
        public Codes.DataSegmentErrorCodes? DataSegmentError {
            get {
                if (string.IsNullOrEmpty(Elements[3].Value)) {
                    return null;
                }
                return _DataSegmentErrorCodeMapper.Map(Elements[3].Value);
            }
            set {
                if (value == null) {
                    Elements[3].Value = null;
                    return;
                }

                Elements[3].Value = _DataSegmentErrorCodeMapper.Map(value.Value);
            }
        }

        public AK3_SegmentAcknowledgementError() {
            _DataSegmentErrorCodeMapper = new CodeMapper<DataSegmentErrorCodes>();
        }

        public override string Value {
            get {
                var dataElements = new List<string>
                {
                    ElementName,
                    ErrorSegmentId,
                    SegmentCount.ToString(),
                    BoundedLoop
                };

                if (!string.IsNullOrEmpty(Elements[3].Value)) {
                    dataElements.Add(Elements[3].Value);
                }

                return string.Join(FormattingOptions.ElementSeparator, dataElements.ToArray());
            }
        }
    }
}