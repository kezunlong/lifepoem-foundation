using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lifepoem.Foundation.Utilities
{
    /// <summary>
    /// Defines a class which can be instantiated
    /// for easing the syntax of repetitively writing colorized console text.
    /// </summary>
    public class ConsoleColorWriter
    {
        #region Fields

        private ConsoleColor? _foregroundColor;
        private ConsoleColor? _backgroundColor;
        private int _foregroundOption;
        private int _backgroundOption;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleColorWriter"/> class.
        /// </summary>
        /// <param name="foregroundColor">Color of the fore ground.</param>
        /// <param name="backgroundColor">Color of the back ground.</param>
        public ConsoleColorWriter(ConsoleColor? foregroundColor, ConsoleColor? backgroundColor)
        {
            ForegroundColor = foregroundColor;
            BackgroundColor = backgroundColor;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of the foreground.
        /// </summary>
        /// <value>The color of the fore ground.</value>
        public ConsoleColor? ForegroundColor
        {
            get { return _foregroundColor; }
            set
            {
                _foregroundColor = value;

                _foregroundOption = (_foregroundColor == null) ? 0 : 1;
            }
        }

        /// <summary>
        /// Gets or sets the color of the background.
        /// </summary>
        /// <value>The color of the background.</value>
        public ConsoleColor? BackgroundColor
        {
            get { return _backgroundColor; }
            set
            {
                _backgroundColor = value;

                _backgroundOption = (_backgroundColor == null) ? 0 : 2;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <typeparam name="T">The type of data to write.</typeparam>
        /// <param name="data">The data.</param>
        public void Write<T>(T data)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(data);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, data);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, data);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, data);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void Write(string format, Object arg0)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(format, arg0);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, format, arg0);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, format, arg0);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void Write(string format, Object arg0, Object arg1)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(format, arg0, arg1);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, format, arg0, arg1);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, format, arg0, arg1);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0, arg1);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void Write(string format, Object arg0, Object arg1, Object arg2)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(format, arg0, arg1, arg2);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, format, arg0, arg1, arg2);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, format, arg0, arg1, arg2);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0, arg1, arg2);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="arg3">The arg3.</param>
        public void Write(string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(format, arg0, arg1, arg2, arg3);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, format, arg0, arg1, arg2, arg3);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, format, arg0, arg1, arg2, arg3);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0, arg1, arg2, arg3);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void Write(string format, Object[] args)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(format, args);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, format, args);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, format, args);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, format, args);
                    break;
            }
        }

        /// <summary>
        /// Writes the specified subarray of unicode characters to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        public void Write(char[] buffer, int index, int count)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.Write(buffer, index, count);
                    break;
                case 1:
                    ConsoleHelper.WriteForegroundColor(ForegroundColor.Value, buffer, index, count);
                    break;
                case 2:
                    ConsoleHelper.WriteBackgroundColor(BackgroundColor.Value, buffer, index, count);
                    break;
                case 3:
                    ConsoleHelper.WriteColors(ForegroundColor.Value, BackgroundColor.Value, buffer, index, count);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <typeparam name="T">The type of the data to write</typeparam>
        /// <param name="data">The data.</param>
        public void WriteLine<T>(T data)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(data);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, data);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, data);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, data);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified object to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        public void WriteLine(string format, Object arg0)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(format, arg0);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, format, arg0);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, format, arg0);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        public void WriteLine(string format, Object arg0, Object arg1)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(format, arg0, arg1);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, format, arg0, arg1);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, format, arg0, arg1);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0, arg1);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        public void WriteLine(string format, Object arg0, Object arg1, Object arg2)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(format, arg0, arg1, arg2);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, format, arg0, arg1, arg2);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, format, arg0, arg1, arg2);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0, arg1, arg2);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified objects to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="arg0">The arg0.</param>
        /// <param name="arg1">The arg1.</param>
        /// <param name="arg2">The arg2.</param>
        /// <param name="arg3">The arg3.</param>
        public void WriteLine(string format, Object arg0, Object arg1, Object arg2, Object arg3)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(format, arg0, arg1, arg2, arg3);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, format, arg0, arg1, arg2, arg3);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, format, arg0, arg1, arg2, arg3);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, format, arg0, arg1, arg2, arg3);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified array of objects to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        public void WriteLine(string format, Object[] args)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(format, args);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, format, args);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, format, args);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, format, args);
                    break;
            }
        }

        /// <summary>
        /// Writes the text representation of the specified subarray of unicode characters to the standard output stream,
        /// in the colors specified by this instances ForegroundColor and BackgroundColor properties,
        /// followed by the current line terminator.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="index">The index.</param>
        /// <param name="count">The count.</param>
        public void WriteLine(char[] buffer, int index, int count)
        {
            switch (_foregroundOption | _backgroundOption)
            {
                case 0:
                    Console.WriteLine(buffer, index, count);
                    break;
                case 1:
                    ConsoleHelper.WriteLineForegroundColor(ForegroundColor.Value, buffer, index, count);
                    break;
                case 2:
                    ConsoleHelper.WriteLineBackgroundColor(BackgroundColor.Value, buffer, index, count);
                    break;
                case 3:
                    ConsoleHelper.WriteLineColors(ForegroundColor.Value, BackgroundColor.Value, buffer, index, count);
                    break;
            }
        }

        #endregion
    }
}
