using Dk.Common;
using DkProCloudMusic.Models;
using System.Windows.Input;
using DkProCloudMusic.Properties;
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
        /// <summary>
        /// 保存命令
        /// </summary>
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
        /// <summary>
        /// 音源帮助命令
        /// </summary>
        public ICommand SoundSourceHelpCommand => new DelegateCommand<object>(delegate (object obj)
        {
            MessageBox.Show(Resources.SoundSourceHelp, "音源排序帮助");
        });

    }
}