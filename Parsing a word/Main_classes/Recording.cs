using Parsing_a_word.Interface;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Parsing_a_word.Main_classes
{
    class Recording : IRecording
    {
        // Запись строк из полученой колекции в файл по указаном пути
        private string _fileName = "result-test-word.tsv";
        public void RecordingToFile(List<string> allRaightTestWords, string pathToFolderRecording)
        {
            var fileMails = new FileStream($"{pathToFolderRecording}\\{_fileName}", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var writer = new StreamWriter(fileMails, Encoding.UTF8);

            for (int i = 0; i < allRaightTestWords.Count; i++)
            {
                writer.WriteLine(allRaightTestWords[i]);
            }
            writer.Close();
            fileMails.Close();
        }
    }
}
