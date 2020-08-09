namespace SupermarketApi.Extensions
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class StringExstensionsTests
    {
        [TestMethod]
        public void AddingSpaceBeforeCapitalLettersWithNullStringShouldThrowArgumentNullException()
        {
            // Act
            Action addingSpaceBeforeCapitalLettersWithNullString = () => _ = StringExstensions.AddSpaceBeforeCapitalLetters(default!);

            // Assert
            _ = addingSpaceBeforeCapitalLettersWithNullString.Should().ThrowExactly<ArgumentNullException>();
        }

        [DataTestMethod]
        [DataRow("Some", "Some")]
        [DataRow("SomeString ", "Some String ")]
        [DataRow("SomeStringCapitalized", "Some String Capitalized")]
        public void AddingSpaceBeforeCapitalLettersStringShouldReturnExpectedString(
            string str,
            string expectedStr)
        {
            // Act
            var strWithSpaces = str.AddSpaceBeforeCapitalLetters();

            // Assert
            _ = strWithSpaces.Should().Be(expectedStr);
        }
    }
}
