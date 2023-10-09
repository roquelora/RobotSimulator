namespace RobotGame.Model
{
    public class Robot
    {
        private Pose? _pose;

        public Pose? GetPose()
        {
            return _pose;
        }

        public Pose? UpdatePose(Pose pose)
        {
            _pose = pose;
            return _pose;
        }
    }
}
