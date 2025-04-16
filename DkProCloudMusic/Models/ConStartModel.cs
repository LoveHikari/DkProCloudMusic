using CommunityToolkit.Mvvm.ComponentModel;
using Dk.Common;

namespace DkProCloudMusic.Models
{
    public class ConStartModel : ObservableObject
    {
        private int _nowPort;
        /// <summary>
        /// 当前软件端口
        /// </summary>
        public int NowPort
        {
            get => _nowPort;
            set { _nowPort = value; OnPropertyChanged(); }
        }
        private bool _isRunning;
        /// <summary>
        /// 脚本是否正在运行
        /// </summary>
        public bool IsRunning
        {
            get => _isRunning;
            set { _isRunning = value; OnPropertyChanged(); }
        }
        private string _runStatus;
        /// <summary>
        /// 运行状态
        /// </summary>
        public string RunStatus
        {
            get => _runStatus;
            set { _runStatus = value; OnPropertyChanged(); }
        }
        private string _startOrClose;
        /// <summary>
        /// 运行按钮文本
        /// </summary>
        public string StartOrClose
        {
            get => _startOrClose;
            set { _startOrClose = value; OnPropertyChanged(); }
        }
    }
}