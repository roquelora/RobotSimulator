using RobotGame.Model;
using RobotGame.Services;

public class RobotSimulator
{
    private static void Main()
    {
        try
        {
            Console.WriteLine("Robot simulator");

            string commandsFilePath = "C:\\work\\Experiment\\RobotSimulator\\RobotSimulator\\commands.txt";
            FileReaderService reader = new();
            var commands = reader.ReadCommands(commandsFilePath);

            Robot robot = new();
            Table table = new(5, 5);
            RobotNavigator robotNavigator = new(robot, table);

            foreach (var command in commands)
            {
                robotNavigator.ExecuteCommand(command);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Robot simulator encountered an exception: {ex.Message} --- Stack trace: {ex.StackTrace}");

            throw;
        }
    }
}