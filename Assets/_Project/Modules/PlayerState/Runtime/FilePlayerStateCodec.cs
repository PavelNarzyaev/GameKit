using GameKit.PlayerState.Contracts;
using JetBrains.Annotations;

namespace GameKit.PlayerState
{
    [UsedImplicitly]
    public class FilePlayerStateCodec : IPlayerStateCodec
    {
        private readonly IPlayerStateSerializer m_playerStateSerializer;
        private readonly ITextCipher m_textCipher;

        public FilePlayerStateCodec(IPlayerStateSerializer playerStateSerializer, ITextCipher textCipher)
        {
            m_playerStateSerializer = playerStateSerializer;
            m_textCipher = textCipher;
        }

        public string Encode(PlayerStateDto state)
        {
            return m_textCipher.Encrypt(m_playerStateSerializer.Serialize(state));
        }

        public PlayerStateDto Decode(string payload)
        {
            return m_playerStateSerializer.Deserialize(m_textCipher.Decrypt(payload));
        }
    }
}
