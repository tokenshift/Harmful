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
        
        /// <summary>
        /// Decodes the leading and trailing surrogates into a
        /// unicode code point.
        /// </summary>
        /// <returns>2 if the trail was part of the surrogate pair, 1 otherwise.</returns>
        public static int ToCode(out uint code, ushort lead, ushort trail = 0) {
            if (lead < 0xD800 || lead > 0xDFFF) {
                code = lead;
                return 1;
            }

            if (lead < 0xD800 || lead > 0xDBFF) {
                throw new ArgumentException("Lead surrogate results in invalid UTF-16 character data.", "lead");
            }

            if (trail < 0xDC00 || trail > 0xDFFF) {
                throw new ArgumentException("Trail surrogate results in invalid UTF-16 character data.", "trail");
            }

            code = 0;

            code = code | ((uint) (lead & 0x3FF) << 10);
            code = code | (uint) (trail & 0x3FF);
            code += 0x10000;

            return 2;
        }

        /// <summary>
        /// Decodes a UTF-16 character or surrogate pair within a string
        /// into a unicode code point.
        /// </summary>
        /// <param name="code">The decoded unicode code point.</param>
        /// <param name="text">The input text containing the character(s) to be decoded.</param>
        /// <param name="offset">The starting location of the character(s) in the string (defaults to 0).</param>
        /// <returns>2 if the trail was part of the surrogate pair, 1 otherwise.</returns>
        public static int ToCode(out uint code, string text, int offset = 0) {
            if (text == null) {
                throw new ArgumentNullException("text");
            }

            if (text.Length == 0) {
                throw new ArgumentException("Input text must contain character data.", "text");
            }

            if (offset >= text.Length) {
                throw new ArgumentOutOfRangeException("offset");
            }

            if (offset + 1 < text.Length) {
                return ToCode(out code, text[offset], text[offset + 1]);
            }
            else {
                return ToCode(out code, text[offset]);
            }
        }
    }
}
