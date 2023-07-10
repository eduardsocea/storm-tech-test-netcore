using System.Text.Json.Serialization;

namespace Todo.Models.Gravatar
{
    public class GravatarEntryDto
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("preferredUsername")]
        public string PreferredUsername { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

    }
}
