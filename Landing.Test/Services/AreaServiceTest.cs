using Landing.Contracts;
using Landing.Models;
using Landing.Services;
using NUnit.Framework;

namespace Landing.Test.Services
{
    public class AreaServiceTest
    {
        private IAreaService areaService;

        [SetUp]
        public void Setup()
        {
            areaService = new AreaService();
        }

        [Test]
        public void IsCoordinateInsideTest_True()
        {
            var landingRequest = new Coordinates(5, 5);
            SquareArea squareArea = areaService.CreateSafetyArea(landingRequest);
            var isCoordinateInside = areaService.IsCoordinateInside(squareArea, new Coordinates(4, 4));
            Assert.IsTrue(isCoordinateInside);
        }

        [Test]
        public void IsCoordinateInsideTest_False()
        {
            var landingRequest = new Coordinates(5, 5);
            SquareArea squareArea = areaService.CreateSafetyArea(landingRequest);
            var isCoordinateInside = areaService.IsCoordinateInside(squareArea, new Coordinates(11, 11));
            Assert.IsFalse(isCoordinateInside);
        }

        [Test]
        public void CreateSafetyAreaTest()
        {
            var landingRequest = new Coordinates(5, 5);
            SquareArea squareArea = areaService.CreateSafetyArea(landingRequest);
            Assert.AreEqual(squareArea.Size, 3);
            Assert.AreEqual(squareArea.TopLeftCorner.X, 4);
            Assert.AreEqual(squareArea.TopLeftCorner.Y, 4);
        }

       
    }
}