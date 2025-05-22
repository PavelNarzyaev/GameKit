using UnityEditor;

public class ClearPersistentDataMenu
{
	[MenuItem("GameKit/Clear Persistent Data", false, 101)]
	public static void ClearPersistentData()
	{
		LocalStateProxy.Delete();
	}
}
