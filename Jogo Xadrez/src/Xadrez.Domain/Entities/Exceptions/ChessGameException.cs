using System;

namespace Xadrez.Domain.Entities.Exceptions
{
    public class ChessGameException : ApplicationException
    {
        public ChessGameException(string message)
            : base(message)
        {

        }
    }
}
