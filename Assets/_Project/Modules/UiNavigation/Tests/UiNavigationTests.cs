using System;
using System.Collections.Generic;
using GameKit.UiBackgrounds;
using GameKit.UiDebugPanel;
using GameKit.UiPages;
using GameKit.UiPopups;
using GameKit.UiRegions;
using GameKit.UiRegions.Contracts;
using GameKit.UiRegionsControl.Contracts;
using GameKit.UiReset;
using static GameKit.UiNavigation.Tests.UiNavigationTestCalls;
using NUnit.Framework;

namespace GameKit.UiNavigation.Tests
{
    [TestFixture]
    public class UiResetEventBusTests
    {
        [Test]
        public void PublishReset_WhenCalled_RaisesResetRequested()
        {
            var resetEventBus = new UiResetEventBus();
            var resetCalls = 0;
            resetEventBus.ResetRequested += () => resetCalls++;

            resetEventBus.PublishReset();

            Assert.That(resetCalls, Is.EqualTo(1));
        }
    }

    [TestFixture]
    public class UiRegionHostPresenterTests
    {
        [Test]
        public void RegionEvents_WhenCalled_RaiseMatchingEvents()
        {
            var resetEventBus = new UiResetEventBus();
            var presenter = new UiRegionHostPresenter(resetEventBus);
            var calls = new List<string>();

            presenter.RegionElementShowing += (addressableId, region) => calls.Add(Show(addressableId, region));
            presenter.RegionElementHidingIfExists += addressableId => calls.Add(Hide(addressableId));
            presenter.AllRegionElementsDestroying += () => calls.Add(DestroyAll());
            presenter.RegionElementIndexSetting += (addressableId, index) => calls.Add(Index(addressableId, index));
            presenter.RegionActivating += (regionId, isActive) => calls.Add(Activate(regionId, isActive));

            presenter.OnRegionElementShowing(UiRegionElementAddressableIds.k_MetaPage, UiRegionId.Page);
            presenter.OnRegionElementHidingIfExists(UiRegionElementAddressableIds.k_MetaPage);
            presenter.OnRegionElementIndexSetting(UiRegionElementAddressableIds.k_PopupBackdrop, 1);
            presenter.OnRegionActivating(UiRegionId.DebugPanelPage, true);
            presenter.OnAllRegionElementsDestroying();

            Assert.That(calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_MetaPage, UiRegionId.Page),
                Hide(UiRegionElementAddressableIds.k_MetaPage),
                Index(UiRegionElementAddressableIds.k_PopupBackdrop, 1),
                Activate(UiRegionId.DebugPanelPage, true),
                DestroyAll()
            }));
        }

        [Test]
        public void PublishReset_WhenPresenterIsSubscribed_RaisesAllRegionElementsDestroying()
        {
            var resetEventBus = new UiResetEventBus();
            var presenter = new UiRegionHostPresenter(resetEventBus);
            var destroyCalls = 0;
            presenter.AllRegionElementsDestroying += () => destroyCalls++;

            resetEventBus.PublishReset();

            Assert.That(destroyCalls, Is.EqualTo(1));
        }

        [Test]
        public void Dispose_WhenResetIsPublished_DoesNotRaiseAllRegionElementsDestroying()
        {
            var resetEventBus = new UiResetEventBus();
            var presenter = new UiRegionHostPresenter(resetEventBus);
            var destroyCalls = 0;
            presenter.AllRegionElementsDestroying += () => destroyCalls++;

            presenter.Dispose();
            resetEventBus.PublishReset();

            Assert.That(destroyCalls, Is.EqualTo(0));
        }
    }

    [TestFixture]
    public class PageNavigatorTests
    {
        [Test]
        public void ShowPage_WhenFirstPageIsRequested_ShowsPageInPageRegion()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PageNavigator(regionHost, new UiResetEventBus());

            navigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_MetaPage, UiRegionId.Page)
            }));
        }

        [Test]
        public void ShowPage_WhenSamePageIsRequestedTwice_DoesNotShowOrHideAgain()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PageNavigator(regionHost, new UiResetEventBus());
            navigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);
            regionHost.Clear();

            navigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);

            Assert.That(regionHost.Calls, Is.Empty);
        }

        [Test]
        public void ShowPage_WhenDifferentPageIsRequested_HidesPreviousAndShowsNext()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PageNavigator(regionHost, new UiResetEventBus());
            navigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);
            regionHost.Clear();

            navigator.ShowPage(UiRegionElementAddressableIds.k_CorePage);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_MetaPage),
                Show(UiRegionElementAddressableIds.k_CorePage, UiRegionId.Page)
            }));
        }

        [Test]
        public void PublishReset_WhenPageWasShown_ClearsCurrentPage()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var resetEventBus = new UiResetEventBus();
            var navigator = new PageNavigator(regionHost, resetEventBus);
            navigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);
            regionHost.Clear();

            resetEventBus.PublishReset();
            navigator.ShowPage(UiRegionElementAddressableIds.k_MetaPage);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_MetaPage, UiRegionId.Page)
            }));
        }
    }

    [TestFixture]
    public class BackgroundNavigatorTests
    {
        [Test]
        public void ShowBackground_WhenFirstBackgroundIsRequested_ShowsBackgroundRegion()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new BackgroundNavigator(regionHost, new UiResetEventBus());

            navigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_MetaPageBackground, UiRegionId.Background)
            }));
        }

        [Test]
        public void ShowBackground_WhenSameBackgroundIsRequestedTwice_DoesNotShowOrHideAgain()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new BackgroundNavigator(regionHost, new UiResetEventBus());
            navigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);
            regionHost.Clear();

            navigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);

            Assert.That(regionHost.Calls, Is.Empty);
        }

        [Test]
        public void ShowBackground_WhenDifferentBackgroundIsRequested_HidesPreviousAndShowsNext()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new BackgroundNavigator(regionHost, new UiResetEventBus());
            navigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);
            regionHost.Clear();

            navigator.ShowBackground(UiRegionElementAddressableIds.k_CorePageBackground);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_MetaPageBackground),
                Show(UiRegionElementAddressableIds.k_CorePageBackground, UiRegionId.Background)
            }));
        }

        [Test]
        public void PublishReset_WhenBackgroundWasShown_ClearsCurrentBackground()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var resetEventBus = new UiResetEventBus();
            var navigator = new BackgroundNavigator(regionHost, resetEventBus);
            navigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);
            regionHost.Clear();

            resetEventBus.PublishReset();
            navigator.ShowBackground(UiRegionElementAddressableIds.k_MetaPageBackground);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_MetaPageBackground, UiRegionId.Background)
            }));
        }
    }

    [TestFixture]
    public class PopupNavigatorTests
    {
        [Test]
        public void Open_WhenFirstPopupIsRequested_ShowsBackdropThenPopupAndSetsFrontPopup()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PopupNavigator(regionHost, new UiResetEventBus());
            var frontPopupChangedCalls = 0;
            navigator.FrontPopupChanged += () => frontPopupChangedCalls++;

            navigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);

            Assert.That(navigator.FrontPopupAddressableId, Is.EqualTo(UiRegionElementAddressableIds.k_SettingsPopup));
            Assert.That(frontPopupChangedCalls, Is.EqualTo(1));
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_PopupBackdrop, UiRegionId.Popups),
                Show(UiRegionElementAddressableIds.k_SettingsPopup, UiRegionId.Popups),
                Index(UiRegionElementAddressableIds.k_PopupBackdrop, 0)
            }));
        }

        [Test]
        public void Open_WhenSecondPopupIsRequested_ShowsPopupAndMovesBackdropBelowFrontPopup()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PopupNavigator(regionHost, new UiResetEventBus());
            navigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
            regionHost.Clear();
            var frontPopupChangedCalls = 0;
            navigator.FrontPopupChanged += () => frontPopupChangedCalls++;

            navigator.Open(UiRegionElementAddressableIds.k_ErrorPopup);

            Assert.That(navigator.FrontPopupAddressableId, Is.EqualTo(UiRegionElementAddressableIds.k_ErrorPopup));
            Assert.That(frontPopupChangedCalls, Is.EqualTo(1));
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_ErrorPopup, UiRegionId.Popups),
                Index(UiRegionElementAddressableIds.k_PopupBackdrop, 1)
            }));
        }

        [Test]
        public void CloseFront_WhenMultiplePopupsAreOpen_HidesFrontPopupAndRestoresPreviousFront()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PopupNavigator(regionHost, new UiResetEventBus());
            navigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
            navigator.Open(UiRegionElementAddressableIds.k_ErrorPopup);
            regionHost.Clear();
            var frontPopupChangedCalls = 0;
            navigator.FrontPopupChanged += () => frontPopupChangedCalls++;

            navigator.CloseFront();

            Assert.That(navigator.FrontPopupAddressableId, Is.EqualTo(UiRegionElementAddressableIds.k_SettingsPopup));
            Assert.That(frontPopupChangedCalls, Is.EqualTo(1));
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_ErrorPopup),
                Index(UiRegionElementAddressableIds.k_PopupBackdrop, 0)
            }));
        }

        [Test]
        public void Close_WhenLastPopupIsClosed_HidesPopupAndBackdropAndResetsModalState()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PopupNavigator(regionHost, new UiResetEventBus())
            {
                IsFrontPopupModal = true
            };
            navigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
            regionHost.Clear();

            navigator.Close(UiRegionElementAddressableIds.k_SettingsPopup);

            Assert.That(navigator.FrontPopupAddressableId, Is.Null);
            Assert.That(navigator.IsFrontPopupModal, Is.False);
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_SettingsPopup),
                Hide(UiRegionElementAddressableIds.k_PopupBackdrop)
            }));
        }

        [Test]
        public void CloseFront_WhenStackIsEmpty_DoesNothing()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new PopupNavigator(regionHost, new UiResetEventBus());

            navigator.CloseFront();

            Assert.That(navigator.FrontPopupAddressableId, Is.Null);
            Assert.That(regionHost.Calls, Is.Empty);
        }

        [Test]
        public void PublishReset_WhenPopupsAreOpen_ClearsStackAndModalState()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var resetEventBus = new UiResetEventBus();
            var navigator = new PopupNavigator(regionHost, resetEventBus);
            navigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);
            navigator.Open(UiRegionElementAddressableIds.k_ErrorPopup);
            navigator.IsFrontPopupModal = true;
            regionHost.Clear();

            resetEventBus.PublishReset();
            navigator.Open(UiRegionElementAddressableIds.k_SettingsPopup);

            Assert.That(navigator.FrontPopupAddressableId, Is.EqualTo(UiRegionElementAddressableIds.k_SettingsPopup));
            Assert.That(navigator.IsFrontPopupModal, Is.False);
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_PopupBackdrop, UiRegionId.Popups),
                Show(UiRegionElementAddressableIds.k_SettingsPopup, UiRegionId.Popups),
                Index(UiRegionElementAddressableIds.k_PopupBackdrop, 0)
            }));
        }
    }

    [TestFixture]
    public class DebugPanelPageNavigatorTests
    {
        [Test]
        public void Show_WhenFirstDebugPageIsRequested_ShowsBackdropThenPageAndRaisesPageChanged()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new DebugPanelPageNavigator(regionHost, new UiResetEventBus());
            var pageChangedCalls = 0;
            navigator.PageChanged += () => pageChangedCalls++;

            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);

            Assert.That(navigator.CurrentPageAddressableId, Is.EqualTo(UiRegionElementAddressableIds.k_TimeDebugPage));
            Assert.That(pageChangedCalls, Is.EqualTo(1));
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_DebugPageBackdrop, UiRegionId.DebugPanelPage),
                Show(UiRegionElementAddressableIds.k_TimeDebugPage, UiRegionId.DebugPanelPage)
            }));
        }

        [Test]
        public void Show_WhenSameDebugPageIsRequestedTwice_DoesNotShowOrHideAgain()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new DebugPanelPageNavigator(regionHost, new UiResetEventBus());
            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);
            regionHost.Clear();
            var pageChangedCalls = 0;
            navigator.PageChanged += () => pageChangedCalls++;

            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);

            Assert.That(pageChangedCalls, Is.EqualTo(0));
            Assert.That(regionHost.Calls, Is.Empty);
        }

        [Test]
        public void Show_WhenDifferentDebugPageIsRequested_HidesPreviousAndShowsNext()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new DebugPanelPageNavigator(regionHost, new UiResetEventBus());
            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);
            regionHost.Clear();
            var pageChangedCalls = 0;
            navigator.PageChanged += () => pageChangedCalls++;

            navigator.Show(UiRegionElementAddressableIds.k_EnergyDebugPage);

            Assert.That(navigator.CurrentPageAddressableId, Is.EqualTo(UiRegionElementAddressableIds.k_EnergyDebugPage));
            Assert.That(pageChangedCalls, Is.EqualTo(1));
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_TimeDebugPage),
                Show(UiRegionElementAddressableIds.k_EnergyDebugPage, UiRegionId.DebugPanelPage)
            }));
        }

        [Test]
        public void Close_WhenDebugPageIsOpen_HidesPageAndBackdropAndRaisesPageChanged()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new DebugPanelPageNavigator(regionHost, new UiResetEventBus());
            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);
            regionHost.Clear();
            var pageChangedCalls = 0;
            navigator.PageChanged += () => pageChangedCalls++;

            navigator.Close();

            Assert.That(navigator.CurrentPageAddressableId, Is.Null);
            Assert.That(pageChangedCalls, Is.EqualTo(1));
            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_TimeDebugPage),
                Hide(UiRegionElementAddressableIds.k_DebugPageBackdrop)
            }));
        }

        [Test]
        public void PublishReset_WhenDebugPageWasShown_ClearsCurrentPage()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var resetEventBus = new UiResetEventBus();
            var navigator = new DebugPanelPageNavigator(regionHost, resetEventBus);
            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);
            regionHost.Clear();

            resetEventBus.PublishReset();
            navigator.Show(UiRegionElementAddressableIds.k_TimeDebugPage);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Show(UiRegionElementAddressableIds.k_DebugPageBackdrop, UiRegionId.DebugPanelPage),
                Show(UiRegionElementAddressableIds.k_TimeDebugPage, UiRegionId.DebugPanelPage)
            }));
        }
    }

    [TestFixture]
    public class DebugPanelMessageNavigatorTests
    {
        [Test]
        public void ShowMessage_WhenCalled_HidesExistingMessageThenShowsMessage()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new DebugPanelMessageNavigator(regionHost);

            navigator.ShowMessage(UiRegionElementAddressableIds.k_DebugPanelMessage);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_DebugPanelMessage),
                Show(UiRegionElementAddressableIds.k_DebugPanelMessage, UiRegionId.DebugPanelMessage)
            }));
        }

        [Test]
        public void HideMessage_WhenCalled_HidesMessage()
        {
            var regionHost = new RecordingUiRegionHostPresenter();
            var navigator = new DebugPanelMessageNavigator(regionHost);

            navigator.HideMessage(UiRegionElementAddressableIds.k_DebugPanelMessage);

            Assert.That(regionHost.Calls, Is.EqualTo(new[]
            {
                Hide(UiRegionElementAddressableIds.k_DebugPanelMessage)
            }));
        }
    }

    internal class RecordingUiRegionHostPresenter : IUiRegionHostPresenter
    {
        public readonly List<string> Calls = new();

        public event Action<string, UiRegionId> RegionElementShowing;
        public event Action<string> RegionElementHidingIfExists;
        public event Action AllRegionElementsDestroying;
        public event Action<string, int> RegionElementIndexSetting;
        public event Action<UiRegionId, bool> RegionActivating;

        public void OnRegionElementShowing(string addressableId, UiRegionId region)
        {
            Calls.Add(UiNavigationTestCalls.Show(addressableId, region));
            RegionElementShowing?.Invoke(addressableId, region);
        }

        public void OnRegionElementHidingIfExists(string addressableId)
        {
            Calls.Add(UiNavigationTestCalls.Hide(addressableId));
            RegionElementHidingIfExists?.Invoke(addressableId);
        }

        public void OnAllRegionElementsDestroying()
        {
            Calls.Add(UiNavigationTestCalls.DestroyAll());
            AllRegionElementsDestroying?.Invoke();
        }

        public void OnRegionElementIndexSetting(string addressableId, int index)
        {
            Calls.Add(UiNavigationTestCalls.Index(addressableId, index));
            RegionElementIndexSetting?.Invoke(addressableId, index);
        }

        public void OnRegionActivating(UiRegionId regionId, bool isActive)
        {
            Calls.Add(UiNavigationTestCalls.Activate(regionId, isActive));
            RegionActivating?.Invoke(regionId, isActive);
        }

        public void Clear()
        {
            Calls.Clear();
        }
    }

    internal static class UiNavigationTestCalls
    {
        public static string Show(string addressableId, UiRegionId region)
        {
            return $"Show:{addressableId}:{region}";
        }

        public static string Hide(string addressableId)
        {
            return $"Hide:{addressableId}";
        }

        public static string DestroyAll()
        {
            return "DestroyAll";
        }

        public static string Index(string addressableId, int index)
        {
            return $"Index:{addressableId}:{index}";
        }

        public static string Activate(UiRegionId regionId, bool isActive)
        {
            return $"Activate:{regionId}:{isActive}";
        }
    }
}
