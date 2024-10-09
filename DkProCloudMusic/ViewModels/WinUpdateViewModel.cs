﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DkProCloudMusic.Models;
using GalaSoft.MvvmLight.Threading;
using Hikari.Common;
using Hikari.Common.IO;
using Hikari.Common.Net.Http;

namespace DkProCloudMusic.ViewModels
{
    public class WinUpdateViewModel
    {
        public WinUpdateModel Model { get; set; }
        private readonly string _scriptDir;
        public WinUpdateViewModel()
        {
            this.Model = new WinUpdateModel();
            this.Model.UpdateBtnText = "立即更新";
            _scriptDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "src", "script");
        }
        /// <summary>
        /// 更新
        /// </summary>
        public ICommand UpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            this.Model.UpdateBtnState = false;
            // 下载
            ThreadPool.QueueUserWorkItem(async state =>
            {

                string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "script.zip");
                //HttpClient client = new HttpClient();
                //this.Model.UpdateBtnText = "正在下载...";
                //await client.GetByteArrayAsync(Model.ZipballUrl, filePath, new Progress<HttpDownloadProgress>((
                //    downloadProgress =>
                //    {
                //        Model.DownloadProgress = (double)downloadProgress.BytesReceived * 90 / downloadProgress.TotalBytesToReceive??0;
                //    })), CancellationToken.None);
                this.Model.UpdateBtnText = "正在解压...";
                // 解压
                new ZipLibHelper().UnzipZip(filePath, System.AppDomain.CurrentDomain.BaseDirectory);
                File.Delete(filePath);
                // 覆盖
                var o = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "script");
                foreach (DirectoryInfo directoryInfo in new DirectoryInfo(o).GetDirectories())
                {
                    if (directoryInfo.Name.Contains("UnblockNeteaseMusic"))
                    {
                        DirectoryHelper.DeleteDirectory(_scriptDir, true);
                        Directory.Move(directoryInfo.FullName, _scriptDir);
                    }
                }
                DirectoryHelper.DeleteDirectory(o, true);
                Model.DownloadProgress = 100;

                this.Model.UpdateBtnText = "更新完成";
                this.Model.UpdateBtnState = true;
                var dr = MessageBox.Show("完成");
                if (dr == MessageBoxResult.OK)
                {
                    if (obj is Window window)
                    {
                        window.Close(); // 关闭传递进来的窗口
                    }
                }
            });


        });
        /// <summary>
        /// 不更新
        /// </summary>
        public ICommand NotUpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            if (obj is Window window)
            {
                window.Close(); // 关闭传递进来的窗口
            }
        });
    }
}