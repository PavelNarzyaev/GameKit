using GameKit.Commands;
using GameKit.Core;
using GameKit.CorePage;
using GameKit.Currencies;
using GameKit.CurrentTime;
using GameKit.Energy;
using GameKit.ErrorPopup;
using GameKit.MetaPage;
using GameKit.PlayerState;
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
            Container.Bind<ProductionModeProvider>().AsSingle();

            InstallCurrentTimeSource();
            Container.Bind<CurrentTimeProvider>().AsSingle();

            Container.Bind<UiRegionHostPresenter>().AsSingle();
            Container.Bind<PageNavigator>().AsSingle();
            Container.Bind<PopupNavigator>().AsSingle();
            Container.Bind<PopupPresenter>().AsSingle();
            Container.Bind<PopupBackdropPresenter>().AsSingle();
            Container.Bind<FilePlayerStateStorage>().AsSingle();
            Container.Bind<IPlayerStateStorage>().To<EncryptedPlayerStateStorage>().AsSingle();
            Container.Bind<PlayerStateProvider>().AsSingle();
            Container.Bind<PlayerStateCurrenciesGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<CurrenciesService>().AsSingle();
            Container.Bind<PlayerStateEnergyGateway>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyService>().AsSingle();
            Container.Bind<UiFontPreloader>().AsSingle();
            Container.Bind<ErrorPopupPresenter>().AsSingle();
            Container.Bind<SettingsPopupPresenter>().AsSingle();
            Container.Bind<TopPanelPresenter>().AsSingle();
            Container.Bind<CorePagePresenter>().AsSingle();
            Container.Bind<MetaPagePresenter>().AsSingle();
            Container.Bind<StateClipboardProxy.StateClipboardProxy>().AsSingle();

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
            Container.Bind<TimeOffsetService>().AsSingle();
            Container.Bind<ICurrentTimeSource>().To<TimeOffsetCurrentTimeSource>().AsSingle();
#endif
        }

        private void InstallDebugPanel()
        {
#if !IS_PRODUCTION
            Container.Bind<DebugPanelNavigator>().AsSingle();
            Container.Bind<DebugPanelTabBarPresenter>().AsSingle();
            Container.Bind<CurrenciesDebugPagePresenter>().AsSingle();
            Container.Bind<EnergyDebugPagePresenter>().AsSingle();
            Container.Bind<StateDebugPagePresenter>().AsSingle();
            Container.Bind<TimeDebugPagePresenter>().AsSingle();
#endif
        }

        private void InstallCommands()
        {
            Container.Bind<DestroyUiCommand>().AsSingle();
            Container.Bind<LaunchCommand>().AsSingle();
            Container.Bind<ShowInitialUiCommand>().AsSingle();
            Container.Bind<ResetStateCommand>().AsSingle();
            Container.Bind<ResetSceneCommand>().AsSingle();
        }

        private void InstallControllers()
        {
            Container.Bind<StateSavingController>().AsSingle().NonLazy();
            Container.Bind<EnergyRestorationController>().AsSingle().NonLazy();
            Container.Bind<ResetUiController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameKitTickController>().AsSingle();
            Container.BindInterfacesTo<EntryPointController>().AsSingle();
        }
    }
}
