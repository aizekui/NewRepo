using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using IWshRuntimeLibrary;
using Microsoft.Win32;

namespace WpfApp1
{
    internal class Code
    {
        public static void Admin()
        {
            WshShell shell = new();
            //通过该对象的 CreateShortcut 方法来创建 IWshShortcut 接口的实例对象
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "//刀剑2登录器.lnk");
            //设置快捷方式的目标所在的位置(源程序完整路径)
            shortcut.TargetPath = Environment.CurrentDirectory + @"\刀剑2登录器.exe";
            //应用程序的工作目录
            //当用户没有指定一个具体的目录时，快捷方式的目标应用程序将使用该属性所指定的目录来装载或保存文件。
            shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
            //目标应用程序窗口类型(1.Normal window普通窗口,3.Maximized最大化窗口,7.Minimized最小化)
            shortcut.WindowStyle = 1;
            //快捷方式的描述
            shortcut.Description = "ChinaDforce YanMang";
            //可以自定义快捷方式图标.(如果不设置,则将默认源文件图标.)
            //shortcut.IconLocation = System.Environment.SystemDirectory + "\\" + "shell32.dll, 165";
            //设置应用程序的启动参数(如果应用程序支持的话)
            //shortcut.Arguments = "/myword /d4s";
            //设置快捷键(如果有必要的话.)
            //shortcut.Hotkey = "CTRL+ALT+D";
            //保存快捷方式
            shortcut.Save();
        }
        public static string GetMD5(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("文件异常，请重新解压登录器");
                //MessageBox.Show(string.Format("<{0}> 不存在", path));
                MainWindow form = Init._form;
                form.Close();
            }
            int bufferSize = 1024 * 16;//自定义缓冲区大小16K
            byte[] buffer = new byte[bufferSize];
            Stream inputStream = System.IO.File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
            HashAlgorithm hashAlgorithm = new MD5CryptoServiceProvider();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
            int readLength;//每次读取长度
            var output = new byte[bufferSize];
            while ((readLength = inputStream.Read(buffer, 0, buffer.Length)) > 0)
            {
                //计算MD5
                hashAlgorithm.TransformBlock(buffer, 0, readLength, output, 0);
            }
            //完成最后计算，必须调用(由于上一部循环已经完成所有运算，所以调用此方法时后面的两个参数都为0)
            hashAlgorithm.TransformFinalBlock(buffer, 0, 0);
            string md5 = BitConverter.ToString(value: hashAlgorithm.Hash);
            hashAlgorithm.Clear();
            inputStream.Close();
            md5 = md5.Replace("-", "");
            return md5;
        }
        public static bool IsRegeditItemExist(string main, string min)
        {
            string[] subkeyNames;
            RegistryKey hkml = Registry.LocalMachine;
            RegistryKey software = hkml.OpenSubKey(main);
            subkeyNames = software.GetSubKeyNames();
            //取得该项下所有子项的名称的序列，并传递给预定的数组中 
            foreach (string keyName in subkeyNames)
            //遍历整个数组 
            {
                if (keyName == min)
                //判断子项的名称 
                {
                    hkml.Close();
                    return true;
                }
            }
            hkml.Close();
            return false;
        }
        public static string Qvalue()
        {
            string info;
            RegistryKey Key;
            Key = Registry.LocalMachine;
            RegistryKey myreg = Key.OpenSubKey(@"software\Microsoft\Rarts");
            info = myreg.GetValue("Rarts").ToString();
            myreg.Close();
            return info;
        }
        public static string Xxreg()
        {
            try
            {
                Init.Getmd5 = Rss.GetstrMD5(new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds().ToString()).ToUpper();
                RegistryKey rkey = Registry.LocalMachine;
                rkey.CreateSubKey(@"software\Microsoft\Rarts");
                RegistryKey? softwarea = rkey.OpenSubKey(@"software\Microsoft\Rarts", true); //该项必须已存在
                softwarea.SetValue("Rarts", Init.Getmd5);
                softwarea.Close();
                return Qvalue();
            }
            catch (Exception)
            {
                return "";
            }
            
        }
        public static string DecryptDES(string decryptString, string decryptKey)
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey);
                byte[] rgbIV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
#pragma warning disable SYSLIB0021 // 类型或成员已过时
                DESCryptoServiceProvider DCSP = new();
#pragma warning restore SYSLIB0021 // 类型或成员已过时
                MemoryStream mStream = new();
                CryptoStream cStream = new(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
        public static void Xml_tw()
        {
            string twpath = Backlist.Iinit;
            string xmld5 = GetMD5(twpath);
            if (xmld5 != "8C7B0DE1019AD83BBE904F852F0976B3")
            {
                string xmld6 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<data>
    <lang value=""zh_tw""/>
    <zone value=""zh_tw""/>
</data>";
                try
                {
                    if (!System.IO.File.Exists(twpath))
                    {
                        using FileStream fs = System.IO.File.Create(twpath);
                        Byte[] info = new UTF8Encoding(true).GetBytes(xmld6);
                        fs.Write(info, 0, info.Length);
                    }
                }
                catch
                {
                    MainWindow aa = Init._form;
                    aa.Close();
                }
            }
        }
        public static void Xmls(string ip, string port)
        {
            string content = string.Format(@" <?xml version=""1.0"" encoding=""utf-8""?>
<cfg name=""update"">
    <ip value=""{0}"" />
    <port value=""{1}"" />
    <url value=""/bs2patch/"" />
</cfg>", ip, port);
            string path = Backlist.Xxml;
            try
            {
                using FileStream fs = System.IO.File.Create(path);
                Byte[] info = new UTF8Encoding(true).GetBytes(content);
                fs.Write(info, 0, info.Length);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
    /// <summary>
    /// 加密、解密
    /// </summary>
    public class EncryptUtilSeal
    {
        private static byte[] key = new byte[] { 78, 56, 61, 94, 12, 88, 56, 63, 66, 111, 102, 77, 1, 186, 97, 45 };
        private static byte[] iv = new byte[] { 36, 34, 42, 122, 242, 87, 2, 90, 59, 117, 123, 63, 72, 171, 130, 61 };
        private static IFormatter S_Formatter;

        static EncryptUtilSeal()
        {
            S_Formatter = new BinaryFormatter();//创建一个序列化的对象
        }
        /// <summary>
        /// 采用Rijndael128位加密二进制可序列化对象至文件
        /// </summary>
        /// <param name="para">二进制对象</param>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static bool EncryptObject(object para, string filePath)
        {
            //创建.bat文件 如果之前错在.bat文件则覆盖，无则创建
            using Stream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
#pragma warning disable SYSLIB0022 // 类型或成员已过时
            RijndaelManaged RMCrypto = new();
#pragma warning restore SYSLIB0022 // 类型或成员已过时
            CryptoStream csEncrypt = new(fs, RMCrypto.CreateEncryptor(key, iv), CryptoStreamMode.Write);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            S_Formatter.Serialize(csEncrypt, para);//将数据序列化后给csEncrypt
#pragma warning restore SYSLIB0011 // 类型或成员已过时
            csEncrypt.Close();
            fs.Close();
            return true;
        }
        /// <summary>
        /// 从采用Rijndael128位加密的文件读取二进制对象
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>二进制对象</returns>
        public static object DecryptObject(string filePath)
        {
            //打开.bat文件
            using Stream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            object para;
#pragma warning disable SYSLIB0022 // 类型或成员已过时
            RijndaelManaged RMCrypto = new();
#pragma warning restore SYSLIB0022 // 类型或成员已过时
            CryptoStream csEncrypt = new(fs, RMCrypto.CreateDecryptor(key, iv), CryptoStreamMode.Read);
#pragma warning disable SYSLIB0011 // 类型或成员已过时
            para = S_Formatter.Deserialize(csEncrypt); //将csEncrypt反序列化回原来的数据格式；
#pragma warning restore SYSLIB0011 // 类型或成员已过时
            csEncrypt.Close();
            fs.Close();
            return para;
        }
    }
}
