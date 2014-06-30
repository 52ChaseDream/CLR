//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：RSA加密解密及RSA签名和验证
// 作    者：王义波
// 创建时间：2014/6/14 11:24:33
// CLR 版本：1.5
//=====================================================

using System;
using System.Security.Cryptography;
using System.Text;

namespace CLR.Security
{
    /// <summary>
    /// RSA加密解密及RSA签名和验证
    /// </summary>
    public sealed class RSACryptionHelper : SecurityUtility
    {
        private string _privateKey = "", _publicKey = "";
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RSACryptionHelper()
        {
            this.RSAKey(out _privateKey, out _publicKey);
        }
        /// <summary>
        /// RSA的解密函数
        /// </summary>
        /// <param name="_input">需要解密的字符串</param>
        /// <returns>RSA的解密之后的字符串</returns>
        protected override string Decrypt(string _input)
        {
            return this.RSADecrypt(this._privateKey, _input);
        }
        /// <summary>
        /// RSA的加密函数
        /// </summary>
        /// <param name="_input">需要加密的字符串</param>
        /// <returns>RSA的加密之后的字符串</returns>
        protected override string Encrypt(string _input)
        {
            return this.RSAEncrypt(this._publicKey, _input);
        }

        /// <summary>
        /// RSA 的密钥产生 产生私钥 和公钥 
        /// </summary>
        /// <param name="xmlPrivateKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        private void RSAKey(out string xmlPrivateKeys, out string xmlPublicKey)
        {
            RSACryptoServiceProvider _rSACryptoServiceProvider = new RSACryptoServiceProvider();
            xmlPrivateKeys = _rSACryptoServiceProvider.ToXmlString(true);
            xmlPublicKey = _rSACryptoServiceProvider.ToXmlString(false);
        }

        //RSA的加密函数  string
        private string RSAEncrypt(string xmlPublicKey, string encryptString)
        {
            RSACryptoServiceProvider _rSACryptoServiceProvider = new RSACryptoServiceProvider();
            _rSACryptoServiceProvider.FromXmlString(xmlPublicKey);
            return Convert.ToBase64String(_rSACryptoServiceProvider.Encrypt((new UnicodeEncoding()).GetBytes(encryptString), false));
        }

        //RSA的解密函数  string
        private string RSADecrypt(string xmlPrivateKey, string m_strDecryptString)
        {
            RSACryptoServiceProvider _rSACryptoServiceProvider = new RSACryptoServiceProvider();
            _rSACryptoServiceProvider.FromXmlString(xmlPrivateKey);
            return (new UnicodeEncoding()).GetString(_rSACryptoServiceProvider.Decrypt(Convert.FromBase64String(m_strDecryptString), false));
        }
    }
}
