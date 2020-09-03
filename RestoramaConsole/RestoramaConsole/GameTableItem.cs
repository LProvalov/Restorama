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

        public GameTableItem(GameTableItemType type)
        {
            Type = type;
        }
        public GameTableItemType Type { get; private set; }
    }
}