using GameKit.UiPopups;
using Zenject;

namespace GameKit.SettingsPopup
{
    public class SettingsPopupView : PopupView
    {
        [Inject] private SettingsPopupPresenter m_presenter;
    }
}
