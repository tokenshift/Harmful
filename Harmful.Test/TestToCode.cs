using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Harmful.Test {
    [TestClass]
    public class TestToCode {
        [TestMethod]
        public void TestLatinCharacter() {
            uint code;

            Assert.AreEqual(1, Utf16.ToCode(out code, 0x7A));
            Assert.AreEqual((uint) 0x7A, code);

            Assert.AreEqual(1, Utf16.ToCode(out code, 0x7A, 0xABCD), "Trail is ignored");
            Assert.AreEqual((uint) 0x7A, code);
        }

        [TestMethod]
        public void TestHighSingleComponent() {
            uint code;

            Assert.AreEqual(1, Utf16.ToCode(out code, 0x6C34));
            Assert.AreEqual((uint) 0x6C34, code);

            Assert.AreEqual(1, Utf16.ToCode(out code, 0x6C34, 0xABCD), "Trail is ignored");
            Assert.AreEqual((uint) 0x6C34, code);
        }

        [TestMethod]
        public void TestNonBasic() {
            uint code;

            Assert.AreEqual(2, Utf16.ToCode(out code, 0xD834, 0xDD1E));
            Assert.AreEqual((uint) 0x1D11E, code);
        }
    }
}