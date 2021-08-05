using Landing.Contracts;
using Landing.Enums;
using Landing.Models;
using System;

namespace Landing.Logic
{
    public class LandingControl
    {
        private readonly IAreaService areaService;

        public SquareArea LandingArea { get; set; }
        public SquareArea LandingPlatform { get; set; }
        private SquareArea PreviousLandingPosition { get; set; }

        /// <summary>
        /// A landing control consists on a landing area and a landing platform.
        /// </summary>
        /// <param name="landingAreaSize">landing area size defaulted to 100</param>
        /// <param name="landingPlatformSize"> landing platform size defaulted to 10</param>
        /// <param name="landingAreaTopLeft">landing area top left defaulted to 0</param>
        /// <param name="landingPlatformTopLeft">landing platform top left defaulted to 5</param>
        public LandingControl(IAreaService areaService, int landingAreaSize = 100, int landingAreaTopLeft = 0, int landingPlatformSize = 10, int landingPlatformTopLeft = 5)
        {
            if (landingPlatformSize + landingPlatformTopLeft > landingAreaSize)
            {
                throw new Exception("[ERROR]: Landing platform must be within the landing area");
            }

            this.areaService = areaService ?? throw new ArgumentNullException(nameof(areaService));

            LandingArea = new SquareArea(new Coordinates(landingAreaTopLeft, landingAreaTopLeft), landingAreaSize);
            LandingPlatform = new SquareArea(new Coordinates(landingPlatformTopLeft, landingPlatformTopLeft), landingPlatformSize);
        }

        /// <summary>
        /// Method to control the requests of landing of rockets
        /// </summary>
        /// <param name="rocketLandingPosition">x,y coordinates to check</param>
        /// <returns type=LandingStatus>
        ///     OkForLanding: when there is no problem to land
        ///     OutOfPlatform: request is outside of landing platform
        ///     Clash: the previous rocket check for the same position or the checked position is within the safety area of the previous rocket
        ///</returns>
        public LandingStatuses LandingRequest(Coordinates rocketLandingPosition)
        {
            LandingStatuses response;

            if (PreviousLandingPosition != null && areaService.IsCoordinateInside(PreviousLandingPosition, rocketLandingPosition))
            {
                response = LandingStatuses.Clash;
            }
            else if (areaService.IsCoordinateInside(LandingPlatform, rocketLandingPosition))
            {
                response = LandingStatuses.OkForLanding;
            }
            else
            {
                response = LandingStatuses.OutOfPlatform;
            }

            PreviousLandingPosition = areaService.CreateSafetyArea(rocketLandingPosition);

            return response;
        }

        public LandingStatuses LandingRequest(int x, int y)
        {
            return LandingRequest(new Coordinates(x, y));
        }
    }
}
