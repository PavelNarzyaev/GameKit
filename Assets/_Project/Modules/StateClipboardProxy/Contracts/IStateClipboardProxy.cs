namespace GameKit.StateClipboardProxy
{
    public interface IStateClipboardProxy
    {
        void CopyStateToClipboard();
        void PasteStateFromClipboard();
    }
}
