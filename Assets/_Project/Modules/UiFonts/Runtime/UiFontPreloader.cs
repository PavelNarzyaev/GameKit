using System;
using GameKit.UiFonts.Contracts;
using JetBrains.Annotations;
using TMPro;
using UnityEngine.AddressableAssets;

namespace GameKit.UiFonts
{
    [UsedImplicitly]
    public class UiFontPreloader : IUiFontPreloader
    {
        private bool m_isPreloaded;

        /// <summary>
        /// Workaround for an Android/TMP runtime issue where UI text loaded from Addressable
        /// can lose its shared font material unless the font asset is preloaded first.
        /// </summary>
        public void Preload()
        {
            if (m_isPreloaded)
            {
                return;
            }

            var fontAddresses = new []
            {
                "RobotoMono-Medium"
            };

            foreach (var fontAddress in fontAddresses)
            {
                var fontHandle = Addressables.LoadAssetAsync<TMP_FontAsset>(fontAddress);
                var fontAsset = fontHandle.WaitForCompletion();
                if (!fontAsset)
                {
                    throw new InvalidOperationException($"TMP font address \"{fontAddress}\" is not found.");
                }
            }

            m_isPreloaded = true;
        }
    }
}
