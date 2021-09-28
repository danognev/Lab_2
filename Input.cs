using System;
using static System.Math;

namespace Lab_2
{
  class Input
  {
    private static int choice = 0;
    private static string input;
    private enum Menu { keyboard = 1, fromFile, close };
    private enum NextMenu { work = 1, save };
    private enum FinalMenu { end = 1, saveres };
    private enum cipher { encode = 1, decode };
    private enum Encryption { hill = 1, caesar };
    public static int Choice { get { return choice; } }
    public static void MainMenu()
    {
      CheckInput();
      switch (choice)
      {
        case (int)Menu.keyboard:
          Console.WriteLine("Введите исходную строку\nВнимание!\nСтрока должна состоять только из букв английского алфавита!");
          Algorithm.CreateString();
          Interface.ShowNextMenu("исходные");
          CheckNextChoice();
          break;
        case (int)Menu.fromFile:
          {
            Files.ReadFile(); 
            Interface.ShowCipher();
            CipherChoice();
            break;
          }
        case (int)Menu.close: { break; }

        default:
          Console.WriteLine("Вы выбрали неверный пункт меню! Попробуйте ещё раз");
          MainMenu();
          break;
      }
    }
    public static void CheckNextChoice()
    {
      CheckInput();
      switch (choice)
      {
        case (int)NextMenu.work:
          {
            Interface.ShowCipher();
            CipherChoice();
            break;
          }
        case (int)NextMenu.save:
          {
            Files.GetFileWay((int)Files.SaveType.source, Algorithm.Input);
            Interface.ShowCipher();
            CipherChoice();
            break;
          }
        default:
          {
            Console.WriteLine("Вы выбрали неверный пункт меню! Попробуйте ещё раз");
            CheckNextChoice();
            break;
          }
      }
    }
    private static void CipherChoice()
    {
      CheckInput();
      switch (choice)
      {
        case (int)cipher.encode: {
            Interface.ShowEncryptionTypes();
            EncryptionChoice((int)cipher.encode);
            break;
          }
        case (int)cipher.decode: {
            Interface.ShowEncryptionTypes();
            EncryptionChoice((int)cipher.decode);
            break;
          }
        default:
          {
            Console.WriteLine("Вы выбрали неверный пункт меню! Попробуйте ещё раз");
            CipherChoice();
            break;
          }
      }
    }
    public static void EncryptionChoice(int type)
    {
      CheckInput();
      switch (choice)
      {
        case (int)Encryption.hill:
          {
            switch(type) {
              case (int)cipher.encode: {
                  Console.WriteLine("\nВведите ключ для шифрования Хилла!\nВнимание!\n" +
                  "Ключ должен быть английского алфавита и длинной равному квадрату целого числа\n"+
                  "и не превышающего размер в {0} символов", Pow((int)Sqrt(Algorithm.Input.Length), 2));
                  while (true)
                  {
                    Algorithm.CreateKey();
                    if (Sqrt(Algorithm.Key.Length) % 1 != 0 || Algorithm.Key.Length > Pow((int)Sqrt(Algorithm.Input.Length), 2))
                    {
                      Console.WriteLine("Ключ должен быть длинной равному квадрату целого числа!");
                      Console.WriteLine("И не превышать размер в {0} символов", Pow((int)Sqrt(Algorithm.Input.Length), 2));
                      Console.WriteLine("Попробуйте ещё раз!");
                      continue;
                    }
                    else break;
                  }
                  ICipher cipher = new Hill();
                  string Result = cipher.Encode(Algorithm.Input, Algorithm.Key);
                  Console.WriteLine("Исходная строка: {0} | Ключ: {1}", Algorithm.Input, Algorithm.Key);
                  Interface.ShowNextMenu("полученные");
                  CheckFinalChoice(Result);
                  break;
                }
              case (int)cipher.decode: {
                  Console.WriteLine("\nВведите ключ для дешифрования Хилла!\nВнимание!\n" +
                  "Ключ должен быть английского алфавита и длинной равному квадрату целого числа\n" +
                  "и не превышающего размер в {0} символов", Pow((int)Sqrt(Algorithm.Input.Length),2));
                  while (true)
                  {
                    Algorithm.CreateKey();
                    if (Sqrt(Algorithm.Key.Length) % 1 != 0 || Algorithm.Key.Length > Pow((int)Sqrt(Algorithm.Input.Length), 2))
                    {
                      Console.WriteLine("Ключ должен быть длинной равному квадрату целого числа!");
                      Console.WriteLine("И не превышать размер в {0} символов", Pow((int)Sqrt(Algorithm.Input.Length), 2));
                      Console.WriteLine("Попробуйте ещё раз!");
                      continue;
                    }
                    else break;
                  }
                  ICipher cipher = new Hill();
                  string Result = cipher.Decode(Algorithm.Input, Algorithm.Key);
                  Console.WriteLine("Исходная строка: {0} | Ключ: {1}", Algorithm.Input, Algorithm.Key);
                  Interface.ShowNextMenu("полученные");
                  CheckFinalChoice(Result);
                  break;
                }
            }
            break;
          }
        case (int)Encryption.caesar:
          {
            switch(type) {
              case (int)cipher.encode: {
                  Console.WriteLine("\nВведите ключ для шифрования шифром Цезаря\nВнимание!\n" +
                  "Ключ должен быть целым числом не более {0}!", Algorithm.ABCSize - 1);
                  string input = Console.ReadLine();
                  ICipher cipher = new Caesar();
                  string Result = cipher.Encode(Algorithm.Input, input);
                  Console.WriteLine("Исходная строка: {0} | Ключ: {1}", Algorithm.Input, Algorithm.KeyInt);
                  Interface.ShowNextMenu("полученные");
                  CheckFinalChoice(Result);
                  break;
                }
              case (int)cipher.decode: {
                  Console.WriteLine("\nВведите ключ для дешифрования шифром Цезаря\nВнимание!\n" +
                  "Ключ должен быть целым числом не более {0}!", Algorithm.ABCSize - 1);
                  string input = Console.ReadLine();
                  ICipher cipher = new Caesar();
                  string Result = cipher.Decode(Algorithm.Input, input);
                  Console.WriteLine("Исходная строка: {0} | Ключ: {1}", Algorithm.Input, Algorithm.KeyInt);
                  Interface.ShowNextMenu("полученные");
                  CheckFinalChoice(Result);
                  break;
                }
            }
            break;
          }
        default:
          {
            Console.WriteLine("Вы выбрали неверный пункт меню! Попробуйте ещё раз");
            EncryptionChoice(type);
            break;
          }
      }
    }
    private static void CheckFinalChoice(string result)
    {
      CheckInput();
      switch (choice)
      {
        case (int)FinalMenu.end:
          {
            Interface.UserVariants();
            MainMenu();
            break;
          }
        case (int)FinalMenu.saveres:
          {
            Files.GetFileWay((int)Files.SaveType.result, result);
            Interface.UserVariants();
            MainMenu();
            break;
          }
        default:
          {
            Console.WriteLine("Вы выбрали неверный пункт меню! Попробуйте ещё раз");
            CheckFinalChoice(result);
            break;
          }
      }
    }
    public static void CheckInput()
    {
      input = Console.ReadLine();
      ConvertToInt(input);
    }
    public static void ConvertToInt(string input)
    {
      if (!int.TryParse(input, out choice))
      {
        Console.WriteLine("Вы ввели некорректные данные. Попробуйте ещё раз!");
        CheckInput();
      }
    }
  }
}
