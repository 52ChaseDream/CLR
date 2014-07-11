//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：数据参数处理基层类
// 作    者：王义波
// 创建时间：2014/7/9 10:29:33
// CLR 版本：1.4
//=====================================================

using System.Data;
using System.Data.Common;

namespace CLR.DataBase
{
    /// <summary>
    /// 数据参数处理基层类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class DBParameterProvider<T> : DBParameterUtility<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        protected DBParameterProvider(string connectionString) : base(connectionString) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        protected abstract DbParameter GetParameter(string parameterName, T dbType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterName"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        protected abstract DbParameter GetParameter(string parameterName, T dbType, int size);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="dirction"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected override DbParameter MakeParameter(string name, T dbType, int size, ParameterDirection dirction, object value)
        {
            var parameter = size > 0 ? this.GetParameter(name, dbType, size) : this.GetParameter(name, dbType);
            parameter.Direction = dirction;
            if (!(dirction == ParameterDirection.Output && value == null))
                parameter.Value = value;
            return parameter;
        }
    }
}
