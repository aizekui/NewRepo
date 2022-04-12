using System.Text;
using System.Security.Cryptography;
using System;
using Org.BouncyCastle.Crypto;
using System.IO;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;

namespace WpfApp1
{
    internal class Rss
    {
        protected static string pripath = @"C:\pythonProject\试试（22）\private.pem";
        public static string DeEncrypt(string data)
        {
            if (string.IsNullOrEmpty(data))
                throw new Exception("字符串不能为空");
            byte[] bytes = Convert.FromBase64String(data);
            AsymmetricCipherKeyPair? keypair;
            AsymmetricKeyParameter? prikey;
            using (var reader = File.OpenText(pripath))
            {
                keypair = new PemReader(reader).ReadObject() as AsymmetricCipherKeyPair;
                prikey = keypair.Private;
            }

            if (prikey == null)
                throw new Exception("私钥读取失败");
            try
            {
                var engine = new Pkcs1Encoding(new RsaEngine());
                engine.Init(false, keypair.Private);
                bytes = engine.ProcessBlock(bytes, 0, bytes.Length);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                throw new Exception("解密失败");
            }
        }
        public static string Encrypt(string data)
        {
            string str = @"-----BEGIN RSA PUBLIC KEY-----
MIGJAoGBAIPtzWm3qhkhFpJ/aF4g3k+fY7vqLrFNlwLmDRmDdbd/h0ejbGtCIrgC
EDmqTLGP77JAqUGGM4jYMLwJPioBV2JO23Iq0BzSEy1rkWZeKRWciG3N821he0wu
jAE2APahBVqdvQNGpMzJPX0XOeM+HX/j2O1umUXRYE/z0weDW+lRAgMBAAE=
-----END RSA PUBLIC KEY-----
";
            TextReader reader = new StringReader(str);
            AsymmetricKeyParameter? publickey;
            publickey = (AsymmetricKeyParameter) new PemReader(reader).ReadObject();
            if (publickey == null)
                throw new Exception("not read");
            try
            {
                var engine = new Pkcs1Encoding(new RsaEngine());
                engine.Init(true, publickey);
                byte[] bytes = Encoding.UTF8.GetBytes(data);
                bytes = engine.ProcessBlock(bytes, 0, bytes.Length);
                return Convert.ToBase64String(bytes);
            }
            catch
            {
                throw new Exception("not rsa");
            }
        }
        public static string GetstrMD5(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.Default.GetBytes(str);
            byte[] md5s = md5.ComputeHash(bytes);
            string md5String = "";
            for (int i = 0; i < md5s.Length; i++)
            {
                md5String += md5s[i].ToString("x2");
            }
            return md5String;
        }
    }
}
