using System;
using System.Collections.Generic;
using System.Linq;
using openx12.Utilities;

namespace openx12.Models
{
    /// <summary>
    /// Implementation of an EDI Element based on the X12 specification
    /// </summary>
    public class Element
    {
        public FormattingOptions FormattingOptions { get; set; } = FormattingOptions.DefaultOptions;

        /// <summary>
        /// A collection of composite elements within the current Element object
        /// </summary>
        /// <seealso cref="ComponentElements"/>
        public List<ComponentElement> ComponentElements { get; set; }

        public Element() { }

        /// <summary>
        /// Creates an element with the given value
        /// </summary>
        /// <param name="value">EDI formatted element either a value or collection of composite element values</param>
        public Element(string value) : this(value, FormattingOptions.DefaultOptions) { }

        /// <summary>Creates an elements with the given value parsed with the givn composite seperator</summary>
        /// <param name="value">EDI formatted element either a value or collection of composite element values</param>
        /// <param name="formattingOptions">The formatting options</param>
        public Element(string value, FormattingOptions formattingOptions)
        {
            FormattingOptions = formattingOptions;
            Parse(value, FormattingOptions);
        }

        /// <summary>
        /// Gets an EDI formated string representation of the element or sets its Composite collection
        /// </summary>
        /// <seealso cref="ToString"/>
        public string Value
        {
            get {
                var values = ComponentElements.Select(c => c.Value).ToArray();
                return string.Join(FormattingOptions.DefaultOptions.ComponentSeparator, values);
            }
            set => Parse(value, FormattingOptions);
        }

        /// <summary>
        /// Populates the Element object's Component Element collection with the provided data
        /// </summary>
        /// <param name="value">The element string to parse</param>
        /// <param name="options">The formatting options</param>
        private void Parse(string value, FormattingOptions options)
        {
            var values = value.Split(options.ComponentSeparator.AsSplitDelimiter(), StringSplitOptions.None);
            ComponentElements = values.Select(v => new ComponentElement(v)).ToList();
            ComponentElements.TrimExcess();
        }

        /// <summary>
        /// Returns an EDI formated string representation of the element
        /// </summary>
        /// <seealso cref="Value"/>
        public override string ToString()
        {
            return Value;
        }

        /// <summary>
        /// Overloads the Element object cast to the type string
        /// </summary>
        /// <param name="e">The element to convert</param>
        public static explicit operator string(Element e)
        {
            return e.ToString();
        }
    }
}
