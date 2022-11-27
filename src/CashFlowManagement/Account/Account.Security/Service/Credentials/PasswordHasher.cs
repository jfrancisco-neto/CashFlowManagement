using System.Security.Cryptography;
using System.Text;

namespace Account.Security.Service;

internal class PasswordHasher
{
    private const string Alphabet = @"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%¨&*()_+=-[]{}\|/";

    public string CreateRandomString(int length)
    {
        var builder = new StringBuilder();

        while (builder.Length < length)
        {
            var randomIdex = Random.Shared.Next(0, Alphabet.Length);
            builder.Append(Alphabet[randomIdex]);
        }

        return builder.ToString();
    }

    public string Hash(params string[] strings)
    {
        var bytes = strings.Select(c => Encoding.Unicode.GetBytes(c)).ToList();
        var buffer = new byte[bytes.Sum(b => b.Length)];
        var offset = 0;

        foreach(var str in bytes)
        {
            Buffer.BlockCopy(str, 0, buffer, offset, str.Length);
            offset += str.Length;
        }

        using var crypto = SHA512.Create();

        return Convert.ToBase64String(crypto.ComputeHash(buffer));
    }
}
