using Dk.Common;
using DkProCloudMusic.Models;
using System.Windows.Input;
using DkProCloudMusic.Properties;
using HandyControl.Controls;
using Hikari.Common;

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
                    ChangeHighQuailty();
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
        /// <summary>
        /// 改变高品质
        /// </summary>
        private void ChangeHighQuailty()
        {
            string text = System.AppDomain.CurrentDomain.BaseDirectory + "src\\script\\package.json";
            string text2 = System.AppDomain.CurrentDomain.BaseDirectory + "src\\script\\src\\provider\\select.js";
            string text3 = System.AppDomain.CurrentDomain.BaseDirectory + "src\\script\\src\\hook.js";
            string text4 = FileHelper.ReadFile(text);
            var t4 = System.Text.Json.JsonDocument.Parse(text4);
            string text5 = FileHelper.ReadFile(text2);
            string text6 = FileHelper.ReadFile(text3);
            string text7 = t4.RootElement.GetProperty("version").GetString();

            if (Model.DKProSet.IsUseHighQualitySupport)
            {
                if (!text4.Contains("-high"))
                {
                    text4 = text4.Replace(text7, text7 + "-high");
                }
                if (!text5.Contains("\nmodule.exports.ENABLE_FLAC = 'true'"))
                {
                    text5 += "\nmodule.exports.ENABLE_FLAC = 'true'";
                }
                text6 = text6.Replace("(item.code != 200 || item.freeTrialInfo)", "(item.code != 200 || item.freeTrialInfo || item.br <= 128000)");
            } else
            {
                text4 = text4.Replace(text7, text7.Replace("-high", ""));
                text5 = text5.Replace("\nmodule.exports.ENABLE_FLAC = 'true'", "");
                text6 = text6.Replace("(item.code != 200 || item.freeTrialInfo || item.br <= 128000)", "(item.code != 200 || item.freeTrialInfo)");
            }
            FileHelper.WriteFile(text, text4);
            FileHelper.WriteFile(text2, text5);
            FileHelper.WriteFile(text3, text6);
        }

    }
}