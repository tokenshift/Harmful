using System;

namespace Harmful {
    /// <summary>
    /// Contains methods for dealing with UTF-16 character data.
    /// </summary>
    public static class Utf16 {
        /// <summary>
        /// Converts a Unicode code point to a UTF-16 string
        /// (native C# string).
        /// </summary>
        public static string FromCode(int code) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines the lead and trail components of the surrogate pair
        /// for the Unicode character specified by code point.
        /// </summary>
        /// <param name="code">The code point of the character to encode.</param>
        /// <param name="lead">The lead/first surrogate.</param>
        /// <param name="trail">
        /// The trail/last surrogate, or null if the character was encoded in
        /// a single 16-bit integer.
        /// </param>
        /// <returns>
        /// Either 1 or 2, depending how many 16-bit units were needed to encode the
        /// character. If 1, the 'trail' component will be 0; only the 'lead' is relevant.
        /// </returns>
        public static int FromCode(int code, out ushort lead, out ushort trail) {
            throw new NotImplementedException();
        }
    }
}
