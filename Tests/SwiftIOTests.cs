using NUnit.Framework;
using System;
using System.IO;
using System.Collections.Generic;

namespace SwiftNTFS.Tests
{
    [TestFixture]
    public class SwiftIOTests
    {
        [Test]
        public void CreateFolder_ShouldCreateDirectory()
        {
            // Arrange
            string testDirectory = Path.Combine(Path.GetTempPath(), "TestFolder");

            // Act
            SwiftIO.CreateFolder(testDirectory);

            // Assert
            Assert.IsTrue(Directory.Exists(testDirectory));

            // Cleanup
            Directory.Delete(testDirectory);
        }

        [Test]
        public void CreateFolder_ShouldHandleException()
        {
            // Arrange
            string invalidPath = "Z:\\InvalidPath\\TestFolder";

            // Act & Assert
            Assert.DoesNotThrow(() => SwiftIO.CreateFolder(invalidPath));
        }
    }

    [TestFixture]
    public class BuildDirTests
    {
        [Test]
        public void BuildDir_ShouldCombinePathsCorrectly()
        {
            // Arrange
            string folderName = "TestFolder";
            string location = "C:\\Root";

            // Act
            string result = SwiftIO.BuildDir(folderName, location);

            // Assert
            Assert.AreEqual("C:\\Root\\TestFolder", result);
        }
    }

    [TestFixture]
    public class DeleteListItemTests
    {
        [Test]
        public void DeleteListItem_ShouldRemoveCorrectItem()
        {
            // Arrange
            var list = new List<PermissionsEngine>
            {
                new PermissionsEngine { Index = 1 },
                new PermissionsEngine { Index = 2 },
                new PermissionsEngine { Index = 3 }
            };

            // Act
            SwiftIO.DeleteListItem(list, 2);

            // Assert
            Assert.AreEqual(2, list.Count);
            Assert.IsFalse(list.Exists(item => item.Index == 2));
        }
    }

    [TestFixture]
    public class PermissionsEngineTests
    {
        [Test]
        public void SetAccessFlags_ShouldSetCorrectFlags()
        {
            // Arrange
            var engine = new PermissionsEngine();

            // Act
            engine.SetAccessFlags("RW", "TestFolder");

            // Assert
            Assert.IsTrue(engine.acl.Write);
            Assert.IsFalse(engine.acl.Modify);
            Assert.AreEqual("TestFolder_RW", engine.gFolderNamewWithFlag);
        }
    }

    [TestFixture]
    public class LocalFunctionsTests
    {
        [Test]
        public void CreadeAdGroup_ShouldHandleExceptions()
        {
            // Arrange
            string invalidServer = "InvalidServer";
            string ouPath = "OU=Test,DC=example,DC=com";
            string name = "TestGroup";
            string description = "Test Description";
            string owner = "TestOwner";

            // Act & Assert
            Assert.DoesNotThrow(() =>
                LocalFunctions.CreadeAdGroup(invalidServer, ouPath, name, description, owner));
        }
    }

    [TestFixture]
    public class WpfRichTextBoxTargetTests
    {
        [Test]
        public void GetColorFromString_ShouldReturnCorrectColor()
        {
            // Arrange
            string color = "Red";
            var defaultBrush = System.Windows.Media.Brushes.Black;

            // Act
            var result = WpfRichTextBoxTarget.GetColorFromString(color, defaultBrush);

            // Assert
            Assert.AreEqual(System.Windows.Media.Colors.Red, result);
        }

        [Test]
        public void GetColorFromString_ShouldReturnDefaultColorForInvalidInput()
        {
            // Arrange
            string invalidColor = "InvalidColor";
            var defaultBrush = System.Windows.Media.Brushes.Black;

            // Act
            var result = WpfRichTextBoxTarget.GetColorFromString(invalidColor, defaultBrush);

            // Assert
            Assert.AreEqual(((System.Windows.Media.SolidColorBrush)defaultBrush).Color, result);
        }
    }
}