using JetBrains.Annotations;
using Proxies;
using Zenject;

namespace Controllers
{
    [UsedImplicitly]
    public class StateSavingController : ITickable
    {
        private string m_filePath;
        [Inject] private LocalStateProxy m_localStateProxy;

        public void Tick()
        {
            if (m_localStateProxy.IsDirty)
            {
                m_localStateProxy.Save();
            }
        }
    }
}
