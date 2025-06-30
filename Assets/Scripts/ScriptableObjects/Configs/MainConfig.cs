using UnityEngine;

namespace ScriptableObjects.Configs
{
    [CreateAssetMenu(fileName = nameof(MainConfig), menuName = EditorMenuConstants.Configs + "/" + nameof(MainConfig))]
    public class MainConfig :ScriptableObject
    {
        public int energyRestorationLimit = 100;
        public int oneEnergyRestorationSeconds = 60;
    }
}
