using System;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using AduSkin.Controls.Metro;
using Dk.Common;
using DkProCloudMusic.Models;
using DkProCloudMusic.Views;

namespace DkProCloudMusic.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowModel Model { get; set; }
        public MainWindowViewModel()
        {
            this.Model = new MainWindowModel();

        }

        public ICommand LoadedCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    DKProSet dkProSet = IOHelper.GetConfiguration();
                    Model.DKProSet = dkProSet;
                    Model.NowPort = Model.DKProSet.SoftwarePort;
                    //this.GetConfigurationAndChangeUi();
                    //if (this._dkProSet.IsMinimizedAfterRun)
                    //{
                    //    base.WindowState = FormWindowState.Minimized;
                    //}
                    //if (this._dkProSet.IsExecuteScriptAfterRun)
                    //{
                    //    this.BtnStartOrClose_Click(null, null);
                    //}
                    //if (this._dkProSet.IsUseLiveAndDieTogether && File.Exists(this._dkProSet.LiveAndDieTogetherSoftwarePath))
                    //{
                    //    Process.Start(this._dkProSet.LiveAndDieTogetherSoftwarePath);
                    //}
                    //this.BindControl();
                    //this.IsLoading = false;


                });
            }
        }

        public ICommand StartScriptCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
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
        public ICommand SaveConfigCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    Model.NowPort = Model.DKProSet.SoftwarePort;
                    IOHelper.SetConfiguration(Model.DKProSet);
                    AduMessageBox.Show("保存成功！");
                });
            }
        }
        public ICommand ClosedCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    this.StopScript();

                    // this.UnSetProxy();
                    //this.notifyIconMain.Visible = false;
                    if (this.Model.DKProSet.IsUseLiveAndDieTogether)
                    {
                        CommandHelper.ExecuteCommandLine("taskkill /f /t /im cloudmusic.exe");
                    }
                    CommandHelper.ExecuteCommandLine("taskkill /f /t /im DKProCloudMusic.exe");
                    Environment.Exit(0);


                });
            }
        }

        private void StartScript()
        {
            Model.RunStatus = "脚本运行中";
            Model.StartOrClose = "你们快住手，不要再打了啦";
            new Thread(delegate ()
            {
                string text = "\"" + AppDomain.CurrentDomain.BaseDirectory + "src\\node\\node.exe\" ";
                string text2 = "\"" + AppDomain.CurrentDomain.BaseDirectory + "src\\script\\app.js\" ";
                string str = string.Concat(new string[]
                {
                    text,
                    text2,
                    " -a 127.0.0.1 -p ",
                    this.Model.DKProSet.SoftwarePort.ToString(),
                    " -o ",
                    this.Model.DKProSet.ResourcePriority
                });
                CommandHelper.ExecuteCommandLine(str);
            }).Start();
        }

        private void StopScript()
        {
            Model.RunStatus = "脚本未启动";
            Model.StartOrClose = "赶快给我启动这个脚本，我急用";
            CommandHelper.CloseNodeProcess();
        }
        

    }
}