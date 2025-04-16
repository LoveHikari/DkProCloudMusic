using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Dk.Common;
using DkProCloudMusic.Models;

namespace DkProCloudMusic.ViewModels
{
    public class ConStartViewModel
    {
        public ConStartModel Model { get; set; }
        public ConStartViewModel()
        {
            this.Model = new ConStartModel();
            Model.IsRunning = false;
            Model.RunStatus = "脚本未启动";
            Model.StartOrClose = "赶快给我启动这个脚本，我急用";
            Model.NowPort = App.DkProSet.SoftwarePort;
        }

        public ICommand StartScriptCommand
        {
            get
            {
                return new RelayCommand<object>(delegate (object obj)
                {
                    this.Model.IsRunning = !this.Model.IsRunning;
                    if (this.Model.IsRunning)
                    {
                        this.StartScript();
                        return;
                    }
                    this.StopScript();


                });
            }
        }






        /// <summary>
        /// 启动脚本
        /// </summary>
        private void StartScript()
        {
            Model.RunStatus = "脚本运行中";
            Model.StartOrClose = "你们快住手，不要再打了啦";
            new Thread(delegate ()
            {
                string text = AppDomain.CurrentDomain.BaseDirectory + "src\\node\\node.exe ";
                string text2 = "\"" + AppDomain.CurrentDomain.BaseDirectory + "src\\script\\app.js\" ";
                string str = string.Concat(new string[]
                {
                    text,
                    text2,
                    "-a 127.0.0.1 -p ",
                    App.DkProSet.SoftwarePort + ":" + (App.DkProSet.SoftwarePort+1),
                    " -o ",
                    App.DkProSet.ResourcePriority
                });
                CommandHelper.ExecuteCommandLine(str);
            }).Start();
        }
        /// <summary>
        /// 停止脚本
        /// </summary>
        public void StopScript()
        {
            Model.RunStatus = "脚本未启动";
            Model.StartOrClose = "赶快给我启动这个脚本，我急用";
            CommandHelper.CloseNodeProcess();
        }

    }
}