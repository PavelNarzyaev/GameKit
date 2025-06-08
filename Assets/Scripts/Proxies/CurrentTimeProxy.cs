using System;
using UnityEngine;
using Utilities;
using Zenject;

namespace Proxies
{
	public class CurrentTimeProxy
	{
		private static long _startupTimestamp;

		[Inject]
		private void Inject()
		{
			_startupTimestamp = TimestampUtility.ConvertDatetimeToTimestamp(DateTime.UtcNow) - (int)Time.realtimeSinceStartup;
		}

		public long GetTimestamp()
		{
			return _startupTimestamp + (int)Time.realtimeSinceStartup;
		}
	}
}
