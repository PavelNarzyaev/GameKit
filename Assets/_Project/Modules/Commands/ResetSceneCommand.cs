using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetSceneCommand
    {
        public void Execute()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
