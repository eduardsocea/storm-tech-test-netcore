namespace Todo.Configurations
{
    public class GravatarOptions
    {
        public const string Section = "Gravatar";

        public string ProfileUri { get; set; }

        /// <summary>
        /// Seeding retry time in minutes;
        /// </summary>
        public int SeedingRetryTime { get; set; } = 60;

        /// <summary>
        /// Request timeout in seconds
        /// </summary>
        public int RequestTimeout { get; set; } = 5;
    }
}
