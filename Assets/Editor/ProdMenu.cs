using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Build;

namespace Editor
{
    [UsedImplicitly]
    public class ProdMenu
    {
        private const string CProd = "PROD";

#if !PROD
        [MenuItem("GameKit/Enable PROD", false, 1)]
        public static void EnableProd()
        {
            ChangeProdDefine(true);
        }
#else
        [MenuItem("GameKit/Disable PROD", false, 1)]
        public static void DisableProd()
        {
            ChangeProdDefine(false);
        }
#endif

        private static void ChangeProdDefine(bool targetDefine)
        {
            var defines = GetDefines();
            if (defines.Contains(CProd) == targetDefine)
            {
                return;
            }

            if (defines.Contains(CProd))
            {
                defines.Remove(CProd);
            }
            else
            {
                defines.Add(CProd);
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
