using System;
using System.Collections.Generic;


namespace Lab_2
{
  class Algorithm
  {
    private static string sourceInput;
    private static string keyStr;
    private static int keyInt;
    private const int abcSize = 56;
    private static List<string> abc = new List<string> { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o",
            "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z","A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"," ", ".", ",", ""};
    public static string Input
    {
      set { sourceInput = value; }
      get { return sourceInput; }
    }
    public static string Key { get { return keyStr; } }
    public static int KeyInt { get { return keyInt; } }
    public static int ABCSize { get { return abcSize; } }
    public static List<string> ABC
    {
      get { return abc; }
    }
    public static void CreateString()
    {
      string input;
      input = Console.ReadLine();
      CheckInput(input);
    }
    public static void CreateKey()
    {
      string input = Console.ReadLine();
      int count = 0;
      foreach (char symbol in input)
      {
        for (int i = 0; i < abcSize; i++)
        {
          if (abc.Contains(symbol.ToString()))
          {
            count++;
            break;
          }
        }
      }
      if (count < input.Length || input.Length > Algorithm.Input.Length)
      {
        Console.WriteLine("Вы ввели некорректную строку! Попробуйте ещё раз");
        CreateKey();
      }
      else
      {
        keyStr = input;
        Console.WriteLine("Ключ для шифрования: {0}", keyStr);
      }
    }
    public static void CreateKeyInt(string input)
    {
      if (!int.TryParse(input, out keyInt) || keyInt > ABCSize-1 || keyInt < 1)
      {
        Console.WriteLine("Вы ввели некорректное значение! Попробуйте ещё раз");
        input = Console.ReadLine();
        CreateKeyInt(input);
      }
      else
      {
        Console.WriteLine("Ключ для шифрования: {0}", keyInt);
      }
    }
    public static void CheckInput(string input)
    {
      int count = 0;
      foreach (char symbol in input)
      {
        for (int i = 0; i < abcSize; i++)
        {
          if (abc.Contains(symbol.ToString()))
          {
            count++;
            break;
          }
        }
      }
      if (count < input.Length)
      {
        Console.WriteLine("Вы ввели некорректную строку! Попробуйте ещё раз");
        CreateString();
      }
      else
      {
        sourceInput = input;
        Console.WriteLine("Исходная строка: {0}", sourceInput);
      }
    }
  }
}
