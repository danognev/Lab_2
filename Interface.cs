using System;

namespace Lab_2
{
  class Interface
  {
    public static void InfoMess()
    {
      Console.WriteLine("Программа выполнена студентом 494 группы Огневым Даниилом," +
      "\nЗадача программы: Уметь шифровать и дешифровывать строки двумя разными методами шифрования:" +
      "\n1. Шифр Хилла\n2. Шифр Цезаря");
      UserVariants();
    }
    public static void UserVariants()
    {
      Console.WriteLine("\n\nЧто вы хотите сделать далее?\n1. Ввести данные с клавиатуры" +
      "\n2. Загрузить из файла\n3. Выйти из программы");
    }
    public static void ShowNextMenu(string type)
    {
      Console.WriteLine("\n\nЧто вы хотите сделать далее?\n1. Продолжить выполнение программы\n2. Сохранить " + type + " данные в файл");
    }
    public static void ShowEncryptionTypes()
    {
      Console.WriteLine("\n\nПожалуйста, выберите вид шифрования:\n1. Шифр Хилла\n2. Шифр Цезаря");
    }
    public static void ShowCipher() {
      Console.WriteLine("\nЧто вы хотите сделать далее?\n1. Шифровать\n2. Дешифровать");
    }
  }
}
