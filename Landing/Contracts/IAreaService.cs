using Landing.Models;

namespace Landing.Contracts
{
    public interface IAreaService
    {        
        bool IsCoordinateInside(SquareArea area, Coordinates coordinates);
        SquareArea CreateSafetyArea(Coordinates coordinates);
    }
}
