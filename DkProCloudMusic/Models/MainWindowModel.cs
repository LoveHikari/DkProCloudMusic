using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using Dk.Common;
using DkProCloudMusic.ViewModels;

namespace DkProCloudMusic.Models
{
    public class MainWindowModel : ObservableObject
    {
        
        


        private ConStartViewModel _conStartViewModel;
        public ConStartViewModel ConStartViewModel
        {
            get => _conStartViewModel;
            set { _conStartViewModel = value; OnPropertyChanged(); }
        }
        private ConSettingViewModel _conSettingViewModel;
        public ConSettingViewModel ConSettingViewModel
        {
            get => _conSettingViewModel;
            set { _conSettingViewModel = value; OnPropertyChanged(); }
        }
        
    }
}