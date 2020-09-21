using System;

namespace RestoramaConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            GameTable gameTable = new GameTable(5, 5);
            gameTable.FillItems();
        }
    }
}
