using System.Globalization;
using Commands;
using Data;
using Proxies;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Utilities;
using Zenject;

namespace MonoBehaviours.Screens
{
    public class StatePageScreen : ScreenAbstract
    {
        [FormerlySerializedAs("_userId")] [SerializeField] private DebugValue userId;
        [FormerlySerializedAs("_firstLaunchTime")] [SerializeField] private DebugValue firstLaunchTime;
        [FormerlySerializedAs("_launchCount")] [SerializeField] private DebugValue launchCount;
        [FormerlySerializedAs("_copyToClipboardButton")] [SerializeField] private Button copyToClipboardButton;
        [FormerlySerializedAs("_pasteFromClipboardButton")] [SerializeField] private Button pasteFromClipboardButton;
        [FormerlySerializedAs("_resetButton")] [SerializeField] private Button resetButton;

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
