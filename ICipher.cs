
namespace Lab_2
{
  public interface ICipher
  {
    string Encode(string input, string key);
    string Decode(string input, string key);
  }
}
