using System.Collections.Generic;

namespace openx12.Models.Primatives {
    /// <summary>
    /// Idenfifies the functional group being acknowledged (AK1)
    /// </summary>
    public class AK1_FunctionalGroupAcknowledgement : Segment {
        public const string ElementName = "AK1";

        /// <summary>
        /// The functional group ID (GS01) of the functional group being acknowledged.
        /// </summary>
        public string FunctionalGroupId {
            get => Elements[0].Value ?? string.Empty;
            set => Elements[0].Value = value;
        }

        /// <summary>
        /// The group control number (GS06 and GE02) of the functional group being acknowledged.
        /// </summary>
        public string GroupControlNumber {
            get => Elements[1].Value ?? string.Empty;
            set => Elements[1].Value = value;
        }

        /// <summary>
        /// Optional. The EDI implementation version sent in the GS08 of the original transaction
        /// </summary>
        public string ImplementationVersion {
            get => Elements[2].Value ?? string.Empty;
            set => Elements[2].Value = value;
        }

        public override string Value {
            get {
                var dataElements = new List<string>
                {
                    ElementName,
                    FunctionalGroupId,
                    GroupControlNumber
                };

                if (!string.IsNullOrEmpty(ImplementationVersion)) {
                    dataElements.Add(ImplementationVersion);
                }

                return string.Join(FormattingOptions.ElementSeparator, dataElements.ToArray());
            }
        }
    }
}
