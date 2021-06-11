using System.Text.Json.Serialization;

namespace Dk.Common
{
    /// <summary>
    /// github发布版本
    /// </summary>
    public class GitHubReleaseJson
    {
        /// <summary>
        /// zip下载地址
        /// </summary>
        [JsonPropertyName("zipball_url")]
        public string ZipballUrl { get; set; }
        /// <summary>
        /// 更新说明
        /// </summary>
        [JsonPropertyName("body")]
        public string Body { get; set; }

        /// <summary>
        /// 版本号
        /// </summary>
        [JsonPropertyName("tag_name")]
        public string TagName { get; set; }
    }
}