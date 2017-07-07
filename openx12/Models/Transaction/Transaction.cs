using System;
using System.Collections.Generic;
using System.Linq;
using openx12.Utilities;

namespace openx12.Models.Transaction
{
    public class Transaction : IComparable<Transaction> {
        public FormattingOptions FormattingOptions { get; set; } = FormattingOptions.DefaultOptions;

        public List<Segment> Segments { get; set; } = new List<Segment>();

        public TransactionSetHeader Header { get; set; }

        public TransactionSetTrailer Trailer { get; set; }

        public string Value {
            get {
                Trailer.NumberOfIncludedSegments = Convert.ToString(Segments.Count + 2); // segments plus the header and trailer

                var strings = new List<string>();
                strings.Add(Header.ToString());
                strings.AddRange(Segments.Select(s => s.ToString()));
                strings.Add(Trailer.ToString());
                return string.Join(FormattingOptions.SegmentTerminator, strings.ToArray());
            }
            set {
                Segments = new List<Segment>();
                Parse(value, this.FormattingOptions);
            }
        }

        public override string ToString() {
            return Value;
        }

        private void Parse(string value, FormattingOptions options) {
            Segments = new List<Segment>();
            var segmentStringValueCollection = value.Split(options.SegmentTerminator.AsSplitDelimiter(), StringSplitOptions.None);
            foreach (var t in segmentStringValueCollection) {
                if (t.Length <= 2) continue;
                if (t.StartsWith(TransactionSetHeader.ElementName)) {
                    Header = new TransactionSetHeader(t, options);
                }
                else if (t.StartsWith(TransactionSetTrailer.ElementName)) {
                    Trailer = new TransactionSetTrailer(t, options);
                }
                else {
                    var s = new Segment(t, options);
                    Segments.Add(s);
                }
            }
            Segments.TrimExcess();
        }

        public int CompareTo(Transaction that) {
            var thisControlNumber = long.Parse(this?.Header.TransactionSetControlNumber);
            var thatControlNumber = long.Parse(that?.Header.TransactionSetControlNumber);
            return thisControlNumber.CompareTo(thatControlNumber);
        }


        public Transaction() {}

        public Transaction(string value) : this(value, FormattingOptions.DefaultOptions) { }

        public Transaction(string value, FormattingOptions options) {
            Parse(value, options);
        }

        public static explicit operator string(Transaction t) {
            return t.ToString();
        }

    }
}
