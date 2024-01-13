using Microsoft.VisualStudio.TestTools.UnitTesting;
using WaveSound.Common.Extensions;

namespace WaveSound.UnitTest
{
    [TestClass]
    public class PathExTest
    {
        [TestMethod]
        public void EscapeFileName_WithValidFileName_ShouldReturnSameFileName()
        {
            // Arrange
            var fileName = "validFileName.txt";

            // Act
            var result = PathEx.EscapeFileName(fileName);

            // Assert
            Assert.AreEqual(fileName, result);
        }

        [TestMethod]
        public void EscapeFileName_WithInvalidCharacters_ShouldReplaceWithUnderscore()
        {
            // Arrange
            var fileName = "file*with?invalid|characters.txt";
            var expected = "file_with_invalid_characters.txt";

            // Act
            var result = PathEx.EscapeFileName(fileName);

            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void EscapeFileName_WithEmptyString_ShouldReturnEmptyString()
        {
            // Arrange
            var fileName = "";

            // Act
            var result = PathEx.EscapeFileName(fileName);

            // Assert
            Assert.AreEqual("", result);
        }
    }
}
