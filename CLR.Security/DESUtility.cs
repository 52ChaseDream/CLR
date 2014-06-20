//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：DES 加密/解密类
// 作    者：王义波
// 创建时间：2014/6/16 10:30:38
// CLR 版本：1.3
//=====================================================

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CLR.Security
{
    /// <summary>
    /// DES 加密/解密类
    /// </summary>
    public abstract class DESUtility : SecurityUtility
    {
        /// <summary>
        /// 加密数据
        /// </summary>
        /// <param name="desCryptoServiceProvider">定义访问数据加密标准</param>
        /// <param name="input">需要加密的字符串</param>
        /// <returns>加密之后的字符串</returns>
        protected string Encrypt(DESCryptoServiceProvider desCryptoServiceProvider, string input)
        {
            byte[] _inputByteArray = Encoding.Default.GetBytes(input);
            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, desCryptoServiceProvider.CreateEncryptor(), CryptoStreamMode.Write);
            _cryptoStream.Write(_inputByteArray, 0, _inputByteArray.Length);
            _cryptoStream.FlushFinalBlock();
            StringBuilder _stringBuilder = new StringBuilder();
            foreach (byte items in _memoryStream.ToArray())
            {
                _stringBuilder.AppendFormat("{0:X2}", items);
            }
            _cryptoStream.Close();
            _memoryStream.Close();
            return _stringBuilder.ToString();
        }

        /// <summary>
        /// 解密数据
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="desCryptoServiceProvider">定义访问数据加密标准</param>
        /// <returns>加密之后的字符串</returns>
        protected string Decrypt(string input, DESCryptoServiceProvider desCryptoServiceProvider)
        {
            int _length = input.Length / 2, _num;
            byte[] _inputByteArray = new byte[_length];
            for (int i = 0; i < _length; i++)
            {
                _num = Convert.ToInt32(input.Substring(i * 2, 2), 0x10);
                _inputByteArray[i] = (byte)_num;
            }
            MemoryStream _memoryStream = new MemoryStream();
            CryptoStream _cryptoStream = new CryptoStream(_memoryStream, desCryptoServiceProvider.CreateDecryptor(), CryptoStreamMode.Write);
            _cryptoStream.Write(_inputByteArray, 0, _inputByteArray.Length);
            _cryptoStream.FlushFinalBlock();
            return Encoding.Default.GetString(_memoryStream.ToArray());
        }
    }
}
