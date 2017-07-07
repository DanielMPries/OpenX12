using System;
using System.Collections.Generic;
using System.Linq;
using openx12.Utilities;
using openx12.Models.FunctionalGroup;

namespace openx12.Models.Interchange
{
    public class Interchange
    {
        public FormattingOptions FormattingOptions { get; set; } = FormattingOptions.DefaultOptions;
        public List<FunctionalGroup.FunctionalGroup> FunctionalGroups { get; set; } = new List<FunctionalGroup.FunctionalGroup>();

        public InterchangeControlHeader Header { get; set; }

        public InterchangeControlTrailer Trailer { get; set; }
        
        public string Value
        {
            get {
                Trailer.IncludedFunctionalGroups = FunctionalGroups.Count;
                var strings = new List<string>
                {
                    Header.Value
                };
                strings.AddRange(FunctionalGroups.Select(fg => fg.Value));
                strings.Add(Trailer.Value);

                return strings.JoinStrings(FormattingOptions.SegmentTerminator) + FormattingOptions.SegmentTerminator;
            }
            set {
                FunctionalGroups = new List<FunctionalGroup.FunctionalGroup>();
                Parse(value, FormattingOptions);
            }
        }

        public override string ToString()
        {
            return Value;
        }

        public string ToString(bool unwrap)
        {
            return (unwrap)
                ? ToString().Replace(FormattingOptions.SegmentTerminator, FormattingOptions.SegmentTerminator + Environment.NewLine)
                : ToString();
        }

        /// <summary>Constructor</summary>
        public Interchange() { }

        /// <summary>Constructor for instantiation when EDI data string is included</summary>
        public Interchange(string edi) {
            FormattingOptions = InterchangeControlHeader.GetFormattingOptionsFromHeading(edi);
            if (edi.Length > 3 && edi.Substring(0, 3).Equals(InterchangeControlHeader.ElementName)) {
                Parse(edi, FormattingOptions);
            } else {
                throw new ArgumentException("EDI is not a valid interchange");
            }
        }

        /// <summary>
        /// Initialization method for when EDI data string is known
        /// and the segment seperator if the interchange is known
        /// </summary>
        private void Parse(string value, FormattingOptions options)
        {
            
            var segmentStringValueCollection = value.Split(options.SegmentTerminator.AsSplitDelimiter(), StringSplitOptions.None);
            var functionalGroupStringArray = new List<System.Text.StringBuilder>();
            foreach (var segment in segmentStringValueCollection) {
                var segmentStringInstance = segment;
                segmentStringInstance = RemoveCharacterFromString(segmentStringInstance, "\r", options);
                segmentStringInstance = RemoveCharacterFromString(segmentStringInstance, "\n", options);

                if (segmentStringInstance.Length < 3) {
                    continue;
                }

                switch (segmentStringInstance.Substring(0, 3)) {
                    case InterchangeControlHeader.ElementName:
                        Header = new InterchangeControlHeader(segmentStringInstance, options);
                        break;
                    case InterchangeControlTrailer.ElementName:
                        Trailer = new InterchangeControlTrailer(segmentStringInstance, options);
                        break;
                    default:
                        if (segmentStringInstance.Substring(0, 2).Equals(FunctionalGroupHeader.ElementName))
                            functionalGroupStringArray.Add(new System.Text.StringBuilder());

                        functionalGroupStringArray[functionalGroupStringArray.Count - 1].Append(segmentStringInstance);
                        functionalGroupStringArray[functionalGroupStringArray.Count - 1].Append(options.SegmentTerminator);
                        break;
                } 
            }

            foreach (var functionalGroupString in functionalGroupStringArray) {
                var fg = new FunctionalGroup.FunctionalGroup(functionalGroupString.ToString(), FormattingOptions);
                FunctionalGroups.Add(fg);
            }
            FunctionalGroups.TrimExcess();
        }

        private static string RemoveCharacterFromString(string value, string searchString, FormattingOptions options) {
            if (!options.SegmentTerminator.Equals(searchString) &&
                !options.ElementSeparator.Equals(searchString) &&
                !options.ComponentSeparator.Equals(searchString)) {
                return value.Replace(searchString, string.Empty);
            }
            return value;
        }

        public static explicit operator string(Interchange i)
        {
            return i.ToString();
        }

        public IEnumerable<Interchange> Unbundle() {
            return Unbundle(this);
        }

        public static IEnumerable<Interchange> Unbundle(Interchange interchange) {
            var returnValue = new List<Interchange>();
            
            foreach (var functionalGroup in interchange.FunctionalGroups) {
                returnValue.AddRange(functionalGroup.Unbundle()
                    .Select(unbundledFunctionalGroup => new Interchange()
                    {
                        Header = interchange.Header,
                        Trailer = interchange.Trailer,
                        FunctionalGroups = new List<FunctionalGroup.FunctionalGroup>()
                        {
                            unbundledFunctionalGroup
                        }
                    }));
            }

            return returnValue;
        }
    }
}
