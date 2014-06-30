//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：加密与解密临时类
// 作    者：王义波
// 创建时间：2014/6/1 8:01:42
// CLR 版本：1.5
//=====================================================

using System;
using System.Collections.Generic;
using System.Text;
using CLR.Common;

namespace CLR.Security
{
    /// <summary>
    /// 安全模式
    /// </summary>
    public enum SecurityMode
    {
        /// <summary>
        /// 解密
        /// </summary>
        DECRYPT = 0x20,
        /// <summary>
        /// 加密
        /// </summary>
        ENCRYPT = 0x30
    }

    /// <summary>
    /// 加密与解密句柄
    /// </summary>
    /// <param name="input">需要加密/解密的字符串</param>
    /// <returns>string 加密/解密 成功返回加密/解密后的字符串，失败返回原字符串</returns>
    public delegate string SecurityUtilityHandler(string input);
    /// <summary>
    /// 加密与解密临时类
    /// </summary>
    public abstract class SecurityUtility
    {
        /// <summary>
        /// 默认密钥
        /// </summary>
        protected string _defaultKey = "19820116";
        /// <summary>
        /// 实例化通用方法与函数集
        /// </summary>
        protected BaseHelper BaseHelperInstance
        {
            get { return new BaseHelper(); }
        }

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="input">string 待加密的字符串</param>
        /// <returns>string 加密成功返回加密后的字符串，失败返回原字符串</returns>
        protected abstract string Decrypt(string input);
        /// <summary>
        /// 解密字符串
        /// </summary>
        /// <param name="input">string 待解密的字符串</param>
        /// <returns>string 解密成功返回解密后的字符串，失败返回原字符串</returns>
        protected abstract string Encrypt(string input);

        /// <summary>
        /// 加密与解密字符串调用
        /// </summary>
        /// <returns>加密与解密句柄</returns>
        public SecurityUtilityHandler SecurityUtilityInvoke(SecurityMode securityMode)
        {
            return result => { return securityMode == SecurityMode.ENCRYPT ? this.Encrypt(result) : this.Decrypt(result); };

        }
    }
}
