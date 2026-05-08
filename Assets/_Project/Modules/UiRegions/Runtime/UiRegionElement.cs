using UnityEngine;

namespace GameKit.UiRegions
{
    public abstract class UiRegionElement : MonoBehaviour
    {
        [SerializeField] private bool isCached;
        [SerializeField] private bool isDebug;
        public string AddressableId { get; set; }

        public bool IsCached => isCached;
        public bool IsDebug => isDebug;
    }
}
