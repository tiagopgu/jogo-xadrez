using Xadrez.Domain.Entities;

namespace Xadrez.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Position position = new Position(5, 6);

            System.Console.WriteLine(position);
        }
    }
}
