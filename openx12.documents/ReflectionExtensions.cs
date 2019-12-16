using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace openx12.documents
{
    // <summary>
    /// Reflection oriented extensions
    /// </summary>

    public static class ReflectionExtensions {
        /// <summary>
        /// Gets the attribute from the item
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Provider">Attribute provider</param>
        /// <param name="Inherit">
        /// When true, it looks up the heirarchy chain for the inherited custom attributes
        /// </param>
        /// <returns>Attribute specified if it exists</returns>
        public static T Attribute<T>(this ICustomAttributeProvider Provider, bool Inherit = true) where T : Attribute {
            Contract.Requires<ArgumentNullException>(Provider != null, "Provider");
            if (Provider.IsDefined(typeof(T), Inherit)) {
                var Attributes = Provider.Attributes<T>(Inherit);
                if (Attributes.Length > 0)
                    return Attributes[0];
            }
            return default;
        }

        /// <summary>
        /// Gets the attributes from the item
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Provider">Attribute provider</param>
        /// <param name="Inherit">
        /// When true, it looks up the heirarchy chain for the inherited custom attributes
        /// </param>
        /// <returns>Array of attributes</returns>
        public static T[] Attributes<T>(this ICustomAttributeProvider Provider, bool Inherit = true) where T : Attribute {
            Contract.Requires<ArgumentNullException>(Provider != null, "Provider");
            return Provider.IsDefined(typeof(T), Inherit) ? Provider.GetCustomAttributes(typeof(T), Inherit).Select(x => (T)x).ToArray() : new T[0];
        }

        /// <summary>
        /// Calls a method on an object
        /// </summary>
        /// <param name="MethodName">Method name</param>
        /// <param name="Object">Object to call the method on</param>
        /// <param name="InputVariables">(Optional)input variables for the method</param>
        /// <typeparam name="ReturnType">Return type expected</typeparam>
        /// <returns>The returned value of the method</returns>
        public static ReturnType Call<ReturnType>(this object Object, string MethodName, params object[] InputVariables) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(MethodName), "MethodName");
            if (InputVariables == null)
                InputVariables = new object[0];
            var ObjectType = Object.GetType();
            Type[] MethodInputTypes = new Type[InputVariables.Length];
            for (int x = 0; x < InputVariables.Length; ++x)
                MethodInputTypes[x] = InputVariables[x].GetType();
            var Method = ObjectType.GetMethod(MethodName, MethodInputTypes);
            if (Method == null)
                throw new InvalidOperationException("Could not find method " + MethodName + " with the appropriate input variables.");
            return (ReturnType)Method.Invoke(Object, InputVariables);
        }

        /// <summary>
        /// Calls a method on an object
        /// </summary>
        /// <param name="MethodName">Method name</param>
        /// <param name="Object">Object to call the method on</param>
        /// <param name="InputVariables">(Optional)input variables for the method</param>
        /// <typeparam name="ReturnType">Return type expected</typeparam>
        /// <typeparam name="GenericType1">Generic method type 1</typeparam>
        /// <returns>The returned value of the method</returns>
        public static ReturnType Call<GenericType1, ReturnType>(this object Object, string MethodName, params object[] InputVariables) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(MethodName), "MethodName");
            if (InputVariables == null)
                InputVariables = new object[0];
            var ObjectType = Object.GetType();
            Type[] MethodInputTypes = new Type[InputVariables.Length];
            for (var x = 0; x < InputVariables.Length; ++x)
                MethodInputTypes[x] = InputVariables[x].GetType();
            var Method = ObjectType.GetMethod(MethodName, MethodInputTypes);
            if (Method == null)
                throw new InvalidOperationException("Could not find method " + MethodName + " with the appropriate input variables.");
            Method = Method.MakeGenericMethod(typeof(GenericType1));
            return Object.Call<ReturnType>(Method, InputVariables);
        }

        /// <summary>
        /// Calls a method on an object
        /// </summary>
        /// <param name="MethodName">Method name</param>
        /// <param name="Object">Object to call the method on</param>
        /// <param name="InputVariables">(Optional)input variables for the method</param>
        /// <typeparam name="ReturnType">Return type expected</typeparam>
        /// <typeparam name="GenericType1">Generic method type 1</typeparam>
        /// <typeparam name="GenericType2">Generic method type 2</typeparam>
        /// <returns>The returned value of the method</returns>
        public static ReturnType Call<GenericType1, GenericType2, ReturnType>(this object Object, string MethodName, params object[] InputVariables) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(MethodName), "MethodName");
            if (InputVariables == null)
                InputVariables = new object[0];
            var ObjectType = Object.GetType();
            Type[] MethodInputTypes = new Type[InputVariables.Length];
            for (int x = 0; x < InputVariables.Length; ++x)
                MethodInputTypes[x] = InputVariables[x].GetType();
            var Method = ObjectType.GetMethod(MethodName, MethodInputTypes);
            if (Method == null)
                throw new InvalidOperationException("Could not find method " + MethodName + " with the appropriate input variables.");
            Method = Method.MakeGenericMethod(typeof(GenericType1), typeof(GenericType2));
            return Object.Call<ReturnType>(Method, InputVariables);
        }

        /// <summary>
        /// Calls a method on an object
        /// </summary>
        /// <param name="MethodName">Method name</param>
        /// <param name="Object">Object to call the method on</param>
        /// <param name="InputVariables">(Optional)input variables for the method</param>
        /// <typeparam name="ReturnType">Return type expected</typeparam>
        /// <typeparam name="GenericType1">Generic method type 1</typeparam>
        /// <typeparam name="GenericType2">Generic method type 2</typeparam>
        /// <typeparam name="GenericType3">Generic method type 3</typeparam>
        /// <returns>The returned value of the method</returns>
        public static ReturnType Call<GenericType1, GenericType2, GenericType3, ReturnType>(this object Object, string MethodName, params object[] InputVariables) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(MethodName), "MethodName");
            if (InputVariables == null)
                InputVariables = new object[0];
            var ObjectType = Object.GetType();
            Type[] MethodInputTypes = new Type[InputVariables.Length];
            for (int x = 0; x < InputVariables.Length; ++x)
                MethodInputTypes[x] = InputVariables[x].GetType();
            var Method = ObjectType.GetMethod(MethodName, MethodInputTypes);
            if (Method == null)
                throw new InvalidOperationException("Could not find method " + MethodName + " with the appropriate input variables.");
            Method = Method.MakeGenericMethod(typeof(GenericType1), typeof(GenericType2), typeof(GenericType3));
            return Object.Call<ReturnType>(Method, InputVariables);
        }

        /// <summary>
        /// Calls a method on an object
        /// </summary>
        /// <param name="Method">Method</param>
        /// <param name="Object">Object to call the method on</param>
        /// <param name="InputVariables">(Optional)input variables for the method</param>
        /// <typeparam name="ReturnType">Return type expected</typeparam>
        /// <returns>The returned value of the method</returns>
        public static ReturnType Call<ReturnType>(this object Object, MethodInfo Method, params object[] InputVariables) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(Method != null, "Method");
            if (InputVariables == null)
                InputVariables = new object[0];
            return (ReturnType)Method.Invoke(Object, InputVariables);
        }

        /// <summary>
        /// Creates an instance of the type and casts it to the specified type
        /// </summary>
        /// <typeparam name="ClassType">Class type to return</typeparam>
        /// <param name="Type">Type to create an instance of</param>
        /// <param name="args">Arguments sent into the constructor</param>
        /// <returns>The newly created instance of the type</returns>
        public static ClassType Create<ClassType>(this Type Type, params object[] args) {
            Contract.Requires<ArgumentNullException>(Type != null, "Type");
            return (ClassType)Type.Create(args);
        }

        /// <summary>
        /// Creates an instance of the type
        /// </summary>
        /// <param name="Type">Type to create an instance of</param>
        /// <param name="args">Arguments sent into the constructor</param>
        /// <returns>The newly created instance of the type</returns>
        public static object Create(this Type Type, params object[] args) {
            Contract.Requires<ArgumentNullException>(Type != null, "Type");
            return Activator.CreateInstance(Type, args);
        }

        /// <summary>
        /// Returns the type's name (Actual C# name, not the funky version from the Name property)
        /// </summary>
        /// <param name="ObjectType">Type to get the name of</param>
        /// <returns>string name of the type</returns>
        public static string GetName(this Type ObjectType) {
            Contract.Requires<ArgumentNullException>(ObjectType != null, "ObjectType");
            var Output = new StringBuilder();
            if (ObjectType.Name == "Void") {
                Output.Append("void");
            }
            else {
                Output.Append(ObjectType.DeclaringType == null ? ObjectType.Namespace : ObjectType.DeclaringType.GetName())
                    .Append(".");
                if (ObjectType.Name.Contains("`")) {
                    var GenericTypes = ObjectType.GetGenericArguments();
                    Output.Append(ObjectType.Name.Remove(ObjectType.Name.IndexOf("`", StringComparison.OrdinalIgnoreCase)))
                        .Append("<");
                    string Seperator = "";
                    foreach (Type GenericType in GenericTypes) {
                        Output.Append(Seperator).Append(GenericType.GetName());
                        Seperator = ",";
                    }
                    Output.Append(">");
                }
                else {
                    Output.Append(ObjectType.Name);
                }
            }
            return Output.ToString().Replace("&", "");
        }

        /// <summary>
        /// Determines if the type has a default constructor
        /// </summary>
        /// <param name="Type">Type to check</param>
        /// <returns>True if it does, false otherwise</returns>
        public static bool HasDefaultConstructor(this Type Type) {
            Contract.Requires<ArgumentNullException>(Type != null, "Type");
            return Type.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                        .Any(x => x.GetParameters().Length == 0);
        }

        /// <summary>
        /// Determines if a mmember has a specified attribute
        /// </summary>
        /// <param name="member"></param>
        /// <param name="inherit"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static bool HasAttribute<TAttribute>(
            this MemberInfo member,
            bool inherit = true) where TAttribute : Attribute {
            return member.GetAttributes<TAttribute>(inherit).Any();
        }


        /// <summary>
        /// Returns any attributes of a type
        /// </summary>
        /// <param name="member"></param>
        /// <param name="inherit"></param>
        /// <typeparam name="TAttribute"></typeparam>
        /// <returns></returns>
        public static TAttribute[] GetAttributes<TAttribute>(
            this MemberInfo member,
            bool inherit = true) where TAttribute : Attribute {
            var attributes = member.GetCustomAttributes(typeof(TAttribute), inherit);
            return (TAttribute[])attributes;
        }

        /// <summary>
        /// Determines if an object is of a specific type
        /// </summary>
        /// <param name="Object">Object</param>
        /// <param name="Type">Type</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool Is(this object Object, Type Type) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(Type != null, "Type");
            return Object.GetType().Is(Type);
        }

        /// <summary>
        /// Determines if an object is of a specific type
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Type">Type</param>
        /// <returns>True if it is, false otherwise</returns>
        public static bool Is(this Type ObjectType, Type Type) {
            Contract.Requires<ArgumentNullException>(Type != null, "Type");
            if (ObjectType == null)
                return false;
            if (Type == typeof(object))
                return true;
            if (Type == ObjectType || ObjectType.GetInterfaces().Any(x => x == Type))
                return true;
            if (ObjectType.BaseType == null)
                return false;
            return ObjectType.BaseType.Is(Type);
        }

        /// <summary>
        /// Determines if an object is of a specific type
        /// </summary>
        /// <param name="Object">Object</param>
        /// <typeparam name="BaseObjectType">Base object type</typeparam>
        /// <returns>True if it is, false otherwise</returns>
        public static bool Is<BaseObjectType>(this object Object) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            return Object.Is(typeof(BaseObjectType));
        }

        /// <summary>
        /// Determines if an object is of a specific type
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <typeparam name="BaseObjectType">Base object type</typeparam>
        /// <returns>True if it is, false otherwise</returns>
        public static bool Is<BaseObjectType>(this Type ObjectType) {
            Contract.Requires<ArgumentNullException>(ObjectType != null, "ObjectType");
            return ObjectType.Is(typeof(BaseObjectType));
        }

        /// <summary>
        /// Loads an assembly by its name
        /// </summary>
        /// <param name="Name">Name of the assembly to return</param>
        /// <returns>The assembly specified if it exists</returns>
        public static System.Reflection.Assembly Load(this AssemblyName Name) {
            Contract.Requires<ArgumentNullException>(Name != null, "Name");
            try {
                return AppDomain.CurrentDomain.Load(Name);
            }
            catch (BadImageFormatException) { return null; }
        }

        /// <summary>
        /// Loads assemblies within a directory and returns them in an array.
        /// </summary>
        /// <param name="Directory">The directory to search in</param>
        /// <param name="Recursive">Determines whether to search recursively or not</param>
        /// <returns>Array of assemblies in the directory</returns>
        public static IEnumerable<Assembly> LoadAssemblies(this DirectoryInfo Directory, bool Recursive = false) {
            Contract.Requires<ArgumentNullException>(Directory != null, "Directory");
            var Assemblies = new List<Assembly>();
            foreach (FileInfo File in Directory.GetFiles("*.dll", Recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)) {
                try {
                    Assemblies.Add(AssemblyName.GetAssemblyName(File.FullName).Load());
                }
                catch (BadImageFormatException) { }
            }
            return Assemblies;
        }

        /// <summary>
        /// Makes a shallow copy of the object
        /// </summary>
        /// <param name="Object">Object to copy</param>
        /// <param name="SimpleTypesOnly">
        /// If true, it only copies simple types (no classes, only items like int, string, etc.),
        /// false copies everything.
        /// </param>
        /// <returns>A copy of the object</returns>
        public static T MakeShallowCopy<T>(this T Object, bool SimpleTypesOnly = false) {
            if (Object == null)
                return default;
            var ObjectType = Object.GetType();
            var ClassInstance = ObjectType.Create<T>();
            foreach (PropertyInfo Property in ObjectType.GetProperties()) {
                try {
                    if (Property.CanRead
                            && Property.CanWrite
                            && SimpleTypesOnly
                            && Property.PropertyType.IsValueType)
                        Property.SetValue(ClassInstance, Property.GetValue(Object, null), null);
                    else if (!SimpleTypesOnly
                                && Property.CanRead
                                && Property.CanWrite)
                        Property.SetValue(ClassInstance, Property.GetValue(Object, null), null);
                }
                catch { }
            }

            foreach (FieldInfo Field in ObjectType.GetFields()) {
                try {
                    if (SimpleTypesOnly && Field.IsPublic)
                        Field.SetValue(ClassInstance, Field.GetValue(Object));
                    else if (!SimpleTypesOnly && Field.IsPublic)
                        Field.SetValue(ClassInstance, Field.GetValue(Object));
                }
                catch { }
            }

            return ClassInstance;
        }

        /// <summary>
        /// Goes through a list of types and determines if they're marked with a specific attribute
        /// </summary>
        /// <typeparam name="T">Attribute type</typeparam>
        /// <param name="Types">Types to check</param>
        /// <param name="Inherit">
        /// When true, it looks up the heirarchy chain for the inherited custom attributes
        /// </param>
        /// <returns>The list of types that are marked with an attribute</returns>
        public static IEnumerable<Type> MarkedWith<T>(this IEnumerable<Type> Types, bool Inherit = true)
            where T : Attribute {
            if (Types == null)
                return null;
            return Types.Where(x => x.IsDefined(typeof(T), Inherit) && !x.IsAbstract);
        }

        /// <summary>
        /// Gets the value of property
        /// </summary>
        /// <param name="Object">The object to get the property of</param>
        /// <param name="Property">The property to get</param>
        /// <returns>Returns the property's value</returns>
        public static object Property(this object Object, PropertyInfo Property) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(Property != null, "Property");
            return Property.GetValue(Object, null);
        }

        /// <summary>
        /// Gets the value of property
        /// </summary>
        /// <param name="Object">The object to get the property of</param>
        /// <param name="Property">The property to get</param>
        /// <returns>Returns the property's value</returns>
        public static object Property(this object Object, string Property) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            Contract.Requires<ArgumentNullException>(!string.IsNullOrEmpty(Property), "Property");
            var Properties = Property.Split(new string[] { "." }, StringSplitOptions.None);
            object TempObject = Object;
            var TempObjectType = TempObject.GetType();
            PropertyInfo DestinationProperty;
            for (int x = 0; x < Properties.Length - 1; ++x) {
                DestinationProperty = TempObjectType.GetProperty(Properties[x]);
                TempObjectType = DestinationProperty.PropertyType;
                TempObject = DestinationProperty.GetValue(TempObject, null);
                if (TempObject == null)
                    return null;
            }
            DestinationProperty = TempObjectType.GetProperty(Properties[Properties.Length - 1]);
            if (DestinationProperty == null)
                throw new NullReferenceException("PropertyInfo can't be null");
            return TempObject.Property(DestinationProperty);
        }


        /// <summary>
        /// Gets a lambda expression that calls a specific property's getter function
        /// </summary>
        /// <typeparam name="ClassType">Class type</typeparam>
        /// <typeparam name="DataType">Data type expecting</typeparam>
        /// <param name="Property">Property</param>
        /// <returns>A lambda expression that calls a specific property's getter function</returns>
        public static Expression<Func<ClassType, DataType>> PropertyGetter<ClassType, DataType>(this PropertyInfo Property) {
            Contract.Requires<ArgumentNullException>(Property != null, "Property");
            if (!Property.PropertyType.Is(typeof(DataType)))
                throw new ArgumentException("Property is not of the type specified");
            if (!Property.DeclaringType.Is(typeof(ClassType)) && !typeof(ClassType).Is(Property.DeclaringType))
                throw new ArgumentException("Property is not from the declaring class type specified");
            var ObjectInstance = Expression.Parameter(Property.DeclaringType, "x");
            var PropertyGet = Expression.Property(ObjectInstance, Property);
            if (Property.PropertyType != typeof(DataType)) {
                var Convert = Expression.Convert(PropertyGet, typeof(DataType));
                return Expression.Lambda<Func<ClassType, DataType>>(Convert, ObjectInstance);
            }
            return Expression.Lambda<Func<ClassType, DataType>>(PropertyGet, ObjectInstance);
        }

        /// <summary>
        /// Gets a lambda expression that calls a specific property's getter function
        /// </summary>
        /// <typeparam name="ClassType">Class type</typeparam>
        /// <param name="Property">Property</param>
        /// <returns>A lambda expression that calls a specific property's getter function</returns>
        public static Expression<Func<ClassType, object>> PropertyGetter<ClassType>(this PropertyInfo Property) {
            Contract.Requires<ArgumentNullException>(Property != null, "Property");
            return Property.PropertyGetter<ClassType, object>();
        }

        /// <summary>
        /// Gets a property name
        /// </summary>
        /// <param name="Expression">LINQ expression</param>
        /// <returns>The name of the property</returns>
        public static string PropertyName(this LambdaExpression Expression) {
            Contract.Requires<ArgumentNullException>(Expression != null, "Expression");
            if (Expression.Body is UnaryExpression expression && Expression.Body.NodeType == ExpressionType.Convert) {
                var Temp = (MemberExpression)expression.Operand;
                return Temp.Expression.PropertyName() + Temp.Member.Name;
            }
            if (!(Expression.Body is MemberExpression))
                throw new ArgumentException("Expression.Body is not a MemberExpression");
            return ((MemberExpression)Expression.Body).Expression.PropertyName() + ((MemberExpression)Expression.Body).Member.Name;
        }

        /// <summary>
        /// Gets a property name
        /// </summary>
        /// <param name="Expression">LINQ expression</param>
        /// <returns>The name of the property</returns>
        public static string PropertyName(this Expression Expression) {
            if (!(Expression is MemberExpression TempExpression))
                return "";
            return TempExpression.Expression.PropertyName() + TempExpression.Member.Name + ".";
        }

        /// <summary>
        /// Gets a property's type
        /// </summary>
        /// <param name="Object">object who contains the property</param>
        /// <param name="PropertyPath">
        /// Path of the property (ex: Prop1.Prop2.Prop3 would be the Prop1 of the source object,
        /// which then has a Prop2 on it, which in turn has a Prop3 on it.)
        /// </param>
        /// <returns>The type of the property specified or null if it can not be reached.</returns>
        public static Type PropertyType(this object Object, string PropertyPath) {
            if (Object == null || string.IsNullOrEmpty(PropertyPath))
                return null;
            return Object.GetType().PropertyType(PropertyPath);
        }

        /// <summary>
        /// Gets a property's type
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="PropertyPath">
        /// Path of the property (ex: Prop1.Prop2.Prop3 would be the Prop1 of the source object,
        /// which then has a Prop2 on it, which in turn has a Prop3 on it.)
        /// </param>
        /// <returns>The type of the property specified or null if it can not be reached.</returns>
        public static Type PropertyType(this Type ObjectType, string PropertyPath) {
            if (ObjectType == null || string.IsNullOrEmpty(PropertyPath))
                return null;
            var SourceProperties = PropertyPath.Split(new string[] { "." }, StringSplitOptions.None);
            for (int x = 0; x < SourceProperties.Length; ++x) {
                PropertyInfo PropertyInfo = ObjectType.GetProperty(SourceProperties[x]);
                ObjectType = PropertyInfo.PropertyType;
            }
            return ObjectType;
        }

        /// <summary>
        /// Gets the version information in a string format
        /// </summary>
        /// <param name="Assembly">Assembly to get version information from</param>
        /// <param name="InfoType">Version info type</param>
        /// <returns>The version information as a string</returns>
        public static string ToString(this Assembly Assembly, VersionInfo InfoType) {
            Contract.Requires<ArgumentNullException>(Assembly != null, "Assembly");
            if (InfoType.HasFlag(VersionInfo.ShortVersion)) {
                Version Version = Assembly.GetName().Version;
                return Version.Major + "." + Version.Minor;
            }
            else {
                return Assembly.GetName().Version.ToString();
            }
        }

        /// <summary>
        /// Dumps the property names and current values from an object
        /// </summary>
        /// <param name="Object">Object to dunp</param>
        /// <param name="HTMLOutput">Determines if the output should be HTML or not</param>
        /// <returns>An HTML formatted table containing the information about the object</returns>
        public static string ToString(this object Object, bool HTMLOutput) {
            Contract.Requires<ArgumentNullException>(Object != null, "Object");
            var TempValue = new StringBuilder();
            TempValue.Append(HTMLOutput ? "<table><thead><tr><th>Property Name</th><th>Property Value</th></tr></thead><tbody>" : "Property Name\t\t\t\tProperty Value");
            var ObjectType = Object.GetType();
            foreach (PropertyInfo Property in ObjectType.GetProperties()) {
                TempValue.Append(HTMLOutput ? "<tr><td>" : System.Environment.NewLine).Append(Property.Name).Append(HTMLOutput ? "</td><td>" : "\t\t\t\t");
                var Parameters = Property.GetIndexParameters();
                if (Property.CanRead && Parameters.Length == 0) {
                    try {
                        var Value = Property.GetValue(Object, null);
                        TempValue.Append(Value == null ? "null" : Value.ToString());
                    }
                    catch { }
                }
                TempValue.Append(HTMLOutput ? "</td></tr>" : "");
            }
            TempValue.Append(HTMLOutput ? "</tbody></table>" : "");
            return TempValue.ToString();
        }

        /// <summary>
        /// Dumps the properties names and current values from an object type (used for static classes)
        /// </summary>
        /// <param name="ObjectType">Object type to dunp</param>
        /// <param name="HTMLOutput">Should this be output as an HTML string</param>
        /// <returns>An HTML formatted table containing the information about the object type</returns>
        public static string ToString(this Type ObjectType, bool HTMLOutput) {
            Contract.Requires<ArgumentNullException>(ObjectType != null, "ObjectType");
            var TempValue = new StringBuilder();
            TempValue.Append(HTMLOutput ? "<table><thead><tr><th>Property Name</th><th>Property Value</th></tr></thead><tbody>" : "Property Name\t\t\t\tProperty Value");
            var Properties = ObjectType.GetProperties();
            foreach (PropertyInfo Property in Properties) {
                TempValue.Append(HTMLOutput ? "<tr><td>" : System.Environment.NewLine).Append(Property.Name).Append(HTMLOutput ? "</td><td>" : "\t\t\t\t");
                if (Property.CanRead && Property.GetIndexParameters().Length == 0) {
                    try {
                        TempValue.Append(Property.GetValue(null, null) == null ? "null" : Property.GetValue(null, null).ToString());
                    }
                    catch { }
                }
                TempValue.Append(HTMLOutput ? "</td></tr>" : "");
            }
            TempValue.Append(HTMLOutput ? "</tbody></table>" : "");
            return TempValue.ToString();
        }

        /// <summary>
        /// Gets a list of types based on an interface
        /// </summary>
        /// <param name="Assembly">Assembly to check</param>
        /// <typeparam name="BaseType">Class type to search for</typeparam>
        /// <returns>List of types that use the interface</returns>
        public static IEnumerable<Type> Types<BaseType>(this Assembly Assembly) {
            Contract.Requires<ArgumentNullException>(Assembly != null, "Assembly");
            return Assembly.Types(typeof(BaseType));
        }

        /// <summary>
        /// Gets a list of types based on an interface
        /// </summary>
        /// <param name="Assembly">Assembly to check</param>
        /// <param name="BaseType">Base type to look for</param>
        /// <returns>List of types that use the interface</returns>
        public static IEnumerable<Type> Types(this Assembly Assembly, Type BaseType) {
            Contract.Requires<ArgumentNullException>(Assembly != null, "Assembly");
            Contract.Requires<ArgumentNullException>(BaseType != null, "BaseType");
            try {
                return Assembly.GetTypes().Where(x => x.Is(BaseType) && x.IsClass && !x.IsAbstract);
            }
            catch { return new List<Type>(); }
        }
    }

    /// <summary>
    /// Version info
    /// </summary>
    public enum VersionInfo {
        /// <summary>
        /// Short version
        /// </summary>
        ShortVersion = 1,

        /// <summary>
        /// Long version
        /// </summary>
        LongVersion = 2
    }
}