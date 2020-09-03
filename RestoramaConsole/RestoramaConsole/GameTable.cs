using System;
using System.Collections.Generic;
using System.Text;

namespace RestoramaConsole
{
    public class GameTable
    {
        public GameTable(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColCount = colCount;

            items = new GameTableItem[RowCount * ColCount];
        }
        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        private Grid grid;
        private GameTableItem[] items;

        public void FillItems()
        {

        }

    }
}
