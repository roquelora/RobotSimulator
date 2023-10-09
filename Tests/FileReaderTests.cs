using NUnit.Framework;
using RobotGame.Model;
using RobotGame.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        [Test]
        public void ReadCommandsFromEmptyList_ReturnsNoCommands()
        {
            // Arrange
            string[] testData = {};
            FileReaderService reader = new();

            // Act
            var commands = reader.ReadCommands(testData);

            // Assert
            Assert.That(commands, Is.Empty);
        }

        [Test]
        public void ReadCommandsFromListWithEmptySpaces_ReturnsNoCommands()
        {
            // Arrange
            string[] testData = { " ", "" };
            FileReaderService reader = new();

            // Act
            var commands = reader.ReadCommands(testData);

            // Assert
            Assert.That(commands, Is.Empty);
        }

        [Test]
        public void ReadCommandsFromListWithInvalidCommands_ReturnsNoCommands()
        {
            // Arrange
            string[] testData = { "JUMP", "MO VE", "PLACE", "PLACE 1,1" };
            FileReaderService reader = new();

            // Act
            var commands = reader.ReadCommands(testData);

            // Assert
            Assert.That(commands, Is.Empty);
        }

        [Test]
        public void ReadCommandsFromValidList_ReturnsAllCommands()
        {
            // Arrange
            string[] testData = { "MOVE", "LEFT", "PLACE 1,1,NORTH", "RIGHT", "MOVE", "REPORT" };
            FileReaderService reader = new();

            // Act
            var commands = reader.ReadCommands(testData);

            // Assert
            Assert.That(commands, Has.Count.EqualTo(6));
        }
    }
}
