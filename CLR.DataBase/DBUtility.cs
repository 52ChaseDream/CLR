//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：数据处理基层类
// 作    者：王义波
// 创建时间：2014/7/8 12:11:33
// CLR 版本：1.4
//=====================================================
using System.Data.Common;
using System.Data;

namespace CLR.DataBase
{
    /// <summary>
    /// 执行数据集的代理
    /// </summary>
    /// <param name="commandText"></param>
    /// <param name="tbName"></param>
    /// <returns></returns>
    public delegate DataSet RunCommandDataSetHandler(string commandText, string tbName);

    /// <summary>
    /// 数据处理基层类
    /// </summary>
    public abstract class DBUtility
    {
        /// <summary>
        /// 执行数据集
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="tbName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected abstract DataSet ExecDataSet(string name, CommandType type, string tbName, params DbParameter[] parameters);

        /// <summary>
        /// 执行数据集的回调函数
        /// </summary>
        /// <returns></returns>
        public RunCommandDataSetHandler RunCommandDataSetInvoke()
        {
            return (commandText, tbName) => { return this.ExecDataSet(commandText, CommandType.Text, tbName); };
        }
    }
}
