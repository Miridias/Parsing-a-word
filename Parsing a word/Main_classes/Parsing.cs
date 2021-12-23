using Parsing_a_word.Interface;
using System.Collections.Generic;
using System.Text;

namespace Parsing_a_word.Main_classes
{
    class Parsing : IParsing
    {
        private List<string> _allDictionaryWords;
        // Поочередная отправка проверочных слов на обработку
        public List<string> Start(List<string> allTestWords)
        {
            List<string> rightWords = new List<string>();
            for (int i = 0; i < allTestWords.Count; i++)
            {
                rightWords.Add(StringValidation(allTestWords[i]));
            }
            return rightWords;
        }
        // Проверка слова на наличие его в словаре или его разбиение на части
        public string StringValidation(string testWord)
        {
            List<string> readyString = new();
            readyString.Add(testWord);
            int lengthConcretWord = 0;
            (string, bool) result;
            string newWord = testWord;

            while (lengthConcretWord < testWord.Length)
            {
                result = ParsingLogic(newWord);
                if (result.Item2 == true)
                {
                    readyString.Add(result.Item1);
                    lengthConcretWord += result.Item1.Length;
                    newWord = testWord.Remove(0, lengthConcretWord);
                }
                else
                {
                    readyString.Clear();
                    readyString.Add(testWord);
                    lengthConcretWord = testWord.Length;
                }
            }
            return CreatingString(readyString);
        }
        // Сравнение проверочного слова из словами в словаре
        public (string, bool) ParsingLogic(string testWord)
        {
            List<string> dictionaryGeneration = _allDictionaryWords;
            List<string> newDictionaryGeneration = new();
            List<string> interimList = new();
            StringBuilder sb = new();
            string newWord = null;
            int minLvl = 0;
            for (int i = 0; i < testWord.Length; i++)
            {
                // Цыкл который в процессе производит сравнение символа находящегося под индексом (i) в слове которое принимает этот метод из символом по такоему-же индексу но в слове из определенной генерации словаря
                for (int j = 0; j < dictionaryGeneration.Count; j++)
                {
                    sb.Append(dictionaryGeneration[j]);//.ToCharArray();
                    // Если символ по индексу соответствует тогда записываем всё слово в новую генерацию словаря
                    if (sb.Length <= testWord.Length && sb.Length > i && sb[i] == testWord[i])
                    {
                        newDictionaryGeneration.Add(dictionaryGeneration[j]);
                    }
                    // Или проверяем не нашли ли мы максимально длинное слово из словаря которое соответствует части нашего слова
                    else if (sb.Length == i - minLvl)
                    {
                        if (minLvl > 0)
                        {
                            if (sb[i - minLvl] == testWord[i - minLvl]) interimList.Add(dictionaryGeneration[j]);
                        }
                        else interimList.Add(dictionaryGeneration[j]);
                    }
                    sb.Clear();
                }
                if (newDictionaryGeneration.Count == 1)
                {
                    newWord = newDictionaryGeneration[0];
                    if (newWord != null) return (newWord, true);
                    else return (testWord, false);
                }
                // Проверка найденой части слова на соответствие и передача его из метода
                else if (newDictionaryGeneration.Count == 0)
                {
                    // Если в промежуточном списке нету записей тогда снижаем уровень записи
                    // Если не нашли соответсвующее слово нужной/максимальной длины 
                    if (interimList.Count == 0)
                    {
                        i = -1;
                        minLvl--;
                        newDictionaryGeneration = _allDictionaryWords.GetRange(0, _allDictionaryWords.Count);
                    }
                    // иначе проверяем то что у нас есть в промежуточном листе на соответствие и передаем из метода
                    else
                    {
                        for (int k = interimList.Count - 1; k >= 0; k--)
                        {
                            if (Confirmation(testWord, interimList[k]))
                            {
                                newWord = interimList[interimList.Count - 1];
                                if (newWord != null) return (newWord, true);
                                else return (testWord, false);
                            }
                        }
                        if (newWord != null) return (newWord, true);
                        else return (testWord, false);
                    }
                    newDictionaryGeneration = _allDictionaryWords.GetRange(0, _allDictionaryWords.Count);
                }
                dictionaryGeneration = newDictionaryGeneration.GetRange(0, newDictionaryGeneration.Count);
                newDictionaryGeneration.Clear();
            }
            if (newWord != null) return (newWord, true);
            else return (testWord, false);
        }
        // Проверка отобраных слов из словаря на соответствие части проверочного слова
        public bool Confirmation(string testWord, string wordToDictionary)
        {
            for (int i = 0; i < wordToDictionary.Length; i++)
            {
                if (wordToDictionary[i] != testWord[i])
                {
                    return false;
                }
            }
            return true;
        }
        // Преобразование полученой колекции в строку
        public string CreatingString(List<string> readyString)
        {
            StringBuilder resultString = new StringBuilder();
            resultString.Append($"(in) {readyString[0]} -> (out) ");
            if (readyString.Count == 2 || readyString.Count == 1) resultString.Append($"{readyString[0]} // слово, которое невозможно разбить");
            else
            {
                for (int i = 1; i < readyString.Count; i++)
                {
                    if (i < readyString.Count - 1) resultString.Append($"{readyString[i]}, ");
                    else resultString.Append($"{readyString[i]} // слово которое смогли разбить");
                }
            }
            return resultString.ToString();
        }
        public Parsing()
        {
            string pathToFolder = @"..//..//..//Static_Files//de-dictionary.tsv";
            Search search = new();
            _allDictionaryWords = search.ReadTextInFile(pathToFolder);
        }
    }
}
