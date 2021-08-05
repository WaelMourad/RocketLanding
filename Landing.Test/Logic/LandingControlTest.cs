using Landing.Contracts;
using Landing.Enums;
using Landing.Logic;
using Landing.Models;
using Landing.Services;
using NUnit.Framework;
using System;

namespace Landing.Test.Logic
{
    public class LandingControlTest
    {
        private IAreaService areaService;
        private LandingControl landingControl;


        [SetUp]
        public void Setup()
        {
            areaService = new AreaService();
            landingControl = new LandingControl(areaService);
        }

        [Test]
        public void LandingRequest_TopCornerPlatform()
        {
            var coordinates = new Coordinates(10, 10);
            var landingStatus = landingControl.LandingRequest(coordinates);
            Assert.AreEqual(LandingStatuses.OkForLanding, landingStatus);
        }

        [Test]
        public void LandingRequest_DownCornerPlatform()
        {
            var coordinates = new Coordinates(9, 9);
            Assert.AreEqual(LandingStatuses.OkForLanding, landingControl.LandingRequest(coordinates));
        }

        [Test]
        public void LandingRequest_Out_Near_Platform()
        {
            var rocketA_coordinates = new Coordinates(15, 16);
            var rocketA_landingStatus = landingControl.LandingRequest(rocketA_coordinates);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, rocketA_landingStatus);

            var rocketB_coordinates = new Coordinates(16, 15);
            var rocketB_landingStatus = landingControl.LandingRequest(rocketB_coordinates);
            Assert.AreEqual(LandingStatuses.Clash, rocketB_landingStatus);
        }

        [Test]
        public void LandingRequest_SomewhereInArea()
        {
            var rocketC_coordinates = new Coordinates(4, 5);
            var rocketC_landingStatus = landingControl.LandingRequest(rocketC_coordinates);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, rocketC_landingStatus);

            var rocketD_coordinates = new Coordinates(16, 5);
            var rocketD_landingStatus = landingControl.LandingRequest(rocketD_coordinates);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, rocketD_landingStatus);
        }

        [Test]
        public void LandingRequest_PreviouslyChecked()
        {
            var rocketE_coordinates = new Coordinates(9, 8);
            var rocketE_landingStatus = landingControl.LandingRequest(rocketE_coordinates);
            Assert.AreEqual(LandingStatuses.OkForLanding, rocketE_landingStatus);

            var rocketF_coordinates = new Coordinates(9, 8);
            var rocketF_landingStatus = landingControl.LandingRequest(rocketF_coordinates);
            Assert.AreEqual(LandingStatuses.Clash, rocketF_landingStatus);
        }

        [Test]
        public void RequestPreviouslyCheckedCornerCases()
        {
            var rocketA = new Coordinates(1, 1);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, landingControl.LandingRequest(rocketA));

            var rocketB = new Coordinates(1, 1);
            Assert.AreEqual(LandingStatuses.Clash, landingControl.LandingRequest(rocketB));

            var rocketC = new Coordinates(2, 2);
            Assert.AreEqual(LandingStatuses.Clash, landingControl.LandingRequest(rocketC));

            var rocketD = new Coordinates(100, 100);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, landingControl.LandingRequest(rocketD));

            var rocketE = new Coordinates(100, 100);
            Assert.AreEqual(LandingStatuses.Clash, landingControl.LandingRequest(rocketE));

            var rocketF = new Coordinates(100, 99);
            Assert.AreEqual(LandingStatuses.Clash, landingControl.LandingRequest(rocketF));
        }

        [Test]
        public void RequestByXAndY()
        {
            var rocketA = landingControl.LandingRequest(5, 5);
            var rocketB = landingControl.LandingRequest(1, 1);
            var rocketC = landingControl.LandingRequest(1, 1);

            Assert.AreEqual(LandingStatuses.OkForLanding, rocketA);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, rocketB);
            Assert.AreEqual(LandingStatuses.Clash, rocketC);
        }

        [Test]
        public void LandingRequest_OutsideArea()
        {           
            var rocketB_coordinates = new Coordinates(200, 200);
            var rocketB_landingStatus = landingControl.LandingRequest(rocketB_coordinates);
            Assert.AreEqual(LandingStatuses.OutOfPlatform, rocketB_landingStatus);
        }    

        [Test]
        public void LandingRequest_BiggerPlatform()
        {
            var ex = Assert.Throws<Exception>(() => new LandingControl(areaService, 100, 0, 110, 10));

            Assert.AreEqual("[ERROR]: Landing platform must be within the landing area", ex.Message);
        }
    }
}