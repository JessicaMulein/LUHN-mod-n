namespace BrightChain.Engine.Tests
{
    using System;
    using System.Collections.Generic;
    using BrightChain.Engine.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Improved from https://stackoverflow.com/a/23640453.
    /// </summary>
    [TestClass]
    public class CheckDigitExtensionTest
    {
        [TestMethod]
        public void ComputeCheckDigitsBase10()
        {
            Assert.AreEqual(0, (new List<int> { 0 }).CheckDigit(stringBase: 10));
            Assert.AreEqual(8, (new List<int> { 1 }).CheckDigit(stringBase: 10));
            Assert.AreEqual(6, (new List<int> { 2 }).CheckDigit(stringBase: 10));

            Assert.AreEqual(0, (new List<int> { 3, 6, 1, 5, 5 }).CheckDigit(stringBase: 10));
            Assert.AreEqual(0, 36155.CheckDigit(stringBase: 10));
            Assert.AreEqual(8, (new List<int> { 3, 6, 1, 5, 6 }).CheckDigit(stringBase: 10));
            Assert.AreEqual(8, 36156.CheckDigit(stringBase: 10));
            Assert.AreEqual(6, 36157.CheckDigit(stringBase: 10));
            Assert.AreEqual("6", "36157".CheckDigit(stringBase: 10));
            Assert.AreEqual("3", "7992739871".CheckDigit(stringBase: 10));
        }

        [TestMethod]
        public void ValidateCheckDigitsBase10()
        {
            Assert.IsTrue((new List<int> { 3, 6, 1, 5, 6, 8 }).HasValidCheckDigit(stringBase: 10));
            Assert.IsTrue(361568.HasValidCheckDigit(stringBase: 10));
            Assert.IsTrue("361568".HasValidCheckDigit(stringBase: 10));
            Assert.IsTrue("79927398713".HasValidCheckDigit(stringBase: 10));
        }

        [TestMethod]
        public void AppendCheckDigitsBase10()
        {
            Console.WriteLine("36156".CheckDigit(stringBase: 10));
            Console.WriteLine("36156".AppendCheckDigit(stringBase: 10));
            Assert.AreEqual("361568", "36156".AppendCheckDigit(stringBase: 10));
            Assert.AreEqual("79927398713", "7992739871".AppendCheckDigit(stringBase: 10));
        }

        [TestMethod]
        public void ValidateCheckDigitsBase16()
        {
            Assert.IsTrue((new List<int> { 3, 10, 6, 13, 1, 15, 5, 6, 6 }).HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue(Convert.ToInt64(value: "3A6D1F566", fromBase: 16).HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("3A6D1F566".HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("7A9D9F27398712".HasValidCheckDigit(stringBase: 16));

            Assert.IsTrue("499602d2f".HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("3A6D1F566".HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("7A9D9F27398712".HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("0B012722900021AC35B25".HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("22111111111111111111f".HasValidCheckDigit(stringBase: 16));
            Assert.IsTrue("211111111111111111111".HasValidCheckDigit(stringBase: 16));
        }

        [TestMethod]
        public void AppendCheckDigitsBase16()
        {
            Console.WriteLine("3A6D1F56".CheckDigit(stringBase: 16));
            Console.WriteLine("3A6D1F56".AppendCheckDigit(stringBase: 16));
            Assert.AreEqual("499602d2f", "499602d2".AppendCheckDigit(stringBase: 16));
            Assert.AreEqual("3A6D1F566", "3A6D1F56".AppendCheckDigit(stringBase: 16));
            Assert.AreEqual("7A9D9F27398712", "7A9D9F2739871".AppendCheckDigit(stringBase: 16));
            Assert.AreEqual("0B012722900021AC35B25", "0B012722900021AC35B2".AppendCheckDigit(stringBase: 16));
            Assert.AreEqual("22111111111111111111f", "22111111111111111111".AppendCheckDigit(stringBase: 16));
            Assert.AreEqual("211111111111111111111", "21111111111111111111".AppendCheckDigit(stringBase: 16));
        }

    }
}
