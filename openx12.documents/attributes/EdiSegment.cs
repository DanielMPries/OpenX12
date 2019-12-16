using System;

namespace openx12.documents.attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false, AllowMultiple = false)]
    public sealed class EdiSegmentAttribute : Attribute
    {
        public string Name { get; set;}
        public string Loop { get; set; }
    }
}