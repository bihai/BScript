﻿namespace BScript.Tests.Compiler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using BScript.Compiler;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class LexerTests
    {
        [TestMethod]
        public void ProcessNullText()
        {
            Lexer lexer = new Lexer(null);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessEmptyText()
        {
            Lexer lexer = new Lexer(string.Empty);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void ProcessWhiteSpaceText()
        {
            Lexer lexer = new Lexer("   ");

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEqualAsOperator()
        {
            Lexer lexer = new Lexer("=");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetDotAsDelimiter()
        {
            Lexer lexer = new Lexer(".");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Delimiter, token.Type);
            Assert.AreEqual(".", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithDigit()
        {
            Lexer lexer = new Lexer("foo1");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo1", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithUnderscore()
        {
            Lexer lexer = new Lexer("foo_bar");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo_bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameStartingWithUnderscore()
        {
            Lexer lexer = new Lexer("_foo_bar");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("_foo_bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetUnexpectedToken()
        {
            Lexer lexer = new Lexer("#r");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.IsInstanceOfType(e, typeof(LexerException));
                Assert.AreEqual(e.Message, "Unexpected '#'");
            }
        }

        [TestMethod]
        public void GetNameEqual()
        {
            Lexer lexer = new Lexer("foo=");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEqualEqualAsOperator()
        {
            Lexer lexer = new Lexer("==");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("==", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetLessGreaterAsOperator()
        {
            Lexer lexer = new Lexer("<>");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("<>", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetLessGreaterAsOperators()
        {
            Lexer lexer = new Lexer("< >");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("<", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual(">", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetLessEqualGreaterEqualAsOperators()
        {
            Lexer lexer = new Lexer("<= >=");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("<=", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual(">=", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIntegerPlus()
        {
            Lexer lexer = new Lexer("1+");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("1", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("+", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetParenthesisAsDelimiters()
        {
            Lexer lexer = new Lexer("()");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Delimiter, token.Type);
            Assert.AreEqual("(", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Delimiter, token.Type);
            Assert.AreEqual(")", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetCommaAsDelimiter()
        {
            Lexer lexer = new Lexer(",");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Delimiter, token.Type);
            Assert.AreEqual(",", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetIntegerMinus()
        {
            Lexer lexer = new Lexer("1-");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("1", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Operator, token.Type);
            Assert.AreEqual("-", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetName()
        {
            Lexer lexer = new Lexer("foo");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNameWithSpaces()
        {
            Lexer lexer = new Lexer("  foo   ");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetNames()
        {
            Lexer lexer = new Lexer("foo bar");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("foo", token.Value);

            token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Name, token.Type);
            Assert.AreEqual("bar", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetInteger()
        {
            Lexer lexer = new Lexer("123");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Integer, token.Type);
            Assert.AreEqual("123", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetReal()
        {
            Lexer lexer = new Lexer("123.45");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.Real, token.Type);
            Assert.AreEqual("123.45", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetString()
        {
            Lexer lexer = new Lexer("\"foo\"");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.String, token.Type);
            Assert.AreEqual("foo", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetUnclosedString()
        {
            Lexer lexer = new Lexer("\"foo");

            try
            {
                lexer.NextToken();
                Assert.Fail();
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(LexerException));
                Assert.AreEqual("Unclosed string", ex.Message);
            }
        }

        [TestMethod]
        public void GetEndOfLineFromNewLine()
        {
            Lexer lexer = new Lexer("\n");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEndOfLineFromCarriageReturn()
        {
            Lexer lexer = new Lexer("\r");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\r", token.Value);

            Assert.IsNull(lexer.NextToken());
        }

        [TestMethod]
        public void GetEndOfLineFromCarriageReturnLineFeed()
        {
            Lexer lexer = new Lexer("\r\n");

            var token = lexer.NextToken();

            Assert.IsNotNull(token);
            Assert.AreEqual(TokenType.EndOfLine, token.Type);
            Assert.AreEqual("\r\n", token.Value);

            Assert.IsNull(lexer.NextToken());
        }
    }
}
