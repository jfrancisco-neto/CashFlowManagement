using Account.Security.Options;
using HashidsNet;

namespace Account.Security.Service;

public class IdHasher : IIdHasher
{
    private readonly Hashids _encoder;

    public IdHasher(IdHasherOptions options)
    {
        _encoder = new Hashids(alphabet: options.Alphabet, salt: options.Salt);
    }

    public long? DecodeLong(string values)
    {
        if (values.Length <= 1 || values[0] != 'i')
        {
            throw new ArgumentException("Invalid id.");
        }

        return _encoder.DecodeLong(values.Substring(1, values.Length -1)).FirstOrDefault();
    }

    public string EncodeLong(long number)
    {
        return "i" + _encoder.EncodeLong(number);
    }
}
