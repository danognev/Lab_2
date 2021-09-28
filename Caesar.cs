using System.Collections.Generic;
using System.Linq;
using System;


namespace Lab_2
{
  public class Caesar : ICipher
  {
    private string result;
    public string Result { get { return result; } }
    public string Encode(string input, string key) {
      result = "";
      List<int> X = new List<int>();
      List<int> Z = new List<int>();
      List<string> tmp = new List<string>();
      int sum;
      Algorithm.CreateKeyInt(key);
      foreach (char symbol in input)
        X.Add(Algorithm.ABC.IndexOf(symbol.ToString()));
      for (int i = 0; i < X.Count(); i++)
      {
        sum = (Algorithm.ABCSize - 1 + X[i] + Algorithm.KeyInt) % (Algorithm.ABCSize - 1);
        Z.Add(sum);
      }
      for (int i = 0; i < Z.Count(); i++)
      {
        tmp.Add(Algorithm.ABC[Z[i]]);
      }
      for (int i = 0; i < tmp.Count(); i++)
        result += tmp[i];
      Console.WriteLine("Получилась строка: {0}", result);
      return result;
    }
    public string Decode(string input, string key) {
      result = "";
      List<int> X = new List<int>();
      List<int> Z = new List<int>();
      List<string> tmp = new List<string>();
      int sum;
      Algorithm.CreateKeyInt(key);
      foreach (char symbol in input)
        X.Add(Algorithm.ABC.IndexOf(symbol.ToString()));
      for (int i = 0; i < X.Count(); i++)
      {
        sum = (Algorithm.ABCSize - 1 + X[i] - Algorithm.KeyInt) % (Algorithm.ABCSize - 1);
        Z.Add(sum);
      }
      for (int i = 0; i < Z.Count(); i++)
      {
        tmp.Add(Algorithm.ABC[Z[i]]);
      }
      for (int i = 0; i < tmp.Count(); i++)
        result += tmp[i];
      Console.WriteLine("Получилась строка: {0}", result);
      return result;
    }
  }
}
