using Parsing_a_word.Main_classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Parsing_a_word
{
    class ParserWork
    {
        public void StartWork()
        {
            Stopwatch stopwatch = new();
            Preparation preparation = new();
            Search search = new();
            Parsing parsing = new();
            Recording recording = new();
            List<string> allTestWords = new List<string>();
            List<string> allRaightTestWords = new List<string>();

            string pathToFolderTheFile;
            string pathToTheFile;
            string pathToFolderRecording;

            Console.Write("Введите путь к файлу который необходимо открыть: ");
            pathToFolderTheFile = preparation.ChosingAPath();
            pathToTheFile = search.SearchFile(pathToFolderTheFile);
            Console.Write("Введите путь куда необходимо записать файл: ");
            pathToFolderRecording = preparation.ChosingAPath();
            stopwatch.Start();

            allTestWords = search.ReadTextInFile(pathToTheFile);
            allRaightTestWords = parsing.Start(allTestWords);
            recording.RecordingToFile(allRaightTestWords, pathToFolderRecording);

            stopwatch.Stop();
            Console.WriteLine($"Время работы программы составило: {stopwatch.Elapsed.Minutes} min " +
                $"{stopwatch.Elapsed.Seconds} sec " +
                $"{stopwatch.Elapsed.Milliseconds} msec.");
            Console.ReadLine();
        }
    }
}
