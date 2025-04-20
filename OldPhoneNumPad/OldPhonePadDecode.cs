using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OldPhoneNumPad
{
    //This class is the responsible for decoding the input from the user.
    internal class OldPhonePadDecode
    {
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("La entrada no puede ser nula o vacía.", nameof(input));

            var phonePad = new Dictionary<string, string>
            {
                { "1", "&" }, { "11", "'" }, { "111", "(" }, { "1111", ")" },
                { "2", "A" }, { "22", "B" }, { "222", "C" },
                { "3", "D" }, { "33", "E" }, { "333", "F" },
                { "4", "G" }, { "44", "H" }, { "444", "I" }, { "4444", "Í" },
                { "5", "J" }, { "55", "K" }, { "555", "L" },
                { "6", "M" }, { "66", "N" }, { "666", "O" }, { "6666", "Ó" },
                { "7", "P" }, { "77", "Q" }, { "777", "R" }, { "7777", "S" },
                { "8", "T" }, { "88", "U" }, { "888", "V" },
                { "9", "W" }, { "99", "X" }, { "999", "Y" }, { "9999", "Z" },
                { "0", " " },
            };

            // Ordena las claves una vez para evitar hacerlo repetidamente
            var orderedKeys = phonePad.Keys.OrderByDescending(k => k.Length).ToList();
            var result = new StringBuilder();
            int currentIndex = 0;

            while (currentIndex < input.Length)
            {
                string matchedSequence = string.Empty;

                // Busca la secuencia más larga que coincida
                foreach (var key in orderedKeys)
                {
                    if (input.Substring(currentIndex).StartsWith(key))
                    {
                        matchedSequence = key;
                        break;
                    }
                }

                if (!string.IsNullOrEmpty(matchedSequence))
                {
                    result.Append(phonePad[matchedSequence]);
                    currentIndex += matchedSequence.Length;
                }
                else if (input[currentIndex] == '*')
                {
                    if (result.Length > 0)
                    result.Remove(result.Length - 1, 1);
                    currentIndex++;
                }
                else if (input[currentIndex] == '#' && input.Count(c => c == '#') == 1) {
                    break; // Termina el bucle si se encuentra un '#'    
                }
                else
                {
                    currentIndex++;
                }
            }

            return result.ToString();
        }
    }
}