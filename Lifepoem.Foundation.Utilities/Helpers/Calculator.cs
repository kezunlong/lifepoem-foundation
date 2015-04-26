using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Lifepoem.Foundation.Utilities
{
    /// <summary>
    /// A simple calculator class.
    /// It supports: 
    /// Simple expression: 
    ///     1 + 2 * 3 - 4 / 5
    /// Operators and Braces: 
    ///     PRight = ")", PLeft = "(", Power = "^", Divide = "/",
    ///     Multiply = "*", UnaryMinus = "_", Add = "+", Subtract = "-",
    ///     Factorial = "!", Mod = "%",
    ///     Sentinel = "#", End = ";", Store = "=", None = " ",
    ///     Separator = ",";
    ///     1 + (2 - 3) * 4
    /// Native variables: 
    ///     pi, e
    /// Custom variables:
    ///     c.SetVariable("ke", 3);
    ///     Console.WriteLine(c.Evaluate("2 * (4 + ke)"));
    /// Math functions: 
    ///     sin, cos, tan, asin, acos, atan, log, log10, ln, exp, abs, sqrt, rt
    ///     
    /// For complicated expression evaluation, please use NCalc instead.
    /// </summary>
    public class Calculator
    {
        #region Calculator Parser

        Stack<double> operands;
        Stack<string> operators;

        string token;
        int tokenPos;
        string expression;

        public Calculator()
        {
            Reset();
        }

        public void Reset()
        {
            LoadConstants();
            Clear();
        }

        public void Clear()
        {
            operands = new Stack<double>();
            operators = new Stack<string>();

            operators.Push(Token.Sentinel);
            token = Token.None;
            tokenPos = -1;
        }

        /// <summary>
        /// Evaluates mathematical expression
        /// </summary>
        /// <param name="expr">Input expression</param>
        /// <returns>Result in double format</returns>
        public double Evaluate(string expr)
        {
            Clear();
            expression = expr;
            if (Normalize(ref expression))
            {
                double result = Parse();
                SetVariable(AnswerVar, result);
                return result;
            }
            else
            {
                ThrowException("Blank input expression");
                return 0;
            }
        }

        private double Parse()
        {
            ParseBinary();
            Expect(Token.End);
            return operands.Peek();
        }

        /// <summary>
        /// Parse binary operations
        /// </summary>
        private void ParseBinary()
        {
            ParsePrimary();

            while (Token.IsBinary(token))
            {
                PushOperator(token);
                NextToken();
                ParsePrimary();
            }

            while (operators.Peek() != Token.Sentinel)
                PopOperator();
        }

        /// <summary>
        /// Parse primary tokens: digits, variables, functions, parentheses
        /// </summary>
        private void ParsePrimary()
        {
            if (Token.IsDigit(token)) // reading numbers
            {
                ParseDigit();
            }
            else if (Token.IsName(token)) // variable or function (both binary and unary)
            {
                ParseName();
            }
            else if (Token.IsUnary(token)) // unary operator (unary minus)
            {
                PushOperator(Token.ConvertOperator(token));
                NextToken();
                ParsePrimary();
            }
            else if (token == Token.PLeft) // parentheses
            {
                NextToken();
                operators.Push(Token.Sentinel); // add sentinel to operators stack
                ParseBinary();
                Expect(Token.PRight, Token.Separator);
                operators.Pop(); // pop sentinel from the stack

                TryInsertMultiply();
                TryRightSideOperator();
            }
            else if (token == Token.Separator) // arguments separator in funtions (',')
            {
                NextToken();
                ParsePrimary();
            }
            else
                ThrowException("Syntax error");
        }



        private void ParseDigit()
        {
            StringBuilder tmpNumber = new StringBuilder();

            while (Token.IsDigit(token))
            {
                CollectToken(ref tmpNumber);
            }

            operands.Push(double.Parse(tmpNumber.ToString(), System.Globalization.CultureInfo.InvariantCulture));
            TryInsertMultiply();
            TryRightSideOperator();
        }

        /// <summary>
        /// Turn name into a variable or a function
        /// </summary>
        private void ParseName()
        {
            StringBuilder tmpName = new StringBuilder();

            while (Token.IsName(token))
            {
                CollectToken(ref tmpName);
            }

            string name = tmpName.ToString();

            if (Token.IsFunction(name)) // Execute operand in case of driver's function (Sin, Cos e.t.c)
            {
                PushOperator(name);
                ParsePrimary();
            }
            else //Variable: char (one or more) and digit (zero or more). Ex: v, a1, bb, c3d
            {
                if (token == Token.Store) // in case of var=Expression
                {
                    NextToken();
                    SetVariable(name, Parse());
                }
                else
                {
                    operands.Push(GetVariable(name));
                    TryInsertMultiply();
                    TryRightSideOperator();

                }
            }
        }

        /// <summary>
        /// Check x(...), make it equal to x*(...)
        /// numberFunc(..) or numberVar, change to -> number*Func(..) or number*Var
        /// Check (...)(...), make it equal to (...)*(...)
        /// </summary>
        private void TryInsertMultiply()
        {
            if (!Token.IsBinary(token) && !Token.IsSpecial(token) && !Token.IsRightSide(token))
            {
                PushOperator(Token.Multiply);
                ParsePrimary();
            }
        }

        /// <summary>
        /// Check for right-side operators (Factorial) and for arguments separator 
        /// </summary>
        private void TryRightSideOperator()
        {
            switch (token)
            {
                case Token.Factorial:
                    {
                        PushOperator(Token.Factorial);
                        NextToken();
                        TryInsertMultiply();
                        break;
                    }
                case Token.Separator:
                    ParsePrimary(); // arguments separator in functions F(x1, x2 ... xn)
                    break;
            }
        }

        private void PushOperator(string op)
        {
            while (Token.Compare(operators.Peek(), op) > 0) //Token.Precedence(operators.Peek()) >= Token.Precedence(op))
                PopOperator();

            operators.Push(op);
        }

        private void PopOperator()
        {
            if (Token.IsBinary(operators.Peek()))
            {
                double o2 = operands.Pop();
                double o1 = operands.Pop();
                Calculate(operators.Pop(), o1, o2);
            }
            else // unary operator
            {
                Calculate(operators.Pop(), operands.Pop());
            }
        }

        /// <summary>
        /// Get next token from the expression
        /// </summary>
        private void NextToken()
        {
            if (token != Token.End)
            {
                token = expression[++tokenPos].ToString();
            }
        }

        /// <summary>
        /// Read token character by character
        /// </summary>
        /// <param name="sb">Temporary buffer</param>
        private void CollectToken(ref StringBuilder sb)
        {
            sb.Append(token);
            NextToken();
        }

        private void Expect(params string[] expectedTokens)
        {
            for (int i = 0; i < expectedTokens.Length; i++)
                if (token == expectedTokens[i])
                {
                    NextToken();
                    return;
                }

            ThrowException("Syntax error: " + Token.ToString(expectedTokens[0]) + "  expected");
        }

        /// <summary>
        /// Normalizes expression.
        /// </summary>
        /// <param name="s">Expression string</param>
        /// <returns>Returns true, if expression is suitable for evaluating.</returns>
        private bool Normalize(ref string s)
        {
            s = s.Replace(" ", "").Replace("\t", " ").ToLower() + Token.End;

            if (s.Length >= 2) // any character and End token
            {
                //if (Token.IsBinary(s[0].ToString()) && !Token.IsName(
                NextToken();
                return true;
            }

            return false;
        }

        private void ThrowException(string message)
        {
            throw new CalculateException(message, tokenPos);
        }

        #endregion

        #region Calculator Processor

        /// <summary>
        /// Calculates binary expressions and pushes the result into the operands stack
        /// </summary>
        /// <param name="op">Binary operator</param>
        /// <param name="operand1">First operand</param>
        /// <param name="operand2">Second operand</param>
        private void Calculate(string op, double operand1, double operand2)
        {
            double res = 0;
            try
            {
                switch (op)
                {
                    case Token.Add: res = operand1 + operand2; break;
                    case Token.Subtract: res = operand1 - operand2; break;
                    case Token.Multiply: res = operand1 * operand2; break;
                    case Token.Divide: res = operand1 / operand2; break;
                    case Token.Mod: res = operand1 % operand2; break;
                    case Token.Power: res = Math.Pow(operand1, operand2); break;
                    case Token.Log: res = Math.Log(operand2, operand1); break;
                    case Token.Root: res = Math.Pow(operand2, 1 / operand1); break;
                }

                operands.Push(PostProcess(res));
            }
            catch (Exception e)
            {
                ThrowException(e.Message);
            }
        }


        /// <summary>
        /// Calculates unary expressions and pushes the result into the operands stack
        /// </summary>
        /// <param name="op">Unary operator</param>
        /// <param name="operand">Operand</param>
        private void Calculate(string op, double operand)
        {
            double res = 1;

            try
            {
                switch (op)
                {
                    case Token.UnaryMinus: res = -operand; break;
                    case Token.Abs: res = Math.Abs(operand); break;
                    case Token.ACosine: res = Math.Acos(operand); break;
                    case Token.ASine: res = Math.Asin(operand); break;
                    case Token.ATangent: res = Math.Atan(operand); break;
                    case Token.Cosine: res = Math.Cos(operand); break;
                    case Token.Ln: res = Math.Log(operand); break;
                    case Token.Log10: res = Math.Log10(operand); break;
                    case Token.Sine: res = Math.Sin(operand); break;
                    case Token.Sqrt: res = Math.Sqrt(operand); break;
                    case Token.Tangent: res = Math.Tan(operand); break;
                    case Token.Exp: res = Math.Exp(operand); break;
                    case Token.Factorial: for (int i = 2; i <= (int)operand; res *= i++) ;
                        break;
                }

                operands.Push(PostProcess(res));
            }
            catch (Exception e)
            {
                ThrowException(e.Message);
            }

        }

        /// <summary>
        /// Result post-processing
        /// </summary>
        private double PostProcess(double result)
        {
            return Math.Round(result, 10);
        }

        public enum CalcMode { Numeric, Logic };

        #endregion

        #region Calculator Variables

        public delegate void CalcVariableDelegate(object sender, EventArgs e);
        public event CalcVariableDelegate OnVariableStore;

        Dictionary<string, double> variables;

        public const string AnswerVar = "r";

        private void LoadConstants()
        {
            variables = new Dictionary<string, double>();
            variables.Add("pi", Math.PI);
            variables.Add("e", Math.E);
            variables.Add(AnswerVar, 0);

            if (OnVariableStore != null)
                OnVariableStore(this, new EventArgs());
        }

        public Dictionary<string, double> Variables
        {
            get { return variables; }
        }

        public void SetVariable(string name, double val)
        {
            if (variables.ContainsKey(name))
                variables[name] = val;
            else
                variables.Add(name, val);

            if (OnVariableStore != null)
                OnVariableStore(this, new EventArgs());
        }

        public double GetVariable(string name)
        {  // return variable's value // if variable ha push default value, 0
            return variables.ContainsKey(name) ? variables[name] : 0;
        }

        #endregion
    }

    public static class Token
    {
        public const string PRight = ")", PLeft = "(", Power = "^", Divide = "/",
                            Multiply = "*", UnaryMinus = "_", Add = "+", Subtract = "-",
                            Factorial = "!", Mod = "%",
                            Sentinel = "#", End = ";", Store = "=", None = " ",
                            Separator = ",";

        public const string Sine = "sin", Cosine = "cos", Tangent = "tan",
                   ASine = "asin", ACosine = "acos", ATangent = "atan",
                   Log = "log", Log10 = "log10", Ln = "ln", Exp = "exp",
                   Abs = "abs", Sqrt = "sqrt", Root = "rt";

        //BoolAnd = "&", BoolNot = "!", BoolOr = "|", BoolImp = ">", BoolXor = "^",

        static string[] binaryOperators = new string[] { Multiply, Divide, Subtract, Add, 
                                                          Power, Log, Root, Mod };

        static string[] unaryOperators = new string[] { Subtract, Sine, Cosine, Tangent, ASine, 
                                                         ACosine, ATangent, Log10, Ln, Exp, 
                                                         Abs, Sqrt};

        static string[] specialOperators = new string[] { Sentinel, End, Store, None, Separator, PRight };

        static string[] rightSideOperators = new string[] { Factorial };

        static string[] FunctionList = new string[] { Sine, Cosine, Tangent, ASine, ACosine, 
                                                       ATangent, Log, Log10, Ln, Exp, Abs, 
                                                       Sqrt, Root };

        static string[] lastProcessedOperators = new string[] { Power }; // 2^3^4 = 2^(3^4)

        private static int Precedence(string op)
        {
            if (Token.IsFunction(op)) return 64;

            switch (op)
            {
                case Subtract: return 4;
                case Add: return 4;
                case UnaryMinus: return 8;
                case Power: return 16;
                case Multiply: return 24;
                case Divide: return 24;
                case Mod: return 32;
                case Factorial: return 48;
                case PLeft: return 64;
                case PRight: return 64;

                default: return 0; //operators END, Sentinel, Store
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="op1"></param>
        /// <param name="op2"></param>
        /// <returns></returns>
        public static int Compare(string op1, string op2)
        {
            if (op1 == op2 && Contains(op1, lastProcessedOperators))
                return -1;
            else
                return Precedence(op1) >= Precedence(op2) ? 1 : -1;
        }

        #region Is... Functions
        public static bool IsBinary(string op)
        {
            return Contains(op, binaryOperators);
        }

        public static bool IsUnary(string op)
        {
            return Contains(op, unaryOperators);
        }

        public static bool IsRightSide(string op)
        {
            return Contains(op, rightSideOperators);
        }

        public static bool IsSpecial(string op)
        {
            return Contains(op, specialOperators);
        }

        public static bool IsName(string token)
        {
            return Regex.IsMatch(token, @"[a-zA-Z0-9]");
        }

        public static bool IsDigit(string token)
        {
            return Regex.IsMatch(token, @"\d|\.");
        }

        public static bool IsFunction(string op)
        {
            return Contains(op, FunctionList);
        }


        #endregion

        /// <summary>
        /// Converts operator from expression to driver-comprehensible mode
        /// </summary>
        /// <param name="op">Unary operator</param>
        /// <returns>Converted operator</returns>
        public static string ConvertOperator(string op)
        {
            switch (op)
            {
                case "-": return "_";
                default: return op;
            }
        }

        public static string ToString(string op)
        {
            switch (op)
            {
                case End: return "END";
                default: return op.ToString();
            }
        }

        static bool Contains(string token, string[] array)
        {
            foreach (string s in array)
                if (s == token) return true;

            return false;
        }
    }

    public class CalculateException : Exception
    {
        int position;

        public CalculateException(string message, int position)
            : base("Error at position " + position.ToString() + "\r\n" + message)
        {
            this.position = position;
        }

        public int TokenPosition
        {
            get { return position; }
        }
    }
}
