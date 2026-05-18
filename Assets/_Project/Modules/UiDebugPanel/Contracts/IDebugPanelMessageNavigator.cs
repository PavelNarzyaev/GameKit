namespace GameKit.UiDebugPanel.Contracts
{
    public interface IDebugPanelMessageNavigator
    {
        void ShowMessage(string addressableId);
        void HideMessage(string addressableId);
    }
}
