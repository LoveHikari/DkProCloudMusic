using System;
using System.IO;
using System.Text;

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
            string text = FileHelper.ReadFile(_softwareConfigurationPath);
            text = DESEncrypt.Decrypt(text, "awerfdgg");
            DKProSet dkProSet = System.Text.Json.JsonSerializer.Deserialize<DKProSet>(text);
            return dkProSet;
        }

        //// Token: 0x06000094 RID: 148 RVA: 0x000084B8 File Offset: 0x000066B8
        //public static ScriptJson GetScriptJsonModel()
        //{
        //    ScriptJson scriptJson;
        //    try
        //    {
        //        string text = IOHelper.ReadFromFile(AppDomain.CurrentDomain.BaseDirectory + "\\src\\script\\package.json");
        //        if (text != null && text.Length > 0)
        //        {
        //            scriptJson = JsonConvert.DeserializeObject<ScriptJson>(text);
        //        }
        //        else
        //        {
        //            scriptJson = new ScriptJson();
        //        }
        //        if (!scriptJson.Version.StartsWith("v"))
        //        {
        //            scriptJson.Version = "v" + scriptJson.Version;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        scriptJson = new ScriptJson();
        //        new FrmError(ex).ShowDialog();
        //    }
        //    return scriptJson;
        //}

        /// <summary>
        /// 设置配置文件
        /// </summary>
        /// <param name="proSet">配置</param>
        public static void SetConfiguration(DKProSet proSet)
        {
            string text = System.Text.Json.JsonSerializer.Serialize(proSet);
            text = DESEncrypt.Encrypt(text, "awerfdgg");
            FileHelper.WriteFile(_softwareConfigurationPath, text);
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