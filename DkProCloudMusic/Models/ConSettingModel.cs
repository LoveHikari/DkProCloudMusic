using System;
using Dk.Common;

namespace DkProCloudMusic.Models
{
    public class ConSettingModel : NotificationObject
    {

        private DKProSet _dkProSet;
        /// <summary>
        /// 配置
        /// </summary>
        public DKProSet DKProSet
        {
            get => _dkProSet;
            set { _dkProSet = value; NotifyPropertyChanged(); }
        }
    }
}