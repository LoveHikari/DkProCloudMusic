using Dk.Common;
using DkProCloudMusic.Models;
using HandyControl.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace DkProCloudMusic.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowModel Model { get; set; }
        public MainWindowViewModel()
        {
            DKProSet dkProSet = IOHelper.GetConfiguration()??new DKProSet();
            App.DkProSet = dkProSet;

            this.Model = new MainWindowModel();
            this.Model.ConSettingViewModel = new ConSettingViewModel();
            this.Model.ConStartViewModel = new ConStartViewModel();

        }

        public ICommand LoadedCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    if (App.DkProSet.IsMinimizedAfterRun)  // 启动后最小化
                    {
                        MainWindow win = (MainWindow)obj;
                        win.WindowState = WindowState.Minimized;
                    }
                    if (App.DkProSet.IsExecuteScriptAfterRun)  // 自动启动脚本
                    {
                        this.Model.ConStartViewModel.StartScriptCommand.Execute(null);
                    }
                    if (App.DkProSet.IsUseLiveAndDieTogether && File.Exists(App.DkProSet.LiveAndDieTogetherSoftwarePath)) // 如果打开了同生共死
                    {
                        Process.Start(App.DkProSet.LiveAndDieTogetherSoftwarePath);
                    }
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

        public ICommand StateChangedCommand => new DelegateCommand<object>(delegate (object obj)
        {
            MainWindow win = (MainWindow)obj;
            if (win.WindowState == WindowState.Minimized)
            {
                win.Hide();
            }


        });

        public ICommand ClosingCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    CancelEventArgs args = (CancelEventArgs)obj;
                    var result = HandyControl.Controls.MessageBox.Show("是否确认关闭？", "滑稽提示", MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        Model.ConStartViewModel.StopScript();

                        // this.UnSetProxy();
                        //this.notifyIconMain.Visible = false;
                        if (App.DkProSet.IsUseLiveAndDieTogether)
                        {
                            CommandHelper.ExecuteCommandLine("taskkill /f /t /im cloudmusic.exe");
                        }
                        CommandHelper.ExecuteCommandLine("taskkill /f /t /im DKProCloudMusic.exe");
                        Environment.Exit(0);
                    }
                    else
                    {
                        args.Cancel = true;
                    }

                });
            }
        }

        public ICommand SelectionChangedCommand => new DelegateCommand<object>(delegate (object obj)
        {
            int index = obj.ToInt32();
            if (index == 0)
            {
                Model.ConStartViewModel.Model.NowPort = App.DkProSet.SoftwarePort;
            }


        });



    }
}