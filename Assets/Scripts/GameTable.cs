using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    private GameTableItem[] _gameTableItems = null;
    private GridModule _gridModule;

    public int colMax = 0, rowMax = 0;
    public GameTableItem gameTableItem;
    public GameObject TableBackground;

    public void Initialize()
    {
        Debug.Assert(gameTableItem != null, "[GameTable] gameTableItem object is null.");
        Debug.Assert(TableBackground != null, "[GameTable] TableBackground object is null.");

        _gameTableItems = new GameTableItem[colMax * rowMax];
        
        _gridModule = GetComponent<GridModule>();
        Debug.Assert(_gridModule != null, "[GameTable] GameTable doesn't contain GridModule object.");

        InstantiateGameTableItems();
    }

    private void InstantiateGameTableItems()
    {
        for (int col = 0; col < colMax; col++)
        {
            for (int row = 0; row < rowMax; row++)
            {
                var instantiatedItem = Instantiate(gameTableItem, new Vector3(_gridModule.ItemStepX * col, _gridModule.ItemStepY * row, 0),
                                                   Quaternion.identity, this.transform);
                if (instantiatedItem != null)
                {
                    instantiatedItem.name = $"GameTableItem(C:{col},R:{row})";
                    var instantiatedGameTableItem =
                        (instantiatedItem.GetComponent(typeof(GameTableItem)) as GameTableItem);
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
        Debug.Assert(_gameTableItems != null, "[GameTable] _gameTableItems list is null, first call GameTable.Initialize(col, row);");
        
        if (item != null)
        {
            _gameTableItems[col * rowMax + row] = item;
        }
    }

    public GameTableItem GetItem(int col, int row)
    {
        Debug.Assert(_gameTableItems != null, "[GameTable] _gameTableItems list is null, first call GameTable.Initialize(col, row);");
        if (col < 0 || row < 0 || col >= colMax || row >= rowMax)
        {
            throw new ArgumentException("col or row attributes has wrong value");
        }

        return _gameTableItems[col * rowMax + row];
    }
    
}
