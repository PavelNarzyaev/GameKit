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
        [SerializeField] private DebugValue _userId;
        [SerializeField] private DebugValue _firstLaunchTime;
        [SerializeField] private DebugValue _launchCount;
        [SerializeField] private Button _copyToClipboardButton;
        [SerializeField] private Button _pasteFromClipboardButton;
        [SerializeField] private Button _resetButton;

        [Inject] private LocalStateProxy _localStateProxy;
        [Inject] private ResetStateCommand _resetStateCommand;
        [Inject] private StateClipboardProxy _stateClipboardProxy;

        private void Awake()
        {
            _copyToClipboardButton.onClick.AddListener(_stateClipboardProxy.CopyStateToClipboard);
            _pasteFromClipboardButton.onClick.AddListener(_stateClipboardProxy.PasteStateFromClipboard);
            _resetButton.onClick.AddListener(_resetStateCommand.Execute);
        }

        private void Start()
        {
            var state = _localStateProxy.data;

            SetUpUserId(state);
            SetUpFirstLaunchTime(state);
            SetUpLaunchCount(state);
        }

        private void SetUpUserId(State state)
        {
            _userId.SetTitleText("User Id");
            _userId.SetValueText(state.userId);
        }

        private void SetUpFirstLaunchTime(State state)
        {
            _firstLaunchTime.SetTitleText("First Launch");
            var timestamp = state.firstLaunchTimestamp;
            var readableTime = TimestampUtility.ConvertTimestampToReadableString(timestamp);
            _firstLaunchTime.SetValueText(readableTime);
        }

        private void SetUpLaunchCount(State state)
        {
            _launchCount.SetTitleText("Launch Count");
            _launchCount.SetValueText(state.launchesCounter.ToString(CultureInfo.InvariantCulture));
        }

        public override bool IsCached()
        {
            return true;
        }
    }
}
