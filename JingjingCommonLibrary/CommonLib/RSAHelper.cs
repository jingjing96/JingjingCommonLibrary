using System;
using System.IO;
using System.Security.Cryptography;
using Org.BouncyCastle.Security;

namespace Jingjing.CommonLib
{
    /// <summary>
    /// 对二进制数据进行RSA加密或解密
    /// </summary>
    internal class RSAHelper
    {
        #region 字段

        /// <summary>
        /// RSA对象
        /// </summary>
        private RSACryptoServiceProvider rsa;

        /// <summary>
        /// XML格式的私钥
        /// </summary>
        private string private_key_xml;

        /// <summary>
        /// XML格式的公钥
        /// </summary>
        private string public_key_xml;

        #endregion

        #region 属性

        /// <summary>
        /// XML格式的私钥
        /// </summary>
        public string PrivateKeyXML
        {
            get { return private_key_xml; }
        }

        /// <summary>
        /// XML格式的公钥
        /// </summary>
        public string PublicKeyXML
        {
            get { return public_key_xml; }
        }

        #endregion

        #region 构造方法

        /// <summary>
        /// 用指定的密钥长度初始化RSA对象
        /// </summary>
        /// <param name="keySize">密钥长度</param>
        public RSAHelper(int keySize)
        {
            rsa = new RSACryptoServiceProvider(keySize);
            private_key_xml = rsa.ToXmlString(true);
            public_key_xml= rsa.ToXmlString(false);
        }

        #endregion

        #region 方法

        /// <summary>
        /// 使用公钥加密（解密需使用私钥）
        /// </summary>
        /// <param name="publicKeyXML">XML格式的公钥</param>
        /// <param name="inputData">待加密的数据</param>
        /// <returns>加密后的数据</returns>
        /// <exception cref="Exception"></exception>
        public byte[] RSAEncrypt(string publicKeyXML, byte[] inputData)
        {
            if (!string.IsNullOrEmpty(publicKeyXML))
            {
                if (inputData == null || inputData.Length == 0)
                {
                    throw new Exception("输入的数据不能为空。");
                }
                try
                {
                    rsa.FromXmlString(publicKeyXML);
                    int buffer_size = (rsa.KeySize / 8) - 11;
                    byte[] buffer = new byte[buffer_size];
                    MemoryStream input_stream = new MemoryStream(inputData);
                    MemoryStream output_stream = new MemoryStream();
                    while (true)
                    {
                        int read_size = input_stream.Read(buffer, 0, buffer_size);
                        if (read_size <= 0)
                            break;
                        byte[] temp_data = new byte[read_size];
                        Array.Copy(buffer, 0, temp_data, 0, read_size);
                        byte[] encrypted_bytes = rsa.Encrypt(temp_data, false);
                        output_stream.Write(encrypted_bytes, 0, encrypted_bytes.Length);
                    }
                    byte[] output_data = output_stream.ToArray();
                    return output_data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("公钥不能为空。");
            }
        }

        /// <summary>
        /// 使用私钥加密（解密需使用公钥）（尚未实现）
        /// </summary>
        /// <param name="privateKeyXML">XML格式的私钥</param>
        /// <param name="inputData">待加密的数据</param>
        /// <returns>加密后的数据</returns>
        /// <exception cref="Exception"></exception>
        public byte[] RSAEncrypt2(string privateKeyXML, byte[] inputData)
        {
            if (!string.IsNullOrEmpty(privateKeyXML))
            {
                if (inputData == null || inputData.Length == 0)
                {
                    throw new Exception("输入的数据不能为空。");
                }
                try
                {
                    //尚未实现
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("私钥不能为空。");
            }
        }

        /// <summary>
        /// 使用私钥解密（用于解密使用公钥加密的数据）
        /// </summary>
        /// <param name="privateKeyXML">XML格式的私钥</param>
        /// <param name="encryptedData">已加密的数据</param>
        /// <returns>解密后的数据</returns>
        /// <exception cref="Exception"></exception>
        public byte[] RSADecrypt(string privateKeyXML, byte[] encryptedData)
        {
            if (!string.IsNullOrEmpty(privateKeyXML))
            {
                if (encryptedData == null || encryptedData.Length == 0)
                {
                    throw new Exception("输入的数据不能为空。");
                }
                try
                {
                    rsa.FromXmlString(privateKeyXML);
                    int buffer_size = rsa.KeySize / 8;
                    byte[] buffer = new byte[buffer_size];
                    MemoryStream input_stream = new MemoryStream(encryptedData);
                    MemoryStream output_stream = new MemoryStream();
                    while (true)
                    {
                        int read_size = input_stream.Read(buffer, 0, buffer_size);
                        if (read_size <= 0)
                            break;
                        byte[] temp_data = new byte[read_size];
                        Array.Copy(buffer, 0, temp_data, 0, read_size);
                        byte[] encrypted_bytes = rsa.Decrypt(temp_data, false);
                        output_stream.Write(encrypted_bytes, 0, encrypted_bytes.Length);
                    }
                    byte[] output_data = output_stream.ToArray();
                    return output_data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("私钥不能为空。");
            }
        }

        /// <summary>
        /// 使用公钥解密（用于解密使用私钥加密的数据）（尚未实现）
        /// </summary>
        /// <param name="publicKeyXML">XML格式的公钥</param>
        /// <param name="encryptedData">已加密的数据</param>
        /// <returns>解密后的数据</returns>
        /// <exception cref="Exception"></exception>
        public byte[] RSADecrypt2(string publicKeyXML, byte[] encryptedData)
        {
            if (!string.IsNullOrEmpty(publicKeyXML))
            {
                if (encryptedData == null || encryptedData.Length == 0)
                {
                    throw new Exception("输入的数据不能为空。");
                }
                try
                {
                    //尚未实现
                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new Exception("公钥不能为空。");
            }
        }

        #endregion
    }
}
