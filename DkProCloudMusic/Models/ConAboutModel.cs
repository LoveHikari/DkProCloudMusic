using CommunityToolkit.Mvvm.ComponentModel;

namespace DkProCloudMusic.Models
{
    public class ConAboutModel : ObservableObject
    {
        // 当前软件版本
        private string _nowVersion;

        public string NowVersion
        {
            get => _nowVersion;
            set { _nowVersion = value;  OnPropertyChanged();}
        }
        // 当前脚本版本
        private string _nowScriptVersion;

        public string NowScriptVersion
        {
            get => _nowScriptVersion;
            set { _nowScriptVersion = value; OnPropertyChanged(); }
        }
    }
}