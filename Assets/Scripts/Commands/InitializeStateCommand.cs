using System;
using Data;
using JetBrains.Annotations;
using Proxies;
using Zenject;

namespace Commands
{
    [UsedImplicitly]
    public class InitializeStateCommand
    {
        [Inject] private CurrentTimeProxy m_currentTimeProxy;
        [Inject] private LocalStateProxy m_localStateProxy;

        public void Execute()
        {
            m_localStateProxy.Data = new State
            {
                userId = Guid.NewGuid().ToString(),
                firstLaunchTimestamp = m_currentTimeProxy.GetTimestamp()
            };
            m_localStateProxy.MarkAsDirty();
        }
    }
}
