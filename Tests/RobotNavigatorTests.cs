using NUnit.Framework;
using RobotGame.Model;
using RobotGame.Enum;
using RobotGame.Services;

namespace Tests
{
    [TestFixture]
    public class RobotNavigatorTests
    {
        [Test]
        public void MoveUninitialized_ReturnsNullPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);

            // Act
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.That(pose, Is.Null);
        }

        [Test]
        public void LeftUninitialized_ReturnsNullPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);

            // Act
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.LEFT));

            // Assert
            Assert.That(pose, Is.Null);
        }

        [Test]
        public void RightUninitialized_ReturnsNullPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);

            // Act
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.RIGHT));

            // Assert
            Assert.That(pose, Is.Null);
        }

        [Test]
        public void ReportUninitialized_ReturnsNullPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);

            // Act
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.REPORT));

            // Assert
            Assert.That(pose, Is.Null);
        }

        [Test]
        public void ReportInitialized_ReturnsPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose placementPose = new() { X = 10, Y = 10, Heading = Heading.NORTH };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, placementPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.REPORT));

            // Assert
            Assert.That(pose, Is.Null);
        }

        [Test]
        public void PlaceValid_ReturnsPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose placementPose = new() { X = 0, Y = 0, Heading = Heading.NORTH };

            // Act
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, placementPose));

            // Assert
            Assert.That(pose, Is.EqualTo(placementPose));
        }

        [Test]
        public void PlaceInvalid_ReturnsNullPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose placementPose = new() { X = 10, Y = 10, Heading = Heading.NORTH };

            // Act
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, placementPose));

            // Assert
            Assert.That(pose, Is.Null);
        }

        [Test]
        public void PlaceInvalidAfterValid_ReturnsPreviousPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose validPose = new() { X = 4, Y = 4, Heading = Heading.NORTH };
            Pose invalidPose = new() { X = 10, Y = 10, Heading = Heading.NORTH };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, validPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, invalidPose));

            // Assert
            Assert.That(pose, Is.EqualTo(validPose));
        }

        [Test]
        public void Left_ReturnsRotatedPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 0, Y = 0, Heading = Heading.NORTH };
            Pose expectedPose = new() { X = 0, Y = 0, Heading = Heading.WEST };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.LEFT));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(expectedPose.X));
                Assert.That(pose?.Y, Is.EqualTo(expectedPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(expectedPose.Heading));
            });
        }

        [Test]
        public void Right_ReturnsRotatedPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 0, Y = 0, Heading = Heading.NORTH };
            Pose expectedPose = new() { X = 0, Y = 0, Heading = Heading.EAST };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.RIGHT));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(expectedPose.X));
                Assert.That(pose?.Y, Is.EqualTo(expectedPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(expectedPose.Heading));
            });
        }

        [Test]
        public void MoveXValid_ReturnsUpdatedPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 0, Y = 0, Heading = Heading.NORTH };
            Pose expectedPose = new() { X = 0, Y = 1, Heading = Heading.NORTH };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(expectedPose.X));
                Assert.That(pose?.Y, Is.EqualTo(expectedPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(expectedPose.Heading));
            });
        }

        [Test]
        public void MoveYValid_ReturnsUpdatedPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 0, Y = 0, Heading = Heading.EAST };
            Pose expectedPose = new() { X = 1, Y = 0, Heading = Heading.EAST };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(expectedPose.X));
                Assert.That(pose?.Y, Is.EqualTo(expectedPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(expectedPose.Heading));
            });
        }

        [Test]
        public void MoveXInvalidSouth_ReturnsPreviousPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 0, Y = 0, Heading = Heading.SOUTH };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(initialPose.X));
                Assert.That(pose?.Y, Is.EqualTo(initialPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(initialPose.Heading));
            });
        }

        [Test]
        public void MoveXInvalidWest_ReturnsPreviousPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 0, Y = 0, Heading = Heading.WEST };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(initialPose.X));
                Assert.That(pose?.Y, Is.EqualTo(initialPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(initialPose.Heading));
            });
        }

        [Test]
        public void MoveXInvalidNorth_ReturnsPreviousPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 4, Y = 4, Heading = Heading.NORTH };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(initialPose.X));
                Assert.That(pose?.Y, Is.EqualTo(initialPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(initialPose.Heading));
            });
        }

        [Test]
        public void MoveXInvalidEast_ReturnsPreviousPose()
        {
            // Arrange
            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);
            Pose initialPose = new() { X = 4, Y = 4, Heading = Heading.EAST };

            // Act
            robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.PLACE, initialPose));
            var pose = robotNavigator.ExecuteCommand(new Command(RobotGame.Enum.Action.MOVE));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(pose?.X, Is.EqualTo(initialPose.X));
                Assert.That(pose?.Y, Is.EqualTo(initialPose.Y));
                Assert.That(pose?.Heading, Is.EqualTo(initialPose.Heading));
            });
        }
    }
}