namespace DkProCloudMusic.Models
{
    public class WinUpdateModel : NotificationObject
    {
        private string _zipballUrl;
        /// <summary>
        /// zip下载地址
        /// </summary>
        public string ZipballUrl
        {
            get => _zipballUrl;
            set
            {
                _zipballUrl = value; NotifyPropertyChanged();
            }
        }

        private string _body;
        /// <summary>
        /// 更新说明
        /// </summary>
        public string Body
        {
            get => _body;
            set
            {
                _body = value; NotifyPropertyChanged();
            }
        }

        private string _tagName;
        /// <summary>
        /// 版本号
        /// </summary>
        public string TagName
        {
            get => _tagName;
            set
            {
                _tagName = value; NotifyPropertyChanged();
            }
        }

        public double _downloadProgress;
        /// <summary>
        /// 下载进度
        /// </summary>
        public double DownloadProgress
        {
            get => _downloadProgress;
            set
            {
                _downloadProgress = value; NotifyPropertyChanged();
            }
        }
    }
}