﻿using Newbe.Mahua.Commands;
using Newbe.Mahua.MahuaEvents;
using Newbe.Mahua.MahuaEvents.Enums;
using System.Collections.Generic;

namespace Newbe.Mahua.MPQ.EventFuns
{
    /// <summary>
    /// 某人被取消管理员
    /// </summary>
    public class EventFun2010 : IEventFun
    {
        private readonly IEnumerable<IGroupAdminChangedMahuaEvent> _groupAdminChangedMahuaEvents;
        private readonly IEnumerable<IGroupAdminDisabledMahuaEvent> _groupAdminDisabledMahuaEvents;

        public EventFun2010(
            IEnumerable<IGroupAdminChangedMahuaEvent> groupAdminChangedMahuaEvents,
            IEnumerable<IGroupAdminDisabledMahuaEvent> groupAdminDisabledMahuaEvents)
        {
            _groupAdminChangedMahuaEvents = groupAdminChangedMahuaEvents;
            _groupAdminDisabledMahuaEvents = groupAdminDisabledMahuaEvents;
        }

        public int EventFun { get; } = 2010;

        public void Handle(EventFunInput eventFunInput)
        {
            _groupAdminChangedMahuaEvents
                .Handle(x =>
                    x.ProcessGroupAdminChange(new GroupAdminChangedContext
                    {
                        FromGroup = eventFunInput.FromNum,
                        ToQq = eventFunInput.EventOperator,
                        GroupAdminChangeType = GroupAdminChangeType.Disabled,
                        SendTime = Clock.Now,
                    }));
            _groupAdminDisabledMahuaEvents
                .Handle(x =>
                    x.ProcessGroupAdminDisabled(new GroupAdminDisabledContext
                    {
                        FromGroup = eventFunInput.FromNum,
                        ToQq = eventFunInput.EventOperator,
                        SendTime = Clock.Now,
                    }));
        }
    }
}
