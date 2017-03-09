namespace TipsAndTricksLibrary.Cryptography.Rsa
{
    using System.Security.Cryptography;

    public interface IRsaKeysParser
    {
        RSAParameters ParsePrivateKey(string key);

        RSAParameters ParsePrivateKey(byte[] key);
    }
}