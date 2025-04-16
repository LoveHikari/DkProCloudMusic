using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Dk.Common;
using GalaSoft.MvvmLight.Threading;
using Hikari.Common;

namespace DkProCloudMusic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DKProSet DkProSet { get; set; }
        public App()
        {
            Current.DispatcherUnhandledException += App_OnDispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            ThreadPoolHelper.BeginThreadPool();
            DispatcherHelper.Initialize();
        }
        /// <summary>
        /// UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void App_OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            HandyControl.Controls.MessageBox.Show(e.Exception.Message, "UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
            //LogHelper.WriteError(e.Exception, "UI线程全局异常");
            e.Handled = true;
            //throw e.Exception;
            DialogHelper.CloseLoading();
        }

        /// <summary>
        /// 非UI线程抛出全局异常事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                HandyControl.Controls.MessageBox.Show(exception.Message, "非UI线程全局异常", MessageBoxButton.OK, MessageBoxImage.Error);
                //LogHelper.WriteError(exception, "非UI线程全局异常");
                //throw exception;
            }
            DialogHelper.CloseLoading();
        }
    }
}
