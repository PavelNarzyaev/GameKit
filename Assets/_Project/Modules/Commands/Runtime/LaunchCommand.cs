using System;
using GameKit.Commands.Contracts;
using GameKit.PlayerState.Contracts;
using GameKit.UiPopups.Contracts;
using GameKit.UiRegionsControl.Contracts;
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
