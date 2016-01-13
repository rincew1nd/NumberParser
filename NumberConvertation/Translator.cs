using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Linq;

namespace NumberConvertation
{
    public class Translator
    {
        public readonly Dictionary<string, string> ErrorDic;
        private readonly Dictionary<string, int> NumberDic;
        private readonly Dictionary<int, string> StarorusskiiDic;
        private readonly List<string> AfterWords;
        private readonly List<string> DozenWords;
        private readonly List<string> UnitsWords;
        private Dictionary<string, string> ResultDic;

        public Translator()
        {
            ErrorDic = new Dictionary<string, string>{
                // Проверки строки
                {"empty" , "Пустая строка"},
                {"spaces" , "В строке только пробелы"},
                {"undefinedsymbols" , "Строка содержит не только латинские символы"},
                // Проверки слов
                {"notfinded" , "Не удалось распознать слово "},
                {"hundreds" , "hundreds - существительное, а не число."},
                // Проверки слов между дэшей
                {"toomanydashes" , "Тире разделяет боллее двух слов.\r\n"},
                {"wrongdash" , "Не удалось распознать слово \r\n"},
                {"dashedword" , "Тире разделяет неверные слова.\r\n"},
                // Проверка слов между and и zero
                {"wrongand" , "Неверные слова между And.\r\n"},
                {"lastand" , "And находится в конце.\r\n"},
                {"firstand" , "And находится в начале.\r\n"},
                {"zero" , "Ничего не может идти перед и после 0."},
                // Новые проверки
                {"hundredAfterHundred" , "Два слова hundred подряд."},
                {"afterWordsAfterAfterWords" , "Два числа формата 10-19 подряд.\r\n"},
                {"dozenWordsAfterAfterWords" , "Числа десятичного формата после чисел формата 10-19.\r\n"},
                {"hundredAfterAfterWords" , "hundred после чисел формата 10-19.\r\n"},
                {"unitsWordsAfterDozenWords" , "Числа единичного формата после чисел десятичного формата.\r\n"},
                {"afterWordsAfterDozenWords" , "Числа формата 10-19 после чисел десятичного формата.\r\n"},
                {"dozenWordsAfterDozenWords" , "Два числа десятичного формата подряд.\r\n"},
                {"hundredAfterDozenWords" , "hundred после чисел десятичного формата.\r\n"},
                {"unitsWordsAfterUnitsWords" , "Два числа единичного формата подряд.\r\n"},
                {"afterWordsAfterUnitsWords" , "Числа десятичного формата после чисел единичного формата.\r\n"},
                {"dozenWordsAfterUnitsWords" , "Числа формата 10-19 после чисел единичного формата.\r\n"},
            };

            #region Словари

            AfterWords = new List<string> {
                "twenty", "thirty", "forty", "fifty", "sixty", "seventy",
                "eighty", "ninety"
            };
            DozenWords = new List<string> {
                "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen",
                "sixteen", "seventeen", "eighteen", "nineteen"
            };
            UnitsWords = new List<string> {
                "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine"
            };

            NumberDic = new Dictionary<string, int> {
                {"and", 0}, {"zero", 0}, {"one", 1}, {"two", 2}, {"three", 3}, {"four", 4}, {"five", 5},
                {"six", 6}, {"seven", 7}, {"eight", 8}, {"nine", 9}, {"ten", 10}, {"eleven", 11}, {"twelve", 12},
                {"thirteen", 13}, {"fourteen", 14}, {"fifteen", 15}, {"sixteen", 16}, {"seventeen", 17},
                {"eighteen", 18}, {"nineteen", 19}, {"twenty", 20}, {"thirty", 30}, {"forty", 40}, {"fifty", 50},
                {"sixty", 60}, {"seventy", 70}, {"eighty", 80}, {"ninety", 90}, {"hundred", 100}
            };

            StarorusskiiDic = new Dictionary<int, string> {
                { 0, " " }, { 1, "А" }, { 2, "В" }, { 3, "Г" }, { 4, "Д" }, { 5, "Е" }, { 6, "S" }, { 7, "З" }, { 8, "Е" }, { 9, "Ѳ" },
                { 10, "I" }, { 20, "К" }, { 30, "Л" }, { 40, "М" }, { 50, "Н" }, { 60, "Ѯ" }, { 70, "О" }, { 80, "П" }, { 90, "Ч" },
                { 100, "Р" }, { 200, "С" }, { 300, "Т" }, { 400, "У" }, { 500, "Ф" }, { 600, "Ч" }, { 700, "Ѱ" }, { 800, "Ѡ" }, { 900, "Ц" },
            };
            //1 A   2 В   8 И   30 Л   100 Р   Ф 500

            ResultDic = new Dictionary<string, string>
            {
                {"result", ""},
                {"error", ""}
            };

            #endregion
        }

        public string ConvertToOldRussianNumber(int num)
        {
            string result = "";

            if (num >= 500)
            {
                num -= 500;
                result += 'Ф';
            }
            if (num >= 100)
            {
                var j = num;
                for (var i = 0; i < j / 100; i++)
                {
                    num -= 100;
                    result += 'Р';
                }
            }
            if (num >= 30)
            {
                var j = num;
                for (var i = 0; i < j / 30; i++)
                {
                    num -= 30;
                    result += 'Л';
                }
            }
            if (num >= 8)
            {
                var j = num;
                for (var i = 0; i < j / 8; i++)
                {
                    num -= 8;
                    result += 'И';
                }
            }
            if (num >= 2)
            {
                var j = num;
                for (var i = 0; i < j / 2; i++)
                {
                    num -= 2;
                    result += 'В';
                }
            }
            if (num == 1)
                result += 'А';

            return result;
        }

        public Dictionary<string, string> TryParse(string stringIn)
        {
            // Задаём дефолтное значение для результата
            ResultDic["result"] = "error";
            ResultDic["error"] = "";

            // Чекаем на пустую строку, только пробелы в строке и неверные символы
            if (stringIn == "")
            {
                ResultDic["error"] = ErrorDic["empty"];
                return ResultDic;
            }
            else if (stringIn.Trim(' ').Length == 0)
            {
                ResultDic["error"] = ErrorDic["spaces"];
                return ResultDic;
            }
            else if (!Regex.IsMatch(stringIn, @"^[a-zA-Z -]+$"))
            {
                ResultDic["error"] = ErrorDic["undefinedsymbols"];
                return ResultDic;
            }

            // Переводим строку в ловеркейс
            stringIn = stringIn.ToLower();

            // Переводим строку в массив строк. Сплитаем по пробелу
            var words = stringIn.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // Пробегаемся по каждому элементу и ищем элементы содержащие '-'. Проверяем их через CheckDashValues
            foreach (var word in words)
                if (word.Contains('-'))
                    ResultDic["error"] = CheckDashValues(word);
            if (ResultDic["error"] != "")
                return ResultDic;

            // Переводим строку в массив строк. Сплитаем по пробелу и по дэшу
            words = stringIn.Split(new char[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries);

            // Пробегаемся по каждому элементу и ищем элементы содержащие 'and', 'hundreds' и 'zero'. Проверяем их через CheckAndValues
            foreach (var word in words)
            {
                if (UnitsWords.Contains(word) || DozenWords.Contains(word) || AfterWords.Contains(word) || word == "hundred" || word == "and") continue;
                if (word == "hundreds")
                    ResultDic["error"] = ErrorDic["hundreds"];
                else
                    ResultDic["error"] = ErrorDic["notfinded"] + word;
                return ResultDic;
            }

            // Пробегаемся по каждому элементу и ищем элементы содержащие 'and', 'hundreds' и 'zero'. Проверяем их через CheckAndValues
            for (var i = 0; i < words.Length; i++)
            {
                if (i > 0 && words[i - 1] == "zero")
                    ResultDic["error"] = ErrorDic["zero"];
                else if (words[i] == "and")
                    ResultDic["error"] = CheckAndValues(words, i, words.Length - 1);

                if (ResultDic["error"] != "")
                    return ResultDic;
            }

            // Правильно ли расположен "zero"
            for (var i = 0; i < words.Length; i++)
            {
                if (i > 0 && words[i] == "zero")
                {
                    ResultDic["error"] = ErrorDic["zero"];
                    return ResultDic;
                }
            }

            // Проверяем порядок слов
            for (var i = 0; i < words.Length; i++)
            {
                ResultDic["error"] = CheckOrder(words, i, words.Length - 1);
                if (ResultDic["error"] != "")
                    return ResultDic;
            }

            var result = 0;
            for (var i = 0; i < words.Length; i++)
            {
                var word = words[i];
                result += ParseWord(words, i, words.Length - 1);
            }

            ResultDic["result"] = result.ToString();
            return ResultDic;
        }

        private string CheckDashValues(string dashedWord)
        {
            // Проверяем количество дэшей в строке. Можно только один
            if (dashedWord.Count(f => f == '-') > 1)
                return ErrorDic["toomanydashes"] + dashedWord;

            // Нет ли лишних символов в строке с дэшем. Доступны только a-z
            if (!Regex.IsMatch(dashedWord, @"^[a-z]+[-][a-z]+$"))
                return ErrorDic["wrongdash"] + dashedWord;

            // Строку в массив
            var words = dashedWord.Split('-');

            // Проверяем 2 элемента между дэшами
            if (UnitsWords.Contains(words[0]) && words[1] == "hundred")
                return "";
            else if (AfterWords.Contains(words[0]) && UnitsWords.Contains(words[1]))
                return "";
            else
                return ErrorDic["dashedword"] + dashedWord;
        }

        private string CheckAndValues(string[] words, int position, int lastposition)
        {
            if (position == 0 && words[position] == "and")
                return ErrorDic["firstand"];
            if (position == lastposition)
                return ErrorDic["lastand"];

            // Проверяем 2 элемента между дэшами
            if (words[position - 1] == "hundred" && !(new String[] { "and", "hundred" }).Contains(words[position + 1]))
                return "";
            else
                return ErrorDic["wrongand"] + words[position - 1] + " " + words[position] + " " + words[position + 1];
        }

        private string CheckOrder(string[] words, int position, int lastPosition)
        {
            #region Bad shit (commited suicide)
            /*
            if (words[position] == "hundred")
            {
                if (position == 0)
                {
                    if (position == lastPosition) return "";
                    if (!(UnitsWords.Contains(words[position + 1]) || AfterWords.Contains(words[position + 1]) ||
                        DozenWords.Contains(words[position + 1]) || words[position + 1] == "and"))
                        return ErrorDic["afterhundred"] + words[position] + " " + words[position + 1];
                }
                else if (position != lastPosition)
                {
                    if (!(UnitsWords.Contains(words[position + 1]) || AfterWords.Contains(words[position + 1]) ||
                        DozenWords.Contains(words[position + 1]) || words[position + 1] == "and"))
                        return ErrorDic["afterhundred"] + words[position - 1] + " " + words[position] + " " + words[position + 1];
                }
            }

            if (AfterWords.Contains(words[position]))
            {
                if (position == 0)
                {
                    if (position == lastPosition)
                        return "";
                    if (!UnitsWords.Contains(words[position + 1]))
                        return ErrorDic["afterdozens"] + words[position] + " " + words[position + 1];
                }
                else if (position != lastPosition)
                {
                    if (!UnitsWords.Contains(words[position + 1]))
                        return ErrorDic["afterdozens"] + words[position] + " " + words[position + 1];
                }
                else
                {
                    return "";
                }
            }

            if (DozenWords.Contains(words[position]))
            {
                if (position == 0)
                {
                    if (position != lastPosition)
                        return ErrorDic["afterotherdozens"] + words[position] + " " + words[position + 1];
                }
                else if (position != lastPosition)
                {
                    return ErrorDic["afterotherdozens"] + words[position - 1] + " " + words[position] + " " + words[position + 1];
                }
            }

            if (UnitsWords.Contains(words[position]))
            {
                if (position != lastPosition && words[position + 1] != "hundred")
                    return ErrorDic["afterunits"] + words[position] + " " + words[position + 1];
            }
            */
            #endregion

            if (position == lastPosition) return "";

            if (words[position] == "hundred")
            {
                if (words[position + 1] == "hundred")
                    return ErrorDic["hundredAfterHundred"] + words[position] + " " + words[position + 1];
            }
            if (AfterWords.Contains(words[position]))
            {
                if (AfterWords.Contains(words[position+1]))
                    return ErrorDic["afterWordsAfterAfterWords"] + words[position] + " " + words[position + 1];
                if (DozenWords.Contains(words[position+1]))
                    return ErrorDic["dozenWordsAfterAfterWords"] + words[position] + " " + words[position + 1];
                if (words[position + 1] == "hundred")
                    return ErrorDic["hundredAfterAfterWords"] + words[position] + " " + words[position + 1];
            }

            if (DozenWords.Contains(words[position]))
            {
                if (UnitsWords.Contains(words[position + 1]))
                    return ErrorDic["unitsWordsAfterDozenWords"] + words[position] + " " + words[position + 1];
                if (AfterWords.Contains(words[position + 1]))
                    return ErrorDic["afterWordsAfterDozenWords"] + words[position] + " " + words[position + 1];
                if (DozenWords.Contains(words[position + 1]))
                    return ErrorDic["dozenWordsAfterDozenWords"] + words[position] + " " + words[position + 1];
                if (words[position + 1] == "hundred")
                    return ErrorDic["hundredAfterDozenWords"] + words[position] + " " + words[position + 1];
            }

            if (UnitsWords.Contains(words[position]))
            {
                if (UnitsWords.Contains(words[position + 1]))
                    return ErrorDic["unitsWordsAfterUnitsWords"] + words[position] + " " + words[position + 1];
                if (AfterWords.Contains(words[position + 1]))
                    return ErrorDic["afterWordsAfterUnitsWords"] + words[position] + " " + words[position + 1];
                if (DozenWords.Contains(words[position + 1]))
                    return ErrorDic["dozenWordsAfterUnitsWords"] + words[position] + " " + words[position + 1];
            }

            return "";
        }

        private int ParseWord(string[] wordArray, int position, int lastposition)
        {
            if (wordArray[position] == "hundred")
            {
                if (position == 0)
                    return NumberDic[wordArray[position]];
                else
                    return NumberDic[wordArray[position]] * NumberDic[wordArray[position - 1]] - NumberDic[wordArray[position - 1]];
            }
            else
            {
                return NumberDic[wordArray[position]];
            }
        }
    }
}
