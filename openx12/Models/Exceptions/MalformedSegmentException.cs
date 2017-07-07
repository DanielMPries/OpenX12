using System;

namespace openx12.Models.Exceptions
{
    public class MalformedSegmentException : Exception
    {
        public MalformedSegmentException() : base("The segment structure is not valid") { }
        public MalformedSegmentException(string s) : base($"The segment structure is not valid {s}") { }
    }
}
