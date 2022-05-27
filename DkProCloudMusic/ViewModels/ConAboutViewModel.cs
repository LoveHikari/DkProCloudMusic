using System;
using System.Reflection;
using System.Windows.Input;
using Dk.Common;
using DkProCloudMusic.Models;
using DkProCloudMusic.Views;
using HandyControl.Controls;
using Hikari.Common;

namespace DkProCloudMusic.ViewModels
{
    public class ConAboutViewModel
    {
        public ConAboutModel Model { get; set; }

        public ConAboutViewModel()
        {
            this.Model = new ConAboutModel();
        }

        /// <summary>
        /// 软件更新
        /// </summary>
        public ICommand SoftwareUpdateCommand => new DelegateCommand<object>(async delegate (object obj)
        {
            string url = "https://api.github.com/repos/LoveHikari/DkProCloudMusic/releases/latest";
            var json = await IOHelper.GetSoftwareVersionModel(url);
            var latestVer = json.TagName.ToNumber();
            var nowVer = Model.NowVersion.ToNumber();
            if (latestVer > nowVer)
            {
                WinUpdate update = new WinUpdate();
                WinUpdateViewModel winUpdateViewModel = update.DataContext as WinUpdateViewModel;
                winUpdateViewModel.Model.TagName = json.TagName;
                winUpdateViewModel.Model.Body = json.Body;
                winUpdateViewModel.Model.ZipballUrl = json.ZipballUrl;
                update.ShowDialog();
            }
            else
            {
                MessageBox.Show("当前软件已经是最新版本", "滑稽提示");
            }
        });
        /// <summary>
        /// 脚本更新
        /// </summary>
        public ICommand ScriptUpdateCommand => new DelegateCommand<object>(async delegate (object obj)
        {
            string url = "https://api.github.com/repos/nondanee/UnblockNeteaseMusic/releases/latest";
            var json = await IOHelper.GetSoftwareVersionModel(url);
            var latestVer = json.TagName.ToNumber();
            var nowVer = Model.NowScriptVersion.ToNumber();
            if (latestVer > nowVer)
            {
                WinUpdate update = new WinUpdate();
                WinUpdateViewModel winUpdateViewModel = update.DataContext as WinUpdateViewModel;
                winUpdateViewModel.Model.TagName = json.TagName;
                winUpdateViewModel.Model.Body = json.Body;
                winUpdateViewModel.Model.ZipballUrl = json.ZipballUrl;
                update.ShowDialog();
            }
            else
            {
                MessageBox.Show( "当前脚本已经是最新版本", "滑稽提示");
            }
            //this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            //var scriptJson = IOHelper.GetScriptJsonModel();
            //this.Model.NowScriptVersion = scriptJson.Version;
        });


        /// <summary>
        /// 加载完成
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return new DelegateCommand<object>(async delegate  (object obj)
                {
                    this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    var scriptJson = await IOHelper.GetScriptJsonModel();
                    this.Model.NowScriptVersion = scriptJson.Version;
                });
            }
        }
    }
}