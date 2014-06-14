//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：扩展数据集对象类
// 作    者：王义波
// 创建时间：2014/6/10 15:08:51
// CLR 版本：1.2
//=====================================================

using System;

namespace CLR.Common
{
    /// <summary>
    /// 扩展数据集对象
    /// </summary>
    public class TableHelper : IDisposable
    {
        /// <summary>
        /// 获取扩展哈希表对象类的接口
        /// </summary>
        public HashHelper[] HashHelperInstance
        {
            get { return new HashHelper[0]; }
        }
        #region IDisposable 成员
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < this.HashHelperInstance.Length; i++)
            {
                this.HashHelperInstance[i].Dispose();
            }
        }

        #endregion
    }
}
