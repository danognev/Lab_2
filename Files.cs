using System;
using System.IO;

namespace Lab_2
{
  class Files
  {
    enum SaveVar { rewrite = 1, newfile };
    public enum SaveType { source = 1, result };
    public static void ReadFile()
    {
      string way;
      while (true)
      {
        Console.WriteLine("Введите путь по которому расположен необходимый файл");
        way = Console.ReadLine();
        if (!File.Exists(way))
        {
          Console.WriteLine("Ошибка! По данному пути не найден файл. Попробуйте ещё раз!");
          continue;
        }
        else { break; }
      }
      try
      {
        using (StreamReader rfile = new StreamReader(way))
        {
          string input = rfile.ReadLine();
          int count = 0;
          foreach (char symbol in input)
          {
            for (int i = 0; i < Algorithm.ABCSize; i++)
            {
              if (Algorithm.ABC.Contains(symbol.ToString()))
              {
                count++;
                break;
              }
            }
          }
          if (count < input.Length)
          {
            Console.WriteLine("Данный файл содержит некорректные данные!");
            Interface.UserVariants();
            Input.MainMenu();
          }
          else
          {
            Algorithm.Input = input;
            Console.WriteLine("Загруженный текст: {0}", Algorithm.Input);
          }
        }
      }
      catch (Exception ex)
      {
        Console.WriteLine("Возникла ошибка при чтении файла! {0}",ex.Message);
        Interface.UserVariants();
        Input.MainMenu();
      }
    }
    public static void GetFileWay(int type, string result)
    {
      Console.WriteLine("Введите путь для сохранения файла");
      string way = Console.ReadLine();
      if (File.Exists(way))
      {
        Console.WriteLine("Файл по такому пути уже существует!\nЧто вы хотите сделать далее?");
        Console.WriteLine("1. Перезаписать в файл\n2. Создать новый");
        Input.CheckInput();
        while (Input.Choice < (int)SaveVar.rewrite || Input.Choice > (int)SaveVar.newfile)
        {
          Console.WriteLine("Ошибка! Вы ввели неверный пункт меню. Попробуйте ещё раз!");
          Input.CheckInput();
        }
        switch (Input.Choice)
        {
          case (int)SaveVar.rewrite:
            {
              TryToSaveFile(type, way, result);
              break;
            }
          case (int)SaveVar.newfile:
            {
              GetFileWay(type, result);
              break;
            }
        }
      }
      else
        TryToSaveFile(type, way, result);
    }
    public static void TryToSaveFile(int type, string way, string text)
    {
      try
      {
        using (StreamWriter file = new StreamWriter(way, false))
        {
          switch (type)
          {
            case (int)SaveType.source:
              {
                file.Write(text);
                file.Close();
                break;
              }
            case (int)SaveType.result:
              {
                file.Write(text);
                file.Close();
                break;
              }
          }
        }
        Console.WriteLine("Запись успешно выполнена по пути: " + way);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        Console.WriteLine("Попробуйте ещё раз!");
        GetFileWay(type,text);
      }
    }
  }
}
