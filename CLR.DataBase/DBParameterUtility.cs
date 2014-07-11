//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：SqlProvider 数据库提供类
// 作    者：王义波
// 创建时间：2014/7/9 10:50:26
// CLR 版本：1.4
//=====================================================
using System.Data;
using System.Data.Common;

namespace CLR.DataBase
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DBParameterUtility<T> : DBUtilityProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        protected DBParameterUtility(string connectionString) : base(connectionString) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="dirction"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract DbParameter MakeParameter(string name, T dbType, int size, ParameterDirection dirction, object value);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public DbParameter MakeParameter(string name, T dbType, int size, object value)
        {
            return this.MakeParameter(name, dbType, size, ParameterDirection.Input, value);
        }
    }
}
