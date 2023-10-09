namespace RobotGame.Model
{
    public class Table
    {
        public int Rows { get; }
        public int Columns { get; }

        public Table(int columns, int rows)
        {
            Rows = rows > 0 ? rows : throw new ArgumentOutOfRangeException($"Invalid rows size: {rows}");
            Columns = columns > 0 ? columns : throw new ArgumentOutOfRangeException($"Invalid columns size: {columns}");
        }
    }
}
