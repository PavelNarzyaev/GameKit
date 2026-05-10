using GameKit.Commands.Contracts;
using JetBrains.Annotations;
using UnityEngine.SceneManagement;

namespace GameKit.Commands
{
    [UsedImplicitly]
    public class ResetSceneCommand : IResetSceneCommand
    {
        public void Execute()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
