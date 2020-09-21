using System;
using System.Collections.Generic;
using System.Text;

namespace RestoramaConsole
{
    public class GameTable
    {
        public class Cell
        {
            public static int MainSelectIndex = 1;
            public enum CellStatus
            {
                Selected,
                Deselected
            }

            public Cell(CellStatus status = CellStatus.Deselected)
            {
                Status = status;
            }

            private CellStatus _status;
            public CellStatus Status
            {
                get => _status;
                set
                {
                    _status = value;
                    if (value == CellStatus.Selected)
                    {
                        SelectedIndex = MainSelectIndex;
                        MainSelectIndex++;
                    }
                    else if (value == CellStatus.Deselected)
                    {
                        SelectedIndex = 0;
                        MainSelectIndex--;
                    }
                }
            }
            public int SelectedIndex { get; private set; } = 0;
        }

        public GameTable(int rowCount, int colCount)
        {
            RowCount = rowCount;
            ColCount = colCount;

            items = new GameTableItem[colCount, rowCount];

            cells = new Cell[colCount, rowCount];
            for (int i = 0; i < colCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    cells[i, j] = new Cell();
                }
            }
        }

        public int RowCount { get; private set; }
        public int ColCount { get; private set; }

        private GameTableItem[,] items;
        private Cell[,] cells;

        public void FillItems()
        {
            for (int c = 0; c < ColCount; c++)
            {
                for (int r = 0; r < RowCount; r++)
                {
                    if (items[c, r] == null)
                    {
                        items[c, r] = new GameTableItem(GameTableItem.GameTableItemType.Dynamic, GameTableItem.GameTableItemStatus.Normal);
                    }
                    else
                    {
                        if (items[c, r].Status == GameTableItem.GameTableItemStatus.Empty)
                        {
                            items.Initialize();
                        }
                    }
                }
            }
        }

        public void SelectCell(int col, int row)
        {
            cells[col, row].Status = Cell.CellStatus.Selected;
        }

        public void DeselectCell(int col, int row)
        {
            cells[col, row].Status = Cell.CellStatus.Deselected;
        }
    }
}
