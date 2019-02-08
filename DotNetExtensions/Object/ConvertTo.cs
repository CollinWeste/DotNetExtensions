using System;

namespace DotNetExtensions.Object
{
    /// <summary>
    /// Contains extension methods for the <see cref="System.Object"/> class
    /// </summary>
    /// <see cref="System.Collections.Generic.IEnumerable{T}"/>
    public static partial class ObjectExtensions
    {
        /// <summary>
        /// Converts the object to the specified type <typeparamref name="T"/>
        /// using the <see cref="System.Convert.ChangeType(object, Type)"/> method.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted object.</returns>
        /// <seealso cref="System.Convert.ChangeType(object, Type)"/>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ConvertTo<T>(this object obj)
        {
            return (T)Convert.ChangeType(obj, typeof(T));
        }
    }
}
