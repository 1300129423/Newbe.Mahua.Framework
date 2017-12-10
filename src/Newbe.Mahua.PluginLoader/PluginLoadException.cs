﻿using System;
using System.Runtime.Serialization;

namespace Newbe.Mahua
{
    /// <summary>
    /// 插件加载异常
    /// </summary>
    [Serializable]
    public class PluginLoadException : Exception
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="pluginName"></param>
        /// <param name="reason"></param>
        public PluginLoadException(string pluginName, string reason)
        {
            PluginName = pluginName;
            Reason = reason;
        }

        protected PluginLoadException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// 插件名称
        /// </summary>
        public string PluginName { get; }

        /// <summary>
        /// 原因
        /// </summary>
        public string Reason { get; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public override string Message => $"{PluginName}插件加载失败，原因是:{Reason}";
    }
}
