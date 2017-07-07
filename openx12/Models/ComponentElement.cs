using System;
using System.Collections.Generic;
using System.Linq;

namespace openx12.Models
{
    public class ComponentElement
    {
        private string _Value;

        /// <summary>
        /// Returns the value assigned to the component element
        /// </summary>
        public string Value
        {
            get => _Value ?? string.Empty;
            set => _Value = value;
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public ComponentElement()
        {
            Value = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">The value of the component element</param>
        public ComponentElement(string value)
        {
            Value = value?.TrimEnd();
        }

        /// <summary>
        /// Attempts to parse a component element string into a collection of component elements
        /// </summary>
        /// <param name="values">The string to parse</param>
        /// <param name="options">The formatting options</param>
        /// <returns></returns>
        public static IEnumerable<ComponentElement> TryParse( string values, FormattingOptions options = null)
        {
            options = options ?? new FormattingOptions();
            values = values?.Trim() ?? string.Empty;
            var parsedValues = values.Split(new[] { options.ComponentSeparator }, StringSplitOptions.None);
            return parsedValues.Select(v => new ComponentElement(v));
        }
    }
}
