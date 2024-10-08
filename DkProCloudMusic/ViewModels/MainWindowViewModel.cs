using Dk.Common;
using DkProCloudMusic.Models;
using HandyControl.Controls;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using HandyControl.Data;
using Hikari.Common;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace DkProCloudMusic.ViewModels
{
    public class MainWindowViewModel
    {
        public MainWindowModel Model { get; set; }
        public MainWindowViewModel()
        {
            DKProSet dkProSet = IOHelper.GetConfiguration() ?? new DKProSet();
            App.DkProSet = dkProSet;

            this.Model = new MainWindowModel();
            this.Model.ConSettingViewModel = new ConSettingViewModel();
            this.Model.ConStartViewModel = new ConStartViewModel();

        }
        /// <summary>
        /// 窗口启动时
        /// </summary>
        public ICommand LoadedCommand
        {
            get
            {
                return new DelegateCommand<object>(delegate (object obj)
                {
                    // 判断证书是否安装，如果没有，则安装
                    new Thread(() =>
                    {
                        X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);//获取本地计算机受信任的根证书的储存区
                        store.Open(OpenFlags.MaxAllowed); //查找证书前，一定要打开
                        X509Certificate2Collection collection = store.Certificates;//获取储存区上的所有证书
                        string thumbprint = "7d3ea9d87ea2f2600fcce237aa8989b8805549dc";
                        //按指纹查找，系统是否内置了这个根证书
                        X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByThumbprint, thumbprint, false);
                        if (fcollection.Count == 0)
                        {
                            string crtFullName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src\\script\\ca.crt");
                            if (File.Exists(crtFullName))
                            {
                                X509Certificate2 x509 = new X509Certificate2(crtFullName);
                                //安装证书,证书（本地计算机）-受信任的根证书的储存区
                                store.Add(x509);
                            }
                        }
                        store.Close();
                    }).Start();

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
        /// <summary>
        /// 窗口状态改变
        /// </summary>
        public ICommand StateChangedCommand => new DelegateCommand<object>(delegate (object obj)
        {
            MainWindow win = (MainWindow)obj;

            if (win.WindowState == WindowState.Minimized)  // 最小化时
            {
                win.Hide();
                NotifyIcon icon = (NotifyIcon)win.FindName("notifyIconContextContent");
                icon.ShowBalloonTip("DKPro通知", "后台运行中！", NotifyIconInfoType.Info);
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
            int index = obj.ToInt32(0);
            if (index == 0)
            {
                Model.ConStartViewModel.Model.NowPort = App.DkProSet.SoftwarePort;
            }


        });
    }
}