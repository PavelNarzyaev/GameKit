using System;
using GameKit.Core.Contracts;
using JetBrains.Annotations;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class GameKitTickController : ITickable, IGameTickSource
    {
        public event Action Ticked;
        private bool m_isTicking;

        public void Launch()
        {
            m_isTicking = true;
        }

        public void Tick()
        {
            if (m_isTicking)
            {
                Ticked?.Invoke();
            }
        }
    }
}
