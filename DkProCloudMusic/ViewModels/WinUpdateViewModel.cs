using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using DkProCloudMusic.Models;
using GalaSoft.MvvmLight.Threading;
using Hikari.Common;
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
            _scriptDir = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "src", "script");
        }

        public ICommand UpdateCommand => new DelegateCommand<object>(delegate (object obj)
        {
            // 下载
            ThreadPool.QueueUserWorkItem(async state =>
            {
                string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "script.zip");
                //HttpClient client = new HttpClient();

                //await Dk.Common.HttpClientExtensions.GetByteArrayAsync(client, Model.ZipballUrl, filePath);
                Model.DownloadProgress = 50;
                // 解压
                new ZipLibHelper().UnzipZip(filePath, System.AppDomain.CurrentDomain.BaseDirectory);
                // 覆盖
                var o = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "script");
                foreach (DirectoryInfo directoryInfo in new DirectoryInfo(o).GetDirectories())
                {
                    if (directoryInfo.Name.Contains("UnblockNeteaseMusic"))
                    {
                        FileHelper.DeleteFolder(_scriptDir,true);
                        Directory.Move(directoryInfo.FullName, _scriptDir);
                    }
                }

                Model.DownloadProgress = 100;
                // FileHelper.CopyDir2(o, Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "src"));

                MessageBox.Show("完成");
            });


        });
    }
}