using System.Globalization;
using Commands;
using Data;
using Proxies;
using UnityEngine;
using UnityEngine.UI;
using Utilities;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class StatePageScreen : ScreenAbstract
    {
        [SerializeField] private DebugValue userId;
        [SerializeField] private DebugValue firstLaunchTime;
        [SerializeField] private DebugValue launchCount;
        [SerializeField] private Button copyToClipboardButton;
        [SerializeField] private Button pasteFromClipboardButton;
        [SerializeField] private Button resetButton;

        [Inject] private LocalStateProxy m_localStateProxy;
        [Inject] private ResetStateCommand m_resetStateCommand;
        [Inject] private StateClipboardProxy m_stateClipboardProxy;

        private void Awake()
        {
            copyToClipboardButton.onClick.AddListener(m_stateClipboardProxy.CopyStateToClipboard);
            pasteFromClipboardButton.onClick.AddListener(m_stateClipboardProxy.PasteStateFromClipboard);
            resetButton.onClick.AddListener(m_resetStateCommand.Execute);
        }

        private void Start()
        {
            var state = m_localStateProxy.Data;

            SetUpUserId(state);
            SetUpFirstLaunchTime(state);
            SetUpLaunchCount(state);
        }

        private void SetUpUserId(State state)
        {
            userId.SetTitleText("User Id");
            userId.SetValueText(state.userId);
        }

        private void SetUpFirstLaunchTime(State state)
        {
            firstLaunchTime.SetTitleText("First Launch");
            var timestamp = state.firstLaunchTimestamp;
            var readableTime = TimestampUtility.ConvertTimestampToReadableString(timestamp);
            firstLaunchTime.SetValueText(readableTime);
        }

        private void SetUpLaunchCount(State state)
        {
            launchCount.SetTitleText("Launch Count");
            launchCount.SetValueText(state.launchesCounter.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsCached()
        {
            return true;
        }
    }
}
