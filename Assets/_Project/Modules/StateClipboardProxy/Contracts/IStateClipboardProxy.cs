namespace GameKit.StateClipboardProxy.Contracts
{
    public interface IStateClipboardProxy
    {
        void CopyStateToClipboard();
        void PasteStateFromClipboard();
    }
}
