using UnityEngine;

namespace MonoBehaviours.Screens
{
    public abstract class ScreenAbstract : MonoBehaviour
    {
        public virtual bool IsCached() => false;
    }
}
