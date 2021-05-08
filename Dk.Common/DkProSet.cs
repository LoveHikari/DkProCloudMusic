namespace Dk.Common
{
    public class DKProSet
    {
        /// <summary>
        /// 跟随系统启动
        /// </summary>
        public bool IsRunWithSystemStart { get; set; }

        /// <summary>
        /// 启动后最小化
        /// </summary>
        public bool IsMinimizedAfterRun { get; set; }

        /// <summary>
        /// 是否自动执行脚本
        /// </summary>
        public bool IsExecuteScriptAfterRun { get; set; }

        /// <summary>
        /// UWP支持
        /// </summary>
        public bool IsUseUWPSupport { get; set; }

        /// <summary>
        /// 高音质支持
        /// </summary>
        public bool IsUseHighQualitySupport { get; set; }

        /// <summary>
        /// 同生共死
        /// </summary>
        public bool IsUseLiveAndDieTogether { get; set; }

        /// <summary>
        /// 同生共死软件路径
        /// </summary>
        public string LiveAndDieTogetherSoftwarePath { get; set; } = string.Empty;

        /// <summary>
        /// 是否使用VIP支持
        /// </summary>
        public bool IsUseVIPSupport { get; set; }

        public string VIPSupportCookie { get; set; } = string.Empty;
        /// <summary>
        /// 音源优先级
        /// </summary>
        public string ResourcePriority { get; set; } = "kuwo kugou qq migu xiami baidu joox youtube";

        /// <summary>
        /// 程序端口
        /// </summary>
        public int SoftwarePort { get; set; } = 8125;


        public string AuthorizationCode { get; set; } = string.Empty;
    }
}