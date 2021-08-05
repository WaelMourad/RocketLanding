namespace Landing.Enums
{
    /// <summary>
    /// The result/status of request landing on the platform
    /// </summary>
    public enum LandingStatuses
    {
        None = 0,
        OkForLanding = 1,
        OutOfPlatform = 2,
        Clash = 3
    }
}
