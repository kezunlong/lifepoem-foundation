using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities
{
    /// <summary>
    /// Defines a static helper class with methods for printing
    /// formatted data to the Standard Output Stream.
    /// </summary>
    public static class ConsoleHelper
    {
        #region Fields

        /// <summary>
        /// Defines the maximum number of times that input prompt methods will attempt to 
        /// retrieve valid input from a user before throwing an exception.
        /// </summary>
        public const Int32 MAX_ATTEMPTS = 100;

        /// <summary>
        /// The default prompt text in the console.
        /// </summary>
        public const string DEFAULT_PROMPT_TEXT = ">";

        /// <summary>
        /// The exception text after failed to input within MAX_APPEMPTS times.
        /// </summary>
        public const string INVALID_INPUT_EXCEPTION = "Invalid input was entered at the Console.";

        #endregion

        #region Constructors


        #endregion

        #region Properties



        #endregion

        #region Methods

        /// <summary>
        /// Prompts the user for input and returns the result.
        /// </summary>
        /// <returns>
        /// Returns a System.String representing the user's input.
        /// </returns>
        public static String PromptForInput()
        {
            return PromptForInput(DEFAULT_PROMPT_TEXT);
        }

        /// <summary>
        /// Prompts the user for input and returns the result.
        /// </summary>
        /// <param name="promptText">
        /// The text to print to the console asking the user for input.
        /// </param>
        /// <returns>
        /// Returns a System.String representing the user's input.
        /// </returns>
        public static String PromptForInput(String promptText)
        {
            Console.Write(promptText);
            return Console.ReadLine();
        }

        /// <summary>
        /// Prompts the user for a valid System.Int32 value.  If invalid input is entered, an error message is printed
        /// to the console until valid input is entered.
        /// </summary>
        /// <returns>
        /// Returns the System.Int32 value entered by the user.
        /// </returns>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if invalid input is entered more than MAX_ATTEMPTS times.
        /// </exception>
        public static Int32 PromptForInt32()
        {
            string invalidInputText = "Invalid Input, please provide a 32-bit integer value.";
            return PromptForInt32(DEFAULT_PROMPT_TEXT, invalidInputText);
        }

        /// <summary>
        /// Prompts the user for a valid System.Int32 value.  If invalid input is entered, an error message is printed
        /// to the console until valid input is entered.
        /// </summary>
        /// <param name="promptText">
        /// The text to print to the console asking the user for input.
        /// </param>
        /// <param name="invalidInputText">
        /// The text to print if the user enters invalid input.
        /// </param>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if invalid input is entered more than MAX_ATTEMPTS times.
        /// </exception>
        /// <returns>
        /// Returns the System.Int32 value entered by the user.
        /// </returns>
        public static Int32 PromptForInt32(string promptText, string invalidInputText)
        {
            int num = 0;
            int i = 0;

            while (!Int32.TryParse(PromptForInput(promptText), out num))
            {
                if (i == MAX_ATTEMPTS)
                {
                    throw new InvalidOperationException(INVALID_INPUT_EXCEPTION);
                }

                ++i;

                ConsoleHelper.WriteLineForegroundColor(ConsoleColor.Red, invalidInputText);
            }

            return num;
        }

        /// <summary>
        /// Prompts the user for a valid System.Int32 value.  If invalid input is entered, an error message is printed
        /// to the console until valid input is entered.
        /// </summary>
        /// <param name="min">
        /// The minimum <see cref="System.Int32" /> value which will be considered valid input.
        /// </param>
        /// <param name="max">
        /// The maximum <see cref="System.Int32" /> value which will be considered valid input.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if the value of max is less than the value of min.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if invalid input is entered more than MAX_ATTEMPTS times.
        /// </exception>
        /// <returns>
        /// Returns the System.Int32 value entered by the user.
        /// </returns>
        public static Int32 PromptForInt32(Int32 min, Int32 max)
        {
            string invalidInputText = string.Format("Invalid Input, please provide a 32-bit integer value between {0} and {1}", min, max);
            return PromptForInt32(min, max, DEFAULT_PROMPT_TEXT, invalidInputText);
        }

        /// <summary>
        /// Prompts the user for a valid System.Int32 value.
        /// </summary>
        /// <param name="min">
        /// The minimum <see cref="System.Int32" /> value which will be considered valid input.
        /// </param>
        /// <param name="max">
        /// The maximum <see cref="System.Int32" /> value which will be considered valid input.
        /// </param>
        /// <param name="promptText">
        /// The text to print to the console asking the user for input.
        /// </param>
        /// <param name="invalidInputText">
        /// The text to print if the user enters invalid input.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// Thrown if the value of max is less than the value of min.
        /// </exception>
        /// <exception cref="System.InvalidOperationException">
        /// Thrown if invalid input is entered more than MAX_ATTEMPTS times.
        /// </exception>
        /// <returns>
        /// Returns the System.Int32 value entered by the user.
        /// </returns>
        public static Int32 PromptForInt32(Int32 min, Int32 max, string promptText, string invalidInputText)
        {
            if (max < min)
            {
                throw new ArgumentException("The value of 'max' must be greater than or equal to the value of 'min'.", "max");
            }

            int num = 0;
            int i = 0;

            while (!Int32.TryParse(PromptForInput(promptText), out num) || (num < min || num > max))
            {
                if (i == MAX_ATTEMPTS)
                {
                    throw new InvalidOperationException(INVALID_INPUT_EXCEPTION);
                }

                ++i;

                ConsoleHelper.WriteLineForegroundColor(ConsoleColor.Red, invalidInputText);
            }

            return num;
        }

        /// <summary>
        /// Prompts the user for a valid value of type T.
        /// </summary>
        /// <typeparam name="T">The desired data type.</typeparam>
        /// <returns>Returns the value of type T entered by the user.</returns>
        public static T PromptFor<T>()
        {
            string invalidInputText = string.Format("Invalid Input, please provide a valid {0} value.", typeof(T).FullName);
            return PromptFor<T>(DEFAULT_PROMPT_TEXT, invalidInputText);
        }

        /// <summary>
        /// Prompts the user for a valid value of type T.
        /// </summary>
        /// <typeparam name="T">The desired data type.</typeparam>
        /// <param name="promptText">The text to print to the console asking the user for input.</param>
        /// <param name="invalidInputText">The text to print if the user enters invalid input.</param>
        /// <returns>Returns the value of type T entered by the user.</returns>
        public static T PromptFor<T>(string promptText, string invalidInputText)
        {
            T num = default(T);
            bool inputValidValue = false;
            int i = 0;

            while (!inputValidValue)
            {
                if (i == MAX_ATTEMPTS)
                {
                    throw new InvalidOperationException(INVALID_INPUT_EXCEPTION);
                }
                try
                {
                    num = (T)Convert.ChangeType(PromptForInput(promptText), typeof(T));
                    inputValidValue = true;
                }
                catch
                {
                    ConsoleHelper.WriteLineForegroundColor(ConsoleColor.Red, invalidInputText);
                }
                i++;
            }

            return num;
        }

        /// <summary>
        /// Prompts the user to provide input to advance the application, then halts the application until
        /// the input is received.
        /// </summary>
        public static void Pause()
        {
            Console.WriteLine("{0}Press [Enter] to continue.", Environment.NewLine);
            Console.ReadLine();
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <typeparam name="T">The type of data to write.</typeparam>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="data">The data.</param>
        public static void WriteForegroundColor<T>(ConsoleColor foregroundColor, T data)
        {
            ConsoleColor originalColor = Console.ForegroundColor;

            Console.ForegroundColor = foregroundColor;
            Console.Write(data);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        public static void WriteForegroundColor(ConsoleColor foregroundColor, String format, Object arg0)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, arg0);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        public static void WriteForegroundColor(ConsoleColor foregroundColor, String format, Object arg0, Object arg1)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, arg0, arg1);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        public static void WriteForegroundColor(ConsoleColor foregroundColor, String format, Object arg0, Object arg1, Object arg2)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, arg0, arg1, arg2);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        /// <param name="arg3">The fourth object to write using the specified format.</param>
        public static void WriteForegroundColor(ConsoleColor foregroundColor, String format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, arg0, arg1, arg2, arg3);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The array of objects to write using the specified format.</param>
        public static void WriteForegroundColor(ConsoleColor foregroundColor, String format, params Object[] args)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, args);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of unicode characters to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="buffer">An array of Unicode characters.</param>
        /// <param name="index">The zero-based starting index from which to begin writing the character array.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void WriteForegroundColor(ConsoleColor foregroundColor, char[] buffer, int index, int count)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(buffer, index, count);
            Console.ForegroundColor = originalColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <typeparam name="T">The type of data to write.</typeparam>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="data">The data.</param>
        public static void WriteBackgroundColor<T>(ConsoleColor backgroundColor, T data)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(data);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0);
            Console.BackgroundColor = originalBackgroundColor; ;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0, Object arg1)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0, arg1);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0, arg1, arg2);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        /// <param name="arg3">The fourth object to write using the specified format.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0, arg1, arg2, arg3);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The array of objects to write using the specified format.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, string format, params Object[] args)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, args);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of unicode characters to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="buffer">An array of Unicode characters.</param>
        /// <param name="index">The zero-based starting index from which to begin writing the character array.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void WriteBackgroundColor(ConsoleColor backgroundColor, char[] buffer, int index, int count)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(buffer, index, count);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors.
        /// </summary>
        /// <typeparam name="T">The type of data to write.</typeparam>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="data">The data.</param>
        public static void WriteColors<T>(ConsoleColor foregroundColor, ConsoleColor backgroundColor, T data)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(data);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        public static void WriteColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        public static void WriteColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0, Object arg1)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0, arg1);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        public static void WriteColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0, arg1, arg2);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The first object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        /// <param name="arg3">The fourth object to write using the specified format.</param>
        public static void WriteColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, arg0, arg1, arg2, arg3);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The array of objects to write using the specified format.</param>
        public static void WriteColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object[] args)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(format, args);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of Unicode characters to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="buffer">An array of Unicode characters.</param>
        /// <param name="index">The zero-based starting index from which to begin writing the character array.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void WriteColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, char[] buffer, int index, int count)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.Write(buffer, index, count);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified data to the standard output stream,
        /// with the specified foreground color,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="data">The data to print.</param>
        public static void WriteLineForegroundColor<T>(ConsoleColor foregroundColor, T data)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(data);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground color,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        public static void WriteLineForegroundColor(ConsoleColor foregroundColor, string format, Object arg0)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, arg0);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        public static void WriteLineForegroundColor(ConsoleColor foregroundColor, string format, Object arg0, Object arg1)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, arg0, arg1);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground color,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        public static void WriteLineForegroundColor(ConsoleColor foregroundColor, string format, Object arg0, Object arg1, Object arg2)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, arg0, arg1, arg2);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foregroundcolor,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        /// <param name="arg3">The fourth object to write using the specified format.</param>
        public static void WriteLineForegroundColor(ConsoleColor foregroundColor, string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, arg0, arg1, arg2, arg3);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The array of objects to write using the specified format.</param>
        public static void WriteLineForegroundColor(ConsoleColor foregroundColor, string format, params Object[] args)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, args);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of Unicode characters to the standard output stream,
        /// with the specified foreground color.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="buffer">An array of Unicode characters.</param>
        /// <param name="index">The zero-based starting index from which to begin writing the character array.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void WriteLineForegroundColor(ConsoleColor foregroundColor, char[] buffer, int index, int count)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(buffer, index, count);
            Console.ForegroundColor = originalForegroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified background color,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="data">The data to print.</param>
        public static void WriteLineBackgroundColor<T>(ConsoleColor backgroundColor, T data)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(data);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground color,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified background color,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0, Object arg1)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0, arg1);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified background color, followed by the current line terminator.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0, arg1, arg2);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified background color, followed by the current line terminator.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        /// <param name="arg3">The fourth object to write using the specified format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0, arg1, arg2, arg3);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The array of objects to write using the specified format.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, string format, params Object[] args)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, args);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of Unicode characters to the standard output stream,
        /// with the specified background color.
        /// </summary>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="buffer">An array of Unicode characters.</param>
        /// <param name="index">The zero-based starting index from which to begin writing the character array.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void WriteLineBackgroundColor(ConsoleColor backgroundColor, char[] buffer, int index, int count)
        {
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(buffer, index, count);
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="data">The data to print.</param>
        public static void WriteLineColors<T>(ConsoleColor foregroundColor, ConsoleColor backgroundColor, T data)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(data);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        public static void WriteLineColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        public static void WriteLineColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0, Object arg1)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0, arg1);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        public static void WriteLineColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0, arg1, arg2);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// with the specified foreground and background colors,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="arg0">An object to write using the specified format.</param>
        /// <param name="arg1">The second object to write using the specified format.</param>
        /// <param name="arg2">The third object to write using the specified format.</param>
        /// <param name="arg3">The fourth object to write using the specified format.</param>
        public static void WriteLineColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, arg0, arg1, arg2, arg3);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The array of objects to write using the specified format.</param>
        public static void WriteLineColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string format, Object[] args)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(format, args);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of Unicode characters to the standard output stream,
        /// with the specified background and foreground colors.
        /// </summary>
        /// <param name="foregroundColor">The desired color of the foreground while printing the data.</param>
        /// <param name="backgroundColor">The desired color of the background while printing the data.</param>
        /// <param name="buffer">An array of Unicode characters.</param>
        /// <param name="index">The zero-based starting index from which to begin writing the character array.</param>
        /// <param name="count">The number of characters to write.</param>
        public static void WriteLineColors(ConsoleColor foregroundColor, ConsoleColor backgroundColor, char[] buffer, int index, int count)
        {
            ConsoleColor originalForegroundColor = Console.ForegroundColor;
            ConsoleColor originalBackgroundColor = Console.BackgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(buffer, index, count);
            Console.ForegroundColor = originalForegroundColor;
            Console.BackgroundColor = originalBackgroundColor;
        }

        #endregion
    }
}
