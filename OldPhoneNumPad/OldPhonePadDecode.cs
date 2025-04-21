using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OldPhoneNumPad
{
    //This class is the responsible for decoding the input from the user. // Esta clase es la responsable de decodificar la entrada del usuario.
    internal class OldPhonePadDecode
    {
        public static string OldPhonePad(string input)
        {
            if (string.IsNullOrEmpty(input)) // Check if the input is null or empty / Verifica si la entrada es nula o vacía
                throw new ArgumentException("La entrada no puede ser nula o vacía.", nameof(input)); // In case that the input is null or empty / En caso de que la entrada sea nula o vacía.

            var phonePad = new Dictionary<string, string> // Dictionary that contains the mapping of the input to the output / Diccionario que contiene el mapeo de la entrada a la salida
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

            
            var orderedKeys = phonePad.Keys.OrderByDescending(k => k.Length).ToList(); // We order the keys by length in descending order to match the longest sequence first / Ordenamos las claves por longitud en orden descendente para hacer coincidir la secuencia más larga primero

            var result = new StringBuilder(); // StringBuilder to store the result / StringBuilder para almacenar el resultado
            int currentIndex = 0; // Current index in the input string / Índice actual en la cadena de entrada

            while (currentIndex < input.Length) // While we have characters to process / Mientras tengamos caracteres para procesar
            {
                string matchedSequence = string.Empty; // Variable to store the matched sequence / Variable para almacenar la secuencia coincidente

                foreach (var key in orderedKeys) // Iterate over the ordered keys / Iterar sobre las claves ordenadas
                {
                    if (input.Substring(currentIndex).StartsWith(key)) // Check if the input starts with the key / Verificar si la entrada comienza con la clave.
                    {
                        matchedSequence = key; // Store the matched sequence / Almacenar la secuencia coincidente
                        break; // Exit the loop if we found a match / Salir del bucle si encontramos una coincidencia
                    }
                }

                if (!string.IsNullOrEmpty(matchedSequence)) // If we found a match / Si encontramos una coincidencia
                {
                    result.Append(phonePad[matchedSequence]); // Append the corresponding character to the result / Agregar el carácter correspondiente al resultado
                    currentIndex += matchedSequence.Length; // And then we move the current index forward / Y movemos el índice actual hacia adelante
                }
                else if (input[currentIndex] == '*') // If we find a '*' / Si encontramos un '*'
                {
                    if (result.Length > 0) // If the result is not empty / Si el resultado no está vacío
                    result.Remove(result.Length - 1, 1); // We remove the last character / Eliminamos el último carácter
                    currentIndex++; // And then we move the current index forward / Y movemos el índice actual hacia adelante
                }
                else if (input[currentIndex] == '#' && input.Count(c => c == '#') == 1) {
                    break; // We end the Loop if we found a '#' / Termina el bucle si se encuentra un '#'    
                }
                else // If anything on the above happens / Si nada de lo anterior sucede
                {
                    currentIndex++; // We move the current index forward / Movemos el índice actual hacia adelante
                }
            }

            return result.ToString(); // Once we have done the loop we return the result / Una vez que hemos terminado el bucle, devolvemos el resultado
        }
    }
}