using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace UnitTest
{
  [TestClass]
  public class UnitTest
  {
    [TestMethod]
    public void TestEncodeCaesar()
    {
      string source = "hello world";
      string result = "olssveDvysk";
      string key = "7";
      Lab_2.Caesar encode = new Lab_2.Caesar();
      encode.Encode(source, key);
      Assert.AreEqual(result, encode.Result);
    }
    [TestMethod]
    public void TestDecodeCaesar()
    {
      string source = "JustDoIt";
      string result = "EpnoyjDo";
      string key = "50";
      Lab_2.Caesar decode = new Lab_2.Caesar();
      decode.Decode(result, key);
      Assert.AreEqual(source, decode.Result);
    }
    [TestMethod]
    public void TestEncodeHill()
    {
      string source = "Hello World";
      string result = "B,DpAiqkFl.J";
      string key = "test";
      Lab_2.ICipher encode = new Lab_2.Hill();
      Assert.AreEqual(result, encode.Encode(source,key));
    }
    [TestMethod]
    public void TestDecodeHill()
    {
      string source = "B,DpAiqkFl.J";
      string result = "Hello World";
      string key = "test";
      Lab_2.ICipher decode = new Lab_2.Hill();
      Assert.AreEqual(result, decode.Decode(source, key));
    }
  }
}
