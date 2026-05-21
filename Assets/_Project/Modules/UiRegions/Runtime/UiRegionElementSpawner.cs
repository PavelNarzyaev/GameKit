using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace GameKit.UiRegions
{
    public class UiRegionElementSpawner
    {
        private readonly Dictionary<string, UiRegionElement> m_elementByAddressableId = new();
        private readonly DiContainer m_diContainer;

        public UiRegionElementSpawner(DiContainer diContainer)
        {
            m_diContainer = diContainer;
        }

        public UiRegionElement Show(string addressableId, Transform parent)
        {
            if (!parent)
            {
                throw new ArgumentNullException(nameof(parent));
            }

            if (m_elementByAddressableId.TryGetValue(addressableId, out var element))
            {
                element.transform.SetParent(parent, false);
                element.gameObject.SetActive(true);
                return element;
            }

            var asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(addressableId);
            var elementPrefab = asyncOperationHandle.WaitForCompletion();
            if (!elementPrefab)
            {
                Addressables.Release(asyncOperationHandle);
                throw new Exception($"Prefab for UI region element \"{addressableId}\" is not found");
            }

            var elementGameObject = m_diContainer.InstantiatePrefab(elementPrefab, parent);
            Addressables.Release(asyncOperationHandle);

            var elementComponent = elementGameObject.GetComponent<UiRegionElement>();
            if (!elementComponent)
            {
                throw new Exception($"UI region element prefab must have a \"{nameof(UiRegionElement)}\" component.");
            }

            elementComponent.AddressableId = addressableId;
            m_elementByAddressableId.Add(addressableId, elementComponent);
            return elementComponent;
        }

        public void HideIfExists(string addressableId)
        {
            if (!m_elementByAddressableId.TryGetValue(addressableId, out var element))
            {
                return;
            }

            if (element.IsCached)
            {
                element.gameObject.SetActive(false);
            }
            else
            {
                DestroyRegionElementGameObject(element);
                m_elementByAddressableId.Remove(addressableId);
            }
        }

        public void DestroyAll()
        {
            foreach (var element in m_elementByAddressableId.Values)
            {
                DestroyRegionElementGameObject(element);
            }

            m_elementByAddressableId.Clear();
        }

        public void SetSiblingIndex(string addressableId, int index)
        {
            if (!m_elementByAddressableId.TryGetValue(addressableId, out var element))
            {
                Debug.LogWarning($"Can't set sibling index for UI region element \"{addressableId}\" because it is not loaded.");
                return;
            }

            element.transform.SetSiblingIndex(index);
        }

        private static void DestroyRegionElementGameObject(UiRegionElement element)
        {
            var elementGameObject = element.gameObject;
            elementGameObject.SetActive(false);
            UnityEngine.Object.Destroy(elementGameObject);
        }
    }
}
