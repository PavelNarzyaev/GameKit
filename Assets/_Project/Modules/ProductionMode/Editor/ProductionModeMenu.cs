using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace GameKit.ProductionMode.Editor
{
    [UsedImplicitly]
    public static class ProductionModeMenu
    {
        private const string k_IsProduction = "IS_PRODUCTION";
        private const string k_PrefabSearchRoot = "Assets/_Project";
        private const string k_DebugUiRegionElementMarker = "\n  isDebug: 1";
        private const string k_AddressableSettingsTypeName =
            "UnityEditor.AddressableAssets.AddressableAssetSettingsDefaultObject, Unity.Addressables.Editor";

#if !IS_PRODUCTION
        [MenuItem("GameKit/Enable Production Mode", false, 1)]
        public static void EnableProductionMode()
        {
            ChangeProductionModeDefine(true);
        }
#else
        [MenuItem("GameKit/Disable Production Mode", false, 1)]
        public static void DisableProductionMode()
        {
            ChangeProductionModeDefine(false);
        }
#endif

        [MenuItem("GameKit/Sync Production Addressables", false, 2)]
        public static void SyncProductionAddressables()
        {
            SyncDebugAddressables(IsProductionDefineEnabled());
        }

        private static void ChangeProductionModeDefine(bool shouldEnableProduction)
        {
            SyncDebugAddressables(shouldEnableProduction);

            var defines = GetDefines();
            if (defines.Contains(k_IsProduction) == shouldEnableProduction)
            {
                return;
            }

            if (defines.Contains(k_IsProduction))
            {
                defines.Remove(k_IsProduction);
            }
            else
            {
                defines.Add(k_IsProduction);
            }

            SetDefines(defines);
        }

        private static void SyncDebugAddressables(bool shouldEnableProduction)
        {
            var settings = GetAddressableSettings();
            var debugPrefabGuids = CollectDebugUiRegionElementPrefabGuids();
            var changedEntryCount = 0;

            if (shouldEnableProduction)
            {
                foreach (var debugPrefabGuid in debugPrefabGuids)
                {
                    if (RemoveAddressableEntry(settings, debugPrefabGuid))
                    {
                        changedEntryCount++;
                    }
                }
            }
            else
            {
                var defaultGroup = GetDefaultAddressableGroup(settings);
                foreach (var debugPrefabGuid in debugPrefabGuids)
                {
                    var entry = FindAddressableEntry(settings, debugPrefabGuid);
                    if (entry == null)
                    {
                        entry = CreateOrMoveAddressableEntry(settings, debugPrefabGuid, defaultGroup);
                        changedEntryCount++;
                    }

                    var prefabPath = AssetDatabase.GUIDToAssetPath(debugPrefabGuid);
                    SetAddressableEntryAddress(entry, Path.GetFileNameWithoutExtension(prefabPath));
                }
            }

            if (changedEntryCount == 0)
            {
                Debug.Log($"Production Addressables are already synced for {(shouldEnableProduction ? "production" : "debug")} mode.");
                return;
            }

            AssetDatabase.SaveAssets();
            Debug.Log($"Synced {changedEntryCount} debug Addressable entries for {(shouldEnableProduction ? "production" : "debug")} mode.");
        }

        private static object GetAddressableSettings()
        {
            var settingsType = Type.GetType(k_AddressableSettingsTypeName, true);
            var settings = settingsType.GetProperty("Settings")?.GetValue(null);
            return settings ?? throw new InvalidOperationException("Addressable asset settings are not configured.");
        }

        private static object GetDefaultAddressableGroup(object settings)
        {
            var defaultGroup = settings.GetType().GetProperty("DefaultGroup")?.GetValue(settings);
            return defaultGroup ?? throw new InvalidOperationException("Default Addressables group is not configured.");
        }

        private static object FindAddressableEntry(object settings, string guid)
        {
            return settings.GetType().GetMethod("FindAssetEntry", new[] { typeof(string) })?.Invoke(settings, new object[] { guid });
        }

        private static bool RemoveAddressableEntry(object settings, string guid)
        {
            var method = settings.GetType().GetMethod("RemoveAssetEntry", new[] { typeof(string), typeof(bool) });
            return (bool)method.Invoke(settings, new object[] { guid, false });
        }

        private static object CreateOrMoveAddressableEntry(object settings, string guid, object defaultGroup)
        {
            var method = settings.GetType().GetMethod(
                "CreateOrMoveEntry",
                new[] { typeof(string), defaultGroup.GetType(), typeof(bool), typeof(bool) });

            return method.Invoke(settings, new[] { guid, defaultGroup, false, false });
        }

        private static void SetAddressableEntryAddress(object entry, string address)
        {
            entry.GetType().GetMethod("SetAddress", new[] { typeof(string), typeof(bool) })?.Invoke(entry, new object[] { address, false });
        }

        private static List<string> CollectDebugUiRegionElementPrefabGuids()
        {
            return AssetDatabase.FindAssets("t:Prefab", new[] { k_PrefabSearchRoot })
                .Where(IsDebugUiRegionElementPrefab)
                .OrderBy(guid => AssetDatabase.GUIDToAssetPath(guid))
                .ToList();
        }

        private static bool IsDebugUiRegionElementPrefab(string prefabGuid)
        {
            var prefabPath = AssetDatabase.GUIDToAssetPath(prefabGuid);
            var prefabText = File.ReadAllText(prefabPath);
            return prefabText.Contains(k_DebugUiRegionElementMarker);
        }

        private static bool IsProductionDefineEnabled()
        {
            return GetDefines().Contains(k_IsProduction);
        }

        private static List<string> GetDefines()
        {
            return PlayerSettings.GetScriptingDefineSymbols(GetCurrentNamedBuildTarget()).Split(';').ToList();
        }

        private static void SetDefines(IEnumerable<string> defines)
        {
            PlayerSettings.SetScriptingDefineSymbols(GetCurrentNamedBuildTarget(), string.Join(";", defines));
        }

        private static NamedBuildTarget GetCurrentNamedBuildTarget()
        {
            var buildTarget = EditorUserBuildSettings.selectedBuildTargetGroup;
            return NamedBuildTarget.FromBuildTargetGroup(buildTarget);
        }
    }
}
