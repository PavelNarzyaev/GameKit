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
using GameKit.UiFonts;
using GameKit.UiPages;
using GameKit.UiPopups;
using GameKit.UiRegions;
using UnityEngine;
using Zenject;
#if !IS_PRODUCTION
using GameKit.CurrenciesDebugPage;
using GameKit.DebugPanelTabBar;
using GameKit.EnergyDebugPage;
using GameKit.LogsDebugPage;
using GameKit.StateDebugPage;
using GameKit.TimeOffset;
using GameKit.TimeOffset.Contracts;
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

            Container.BindInterfacesAndSelfTo<UiRegionHostPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<PageNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PopupPresenter>().AsSingle();
            Container.Bind<PopupBackdropPresenter>().AsSingle();
            Container.Bind<FilePlayerStateStorage>().AsSingle();
            Container.Bind<IPlayerStateStorage>().To<EncryptedPlayerStateStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<EncryptionKeysProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<JsonPlayerStateSerializer>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateValidator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerStateProvider>().AsSingle();
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
            Container.Bind<ICurrentTimeSource>().To<SystemUtcCurrentTimeSource>().AsSingle();
#else
            Container.Bind<ICurrentTimeSource>().WithId(CurrentTimeSourceIds.k_BaseCurrentTimeSource).To<SystemUtcCurrentTimeSource>().AsSingle();
            Container.Bind<PlayerStateTimeOffsetGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimeOffsetService>().AsSingle();
            Container.Bind<ICurrentTimeSource>().To<TimeOffsetCurrentTimeSource>().AsSingle();
#endif
        }

        private void InstallDebugPanel()
        {
#if !IS_PRODUCTION
            Container.BindInterfacesAndSelfTo<DebugPanelNavigator>().AsSingle();
            Container.BindInterfacesAndSelfTo<DebugPanelTabBarPresenter>().AsSingle();
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
            Container.BindInterfacesAndSelfTo<DestroyUiCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<LaunchCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ShowInitialUiCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResetStateCommand>().AsSingle();
            Container.BindInterfacesAndSelfTo<ResetSceneCommand>().AsSingle();
        }

        private void InstallControllers()
        {
            Container.Bind<StateSavingController>().AsSingle().NonLazy();
            Container.Bind<EnergyRestorationController>().AsSingle().NonLazy();
            Container.Bind<ResetUiController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameKitTickController>().AsSingle();
            Container.BindInterfacesAndSelfTo<LogMessagesController>().AsSingle().NonLazy();
            Container.BindInterfacesTo<EntryPointController>().AsSingle();
        }
    }
}
