using Dk.Common;
using DkProCloudMusic.Models;
using System.Windows.Input;
using HandyControl.Controls;

namespace DkProCloudMusic.ViewModels
{
    public class ConSettingViewModel
    {
        public ConSettingModel Model { get; set; }

        public ConSettingViewModel()
        {
            this.Model = new ConSettingModel();
            this.Model.DKProSet = App.DkProSet;
        }

        public ICommand SaveConfigCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    App.DkProSet = Model.DKProSet;
                    IOHelper.SetConfiguration(App.DkProSet);
                    MessageBox.Show("保存成功！");
                });
            }
        }

    }
}