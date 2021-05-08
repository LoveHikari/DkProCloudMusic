using System.Diagnostics;

namespace Dk.Common
{
    public class CommandHelper
    {
        public static void CloseNodeProcess()
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "node")
                {
                    process.Kill();
                    process.Dispose();
                }
            }
        }
        public static string ExecuteCommandLine(string str)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine(str + "&exit");
            process.StandardInput.AutoFlush = true;
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            process.Close();
            return result;
        }
    }
}