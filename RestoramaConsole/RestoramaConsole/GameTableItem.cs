using System;

namespace RestoramaConsole
{
    internal class GameTableItem
    {
        public enum GameTableItemType
        {
            None,
            Dynamic,
            Static
        }

        public enum GameTableItemStatus
        {
            Empty,
            Normal,

        }

        public GameTableItem(GameTableItemType type, GameTableItemStatus status)
        {
            Type = type;
            Status = status;
        }
        public GameTableItemType Type { get; private set; }
        public GameTableItemStatus Status { get; set; }

        public void InitializeNew()
        {
            if (Status == GameTableItemStatus.Empty)
            {
                Status = GameTableItemStatus.Normal;
            }
            else
            {
                // Log warning
            }
        }
    }
}