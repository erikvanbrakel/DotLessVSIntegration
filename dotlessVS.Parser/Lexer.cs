using System;
using System.Text;
using TimeStamper.DSL;

namespace TimeStamper.DSL
{
    public class Lexer
    {
        /// <summary>
        /// Exposed for unit testing
        /// </summary>
        internal ICharacterBuffer buffer;

        public Lexer(ICharacterBuffer charBuffer)
        {
            buffer = charBuffer;
        }

        /// <summary>
        /// Gets the next Token
        /// </summary>
        /// <returns></returns>
        public Token GetNextToken()
        {
            Token returnToken = null;
            while (true)
            {
                switch (buffer.Peek(1))
                {
                    case ' ':
                        buffer.Load();
                        break;
                    case -1:
                        returnToken = new Token(TokenKind.EOF, "<eof>");
                        break;
                    case '-':
                        returnToken = new Token(TokenKind.TO, "<to>");
                        buffer.Load();
                        break;
                    case ':':
                        returnToken = new Token(TokenKind.COLON, "<:>");
                        buffer.Load();
                        break;
                       
                    default:
                        if (char.IsLetter((char)buffer.Peek(1)))
                        {
                            returnToken = GetIdentifier();
                        }
                        else if (char.IsNumber((char)buffer.Peek(1)))
                        {
                            if (buffer.Peek(2) == '-' || buffer.Peek(2) == '/' ||
                                buffer.Peek(3) == '-' || buffer.Peek(3) == '/')
                            {
                                return new Token(TokenKind.DATE_IDENTIFIER, ReadIdentifierToEnd());
                            }
           
                            return GetNumber();
                        }
                        else
                        {
                            buffer.Load();
                        }
                        break;
                }

                if (returnToken != null) return returnToken;
            }
        }

        /// <summary>
        /// Get keyword identifier/datetime variable
        /// </summary>
        /// <returns></returns>
        private Token GetIdentifier()
        {
            Token returnToken = null;
            var identifier = ReadIdentifierToEnd();
            switch (identifier.ToUpper())
            {
                case "TODAY":
                    returnToken = new Token(TokenKind.TODAY, "<today>");
                    break;
                case "TOMMOROW":
                    returnToken = new Token(TokenKind.TOMMOROW, "<tommorow>");
                    break;
                case "YESTERDAY":
                    returnToken = new Token(TokenKind.YESTERDAY, "<yesterday>");
                    break;
                case "JAN":
                case "JANUARY":
                case "FEB":
                case "FEBUARY":
                case "MAR":
                case "MARCH":
                case "APR":
                case "APRIL":
                case "MAY":
                case "JUN":
                case "JUNE":
                case "JUL":
                case "JULY":
                case "AUG":
                case "AUGUST":
                case "SEPT":
                case "SEP":
                case "SEPTEMBER":
                case "OCT":
                case "OCTOBER":
                case "NOV":
                case "NOVEMBER":
                case "DEC":
                case "DECEMBER":
                    returnToken = new Token(TokenKind.MONTH_IDENTIFIER, identifier.ToUpper());
                    break;
                case "MONDAY":
                case "TUESDAY":
                case "WEDNESDAY":
                case "THURSDAY":
                case "FRIDAY":
                case "SATURDAY":
                case "SUNDAY":
                    returnToken = new Token(TokenKind.DAY_IDENTIFIER, identifier.ToUpper());
                    break;
                case "YEAR":
                case "YEARS":
                    returnToken = new Token(TokenKind.YEAR, "<year>");
                    break;
                case "MONTH":
                case "MONTHS":
                    returnToken = new Token(TokenKind.MONTH, "<month>");
                    break;
                case "WEEK":
                case "WEEKS":
                    returnToken = new Token(TokenKind.WEEK, "<week>");
                    break;
                case "DAY":
                case "DAYS":
                    returnToken = new Token(TokenKind.DAY, "<day>");
                    break;
                case "NEXT":
                    returnToken = new Token(TokenKind.NEXT, "<next>");
                    break;
                case "PREVIOUS":
                    returnToken = new Token(TokenKind.PREVIOUS, "<previous>");
                    break;
                case "AT":
                    returnToken = new Token(TokenKind.AT, "<at>");
                    break;
                case "TO":
                    returnToken = new Token(TokenKind.TO, "<to>");
                    break;
                case "AGO":
                    returnToken = new Token(TokenKind.AGO, "<ago>");
                    break;
                case "TH":
                case "RD":
                case "ND":
                case "ST":
                    returnToken = new Token(TokenKind.MONTH_MODIFIER, "<month_modifier>");
                    break;
                case "AM":
                case "PM":
                    returnToken = new Token(TokenKind.TIME_MODIFIER, identifier.ToUpper());
                    break;
                case "END":
                    returnToken = new Token(TokenKind.EOF, "<eof>");
                    break;
                   
            }
            return returnToken;
        }
        /// <summary>
        /// Read Identifier to the end
        /// </summary>
        /// <returns></returns>
        private string ReadIdentifierToEnd()
        {
            var s = new StringBuilder();
            while (char.IsLetter((char)buffer.Peek(1)) || char.IsNumber((char)buffer.Peek(1)) || (char)buffer.Peek(1) == '_' || (char)buffer.Peek(1) == '/' || (char)buffer.Peek(1) == '-' || (char)buffer.Peek(1) == '.')
            {
                s.Append((char)buffer.Peek(1));
                buffer.Load();
            }
            return s.ToString();
        }

        /// <summary>
        /// Read the full number
        /// </summary>
        /// <returns></returns>
        private Token GetNumber()
        {
            var s = new StringBuilder();
            var c = (char)buffer.Peek(1);
            while (char.IsNumber(c) )
            {
                s.Append(c);
                buffer.Load();
                c = (char)buffer.Peek(1);
            }
            var stemp = s.ToString();
            return new Token(TokenKind.NUMBER, stemp);
        }
    }
}