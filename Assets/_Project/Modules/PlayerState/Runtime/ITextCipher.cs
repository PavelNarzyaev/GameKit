namespace GameKit.PlayerState
{
    public interface ITextCipher
    {
        string Encrypt(string plainText);
        string Decrypt(string cipherText);
    }
}
