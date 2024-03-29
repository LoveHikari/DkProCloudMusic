﻿using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Hikari.Common;
using Hikari.Common.IO;
using Hikari.Common.Net.Http;

namespace Dk.Common
{
    public class IOHelper
    {
        private static readonly string _softwareConfigurationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DKProCloudMusic.config");

        /// <summary>
        /// 获得配置
        /// </summary>
        /// <returns></returns>
        public static DKProSet GetConfiguration()
        {
            try
            {
                string text = FileHelper.Read(_softwareConfigurationPath);
                text = DESEncrypt.Decrypt(text, "awerfdgg");
                DKProSet dkProSet = System.Text.Json.JsonSerializer.Deserialize<DKProSet>(text);
                return dkProSet;
            }
            catch (Exception ex)
            {
                return null;
            }
            
        }

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="proSet">配置</param>
        public static void SetConfiguration(DKProSet proSet)
        {
            string text = System.Text.Json.JsonSerializer.Serialize(proSet);
            text = DESEncrypt.Encrypt(text, "awerfdgg");
            FileHelper.Write(_softwareConfigurationPath, text);
        }

        /// <summary>
        /// 获得脚本版本
        /// </summary>
        /// <returns></returns>
        public static ScriptJson GetScriptJsonModel()
        {
            var dir = AppDomain.CurrentDomain.BaseDirectory + "\\src\\script";
            if (!Directory.Exists(dir))  // 如果脚本不存在，则获取最新脚本
            {
                DownloadScript();
            }

            ScriptJson scriptJson;
            string text = FileHelper.Read(AppDomain.CurrentDomain.BaseDirectory + "\\src\\script\\package.json");
            if (text != null && text.Length > 0)
            {
                scriptJson = System.Text.Json.JsonSerializer.Deserialize<ScriptJson>(text);
            }
            else
            {
                scriptJson = new ScriptJson();
            }
            if (!scriptJson.Version.StartsWith("v"))
            {
                scriptJson.Version = "v" + scriptJson.Version;
            }
            return scriptJson;
        }
        /// <summary>
        /// 获得软件最新版本
        /// </summary>
        /// <returns></returns>
        public static GitHubReleaseJson GetSoftwareVersionModel(string url)
        {
            HttpClientHelper httpClient = new HttpClientHelper();
            var json = httpClient.Get(url);
            return System.Text.Json.JsonSerializer.Deserialize<GitHubReleaseJson>(json);

        }

        /// <summary>
        ///  下载脚本
        /// </summary>
        public static void DownloadScript()
        {
            string url = "https://api.github.com/repos/nondanee/UnblockNeteaseMusic/releases/latest";
            var json = IOHelper.GetSoftwareVersionModel(url);
            var zipballUrl = json.ZipballUrl;

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "script.zip");
            Download(zipballUrl, filePath);
            // 解压
            new ZipLibHelper().UnzipZip(filePath, System.AppDomain.CurrentDomain.BaseDirectory);
            // 覆盖
            var o = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "script");
            var nondanee = DirectoryHelper.FindDirectories(o)[0];

            DirectoryHelper.RenameDirectory(nondanee, Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "src", "script"));
            DirectoryHelper.DeleteDirectory(o, true);
            File.Delete(filePath);
        }


        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="requestUri">请求地址</param>
        /// <param name="path">保持文件路径带文件名</param>
        /// <returns></returns>
        private static void Download(string requestUri, string path)
        {
            using HttpClient client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, new Uri(requestUri));
            request.Headers.Add("user-agent", "Anything");
            using var responseMessage = client.Send(request);
            var content = responseMessage.Content;

            using var responseStream = content.ReadAsStream();

            using var fileStream = new FileInfo(path).Create();
            responseStream.CopyTo(fileStream);

        }
        //// Token: 0x06000096 RID: 150 RVA: 0x00008598 File Offset: 0x00006798
        //public static void WriteToFile(string filePath, string content)
        //{
        //    File.WriteAllText(filePath, content, Encoding.UTF8);
        //}

        //// Token: 0x06000097 RID: 151 RVA: 0x000085A8 File Offset: 0x000067A8
        //public static bool DeleteDirectory(string dirPath)
        //{
        //    if (Directory.Exists(dirPath))
        //    {
        //        DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
        //        FileInfo[] files = directoryInfo.GetFiles();
        //        for (int i = 0; i < files.Length; i++)
        //        {
        //            File.Delete(files[i].FullName);
        //        }
        //        DirectoryInfo[] directories = directoryInfo.GetDirectories();
        //        for (int i = 0; i < directories.Length; i++)
        //        {
        //            IOHelper.DeleteDirectory(directories[i].FullName);
        //        }
        //        Directory.Delete(dirPath);
        //        return true;
        //    }
        //    return true;
        //}
    }
}