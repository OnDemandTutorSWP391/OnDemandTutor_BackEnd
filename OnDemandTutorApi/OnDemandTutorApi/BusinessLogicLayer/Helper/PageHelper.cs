namespace OnDemandTutorApi.BusinessLogicLayer.Helper
{
    public static class PageHelper
    {
        public static string Search { get; set; } = null!;
        public static string From { get; set; } = null!;
        public static string To { get; set; } = null!;
        public static string SortBy { get; set; } = null!;
        public static int Page { get; set; } = 1;
    }
}
