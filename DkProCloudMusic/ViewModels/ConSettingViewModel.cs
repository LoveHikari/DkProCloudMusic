using System.Windows;
using System.Windows.Input;
using AduSkin.Controls.Metro;
using Dk.Common;
using DkProCloudMusic.Models;

namespace DkProCloudMusic.ViewModels
{
    public class ConSettingViewModel
    {
        public ConSettingModel Model { get; set; }

        public ConSettingViewModel()
        {
            this.Model = new ConSettingModel();
        }

        
    }
}