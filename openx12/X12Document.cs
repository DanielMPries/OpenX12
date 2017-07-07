using System;
using System.Collections.Generic;
using openx12.Models.Interchange;

namespace openx12
{
    public class X12Document
    {
        /// <summary>
        /// A collection of interchanges that are defined in the document
        /// </summary>
        public List<Interchange> Interchanges { get; set; } = new List<Interchange>();

        /// <summary>Returns a string representation of an x12 document</summary>
        public override string ToString()
        {
            var returnValue = new System.Text.StringBuilder();
            foreach (var interchange in Interchanges) {
                returnValue.Append(interchange);
            }
            return returnValue.ToString();
        }

        /// <summary>Returns a string representation of an x12 document blocked to a given size</summary>
        public string ToString(uint blockSize)
        {
            var returnValue = new System.Text.StringBuilder();
            var value = ToString().ToCharArray();
            var charCounter = 0;
            foreach (var c in value) {
                returnValue.Append(c);
                charCounter++;
                if (charCounter != blockSize) {
                    continue;
                }
                returnValue.Append(Environment.NewLine);
                charCounter = 0;
            }
            return returnValue.ToString();
        }

        /// <summary>
        /// Returns a string representation of an x12 document.  Returns an unwrapped x12 string if given value is true
        /// </summary>
        public string ToString(bool unwrap)
        {
            var returnValue = new System.Text.StringBuilder();
            foreach (var interchange in Interchanges) {
                returnValue.Append(interchange.ToString(unwrap));
            }
            return returnValue.ToString();
        }
    }
}
