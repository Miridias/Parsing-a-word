using System.Collections.Generic;

namespace Parsing_a_word.Interface
{
    interface IParsing
    {
        public List<string> Start(List<string> allTestWords);
        public string StringValidation(string testWord);
        public (string, bool) ParsingLogic(string testWord);
        public bool Confirmation(string testWord, string wordToDictionary);
        public string CreatingString(List<string> readyString);
    }
}
