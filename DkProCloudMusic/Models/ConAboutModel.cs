namespace DkProCloudMusic.Models
{
    public class ConAboutModel : NotificationObject
    {
        // 当前软件版本
        private string _nowVersion;

        public string NowVersion
        {
            get => _nowVersion;
            set { _nowVersion = value;  NotifyPropertyChanged();}
        }
        // 当前脚本版本
        private string _nowScriptVersion;

        public string NowScriptVersion
        {
            get => _nowScriptVersion;
            set { _nowScriptVersion = value; NotifyPropertyChanged(); }
        }
    }
}