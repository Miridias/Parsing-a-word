using System.Collections.Generic;

namespace Parsing_a_word.Interface
{
    interface ISearch
    {
        public string SearchFile(string pathToFolder);
        public List<string> ReadTextInFile(string pathToFolder);
    }
}
