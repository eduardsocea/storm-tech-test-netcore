using System.Text.Json.Serialization;

namespace Todo.Models.Gravatar
{
    public class GravatarDto
    {
        [JsonPropertyName("entry")]
        public GravatarEntryDto[] Entry { get; set; }
    }
}
