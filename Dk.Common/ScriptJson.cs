using System.Text.Json.Serialization;

namespace Dk.Common
{
    public class ScriptJson
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = "@nondanee/unblockneteasemusic";
        [JsonPropertyName("version")]
        public string Version { get; set; } = "v0.24.1";
        [JsonPropertyName("description")]
        public string Description { get; set; } = "Revive unavailable songs for Netease Cloud Music";
        [JsonPropertyName("author")]
        public string Author { get; set; } = "nondanee";
        [JsonPropertyName("license")]
        public string License { get; set; } = "MIT";
    }
}