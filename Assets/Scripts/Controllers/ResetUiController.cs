using Commands;
using JetBrains.Annotations;
using Proxies;
using Zenject;

namespace Controllers
{
    [UsedImplicitly]
    public class ResetUiController
    {
        [Inject] private LocalStateProxy m_localStateProxy;
        [Inject] private ResetUiCommand m_resetUiCommand;

        [Inject]
        private void Inject()
        {
            m_localStateProxy.RefreshFromJsonEvent += m_resetUiCommand.Execute;
        }
    }
}
