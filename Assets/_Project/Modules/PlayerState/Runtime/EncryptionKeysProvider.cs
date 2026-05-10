using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class EncryptionKeysProvider : IEncryptionKeysProvider
    {
        public bool HasValues => EncryptionKeys.HasValues;
    }
}
