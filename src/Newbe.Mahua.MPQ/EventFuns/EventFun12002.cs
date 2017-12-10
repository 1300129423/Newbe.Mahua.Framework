﻿using Newbe.Mahua.Commands;
using Newbe.Mahua.MahuaEvents;
using System.Collections.Generic;

namespace Newbe.Mahua.MPQ.EventFuns
{
    /// <summary>
    /// 用户禁用本插件 无权拒绝
    /// </summary>
    public class EventFun12002 : IEventFun
    {
        private readonly IEnumerable<IPluginDisabledMahuaEvent> _pluginDisabledMahuaEvents;

        public EventFun12002(
            IEnumerable<IPluginDisabledMahuaEvent> pluginDisabledMahuaEvents)
        {
            _pluginDisabledMahuaEvents = pluginDisabledMahuaEvents;
        }

        public int EventFun { get; } = 12002;

        public void Handle(EventFunInput eventFunInput)
        {
            _pluginDisabledMahuaEvents
                .Handle(x => x.Disable(new PluginDisabledContext()));
        }
    }
}
