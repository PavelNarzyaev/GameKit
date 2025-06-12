using JetBrains.Annotations;
using Proxies;
using Zenject;

namespace Controllers
{
    [UsedImplicitly]
    public class StateSavingController : ITickable
    {
        private string _filePath;
        [Inject] private LocalStateProxy _localStateProxy;

        public void Tick()
        {
            if (_localStateProxy.IsDirty)
            {
                _localStateProxy.Save();
            }
        }
    }
}
