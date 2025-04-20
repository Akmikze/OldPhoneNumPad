using OldPhoneNumPad;
using System;
using System.Text.RegularExpressions;


public static class Program
{
    private const string WelcomeMessage = "Welcome to the Old Phone Number Pad Experience / Bienvenido a la Experiencia de escribir en un Pad Numerico Viejo";
    private const string ExitMessage = "Thanks for using the Old Phone Number Pad Experience / Gracias por usar la Experiencia de escribir en un Pad Numerico Viejo";
    private const string InvalidInputMessage = "Remember to enter Valid Data (Numbers, Spaces, 0 with other numbers and one # are valid, letters, 0#, # or any number without # are not valid)";

    public static void Main()
    {
        Console.WriteLine(WelcomeMessage);
        while (true)
        {
            HandleUserInput();
        }
    }

    private static void HandleUserInput()
    {
        Console.WriteLine("Please type a Valid Input, TEST to see the test cases or Type Q if you want to exit");
        string? input = Console.ReadLine();

        if (input?.ToUpper() == "Q")
        {
            Console.WriteLine(ExitMessage);
            Environment.Exit(0); // Salida segura
        } else if (input?.ToUpper() == "TEST")
        {
            TestCases();
            return;
        }

        if (input != null && IsValidInput(input))
        {
            input = input[..^1];
            try
            {
                string result = OldPhonePadDecode.OldPhonePad(input);
                Console.WriteLine("Pst... this is what you wrote: " + result + " right?");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while decoding the input: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine(InvalidInputMessage);
        }
    }


    private static void RunTestCase(string input, string expectedOutput)
    {
        try
        {
            if (IsValidInput(input))
            {
                string result = OldPhonePadDecode.OldPhonePad(input);
                Console.WriteLine($"Input: {input} | Expected: {expectedOutput} | Result: {result}");
            }
            else
            {
                Console.WriteLine($"Input: {input} is invalid.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing input '{input}': {ex.Message}");
        }
    }

    private static void TestCases()
    {
        RunTestCase("33#", "E");
        RunTestCase("227*#", "B");
        RunTestCase("4433555 555666#", "HELLO");
        RunTestCase("8 88777444666*664#", "TURING");
        RunTestCase("344477733222833 3022999092777 777336607776662244466338 8#", "DIRECTED BY WARREN ROBINETT");
        RunTestCase("0", "ERROR: YOU DIDN'T USE #");
        RunTestCase("7**", "ERROR: YOU DIDN'T USE #");
        RunTestCase("0#", "ERROR: YOU ONLY ENTER A SPACE");
        RunTestCase("#", "ERROR: YOU DIDN'T ENTER ANYTHING");
        RunTestCase("#", "ERROR: YOU DIDN'T ENTER ANYTHING");
        RunTestCase("7##", "ERROR: TOO MANY #");
        RunTestCase("7#7#7#", "ERROR: TOO MANY #");
        RunTestCase("255644455999933#", "AKMIKZE");
    }
    private static bool IsValidInput(string input)
    {
        return input.EndsWith('#') &&
                      Regex.IsMatch(input, @"^[0-9#* ]+$") &&
                      input.Length > 1 &&
                      input.Count(c => c == '#') == 1 &&
                      input != "0#" && input != "*#";
    }
}
