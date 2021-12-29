using Parsing_a_word.Interface;
using System;
using System.Collections.Generic;
using System.IO;

namespace Parsing_a_word.Main_classes
{
    class Search : ISearch
    {
        private List<string> allWordsInFile = new();
        private DirectoryInfo directory;
        private string _pathToTheFile { get; set; }
        
        // Поиск файла в папке по указаному пути
        public string SearchFile(string pathToFolder)
        {
            directory = new DirectoryInfo(pathToFolder);
            FileInfo[] files = directory.GetFiles("*.tsv");
            if (files.Length != 0)
            {
                if (files.Length > 1)
                {
                    Console.WriteLine("В указаной вами папке находится несколько файлов формата .tsv");
                    for (int i = 0; i < files.Length; i++)
                    {
                        Console.WriteLine("{0}){1}", i, files[i].Name);
                    }
                    bool specificFileSelected = false;
                    while (specificFileSelected == false)
                    {
                        Console.Write("Выберите номер файла который нужно использовать: ");
                        if (int.TryParse(Console.ReadLine(), out int fileNumber));
                        else
                        {
                            Console.WriteLine("Вы ввели не число");
                            continue;
                        }
                        if (fileNumber >= 0 && fileNumber < files.Length)
                        {
                            _pathToTheFile = files[fileNumber].FullName;
                            specificFileSelected = true;
                        }
                        else Console.WriteLine("Выход за пределы допустимого значения");
                    }
                }
                else _pathToTheFile = files[0].FullName;
                Console.WriteLine("Используется файл {0}", _pathToTheFile.ToString());
            }
            else
            {
                Console.WriteLine("По указаному пути нету файлов в формате .tsv! Укажите другой путь");
                Preparation preparation = new();
                _pathToTheFile = preparation.ChosingAPath();
                SearchFile(_pathToTheFile);
            }
            return _pathToTheFile;
        }
        // Чтение слов из файла и их запись в колекцию
        public List<string> ReadTextInFile(string pathToFolder)
        {
            string[] allWords = File.ReadAllLines(pathToFolder);
            for (int i = 0; i < allWords.Length; i++)
            {
                allWordsInFile.Add(allWords[i].ToLower());
            }
            return allWordsInFile;
        }
    }
}