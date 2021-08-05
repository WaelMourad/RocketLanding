namespace Landing.Models
{
    /// <summary>
    /// Whole landing area (area that contains landing platform and surroundings)
    /// consists of multiple squares that set a perimeter/dimensions that can be described with coordinates (x,y)
    /// </summary>
    public class Coordinates
    {
        public int X { get; set; }
        public int Y { get; set; }

        /// <summary>
        /// Coordinates (x,y) of Whole landing area (area that contains landing platform and surroundings)
        /// </summary>
        /// <param name="x">horizontal axis</param>
        /// <param name="y">vertial axis</param>
        public Coordinates(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";
    }
}
