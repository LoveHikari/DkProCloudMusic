using System.Security.Cryptography.X509Certificates;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.CurrentUser);//��ȡ���ؼ���������εĸ�֤��Ĵ�����
            store.Open(OpenFlags.MaxAllowed); //����֤��ǰ��һ��Ҫ��
            X509Certificate2Collection collection = store.Certificates;//��ȡ�������ϵ�����֤��
            string thumbprint = "7d3ea9d87ea2f2600fcce237aa8989b8805549dc";
            //��ָ�Ʋ��ң�ϵͳ�Ƿ������������֤��
            X509Certificate2Collection fcollection = collection.Find(X509FindType.FindByThumbprint, thumbprint, false);
            if (fcollection.Count == 0)
            {
                string crtFullName = @"D:\project\git\DkProCloudMusic\DkProCloudMusic\bin\Debug\net7.0-windows\src\script\ca.crt";//Path.Combine(Application.StartupPath, "DigiCertGlobalRootCA.crt");
                if (File.Exists(crtFullName))
                {
                    X509Certificate2 x509 = new X509Certificate2(crtFullName);
                    //��װ֤��,֤�飨���ؼ������-�����εĸ�֤��Ĵ�����
                    store.Add(x509);
                }
            }
            store.Close();
            Assert.True(true);
        }
    }
}