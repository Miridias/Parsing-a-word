using Parsing_a_word.Interface;
using System;
using System.IO;

namespace Parsing_a_word.Main_classes
{
    class Preparation : IPreparation
    {
        private string PathToFolder { get; set; }
        private DirectoryInfo directory;
        
        
        // Запрос на ввод пути
        public string ChosingAPath()
        {
            PathToFolder = Console.ReadLine();
            if (PathToFolder != "") directory = new DirectoryInfo(PathToFolder);
            if (directory == null || !directory.Exists)
            {
                Console.Write("Указаного пути не существует! Введите другой путь: ");
                PathToFolder = ChosingAPath();
            }
            return PathToFolder;
        }
    }
}
