using UnityEngine;

namespace GameKit
{

	public abstract class ScreenAbstract : MonoBehaviour
	{
		public virtual bool IsCached()
		{
			return false;
		}
	}
}
