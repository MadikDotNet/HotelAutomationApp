namespace HotelAutomationApp.Application.Auth.Constants
{
    public static class Roles
    {
        public const string User = "User";
        public const string Admin = "Admin";
        public const string Root = "Root";

        public static IList<string> All => new List<string>()
        {
            User,
            Admin,
            Root
        };
    }
}