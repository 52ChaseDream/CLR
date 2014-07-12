//======================================================
// Copyright (C) 2014-2020 CLR. All rights reserved. 
// 功    能：数据处理基层类
// 作    者：王义波
// 创建时间：2014/7/8 12:11:33
// CLR 版本：1.4
//=====================================================
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace CLR.DataBase
{
    /// <summary>
    /// 执行数据集的代理
    /// </summary>
    /// <param name="commandText"></param>
    /// <param name="tbName"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate DataSet RunCommandDataSetHandler(string commandText, string tbName, DbParameter[] parameters);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public delegate int RunExecHandler(string name, params DbParameter[] parameters);

    public delegate DbDataReader RunCommandReaderHander(string procName, DbParameter[] parameters);

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
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected abstract int Exec(string name, CommandType type, params DbParameter[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        protected abstract DbDataReader ExecReader(string name, CommandType type, params DbParameter[] parameters);

        /// <summary>
        /// 执行数据集的回调函数
        /// </summary>
        /// <returns></returns>
        public RunCommandDataSetHandler RunCommandDataSetInvoke()
        {
            return (commandText, tbName, parameters) => parameters == null ? this.ExecDataSet(commandText, CommandType.Text, tbName) : this.ExecDataSet(commandText, CommandType.StoredProcedure, tbName, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RunExecHandler RunExecInvoke()
        {
            return (name, parameters) => this.Exec(name, CommandType.StoredProcedure, parameters);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public RunCommandReaderHander RunCommandReaderInvoke()
        {
            return (name, parameters) => this.ExecReader(name, CommandType.StoredProcedure, parameters);
        }


    }
}
