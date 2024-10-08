using System;
using System.Reflection;
using System.Threading;
using System.Windows.Input;
using Dk.Common;
using DkProCloudMusic.Models;
using DkProCloudMusic.Views;
using GalaSoft.MvvmLight.Threading;
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
        public ICommand SoftwareUpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                string url = "https://api.github.com/repos/LoveHikari/DkProCloudMusic/releases/latest";
                var json = IOHelper.GetSoftwareVersionModel(url);
                var latestVer = json.TagName.ToNumber();
                var nowVer = Model.NowVersion.ToNumber();
                if (latestVer > nowVer)
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        WinUpdate update = new WinUpdate();
                        WinUpdateViewModel winUpdateViewModel = update.DataContext as WinUpdateViewModel;
                        winUpdateViewModel.Model.TagName = json.TagName;
                        winUpdateViewModel.Model.Body = json.Body;
                        winUpdateViewModel.Model.ZipballUrl = json.ZipballUrl;
                        update.ShowDialog();
                    });

                }
                else
                {
                    DispatcherHelper.CheckBeginInvokeOnUI((() =>
                    {
                        MessageBox.Show("当前软件已经是最新版本", "滑稽提示");
                    }));

                }
            });

        });
        /// <summary>
        /// 脚本更新
        /// </summary>
        public ICommand ScriptUpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            ThreadPool.QueueUserWorkItem(state =>
            {
                string url = "https://api.github.com/repos/unblockneteasemusic/server/releases/latest";
                var json = IOHelper.GetSoftwareVersionModel(url);
                var latestVer = json.TagName.ToNumber();
                var nowVer = Model.NowScriptVersion.ToNumber();
                if (latestVer > nowVer)
                {
                    DispatcherHelper.CheckBeginInvokeOnUI(() =>
                    {
                        WinUpdate update = new WinUpdate();
                        WinUpdateViewModel winUpdateViewModel = update.DataContext as WinUpdateViewModel;
                        winUpdateViewModel.Model.TagName = json.TagName;
                        winUpdateViewModel.Model.Body = json.Body;
                        winUpdateViewModel.Model.ZipballUrl = json.ZipballUrl;
                        update.ShowDialog();
                    });
                }
                else
                {
                    DispatcherHelper.CheckBeginInvokeOnUI((() =>
                    {
                        MessageBox.Show("当前脚本已经是最新版本", "滑稽提示");
                    }));
                }
                //this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                //var scriptJson = IOHelper.GetScriptJsonModel();
                //this.Model.NowScriptVersion = scriptJson.Version;
            });

        });


        /// <summary>
        /// 加载完成
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    this.Model.NowVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                    var scriptJson = IOHelper.GetScriptJsonModel();
                    this.Model.NowScriptVersion = scriptJson.Version;
                });
            }
        }
    }
}