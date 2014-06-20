//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：MD5加密/解密类。
// 作    者：王义波
// 创建时间：2014/6/14 14:55:37
// CLR 版本：1.5
//=====================================================

using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace CLR.Security
{
    /// <summary>
    /// MD5加密/解密类。
    /// </summary>
    public sealed class MD5Helper : DESUtility
    {
        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns>解密之后的字符串</returns>
        public override string Decrypt(string input)
        {
            return this.Decrypt(input, "wangyibo");
        }
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>加密之后的字符串</returns>
        public override string Encrypt(string input)
        {
            return this.Encrypt(input, "wangyibo");
        }

        /// <summary> 
        /// 加密数据 
        /// </summary> 
        /// <param name="input">需要加密的字符串</param> 
        /// <param name="sKey">加密密钥</param> 
        /// <returns>加密之后的字符串</returns> 
        private string Encrypt(string input, string sKey)
        {
            return this.Encrypt(this.GetDESCryptoServiceProvider(sKey), input);
        }
        /// <summary> 
        /// 解密数据 
        /// </summary> 
        /// <param name="input">需要解密的字符串</param> 
        /// <param name="sKey">解密密钥</param> 
        /// <returns>解密之后的字符串</returns> 
        private string Decrypt(string input, string sKey)
        {
            return this.Decrypt(input,this.GetDESCryptoServiceProvider(sKey));
        }

        private DESCryptoServiceProvider GetDESCryptoServiceProvider(string sKey)
        {
            DESCryptoServiceProvider _desCryptoServiceProvider = new DESCryptoServiceProvider
            {
                Key = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8)),
                IV = ASCIIEncoding.ASCII.GetBytes(FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8))
            };
            return _desCryptoServiceProvider;
        }
    }
}
