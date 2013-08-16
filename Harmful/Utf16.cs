using System;
using System.ComponentModel;

namespace Harmful {
    /// <summary>
    /// Contains methods for dealing with UTF-16 character data.
    /// </summary>
    public static class Utf16 {
        /// <summary>
        /// Converts a Unicode code point to a UTF-16 string
        /// (native C# string).
        /// </summary>
        public static string FromCode(uint code) {
            ushort lead, trail;

            switch (FromCode(code, out lead, out trail)) {
                case 1:
                    return new string(Convert.ToChar(lead), 1);
                case 2:
                    return new string(new[] {
                        Convert.ToChar(lead),
                        Convert.ToChar(trail)
                    });
                default:
                    throw new NotSupportedException();
            }
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
        public static int FromCode(uint code, out ushort lead, out ushort trail) {
            // This implementation is derived from RFC 2781, section 2.1.
            // http://tools.ietf.org/html/rfc2781

            if (code < 0x10000) {
                lead = (ushort) code;
                trail = 0;
                return 1;
            }

            // Code is now within 20 bits.
            code = code - 0x10000;

            lead = 0xD800;
            trail = 0xDC00;

            // Lead gets higher-order 10 bits.
            lead = (ushort) (lead | (ushort) (code >> 10));

            // Trail gets lower-order 10 bits.
            trail = (ushort) (trail | (ushort) (code & 0x3FF));

            return 2;
        }
    }
}
