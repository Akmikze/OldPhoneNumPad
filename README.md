# OldPhoneNumPad

This is a Solution to the Challenge proposed by Iron Software for more information please enter [here](https://drive.google.com/file/d/1LMV0_YUqC2KqObQPnYd3mgQiIpUSVP7-/view?usp=sharing)

# Description of the problem

As you can see this is an Old Phone Key Pad with **Alphabetical Letters**, **Backspace**, **Space**, **Special Characters or Symbols** and a **Send Button**.

Each button is identified by a number and by pressing it we must cycle through them in order to write the letter or symbol needed so this means it needs to be pressed multiple times.

For example: 

Pressing **2** once is gonna end up showing **A**. That means: **2# = A**. 

On the other han pressing twice the number **2** is gonna generate a **B** in this case **22# = B**.

One of the requirements for this challenge is that you **must** pause for a second in order to type two characters from the same button after each other. 

Here is an Example of it: **222 2 22#** = **CAB** As you can see here spaces are not counted and the message is not show as **C A B** this is because the space is the equivalent of an right arrow you could use the **0** between those spaces like this **222020200#** but you'll generate this instead **C A B**.

The other requirement for this challenge is that you **must** end every message with **#**

# How to install it?

1. Clone the repository (Or download the Zip File).

2. Open it on your IDE. 

3. Make sure you are using **.Net 8**

4. Run the Program and Follow the instructions on the console. 

# How does it work? 

## 1. Constant Messages 

The program possess four constant messages: 

WelcomeMessage: This one is Welcome Message. 

ExitMessage: An Exit Message once you exit the program. 

InvalidInputMessage: A Message that explains which inputs are valid.

ReminderMessage: A Reminder Message to remind the users to inser valid input.

## 2. Main Method 

The Main Method is the entry point. And it does the following: 
  
  2.1. Shows the Welcome Message.
  
  2.2. Enters the Main Loop and then it proceeds to call the method         
       HandleUserInput so the user input is handle.  

## 3. HandlerUserInput Method 

This method manages the interaction of the user. And it does the following:

  3.1. We request the user to enter a valid input.

  3.2. We allow the user writing a command or a sequence of keys.

Then we process the existence of special commands. 

  3.3. If the user writes **Q** or **q** the program shows an exit message and ends with Environment.Exit(0). 
       (This was used in order to end the application safely)

  3.4. If the user writes **TEST** or **test** the program uses the method TestCases and showcases the test cases.

Finally we validate the user input if it's not a special command

  3.5 We call the IsValidInput Method if the input is valid, we eliminate the # character by eliminating the last position of the string and then we call the OldPhonePad          Method to decode the entry.
  
  3.6 On the contrary if the input is invalid we show a message to the user indicating that the input is invalid.

If there is an exception decoding the input we capture it and then we showed it.

## 4. IsValidInput Method
  
This method validates if the input suministrated by the user satisfy the rules of the program. And this are:
 
  4.1. It can only end with **#**

  4.2. It can only contain **numbers (0-9)**, **spaces**, the **asterisk** and **#**.

  4.3. It must contain at least two characters. 

  4.4. It can only contain **one #**.

  4.5. It can't be **0#** or ***#**.
  
## 5. TestCases

This method executes a series of predefined test cases to verify the behaviour of the program. And it works as it follows: 

  5.1. The method calls another method called RunTestCase and this one validates the entry if the entry is valid then it decodes the text using OldPhonePadDecode.OldPhonePad.

  5.2. Then it proceeds to show the expected result and the real result.

## 6. RunTestCase Method 

This method executes an indivual test doing the following: 

  6.1. Validates the entry using the IsValidInput method.

  6.2. If it's valid, it calls OldPhonePadDecode.OldPhonePad method to decode it.

  6.3. Finally it shows the input, the expected one and the one obtain.

## 7. OldPhonePad Method

This method is the main one this one decodes the inputs of the users. As it follows: 

  7.1. If the input is null or empty, it throws an exception with a message error. 

  7.2. Then the dictionary is created (A.K.A phonePad), this dictionary maps the combination of the keys such as "2", "22", "222" to their correspondent characters.  

  7.3. The keys of the dictionary are order by the length in a descendent way. This ensures that the longest combinations are prioritize while searching for matches on the input. 

  7.4. Then a while loop is used to process each position on the input as we search the longest combination that matches the current string. 
  
    7.4.1. If we found a match, we add the character that corresponds to the result and we move to the next position on the index.

    7.4.2. If we found an *, we eliminate the last character (Simulating the erase function).

    7.4.3 If we found a # and is the only entry, we end the cycle.

    7.4.4. If none of the above applies then we move on to the next character.
