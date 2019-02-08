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
        /// using the as keyword.
        /// </summary>
        /// <typeparam name="T">The target type.</typeparam>
        /// <param name="obj">The object to cast.</param>
        /// <returns>The casted object, if successful; else null.</returns>
        public static T CastAs<T>(this object obj) where T : class
        {
            return obj as T;
        }
    }
}
