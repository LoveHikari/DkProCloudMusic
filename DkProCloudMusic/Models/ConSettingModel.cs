using System;
using CommunityToolkit.Mvvm.ComponentModel;
using Dk.Common;

namespace DkProCloudMusic.Models
{
    public class ConSettingModel : ObservableObject
    {

        private DKProSet _dkProSet;
        /// <summary>
        /// 配置
        /// </summary>
        public DKProSet DKProSet
        {
            get
            {
                CloudMusicPathIsReadOnly = !_dkProSet.IsUseLiveAndDieTogether;
                return _dkProSet;
            }
            set { _dkProSet = value; OnPropertyChanged(); }
        }

        private bool _cloudMusicPathIsReadOnly;  // 输入路径框是否只读

        public bool CloudMusicPathIsReadOnly
        {
            get => _cloudMusicPathIsReadOnly;
            set { _cloudMusicPathIsReadOnly = value; OnPropertyChanged(); }
        }
    }
}