namespace Account.Service;

public interface IIdHasher
{
    string EncodeLong(long number);
    long DecodeLong(string values);
}
