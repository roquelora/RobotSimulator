namespace RobotGame.Model
{
    public class Pose
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Enum.Heading Heading { get; set; }

        public Pose() { }

        public Pose(int x, int y, Enum.Heading heading)
        {
            X = x;
            Y = y;
            Heading = heading;
        }

        override public string ToString()
        {
            return $"X: {X}, Y: {Y}, Heading:{Heading}";
        }
    }
}
