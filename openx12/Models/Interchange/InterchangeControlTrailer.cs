using System;

namespace openx12.Models.Interchange
{
    /// <summary>
    /// define the end of an interchange of zero or more functional groups 
    /// and interchange-related control segments
    /// </summary>
    public class InterchangeControlTrailer : Segment, IInterchangeControlTrailer
    {
        public const string ElementName = "IEA";

        public const string TrailerRegexPattern = @"(IEA)(.{1})(\d{1,5})(.{1})(\d{9})(.{1})";

        /// <summary>
        /// Number of Included Functional Groups
        /// A count of the number of functional groups included in an interchange
        /// </summary>
        public int IncludedFunctionalGroups
        {
            get => (Elements.Count >= 1) ? int.Parse(Elements[0].Value) : 0;
            set
            {
                if (Elements.Count >= 1)
                {
                    Elements[0].Value = value.ToString();
                }
                else
                {
                    Elements.Add(new Element(value.ToString()));
                }
            }
        }

        /// <summary>
        /// Interchange Control Number
        /// A control number assigned by the interchange sender
        /// </summary>
        public string InterchangeControlNumber
        {
            get => (Elements.Count >= 2) ? Elements[1].Value : string.Empty;
            set
            {
                if (Elements.Count.Equals(1))
                {
                    Elements.Add(new Element(value));
                }
                else if (Elements.Count >= 2)
                {
                    Elements[1].Value = value;
                }
            }
        }

        /// <summary>Constructor</summary>
        public InterchangeControlTrailer()
        {
            Name = ElementName;
        }

        public InterchangeControlTrailer(string value)
        {
            Parse(value, FormattingOptions.DefaultOptions);
            if (!Elements.Count.Equals(2))
                throw new ArgumentException("Functional Group Trailer is not valid");
        }

        public InterchangeControlTrailer(string value, FormattingOptions options)
        {
            Parse(value, options);
            if (!Elements.Count.Equals(2))
                throw new ArgumentException("Functional Group Trailer is not valid");
        }
    }
}
