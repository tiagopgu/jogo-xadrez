using System;

namespace Xadrez.Domain.Entities.Exceptions
{
    public class BoardException : ApplicationException
    {
        public BoardException(string message)
            : base(message)
        {

        }
    }
}
