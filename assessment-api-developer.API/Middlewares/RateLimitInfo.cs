namespace assessment_api_developer.API.Middlewares
{
    public class RateLimitInfo
    {
        public DateTime LastRequestTime { get; set; }
        public int RequestCount { get; set; }
    }
}
