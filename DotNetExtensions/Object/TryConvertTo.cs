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
        /// If the conversion fails, returns null.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="obj">The object to convert.</param>
        /// <returns>The converted object, if successful; else default(T).</returns>
        /// <seealso cref="System.Convert.ChangeType(object, Type)"/>
        public static T TryConvertTo<T>(this object obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch { return default(T); }
        }
    }
}
