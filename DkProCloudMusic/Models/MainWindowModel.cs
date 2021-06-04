using System.Windows.Controls;
using Dk.Common;
using DkProCloudMusic.ViewModels;

namespace DkProCloudMusic.Models
{
    public class MainWindowModel : NotificationObject
    {
        
        


        private ConStartViewModel _conStartViewModel;
        public ConStartViewModel ConStartViewModel
        {
            get => _conStartViewModel;
            set { _conStartViewModel = value; NotifyPropertyChanged(); }
        }
        private ConSettingViewModel _conSettingViewModel;
        public ConSettingViewModel ConSettingViewModel
        {
            get => _conSettingViewModel;
            set { _conSettingViewModel = value; NotifyPropertyChanged(); }
        }
        
    }
}