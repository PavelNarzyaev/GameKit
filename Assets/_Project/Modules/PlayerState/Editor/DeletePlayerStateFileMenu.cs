using JetBrains.Annotations;
using UnityEditor;

namespace GameKit.PlayerState.Editor
{
    [UsedImplicitly]
    public class DeletePlayerStateFileMenu
    {
        [MenuItem("GameKit/Delete Player State File", false, 101)]
        public static void DeletePlayerStateFile()
        {
            new FilePlayerStateStorage().Delete();
        }
    }
}
