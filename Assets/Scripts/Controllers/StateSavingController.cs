using Proxies;
using JetBrains.Annotations;
using Zenject;

namespace Controllers
{
    [UsedImplicitly]
    public class StateSavingController : ITickable
    {
        [Inject] private LocalStateProxy _localStateProxy;

        private string _filePath;

        public void Tick()
        {
            if (_localStateProxy.IsDirty)
            {
                _localStateProxy.Save();
            }
        }
    }
}
