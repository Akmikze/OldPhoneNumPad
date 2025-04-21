using OldPhoneNumPad;
using System;
using System.Text.RegularExpressions;


public static class Program
{
    // Mensajes de bienvenida y despedida
    private const string WelcomeMessage = "Welcome to the Old Phone Number Pad Experience / Bienvenido a la Experiencia de escribir en un Pad Numerico Viejo";
    private const string ExitMessage = "Thanks for using the Old Phone Number Pad Experience / Gracias por usar la Experiencia de escribir en un Pad Numerico Viejo";
    private const string InvalidInputMessage = "Remember to enter Valid Data (Numbers, Spaces, 0 with other numbers and one # are valid, letters, 0#, #, *# or any number without # are not valid) / Recuerda usar datos validos (Numeros, Espacio 0 con otros numeros y un # son validos, 0#, #, *# or cualquier otro numero sin # no son validos)";
    private const string ReminderMessage = "Please type a Valid Input, TEST to see the test cases or Type Q if you want to exit / Porfavor ingresa datos validos, ingresa TEST para ver los casos de uso o Q si quieres salir del programa";

    public static void Main()
    {
        Console.WriteLine(WelcomeMessage); // Welcome Message / Mensaje de bienvenida
        while (true) // Main Loop / Bucle Principal
        {
            HandleUserInput(); // Handler for Users Input / Manejo de la entrada del usuario
        }
    }

    private static void HandleUserInput()
    {
        Console.WriteLine(ReminderMessage); // Reminder to the User / Mensaje de recordatorio
        string? input = Console.ReadLine(); // User Input / Entrada del usuario

        if (input?.ToUpper() == "Q") // If the user wants to exit and he pressed Q o q / Si el usuario quiere salir y presiono Q o q
        {
            Console.WriteLine(ExitMessage); // Exit Message / Mensaje de despedida
            Environment.Exit(0); // We exit the application safely / Salida segura
        } else if (input?.ToUpper() == "TEST") // If the user wants to see the test cases / Si el usuario quiere ver los casos de uso 
        {
            Console.Clear(); // Clear the console / Limpiar la consola
            TestCases(); // Llamar a los casos de uso / Call the test cases
            return;
        }

        if (input != null && IsValidInput(input)) // We check if the input is valid / Verificamos si la entrada es valida
        {
            input = input[..^1]; // We remove the last character (#) / Removemos el ultimo caracter (#)
            try
            {
                string result = OldPhonePadDecode.OldPhonePad(input); // We decode the input / Decodificamos la entrada
                Console.WriteLine("Pst... this is what you wrote: " + result + " right?"); // We show the result / Mostramos el resultado.
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while decoding the input: " + ex.Message); // In case we have an error / En caso de que tengamos un error
            }
        }
        else // If the input is input is invalid / Si la entrada es invalida
        {
            Console.WriteLine(InvalidInputMessage); // We show the invalid input message / Mostramos el mensaje de entrada invalida
        }
    }


    private static void RunTestCase(string input, string expectedOutput) // Test case runner / Ejecutar caso de prueba
    {
        try // Try to run the test case / Intentar ejecutar el caso de prueba
        {
            if (IsValidInput(input)) // If the input is valid / Si la entrada es valida
            {
                string result = OldPhonePadDecode.OldPhonePad(input); // Decode the input / Decodificamos la entrada
                Console.WriteLine($"Input: {input} | Expected: {expectedOutput} | Result: {result}");
            }
            else // If the input is invalid / Si la entrada es invalida
            {
                Console.WriteLine($"Input: {input} is invalid."); // We show the invalid input message / Mostramos el mensaje de entrada invalida
            }
        }
        catch (Exception ex) // In case we have an error processing the input / En caso de que tengamos un error procesando la entrada. 
        {
            Console.WriteLine($"Error processing input '{input}': {ex.Message}"); // We show the error message / Mostramos el mensaje de error
        }
    }

    private static void TestCases() // Test cases / Casos de prueba
    {
        RunTestCase("33#", "E");
        RunTestCase("227*#", "B");
        RunTestCase("4433555 555666#", "HELLO");
        RunTestCase("8 88777444666*664#", "TURING");
        RunTestCase("344477733222833 3022999092777 777336607776662244466338 8#", "DIRECTED BY WARREN ROBINETT"); 
        RunTestCase("7**", "ERROR: YOU DIDN'T USE #");
        RunTestCase("0#", "ERROR: YOU ONLY ENTER A SPACE");
        RunTestCase("#", "ERROR: YOU DIDN'T ENTER ANYTHING");
        RunTestCase("#", "ERROR: YOU DIDN'T ENTER ANYTHING");
        RunTestCase("7##", "ERROR: TOO MANY #");
        RunTestCase("7#7#7#", "ERROR: TOO MANY #");
        RunTestCase("255644455999933#", "AKMIKZE");
    }
    private static bool IsValidInput(string input) // Input Validations / Validaciones de entrada
    {
        return input.EndsWith('#') &&
                      Regex.IsMatch(input, @"^[0-9#* ]+$") &&
                      input.Length > 1 &&
                      input.Count(c => c == '#') == 1 &&
                      input != "0#" && input != "*#"; 
        
        // We limit the input to only be from 0 to 9, # and * and to only have one # at the end, to have more than 1 character and to not be 0# or *# / Limitamos la entrada a solo ser de 0 a 9, # y * y tener solo un # al final, tener mas de 1 caracter y no ser 0# o *#
    }
}
