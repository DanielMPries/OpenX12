using openx12.Models;
using System;

namespace openx12.Models.Transaction
{
    /// <summary>
    /// Transaction level data member which contains a collection of data 
    /// elements in relation to the trailer level of a transaction set
    /// </summary>
    public class TransactionSetTrailer : Segment {
        public const string ElementName = "SE";

        
        /// <summary>Total number of segments included in a transaction set including ST and SE segments</summary>
        /// <remarks>Number of Included Segments M N0 1/10</remarks>
        public string NumberOfIncludedSegments {
            get => (Elements.Count >= 1) ? Elements[0].Value : string.Empty;
            set
            {
                if ( Elements.Count.Equals(0) ) {
                    Elements.Add(new Element(ElementName));
                }
                if ( Elements.Count >= 1 ) {
                    Elements [ 0 ].Value = value;
                } else {
                    Elements.Add(new Element(value));
                }
            }
        }

        /// <summary>
        /// Identifying control number that must be unique within the transaction set
        /// functional group assigned by the originator for a transaction set
        /// </summary>
        /// <remarks>Transaction Set Control Number M AN 4/9</remarks>
        public string TransactionSetControlNumber {
            get => (Elements.Count >= 2) ? Elements[1].Value : string.Empty;
            set
            {
                if ( Elements.Count.Equals(1) ) {
                    Elements.Add(new Element(value));
                } else if ( Elements.Count >= 2 ) {
                    Elements [ 1 ].Value = value;
                }
            }
        }

        public TransactionSetTrailer ( )
        {
            Elements.Add(new Element(ElementName));
        }

        public TransactionSetTrailer(string value) : this (value, FormattingOptions.DefaultOptions) { }

        public TransactionSetTrailer ( string value,FormattingOptions options)
        {
            Parse(value, options);
            if ( !Elements.Count.Equals(2) ) {
                throw new ArgumentException("Transaction Set Trailer is not valid");
            }
        }
    }
}
