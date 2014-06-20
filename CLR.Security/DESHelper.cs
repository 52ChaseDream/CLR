//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：DES 加密与解密
// 作    者：王义波
// 创建时间：2014/6/16 10:50:12
// CLR 版本：1.3
//=====================================================

using System.Security.Cryptography;
using System.Text;

namespace CLR.Security
{
    /// <summary>
    /// DES 加密与解密
    /// </summary>
    public sealed class DESHelper : DESUtility
    {
        /// <summary>
        /// DES 解密字符串
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns>解密之后的字符串</returns>
        public override string Decrypt(string input)
        {
            return this.Decrypt(input, "wangyibo");
        }

        ///<summary>
        ///DES 加密字符串
        ///</summary>
        ///<param name="input">需要加密的字符串</param>
        ///<returns>加密之后的字符串</returns>
        public override string Encrypt(string input)
        {
            return this.Encrypt(input, "wangyibo");
        }
        /// <summary>
        /// DES 解密字符串
        /// </summary>
        /// <param name="input">string 待解密的字符串</param>
        /// <param name="key">string 解密密钥，要求为8位</param>
        /// <returns>string 解密成功返回解密后的字符串，失败返回原字符串</returns>
        private string Decrypt(string input, string key)
        {
            return this.Decrypt(this.BaseHelperInstance.ToString(input), this.GetDESCryptoServiceProvider(key));
        }

        /// <summary>
        /// DES 加密字符串
        /// </summary>
        /// <param name="input">string 待加密的字符串</param>
        /// <param name="key">string 加密密钥，要求为8位</param>
        /// <returns>string 加密成功返回加密后的字符串，失败返回原字符串</returns>
        private string Encrypt(string input, string key)
        {
            return this.Encrypt(GetDESCryptoServiceProvider(key), this.BaseHelperInstance.ToString(input));
        }

        private DESCryptoServiceProvider GetDESCryptoServiceProvider(string key)
        {
            string _key = (((key == null) || (key.Length < 8)) ? this._defaultKey : key).Substring(0, 8);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(_key),
                IV = Encoding.ASCII.GetBytes(_key)
            };
            return provider;
        }
    }
}
