using RobotGame.Model;

namespace RobotGame.Services
{
    public class RobotNavigator
    {
        private readonly Robot _robot;
        private readonly Table _table;

        public RobotNavigator(Robot robot, Table table)
        {
            _robot = robot ?? throw new ArgumentNullException(nameof(robot));
            _table = table ?? throw new ArgumentNullException(nameof(table));
        }

        private void Place(Pose? placementPose)
        {
            if (placementPose is null)
            {
                Console.WriteLine($"Ignoring PLACE command. No position was specified.");
            }
            else if (IsPoseValid(placementPose) == false)
            {
                Console.WriteLine($"Ignoring PLACE command. The placement position is not valid. {placementPose}");
            }
            else
            {
                _robot.UpdatePose(placementPose);
            }
        }

        private void Turn(int degrees)
        {
            var currentPose = _robot.GetPose();
            if (currentPose == null)
            {
                Console.WriteLine($"Ignoring TURN command. The robot has not been placed on the table.");
                return;
            }

            Pose newPose = new()
            {
                X = currentPose.X,
                Y = currentPose.Y,
                Heading = currentPose.Heading
            };

            int headingDegrees = ((int)currentPose.Heading + degrees) % 360;
            newPose.Heading = (Enum.Heading)headingDegrees;
            _robot.UpdatePose(newPose);
        }

        private void Report()
        {
            var currentPose = _robot.GetPose();
            if (currentPose == null)
            {
                Console.WriteLine($"Ignoring REPORT command. The robot has not been placed on the table.");
                return;
            }

            Console.WriteLine($"Current robot pose: {currentPose}");
        }

        private void Move()
        {
            var currentPose = _robot.GetPose();
            if (currentPose == null)
            {
                Console.WriteLine($"Ignoring MOVE command. The robot has not been placed on the table.");
                return;
            }

            Pose newPose = new()
            {
                X = currentPose.X,
                Y = currentPose.Y,
                Heading = currentPose.Heading
            };

            if (currentPose.Heading == Enum.Heading.NORTH)
            {
                newPose.Y++;
            }
            else if (currentPose.Heading == Enum.Heading.SOUTH)
            {
                newPose.Y--;
            }
            else if (currentPose.Heading == Enum.Heading.EAST)
            {
                newPose.X++;
            }
            else if (currentPose.Heading == Enum.Heading.WEST)
            {
                newPose.X--;
            }

            if (IsPoseValid(newPose))
            {
                _robot.UpdatePose(newPose);
            }
        }

        private bool IsPoseValid(Pose pose)
        {
            return pose.X >= 0 &&
                   pose.Y >= 0 &&
                   pose.X < _table.Columns &&
                   pose.Y < _table.Rows;
        }

        public Pose? ExecuteCommand(Command command)
        {
            switch (command.Action)
            {
                case Enum.Action.MOVE:
                    Move();
                    break;
                case Enum.Action.LEFT:
                    Turn(270);
                    break;
                case Enum.Action.RIGHT:
                    Turn(90);
                    break;
                case Enum.Action.REPORT:
                    Report();
                    break;
                case Enum.Action.PLACE:
                    Place(command.OptionalPose);
                    break;
                default:
                    break;
            }

            return _robot.GetPose();
        }

    }
}
