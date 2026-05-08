using System;
using System.IO;
using System.Security.Cryptography;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace GameKit.PlayerState.Editor
{
    [UsedImplicitly]
    public class EncryptionKeysWindow : EditorWindow
    {
        private const string k_WindowTitle = "Encryption Keys";
        private const string k_FileRelativePathFromModuleRoot = "Runtime/Constants/EncryptionKeys.Generated.cs";
        private const string k_DocumentationPath = "Documents/GameKit/Engineering/encryption-keys-editor-window.md";
        private const int k_KeySizeBytes = 32;
        private const int k_IvSizeBytes = 16;
        private string m_draftKeyBase64;
        private string m_draftIvBase64;

        [MenuItem("GameKit/Encryption Keys", false, 102)]
        public static void Open()
        {
            var window = GetWindow<EncryptionKeysWindow>();
            window.titleContent = new GUIContent(k_WindowTitle);
            window.minSize = new Vector2(520f, 260f);
        }

        private void OnGUI()
        {
            var filePath = GetKeysFileAssetPath();

            EditorGUILayout.LabelField("Encryption Keys", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(
                "This tool stores save-encryption settings in a git-ignored file: " + filePath + ".",
                MessageType.Info);
            EditorGUILayout.HelpBox(
                "BE CAREFUL! THESE VALUES DON'T HAVE BACKUPS IN YOUR REPOSITORY! READ DOCUMENTATION FOR ADDITIONAL DETAILS.",
                MessageType.Warning);

            if (GUILayout.Button("Open documentation"))
            {
                OpenDocumentation();
            }

            EditorGUILayout.Space();
            DrawDraftBlock();
            EditorGUILayout.Space();
            DrawCurrentValuesBlock();
        }

        private void DrawDraftBlock()
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("DRAFT VALUES", EditorStyles.boldLabel);
                EditorGUILayout.LabelField("Key (Base64, 32 bytes)");
                m_draftKeyBase64 = EditorGUILayout.TextField(m_draftKeyBase64 ?? string.Empty);

                var isKeyValid = TryValidateBase64(m_draftKeyBase64, k_KeySizeBytes, out var keyWarningMessage);
                DrawValueValidationStatus(isKeyValid, keyWarningMessage);

                EditorGUILayout.LabelField("IV (Base64, 16 bytes)");
                m_draftIvBase64 = EditorGUILayout.TextField(m_draftIvBase64 ?? string.Empty);

                var isIvValid = TryValidateBase64(m_draftIvBase64, k_IvSizeBytes, out var ivWarningMessage);
                DrawValueValidationStatus(isIvValid, ivWarningMessage);

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Generate"))
                    {
                        ClearTextFieldFocus();
                        GenerateNewValues();
                    }

                    var canSave = isKeyValid && isIvValid;
                    EditorGUI.BeginDisabledGroup(!canSave);
                    if (GUILayout.Button("Save"))
                    {
                        SaveToFile();
                    }
                    EditorGUI.EndDisabledGroup();
                }
            }
        }

        private void DrawValueValidationStatus(bool isValid, string warningMessage)
        {
            if (!isValid)
            {
                EditorGUILayout.HelpBox(warningMessage, MessageType.Warning);
            }
            else
            {
                EditorGUILayout.HelpBox("Value is correct", MessageType.Info);
            }
        }

        private static void ClearTextFieldFocus()
        {
            GUI.FocusControl(null);
            EditorGUIUtility.editingTextField = false;
            GUIUtility.keyboardControl = 0;
        }

        private void GenerateNewValues()
        {
            using var aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();
            aes.GenerateIV();

            m_draftKeyBase64 = Convert.ToBase64String(aes.Key);
            m_draftIvBase64 = Convert.ToBase64String(aes.IV);
        }

        private void SaveToFile()
        {
            var assetPath = GetKeysFileAssetPath();
            var absolutePath = GetAbsoluteProjectPath(assetPath);
            var directoryPath = Path.GetDirectoryName(absolutePath);
            if (!string.IsNullOrEmpty(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            File.WriteAllText(absolutePath, BuildFileContent(m_draftKeyBase64, m_draftIvBase64));
            AssetDatabase.Refresh();
        }

        private void DrawCurrentValuesBlock()
        {
            using (new EditorGUILayout.VerticalScope(EditorStyles.helpBox))
            {
                EditorGUILayout.LabelField("CURRENT VALUES", EditorStyles.boldLabel);

                EditorGUILayout.LabelField("Current key");
                EditorGUILayout.SelectableLabel(
                    GetDisplayValue(EncryptionKeys.KeyBase64),
                    EditorStyles.textField,
                    GUILayout.Height(EditorGUIUtility.singleLineHeight));

                EditorGUILayout.LabelField("Current IV");
                EditorGUILayout.SelectableLabel(
                    GetDisplayValue(EncryptionKeys.IvBase64),
                    EditorStyles.textField,
                    GUILayout.Height(EditorGUIUtility.singleLineHeight));

                if (!EncryptionKeys.HasValues)
                {
                    EditorGUILayout.HelpBox(
                        "Encryption keys file is missing or empty.",
                        MessageType.Warning);
                }
            }
        }

        private static string GetDisplayValue(string value)
        {
            return string.IsNullOrWhiteSpace(value) ? "<empty>" : value;
        }

        private static bool TryValidateBase64(string base64Value, int expectedLength, out string warningMessage)
        {
            if (string.IsNullOrWhiteSpace(base64Value))
            {
                warningMessage = $"Value is required and must decode to {expectedLength} bytes.";
                return false;
            }

            try
            {
                var bytes = Convert.FromBase64String(base64Value);
                if (bytes.Length != expectedLength)
                {
                    warningMessage = $"Value must decode to {expectedLength} bytes, but decodes to {bytes.Length}.";
                    return false;
                }
            }
            catch (FormatException)
            {
                warningMessage = "Value must be a valid Base64 string.";
                return false;
            }

            warningMessage = string.Empty;
            return true;
        }

        private static string BuildFileContent(string keyBase64, string ivBase64)
        {
            return
$@"//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by the GameKit Encryption Keys tool.
//     Changes to this file will be overwritten the next time the keys are saved.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GameKit.PlayerState
{{
    public static partial class EncryptionKeys
    {{
        static partial void OverrideValues(ref string keyBase64, ref string ivBase64)
        {{
            keyBase64 = ""{EscapeForCSharp(keyBase64)}"";
            ivBase64 = ""{EscapeForCSharp(ivBase64)}"";
        }}
    }}
}}
";
        }

        private static string EscapeForCSharp(string value)
        {
            return value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        private string GetKeysFileAssetPath()
        {
            var script = MonoScript.FromScriptableObject(this);
            var scriptPath = AssetDatabase.GetAssetPath(script);
            var editorDirectoryPath = Path.GetDirectoryName(scriptPath);
            var moduleRootPath = Path.GetDirectoryName(editorDirectoryPath);

            return Path.Combine(moduleRootPath ?? string.Empty, k_FileRelativePathFromModuleRoot).Replace("\\", "/");
        }

        private static string GetAbsoluteProjectPath(string assetPath)
        {
            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrEmpty(projectRootPath))
            {
                throw new InvalidOperationException("Project root path could not be resolved.");
            }

            return Path.GetFullPath(Path.Combine(projectRootPath, assetPath));
        }

        private static void OpenDocumentation()
        {
            var projectRootPath = Directory.GetParent(Application.dataPath)?.FullName;
            if (string.IsNullOrEmpty(projectRootPath))
            {
                Debug.LogWarning("Project root path could not be resolved.");
                return;
            }

            var absolutePath = Path.Combine(projectRootPath, k_DocumentationPath);
            if (!File.Exists(absolutePath))
            {
                Debug.LogWarning($"Documentation file was not found at {absolutePath}.");
                return;
            }

            UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(absolutePath, 1);
        }
    }
}
