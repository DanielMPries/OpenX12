using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using openx12.Attributes;

namespace openx12.Mappers {
    public class CodeMapper<T> : ICodeMapper<T> where T : struct, IConvertible {
        private static readonly HashSet<KeyValuePair<T, string>> _ValuePairs = new HashSet<KeyValuePair<T, string>>();

        /// <summary>Returns the code associated with the enumerable value</summary>
        /// <param name="value">The enumerable value</param>
        /// <returns></returns>
        public string Map(T value) {
            return _ValuePairs
                .Where(v => v.Key.Equals(value))
                .Select(v => v.Value)
                .Single();
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
            Debug.Assert(IsOfEnumType(enumType), $"{enumType} must be an enumerated type");

            if (_ValuePairs.Count > 0) {
                return;
            }

            lock (_ValuePairs) {
                if (_ValuePairs.Count > 0) {
                    return;
                }

                string code;
                foreach (T enumValue in Enum.GetValues(enumType)) {
                    code = GetCodeAttribute(enumType, enumValue.ToString(CultureInfo.InvariantCulture))?.Code;

                    Debug.Assert(!_ValuePairs.Any(v => v.Value.Equals(code) || v.Key.Equals(enumValue)));
                    _ValuePairs.Add(new KeyValuePair<T, string>(enumValue, code));
                }
            }
        }

        /// <summary>Returns the code attribute associated with the enum value</summary>
        /// <param name="type">The enum type</param>
        /// <param name="value">The enum value</param>
        /// <returns></returns>
        private static CodeAttribute GetCodeAttribute(Type type, string value) {
#if NET_STANDARD
            return type.GetRuntimeField(value)
                       .GetCustomAttributes<CodeAttribute>(inherit: false)
                       .SingleOrDefault();
#else
            return type.GetField(value)
                       .GetCustomAttributes(typeof(CodeAttribute), false)
                       .Single() as CodeAttribute;
#endif
        }

        /// <summary>
        /// Determines if a type is an enum type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsOfEnumType(Type type) {
#if NET_STANDARD
            return (type.GetTypeInfo().IsEnum);
#else
            return type.IsEnum;
#endif
        }
    }
}