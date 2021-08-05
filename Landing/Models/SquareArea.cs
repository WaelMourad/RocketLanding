namespace Landing.Models
{
    /// <summary>
    /// Whole landing area (area that contains landing platform and surroundings)
    /// consists of multiple squares that set a perimeter/dimensions that can be described with coordinates (x,y)
    /// </summary>
    public class SquareArea
    {
        public Coordinates TopLeftCorner { get; set; }
        public int Size { get; set; }

        /// <summary>
        /// Create a square using topLeftCorner and the size
        /// Square Width and height are supposed to be the same size
        /// </summary>
        /// <param name="topLeftCorner" type=Coordinate>x,y coordinates of the top left corner</param>
        /// <param name="size" type=int>vertial and horizontal size of a square</param>
        public SquareArea(Coordinates topLeftCorner, int size)
        {
            TopLeftCorner = topLeftCorner;
            Size = size;           
        }
    }
}
