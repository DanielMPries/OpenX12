using System;

namespace openx12.Models.FunctionalGroup
{
    /// <summary>
    /// Functional Group level data member which contains a collection of 
    /// data elements in relation to the trailer level of a functional group
    /// </summary>
    public class FunctionalGroupTrailer : Segment
    {
        public const string ElementName = "GE";

        
        /// <summary>
        /// Total number of transaction sets included in the functional group or interchange
        /// (transmission) group terminated by the trailer containing this data element
        /// </summary>
        /// <remarks>Number of Transaction Sets Included M N0 1/6</remarks>
        public string IncludedTransactionSets
        {
            get => (Elements.Count >= 1) ? Elements[0].Value : string.Empty;
            set {
                if (Elements.Count >= 1) {
                    Elements[0].Value = value;
                } else {
                    Elements.Add(new Element(value));
                }
            }
        }

        /// <summary>Assigned number originated and maintained by the sender</summary>
        /// <remarks>Group Control Number M N0 1/9</remarks>
        public string GroupControlNumber
        {
            get => (Elements.Count >= 2) ? Elements[1].Value : string.Empty;
            set {
                if (Elements.Count.Equals(1)) {
                    Elements.Add(new Element(value));
                } else if (Elements.Count >= 2) {
                    Elements[1].Value = value;
                }
            }
        }

        /// <summary>Constructor</summary>
        public FunctionalGroupTrailer() {
            Name = ElementName;
        }


        public FunctionalGroupTrailer(string value) : this()
        {
            Parse(value, FormattingOptions.DefaultOptions);
            if (!Elements.Count.Equals(2)) {
                throw new ArgumentException("Functional Group Trailer is not valid");
            }
        }

        public FunctionalGroupTrailer(string value, FormattingOptions options) : this()
        {
            Parse(value, options);
            if (!Elements.Count.Equals(2)) {
                throw new ArgumentException("Functional Group Trailer is not valid");
            }
        }
    }
}
