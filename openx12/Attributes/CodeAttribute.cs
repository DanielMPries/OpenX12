using System;

namespace openx12.Attributes
{
    /// <summary>
    /// Denotes a field or property that can be derived from a code qualifier field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class CodeAttribute : Attribute
    {
        /// <summary>
        /// The qualifier code 
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// The qualifier description or intent
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">The qualifier code</param>
        public CodeAttribute(string code)
        {
            Code = code;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="code">The qualifier code</param>
        /// <param name="description">The qualifier description</param>
        public CodeAttribute(string code, string description) : this(code)
        {
            Description = description;
        }
    }
}
