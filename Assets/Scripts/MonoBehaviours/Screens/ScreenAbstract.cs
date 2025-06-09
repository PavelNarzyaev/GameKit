using UnityEngine;

public abstract class ScreenAbstract : MonoBehaviour
{
    public virtual bool IsCached()
    {
        return false;
    }
}
