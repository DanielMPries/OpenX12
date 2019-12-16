using System;
using System.Collections.Generic;

namespace openx12.documents
{
    public interface IEdiPath
    {
        string LoopName { get; }
        string SegmentName { get; }
        string Qualifier { get; }
    }
    public class EdiRegister
    {
        public IDictionary<IEdiPath, Type> _Catalog { get; private set; } = new Dictionary<IEdiPath, Type>();

        public void Register(IEdiPath path, Type t) => _Catalog.Add(path, t);
        
        public Type GetType(IEdiPath path) {
            if( _Catalog.ContainsKey(path)) {
                return _Catalog[path];
            }
            var ex = new ArgumentException($"The edi path is not registered to a type: {path}");
            ex.Data.Add("Edi Path", path);
            throw ex;
        }
    }
}