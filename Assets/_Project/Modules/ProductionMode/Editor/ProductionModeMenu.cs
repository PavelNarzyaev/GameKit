using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build;

namespace GameKit.ProductionMode.Editor
{
    [UsedImplicitly]
    public class ProductionModeMenu
    {
        private const string k_IsProduction = "IS_PRODUCTION";

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

        private static void ChangeProductionModeDefine(bool shouldEnableProduction)
        {
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
