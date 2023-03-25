using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);//获取本地计算机受信任的根证书的储存区
            store.Open(OpenFlags.MaxAllowed); //查找证书前，一定要打开
            X509Certificate2Collection collection = store.Certificates;//获取储存区上的所有证书
            string thumbprint = "7d3ea9d87ea2f2600fcce237aa8989b8805549dc";
            //按指纹查找，系统是否内置了这个根证书
            X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByThumbprint, thumbprint, false);
            if (fcollection.Count == 0)
            {
                string crtFullName = @"D:\project\git\DkProCloudMusic\DkProCloudMusic\bin\Debug\net7.0-windows\src\script\ca.crt";//Path.Combine(Application.StartupPath, "DigiCertGlobalRootCA.crt");
                if (File.Exists(crtFullName))
                {
                    X509Certificate2 x509 = new X509Certificate2(crtFullName);
                    //安装证书,证书（本地计算机）-受信任的根证书的储存区
                    store.Add(x509);
                }
            }
            store.Close();
            Assert.True(true);
        }
    }
}