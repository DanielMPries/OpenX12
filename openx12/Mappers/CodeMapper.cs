using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

using openx12.Attributes;

namespace openx12.Mappers
{
    public class CodeMapper<T> : ICodeMapper<T> where T : struct, IConvertible {
        private static readonly ConcurrentDictionary<T, string> _ValuePairs = new ConcurrentDictionary<T, string>();
        
        // <summary>Returns the code associated with the enumerable value</summary>
        /// <param name="value">The enumerable value</param>
        /// <returns></returns>
        public string Map(T value) {
            return _ValuePairs[value];
        }

        /// <summary>Returns the enumerable value associated with the code</summary>
        /// <param name="code">The code</param>
        /// <returns></returns>
        public T Map(string code) {
            return _ValuePairs
                .Where(v => v.Value.Equals(code))
                .Select(v => v.Key)
                .Single();
        }

        /// <summary>Returns a nullable enumerable value associated with the code</summary>
        /// <param name="code">The code</param>
        /// <returns></returns>
        public T? MapNullable(string code) {
            if (!_ValuePairs.Where(v => v.Value.Equals(code)).Select(v => v.Key).Any()) {
                return null;
            }
            return _ValuePairs.Where(v => v.Value.Equals(code)).Select(v => v.Key).Single();
        }

        /// <summary>Constructor</summary>
        public CodeMapper() {
            var enumType = typeof(T);
            Debug.Assert(enumType.IsEnum, $"{enumType} must be an enumerated type");

            if (_ValuePairs.Count > 0) {
                return;
            }


            string code;
            foreach (T enumValue in Enum.GetValues(enumType)) {
                code = GetCodeAttribute(enumType, enumValue.ToString(CultureInfo.InvariantCulture))?.Code;
                Debug.Assert(!_ValuePairs.Any(v => v.Value.Equals(code) || v.Key.Equals(enumValue)));
                _ValuePairs.TryAdd(enumValue, code);
            }
        }

        /// <summary>Returns the code attribute associated with the enum value</summary>
        /// <param name="type">The enum type</param>
        /// <param name="value">The enum value</param>
        /// <returns></returns>
        private static CodeAttribute GetCodeAttribute(Type type, string value) {
            return type.GetField(value)
                       .GetCustomAttributes(typeof(CodeAttribute), false)
                       .Single() as CodeAttribute;
        }
    }
}