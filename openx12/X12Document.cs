using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using openx12.Models.Interchange;
using openx12.Utilities;

namespace openx12 {
    public class X12Document {
        /// <summary>
        /// A collection of interchanges that are defined in the document
        /// </summary>
        public List<Interchange> Interchanges { get; set; } = new List<Interchange>();

        /// <summary>Creates an X12Document from a file</summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static X12Document Read(string path) {
            
            using (var fs = new System.IO.FileStream(path, System.IO.FileMode.Open)) {
                return Read(fs);
            }
        }

        /// <summary>Populates and X12 Document based on the provided stream</summary>
        /// <remarks>
        /// This method uses regular expressions to divide the stream based on the 
        /// Interchange Control Header fixed format pattern
        /// </remarks>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <seealso cref="Interchange"/>
        /// <seealso cref="InterchangeControlHeader"/>
        public static X12Document Read(System.IO.Stream stream) {
            var returnValue = new X12Document();
            
            var data = SimpleBufferedReader.Read(stream);
            var mc = Regex.Matches(data, InterchangeControlHeader.HeaderRegexPattern, RegexOptions.Multiline);
            for (var i = 0; i < mc.Count; i++) {
                returnValue.Interchanges.Add(
                    i < mc.Count - 1
                        ? new Interchange(data.Substring(mc[i].Index, mc[i + 1].Index - mc[i].Index))
                        : new Interchange(data.Substring(mc[i].Index)));
            }
            returnValue.Interchanges.TrimExcess();
            return returnValue;
        }

        /// <summary>Returns a string representation of an x12 document</summary>
        public override string ToString() {
            var returnValue = new System.Text.StringBuilder();
            foreach (var interchange in Interchanges) {
                returnValue.Append(interchange);
            }
            return returnValue.ToString();
        }

        /// <summary>Returns a string representation of an x12 document blocked to a given size</summary>
        public string ToString(uint blockSize) {
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
        public string ToString(bool unwrap) {
            var returnValue = new System.Text.StringBuilder();
            foreach (var interchange in Interchanges) {
                returnValue.Append(interchange.ToString(unwrap));
            }
            return returnValue.ToString();
        }
    }
}