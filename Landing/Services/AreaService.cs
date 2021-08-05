using Landing.Contracts;
using Landing.Models;
using System;

namespace Landing.Services
{
    public class AreaService : IAreaService
    {
        /// <summary>
        /// Check if a given coordinates belong to the square area
        /// </summary>
        /// <param name="squareArea">square area</param>
        /// <param name="coordinate">x,y coordinates to check</param>
        /// <returns>true if the coordinate belongs to the square area, otherwise false</returns>
        public bool IsCoordinateInside(SquareArea squareArea, Coordinates coordinates)
        {
            _ = squareArea ?? throw new ArgumentNullException(nameof(squareArea));
            _ = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

            Coordinates topLeftCorner = squareArea.TopLeftCorner;
            int squareAreaSize = squareArea.Size;
            int x = coordinates.X;
            int y = coordinates.Y;

            return x >= topLeftCorner.X &&
                   x <= topLeftCorner.X + squareAreaSize - 1 &&
                   y >= topLeftCorner.Y &&
                   y <= topLeftCorner.Y + squareAreaSize - 1;
        }

        /// <summary>
        /// Create safety square area to requested position, including all the nearest coordinates.
        /// </summary>
        /// <param name="coordinates"> x,y coordinates to book the safety square area</param>
        /// <returns>safety square area</returns>
        public SquareArea CreateSafetyArea(Coordinates coordinates)
        {
            _ = coordinates ?? throw new ArgumentNullException(nameof(coordinates));

            var topLeftCorner = new Coordinates(coordinates.X - 1, coordinates.Y - 1);
            return new SquareArea(topLeftCorner, 3);
        }
    }
}
