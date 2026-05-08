namespace GameKit.PlayerState
{
    public static partial class EncryptionKeys
    {
        public static string KeyBase64 { get; }

        public static string IvBase64 { get; }

        public static bool HasValues =>
            !string.IsNullOrWhiteSpace(KeyBase64) &&
            !string.IsNullOrWhiteSpace(IvBase64);

        static EncryptionKeys()
        {
            var keyBase64 = string.Empty;
            var ivBase64 = string.Empty;

            OverrideValues(ref keyBase64, ref ivBase64);

            KeyBase64 = keyBase64;
            IvBase64 = ivBase64;
        }

        static partial void OverrideValues(ref string keyBase64, ref string ivBase64);
    }
}
