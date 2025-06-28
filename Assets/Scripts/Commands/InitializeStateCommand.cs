using System;
using Data;
using JetBrains.Annotations;
using Proxies;
using Zenject;
using Random = UnityEngine.Random;

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

            // TODO: <remove_temporary_code>
            m_localStateProxy.Data.currencies.softCurrency = Random.Range(1, 100);
            m_localStateProxy.Data.currencies.hardCurrency = Random.Range(1, 100);
            m_localStateProxy.Data.currencies.energy = Random.Range(1, 100);
            // TODO: </remove_temporary_code>

            m_localStateProxy.MarkAsDirty();
        }
    }
}
