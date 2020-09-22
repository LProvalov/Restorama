using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    private GameTableItem[] gameTableItems = null;

    public int colMax = 0, rowMax = 0;
    public GameTableItem gameTableItem;

    public void Initialize()
    {
        Debug.Assert(gameTableItem != null, "[GameTable] gameTableItem object is null.");
        gameTableItems = new GameTableItem[colMax * rowMax];

        for (int col = 0; col < colMax; col++)
        {
            for (int row = 0; row < rowMax; row++)
            {
                var instantiatedItem = Instantiate(gameTableItem, new Vector3(50 * col, 50 * row, 0), Quaternion.identity, this.transform);
                if (instantiatedItem != null)
                {
                    instantiatedItem.name = $"GameTableItem({"Bacon"})";
                    var instantiatedGameTableItem = (instantiatedItem.GetComponent(typeof(GameTableItem)) as GameTableItem);
                    if (instantiatedGameTableItem != null)
                    {
                        AddGameTableItem(instantiatedGameTableItem, col, row);
                    }
                }
            }
        }
    }

    public void AddGameTableItem(GameTableItem item, int col, int row)
    {
        Debug.Assert(gameTableItems != null, "[GameTable] gameTableItems list is null, first call GameTable.Initialize(col, row);");
        
        if (item != null)
        {
            gameTableItems[col * rowMax + row] = item;
        }
    }

    public GameTableItem GetItem(int col, int row)
    {
        Debug.Assert(gameTableItems != null, "[GameTable] gameTableItems list is null, first call GameTable.Initialize(col, row);");
        if (col < 0 || row < 0 || col >= colMax || row >= rowMax)
        {
            throw new ArgumentException("col or row attributes has wrong value");
        }

        return gameTableItems[col * rowMax + row];
    }
    
}
