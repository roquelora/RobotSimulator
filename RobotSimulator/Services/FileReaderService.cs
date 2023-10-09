using RobotGame.Model;

namespace RobotGame.Services
{
    public class FileReaderService
    {
        public List<Command> ReadCommands(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            return ReadCommands(lines);
        }

        public List<Command> ReadCommands(string[] lines)
        {
            List<Command> commands = new();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line) == false && TryParseLine(line, out Command command))
                {
                    commands.Add(command);
                }
            }

            return commands;
        }

        private static bool TryParseLine(string line, out Command command)
        {
            bool success = false;
            command = new Command();

            var words = line.Split(" ");
            if (words.Length < 1)
            {
                return success;
            }

            if (System.Enum.TryParse(words[0], true, out Enum.Action action))
            {
                if (action != Enum.Action.PLACE)
                {
                    command.Action = action;
                    success = true;
                }
                else if (words.Length > 1 && TryParsePose(words[1], out Pose pose))
                {
                    command.Action = action;
                    command.OptionalPose = pose;
                    success = true;
                }
            }

            return success;
        }

        private static bool TryParsePose(string optionalParameter, out Pose pose)
        {
            bool success = false;
            pose = new Pose();

            optionalParameter = optionalParameter.Replace(" ", "");

            var coordinates = optionalParameter.Split(",");
            if (coordinates.Length != 3)
            {
                return success;
            }

            if (int.TryParse(coordinates[0], out int x) && int.TryParse(coordinates[1], out int y) && System.Enum.TryParse(coordinates[2], true, out Enum.Heading heading))
            {
                pose = new Pose(x, y, heading);
                success = true;
            }

            return success;
        }
    }
}
