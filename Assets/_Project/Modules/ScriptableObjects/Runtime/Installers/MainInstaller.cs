using GameKit.Commands;
using GameKit.Core;
using GameKit.Core.Contracts;
using GameKit.CorePage;
using GameKit.Currencies;
using GameKit.CurrentTime;
using GameKit.Energy;
using GameKit.ErrorPopup;
using GameKit.Logs;
using GameKit.MetaPage;
using GameKit.PlayerState;
using GameKit.PlayerState.Contracts;
using GameKit.ProductionMode;
using GameKit.SettingsPopup;
using GameKit.TopPanel;
using GameKit.UiBackgrounds;
using GameKit.UiFonts;
using GameKit.UiPages;
using GameKit.UiPopups;
using GameKit.UiRegions;
using GameKit.UiReset;
using UnityEngine;
using Zenject;
#if !IS_PRODUCTION
using GameKit.CurrenciesDebugPage;
using GameKit.DebugPanelMessage;
using GameKit.DebugToolBar;
using GameKit.EnergyDebugPage;
using GameKit.LogsDebugPage;
using GameKit.StateDebugPage;
using GameKit.TimeOffset;
using GameKit.TimeDebugPage;
using GameKit.UiDebugPanel;
#endif

namespace GameKit.ScriptableObjects
{
    [CreateAssetMenu(fileName = nameof(MainInstaller), menuName = AssetMenuConstants.k_Installers + "/" + nameof(MainInstaller))]
    public class MainInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ProductionModeProvider>().AsSingle();

            InstallCurrentTimeSource();
            Container.BindInterfacesAndSelfTo<CurrentTimeProvider>().AsSingle();

            Container.BindInterfacesAndSelfTo<UiResetEventBus>().AsSingle();
            Container.Bind<UiRegionElementSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiRegionHostPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<BackgroundNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PageNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupPresenter>().AsSingle();
            Container.Bind<PopupBackdropPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<FilePlayerStateStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<EncryptionKeysProvider>().AsSingle();
            PlayerStateInstaller.Install(Container);
            Container.BindInterfacesAndSelfTo<LogsProvider>().AsSingle();
            Container.Bind<PlayerStateCurrenciesGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrenciesService>().AsSingle();
            Container.Bind<PlayerStateEnergyGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyService>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiFontPreloader>().AsSingle();
            Container.Bind<ErrorPopupPresenter>().AsSingle();
            Container.Bind<SettingsPopupPresenter>().AsSingle();
            Container.Bind<TopPanelPresenter>().AsSingle();
            Container.Bind<CorePagePresenter>().AsSingle();
            Container.Bind<MetaPagePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<StateClipboardProxy.StateClipboardProxy>().AsSingle();

            InstallDebugPanel();
            InstallCommands();
            InstallControllers();
        }

        private void InstallCurrentTimeSource()
        {
#if IS_PRODUCTION
            Container.Bind(typeof(IRealTimeSource), typeof(ICurrentTimeSource)).To<SystemUtcCurrentTimeSource>().AsSingle();
#else
            Container.Bind<IRealTimeSource>().To<SystemUtcCurrentTimeSource>().AsSingle();
            Container.Bind<PlayerStateTimeOffsetGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeOffsetService>().AsSingle();
            Container.Bind<ICurrentTimeSource>().To<TimeOffsetCurrentTimeSource>().AsSingle();
#endif
        }

        private void InstallDebugPanel()
        {
#if !IS_PRODUCTION
            Container.BindInterfacesAndSelfTo<DebugPanelPageNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugPanelMessageNavigator>().AsSingle();
            Container.Bind<DebugPanelMessagePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugToolBarPageTabsPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugToolBarLogsIndicatorPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugToolBarCloseButtonPresenter>().AsSingle();
            Container.Bind<CurrenciesDebugPagePresenter>().AsSingle();
            Container.Bind<EnergyDebugPagePresenter>().AsSingle();
            Container.Bind<StateDebugPagePresenter>().AsSingle();
            Container.Bind<TimeDebugPagePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<LogsDebugPagePresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugLogsFilterSelectorPresenter>().AsSingle();
#endif
        }

        private void InstallCommands()
        {
            Container.BindInterfacesAndSelfTo<LaunchCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShowInitialUiCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResetSceneCommand>().AsSingle();
        }

        private void InstallControllers()
        {
            Container.BindInterfacesAndSelfTo<EnergyRestorationController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ResetUiController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameKitTickController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LogMessagesController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EntryPointController>().AsSingle();
        }
    }
}
