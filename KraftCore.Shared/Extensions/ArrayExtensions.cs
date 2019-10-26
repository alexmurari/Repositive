namespace KraftCore.Shared.Extensions
{
    using System;

    /// <summary>
    ///     Provides methods to extend the <see cref="Array" /> class.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        ///     Performs the specified action on each element of the <see cref="Array" />.
        /// </summary>
        /// <param name="array">The <see cref="Array" /> object which the action will be performed.</param>
        /// <param name="action">The action to be performed.</param>
        public static void ForEach(this Array array, Action<Array, int[]> action)
        {
            if (array.LongLength == 0)
                return;

            var walker = new ArrayTraverse(array);

            do
            {
                action(array, walker.Position);
            }
            while (walker.Step());
        }

        /// <summary>
        ///     Walks through the elements of an <see cref="Array" />.
        /// </summary>
        internal class ArrayTraverse
        {
            /// <summary>
            ///     Contains the total number of elements of each dimension of the accessed <see cref="Array" /> instance.
            /// </summary>
            private readonly int[] _maxLengths;

            /// <summary>
            ///     Initializes a new instance of the <see cref="ArrayTraverse" /> class.
            /// </summary>
            /// <param name="array">
            ///     The array to be accessed.
            /// </param>
            internal ArrayTraverse(Array array)
            {
                _maxLengths = new int[array.Rank];

                for (var i = 0; i < array.Rank; ++i)
                    _maxLengths[i] = array.GetLength(i) - 1;

                Position = new int[array.Rank];
            }

            /// <summary>
            ///     Gets the current position of the array.
            /// </summary>
            internal int[] Position { get; }

            /// <summary>
            ///     Moves to the next element of the <see cref="Array" />.
            /// </summary>
            /// <returns>True whether the array has next elements to be accessed; otherwise false.</returns>
            internal bool Step()
            {
                for (var i = 0; i < Position.Length; ++i)
                {
                    if (Position[i] >= _maxLengths[i])
                        continue;

                    Position[i]++;

                    for (var j = 0; j < i; j++)
                        Position[j] = 0;

                    return true;
                }

                return false;
            }
        }
    }
}