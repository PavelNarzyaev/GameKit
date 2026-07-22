using GameKit.Core;
using UnityEngine;
using Zenject;
#if !IS_PRODUCTION
using GameKit.CurrenciesDebugPage;
using GameKit.DebugPanelMessage;
using GameKit.DebugToolBar;
using GameKit.EnergyDebugPage;
using GameKit.LogsDebugPage;
using GameKit.StateDebugPage;
using GameKit.TimeDebugPage;
using GameKit.TimeOffset;
using GameKit.UiDebugPanel;
#endif

namespace GameKit.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(DevelopmentInstaller), menuName = AssetMenuConstants.k_Installers + "/" + nameof(DevelopmentInstaller))]
    public class DevelopmentInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
#if !IS_PRODUCTION
            TimeOffsetInstaller.Install(Container);
            UiDebugPanelInstaller.Install(Container);
            DebugPanelMessageInstaller.Install(Container);
            DebugToolBarInstaller.Install(Container);
            CurrenciesDebugPageInstaller.Install(Container);
            EnergyDebugPageInstaller.Install(Container);
            StateDebugPageInstaller.Install(Container);
            TimeDebugPageInstaller.Install(Container);
            LogsDebugPageInstaller.Install(Container);
#endif
        }
    }
}
