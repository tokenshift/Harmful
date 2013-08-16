using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Harmful.Test {
    [TestClass]
    public class TestFromCode {
        [TestMethod]
        public void TestFromLatinCharacter() {
            ushort lead, trail;
            Assert.AreEqual(0, Utf16.FromCode(0x7a, out lead, out trail));

            Assert.AreEqual(0x7a, lead);
            Assert.AreEqual(0, trail);

            Assert.AreEqual("\u007A", Utf16.FromCode(0x7A));
        }

        [TestMethod]
        public void TestHighSingleComponent() {
            ushort lead, trail;
            Assert.AreEqual(0, Utf16.FromCode(0x6c34, out lead, out trail));

            Assert.AreEqual(0x6c34, lead);
            Assert.AreEqual(0, trail);

            Assert.AreEqual("\u6C34", Utf16.FromCode(0x6C34));
        }

        [TestMethod]
        public void TestNonBasic() {
            ushort lead, trail;
            Assert.AreEqual(2, Utf16.FromCode(0x1D11E, out lead, out trail));

            Assert.AreEqual(0xD834, lead);
            Assert.AreEqual(0xDD1E, trail);

            Assert.AreEqual("\uD834\uDD1E", Utf16.FromCode(0x1D11E));
        }
    }
}