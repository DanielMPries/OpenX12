using System;

namespace openx12.Mappers
{
    public interface ICodeMapper<T> where T : struct, IConvertible
    {
        T Map(string code);
        string Map(T value);
        T? MapNullable(string code);
    }
}
