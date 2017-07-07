using System.Collections.Generic;

namespace openx12.Models.Primatives {
    /// <summary>
    /// Idenfifies the transaction set being acknowledged (AK2)
    /// </summary>
    public class AK2_TransactionSetAcknowledgment : Segment {
        public const string ElementName = "AK2";

        /// <summary>
        /// The transaction set ID (ST01) of the transaction set being acknowledged.
        /// </summary>
        public string TransactionSetId
        {
            get => Elements[0].Value ?? string.Empty;
            set => Elements[0].Value = value;
        }

        /// <summary>
        /// The transaction set control number (ST02 and SE02) of the transaction set being acknowledged.
        /// </summary>
        public string TransactionSetControlNumber
        {
            get => Elements[1].Value ?? string.Empty;
            set => Elements[1].Value = value;
        }

        /// <summary>
        /// Optional. The EDI implementation version sent in the ST03 of the original transaction
        /// </summary>
        public string ImplementationVersion
        {
            get => Elements[2].Value ?? string.Empty;
            set => Elements[2].Value = value;
        }

        public override string Value
        {
            get {
                var dataElements = new List<string>
                {
                    ElementName,
                    TransactionSetId,
                    TransactionSetControlNumber
                };

                if (!string.IsNullOrEmpty(ImplementationVersion)) {
                    dataElements.Add(ImplementationVersion);
                }

                return string.Join(FormattingOptions.ElementSeparator, dataElements.ToArray());
            }
        }
    }
}
