using System;
using GameKit.UiRegionsControl;
using GameKit.PlayerState;
using GameKit.UiPopups;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class LaunchCommand : ILaunchCommand
    {
        [Inject] private IPlayerStateProvider m_playerStateProvider;
        [Inject] private IPopupNavigator m_popupNavigator;
        [Inject] private IShowInitialUiCommand m_showInitialUiCommand;
        [Inject] private GameKitTickController m_gameKitTickController;

        public void Execute()
        {
            try
            {
                m_playerStateProvider.Refresh();
                m_playerStateProvider.Data.LaunchesCounter++;
                m_playerStateProvider.MarkAsDirty();

                m_showInitialUiCommand.Execute();
                m_gameKitTickController.Launch();
            }
            catch (Exception e)
            {
                Debug.LogError($"Launch error: \"{e.Message}\"");
                m_popupNavigator.Open(UiRegionElementAddressableIds.k_ErrorPopup);
            }
        }
    }
}
