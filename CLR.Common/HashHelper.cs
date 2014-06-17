//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：扩展哈希表对象类
// 作    者：王义波
// 创建时间：2014/6/10 14:31:51
// CLR 版本：1.3
//=====================================================

using System;
using System.Collections;

namespace CLR.Common
{
    /// <summary>
    /// 扩展哈希表对象类
    /// </summary>
    public class HashHelper : Hashtable, IDisposable
    {
        /// <summary>
        /// 通用方法、函数集接口
        /// </summary>
        public BaseHelper BaseHelperInstance
        {
            get { return new BaseHelper(); }
        }

        /// <summary>
        /// 获取指定键值的字符串格式
        /// </summary>
        /// <param name="key">string 键</param>
        /// <returns>string 字符串</returns>
        public string GetString(string key)
        {
            return this.BaseHelperInstance.IsNull(this[key]) ? string.Empty : this[key].ToString();
        }

        #region IDisposable 成员
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.Clear();
        }

        #endregion
    }
}
