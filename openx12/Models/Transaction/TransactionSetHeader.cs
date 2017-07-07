using openx12.Models;
using System;

namespace openx12.Models.Transaction
{
    /// <summary>
    /// Transaction level data member which contains a collection of data
    /// elements in relation to the header level of a transaction set
    /// </summary>
    public class TransactionSetHeader : Segment {
        public const string ElementName = "ST";

        /// <summary>Code uniquely identifying a Transaction Set</summary>
        /// <remarks>Transaction Set Identifier Code M ID 3/3</remarks>
        public string TransactionSetIdentifier
        {
            get => ( Elements.Count >= 2 ) ? Elements[0].Value : string.Empty;
            set
            {
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
        public string TransactionSetControlNumber
        {
            get => ( Elements.Count >= 2 ) ? Elements [ 1 ].Value : string.Empty;
            set
            {
                if ( Elements.Count.Equals(2) ) {
                    Elements.Add(new Element(value));
                } else if ( Elements.Count >= 2 ) {
                    Elements [ 1 ].Value = value;
                }
            }
        }

        public string ImplementationConventionReference {
            get => (Elements.Count >= 3) ? Elements[2].Value : string.Empty;
            set {
                if (Elements.Count.Equals(2)) {
                    Elements.Add(new Element(value));
                } else if (Elements.Count >= 3) {
                    Elements[2].Value = value;
                }
            }
        }

        public TransactionSetHeader ( )
        {
            Elements.Add(new Element(ElementName));
        }

        public TransactionSetHeader ( string value )
        {
            Parse(value , FormattingOptions.DefaultOptions);
            if ( !Elements.Count.Equals(3))
                throw new ArgumentException("Transaction Set Header is not valid");
        }

        public TransactionSetHeader ( string value , FormattingOptions options)
        {
            
            Parse(value, options);
            if ( !(Elements.Count >= (3)) )
                throw new ArgumentException("Transaction Set Header is not valid");
        }
    }
}
