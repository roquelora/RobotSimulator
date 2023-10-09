namespace RobotGame.Model
{
    public class Command
    {
        public Command() { }
        public Command(Enum.Action action)
        {
            Action = action;
        }
        public Command(Enum.Action action, Pose? pose)
        {
            Action = action;
            OptionalPose = pose;
        }

        public Enum.Action Action { get; set; }
        public Pose? OptionalPose { get; set; }
    }
}
