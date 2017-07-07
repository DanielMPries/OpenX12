using System;
using System.Collections.Generic;
using System.Linq;

using openx12.Models.Transaction;
using openx12.Utilities;

namespace openx12.Models.FunctionalGroup
{
    public class FunctionalGroup : IComparable<FunctionalGroup>
    {
        public FormattingOptions FormattingOptions { get; set; } = FormattingOptions.DefaultOptions;

        public List<Transaction.Transaction> TransactionSets { get; set; }

        public FunctionalGroupHeader Header { get; set; }

        public FunctionalGroupTrailer Trailer { get; set; }

        public string Value
        {
            get {
                Trailer.IncludedTransactionSets = Convert.ToString(TransactionSets.Count);

                var strings = new List<string>
                {
                    Header.ToString()
                };
                // TODO: allow formatting options to determine if controls numbers can be assigned
                // by index
                strings.AddRange(TransactionSets.Select(ts => ts.ToString()));
                strings.Add(Trailer.ToString());
                return string.Join(FormattingOptions.SegmentTerminator, strings.ToArray());
            }
            set {
                TransactionSets = new List<Transaction.Transaction>();
                Parse(value, FormattingOptions);
            }
        }

        public override string ToString()
        {
            return Value;
        }

        private void Parse(string value, FormattingOptions options)
        {
            FormattingOptions = options;
            var segmentStringValueCollection = value.Split(options.SegmentTerminator.AsSplitDelimiter(), StringSplitOptions.None);
            var transactionStringArray = new List<System.Text.StringBuilder>();
            foreach (var segment in segmentStringValueCollection) {
                if (segment.Length < 2) {
                    continue;
                }
                switch (segment.Substring(0, 2)) {
                    case FunctionalGroupHeader.ElementName:
                        Header = new FunctionalGroupHeader(segment, FormattingOptions);
                        break;
                    case FunctionalGroupTrailer.ElementName:
                        Trailer = new FunctionalGroupTrailer(segment, FormattingOptions);
                        break;
                    case TransactionSetHeader.ElementName:
                        transactionStringArray.Add(new System.Text.StringBuilder());
                        transactionStringArray[transactionStringArray.Count - 1].Append(segment);
                        transactionStringArray[transactionStringArray.Count - 1].Append(options.SegmentTerminator);
                        break;
                    default:
                        transactionStringArray[transactionStringArray.Count - 1].Append(segment);
                        transactionStringArray[transactionStringArray.Count - 1].Append(options.SegmentTerminator);
                        break;
                }
            }

            foreach (var transactionString in transactionStringArray) {
                var tx = new Transaction.Transaction(transactionString.ToString(), FormattingOptions);
                TransactionSets.Add(tx);
                
            }

            TransactionSets.TrimExcess();
        }

        /// <summary>Compares a transaction set</summary>
        public int CompareTo(FunctionalGroup that)
        {
            var thisControlNumber = long.Parse(this?.Header.GroupControlNumber);
            var thatControlNumber = long.Parse(that?.Header.GroupControlNumber);
            return thisControlNumber.CompareTo(thatControlNumber);
        }

        public FunctionalGroup() {
            TransactionSets = new List<Transaction.Transaction>();
        }

        public FunctionalGroup(string value) : this(value, FormattingOptions.DefaultOptions) { }

        public FunctionalGroup(string value, FormattingOptions options) : this()
        {

            Parse(value, options);
        }

        public static explicit operator string(FunctionalGroup f)
        {
            return f.ToString();
        }

        public IEnumerable<FunctionalGroup> Unbundle() {
            return Unbundle(this);
        }

        /// <summary>
            /// Unbundles multiple transactions in a single functional group into single transactions in a single functional group
            /// </summary>
            /// <param name="functionalGroup"></param>
            /// <returns></returns>
            public static IEnumerable<FunctionalGroup> Unbundle(FunctionalGroup functionalGroup) {
            return functionalGroup
                .TransactionSets
                .Select(transaction => new FunctionalGroup()
                {
                    Header = functionalGroup.Header,
                    Trailer = functionalGroup.Trailer,
                    TransactionSets = new List<Transaction.Transaction>()
                    {
                        transaction
                    }
                })
                .ToList();
        }
    }
}
