using System;
using System.Collections.Generic;
using BrightChain.Engine.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BrightChain.Engine.Tests;

/// <summary>
///     Improved from https://stackoverflow.com/a/23640453.
/// </summary>
[TestClass]
public class CheckDigitExtensionTest
{
    [TestMethod]
    public void ComputeCheckDigitsBase10()
    {
        Assert.AreEqual(
            expected: 0,
            actual: new List<int> {0}.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: 8,
            actual: new List<int> {1}.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: 6,
            actual: new List<int> {2}.CheckDigit(digitsBase: 10));

        Assert.AreEqual(
            expected: 0,
            actual: new List<int> {3, 6, 1, 5, 5}.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: 0,
            actual: 36155.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: 8,
            actual: new List<int> {3, 6, 1, 5, 6}.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: 8,
            actual: 36156.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: 6,
            actual: 36157.CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: "6",
            actual: "36157".CheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: "3",
            actual: "7992739871".CheckDigit(digitsBase: 10));
    }

    [TestMethod]
    public void ValidateCheckDigitsBase10()
    {
        Assert.IsTrue(condition: new List<int> {3, 6, 1, 5, 6, 8}.HasValidCheckDigit(digitsBase: 10));
        Assert.IsTrue(condition: 361568.HasValidCheckDigit(digitsBase: 10));
        Assert.IsTrue(condition: "361568".HasValidCheckDigit(digitsBase: 10));
        Assert.IsTrue(condition: "79927398713".HasValidCheckDigit(digitsBase: 10));
    }

    [TestMethod]
    public void AppendCheckDigitsBase10()
    {
        Console.WriteLine(value: "36156".CheckDigit(digitsBase: 10));
        Console.WriteLine(value: "36156".AppendCheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: "361568",
            actual: "36156".AppendCheckDigit(digitsBase: 10));
        Assert.AreEqual(
            expected: "79927398713",
            actual: "7992739871".AppendCheckDigit(digitsBase: 10));
    }

    [TestMethod]
    public void ValidateCheckDigitsBase16()
    {
        Assert.IsTrue(condition: new List<int> {3, 10, 6, 13, 1, 15, 5, 6, 6}.HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: Convert.ToInt64(
            value: "3A6D1F566",
            fromBase: 16).HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "3A6D1F566".HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "7A9D9F27398712".HasValidCheckDigit(digitsBase: 16));

        Assert.IsTrue(condition: "499602d2f".HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "3A6D1F566".HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "7A9D9F27398712".HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "0B012722900021AC35B25".HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "22111111111111111111f".HasValidCheckDigit(digitsBase: 16));
        Assert.IsTrue(condition: "211111111111111111111".HasValidCheckDigit(digitsBase: 16));
    }

    [TestMethod]
    public void AppendCheckDigitsBase16()
    {
        Console.WriteLine(value: "3A6D1F56".CheckDigit(digitsBase: 16));
        Console.WriteLine(value: "3A6D1F56".AppendCheckDigit(digitsBase: 16));
        Assert.AreEqual(
            expected: "499602d2f",
            actual: "499602d2".AppendCheckDigit(digitsBase: 16));
        Assert.AreEqual(
            expected: "3A6D1F566",
            actual: "3A6D1F56".AppendCheckDigit(digitsBase: 16));
        Assert.AreEqual(
            expected: "7A9D9F27398712",
            actual: "7A9D9F2739871".AppendCheckDigit(digitsBase: 16));
        Assert.AreEqual(
            expected: "0B012722900021AC35B25",
            actual: "0B012722900021AC35B2".AppendCheckDigit(digitsBase: 16));
        Assert.AreEqual(
            expected: "22111111111111111111f",
            actual: "22111111111111111111".AppendCheckDigit(digitsBase: 16));
        Assert.AreEqual(
            expected: "211111111111111111111",
            actual: "21111111111111111111".AppendCheckDigit(digitsBase: 16));
    }
}