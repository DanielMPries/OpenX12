using System;
using System.Collections.Generic;
using System.Linq;
using openx12.Utilities;
using openx12.Models.Exceptions;
using System.Diagnostics;

namespace openx12.Models {
    [DebuggerDisplay("[{Index.Loop}|{Index.LoopIteration}] {Value}")]
    public class Segment {
        public DataElementIndex.DataIndex Index { get; set; } = new DataElementIndex.DataIndex();
        /// <summary>
        /// Gets/Sets the formatting options for the Segment
        /// </summary>
        public FormattingOptions FormattingOptions { get; set; } = FormattingOptions.DefaultOptions;

        /// <summary>
        /// A collection of elements within the current Segment
        /// </summary>
        /// <seealso cref="Elements"/>
        public List<Element> Elements { get; set; } = new List<Element>();

        /// <summary>Returns an EDI formated string representation of the segment</summary>
        public virtual string Value {
            get {
                var lastFilledElement = Elements.FindLastIndex(e => !string.IsNullOrEmpty(e.Value));
                var limitedElementCollection = Elements.Take(lastFilledElement + 1);
                var elements = limitedElementCollection.Select(e => e.Value).JoinStrings(FormattingOptions.ElementSeparator);
                return $"{Name}{FormattingOptions.ElementSeparator}{elements}";
            }
        }

        /// <summary>Returns an EDI formated string representation of the segment</summary>
        public override string ToString() {
            return Value;
        }

        public Segment() { }

        public Segment(string value) : this(value, FormattingOptions.DefaultOptions) { }

        public Segment(string value, FormattingOptions options) => Parse(value, options);

        public string Name { get; set; }

        /// <summary>Applies the given values to the segment using the given element and composite seperator</summary>
        /// <param name="value">EDI formatted segment string</param>
        /// <param name="options">The formatting options</param>
        protected virtual void Parse(string value, FormattingOptions options) {
            try {
                var stringValueCollection = value.Split(options.ElementSeparator.AsSplitDelimiter(), StringSplitOptions.None);
                Name = stringValueCollection?.ElementAt(0);
                for (var i = 1; i < stringValueCollection?.Length; i++) {
                    var element = new Element(stringValueCollection[i], options);
                    Elements.Add(element);
                }
                Elements.TrimExcess();
            } catch (Exception) {
                throw new MalformedSegmentException(value);
            }
        }

        /// <summary>Segment qualifier.  Equavlent to the value of the first symantic element 01 and the second element index</summary>
        public string Qualifier => (Elements.Count >= 1) ? Elements[0].Value : string.Empty;

        /// <summary>Determines if the segment's first element is the same as at least one value in the given list</summary>
        /// <param name="qualifierList">string qualifiers to be compared</param>
        /// <returns>True if the segment's first element is the same as at least one value in the given list</returns>
        public bool IsOfQualified(params string[] qualifierList) {
            return qualifierList.Any(q => q.Equals(Qualifier));
        }

        /// <summary>Explicit conversion of a Segment object to an EDI formated string representation of the segment</summary>
        /// <param name="s">Segment to convert</param>
        /// <returns>EDI formated string representation of the segment</returns>
        public static explicit operator string(Segment s) {
            return s.ToString();
        }
    }
}
