//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：TripleDES加密/解密
// 作    者：王义波
// 创建时间：2014/6/14 15:45:40
// CLR 版本：1.6
//=====================================================

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CLR.Security
{
    /// <summary>
    /// TripleDES加密/解密
    /// </summary>
    public sealed class TripleDESHelper : SecurityUtility
    {
        /// <summary>
        /// TripleDES解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns>解密之后的字符串</returns>
        protected override string Decrypt(string input)
        {
            return this.TripleDESDecrypting(input);
        }

        /// <summary>
        /// TripleDES加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>加密之后的字符串</returns>
        protected override string Encrypt(string input)
        {
            return this.TripleDESEncrypting(input);
        }

        #region TripleDES加密
        /// <summary>
        /// TripleDES加密
        /// </summary>
        private string TripleDESEncrypting(string strSource)
        {
            try
            {
                byte[] _byte = Encoding.Default.GetBytes(strSource);
                MemoryStream _memoryStream = new MemoryStream();
                CryptoStream _cryptoStream = new CryptoStream(_memoryStream, this.TripleDESCryptoServiceProviderInstance.CreateEncryptor(), CryptoStreamMode.Write);
                _cryptoStream.Write(_byte, 0, _byte.Length);
                _cryptoStream.FlushFinalBlock();
                return System.Convert.ToBase64String(_memoryStream.ToArray());
            }
            catch (Exception ex)
            {
                throw new Exception("加密时候出现错误!错误提示:\n" + ex.Message);
            }
        }
        #endregion

        #region TripleDES解密
        /// <summary>
        /// TripleDES解密
        /// </summary>
        private string TripleDESDecrypting(string Source)
        {
            try
            {
                byte[] _byte = Convert.FromBase64String(Source);
                CryptoStream cs = new CryptoStream(new MemoryStream(_byte, 0, _byte.Length), this.TripleDESCryptoServiceProviderInstance.CreateDecryptor(), CryptoStreamMode.Read);
                return new StreamReader(cs, Encoding.Default).ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("解密时候出现错误!错误提示:\n" + ex.Message);
            }
        }

        private TripleDESCryptoServiceProvider TripleDESCryptoServiceProviderInstance
        {
            get
            {
                byte[] key = { 42, 16, 93, 156, 78, 4, 218, 32, 15, 167, 44, 80, 26, 20, 155, 112, 2, 94, 11, 204, 119, 35, 184, 197 }; //定义密钥
                byte[] IV = { 55, 103, 246, 79, 36, 99, 167, 3 };  //定义偏移量
                TripleDESCryptoServiceProvider _tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider
                {
                    Key = key,
                    IV = IV
                };
                return _tripleDESCryptoServiceProvider;
            }
        }
        #endregion
    }
}
