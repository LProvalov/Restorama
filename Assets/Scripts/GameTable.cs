using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    private GameTableItem[] _gameTableItems;
    private GridModule _gridModule;

    public int ColMax = 0, RowMax = 0;
    public GameObject TableBackground;

    private const string MODULE_TAG = "[GameTable]";
    private bool _isInitialized = false;

    public void Start()
    {
        Debug.Log($"{MODULE_TAG} start");
        Debug.Assert(TableBackground != null, $"{MODULE_TAG} TableBackground object is null.");

        _gameTableItems = new GameTableItem[ColMax * RowMax];

        _gridModule = GetComponentInChildren<GridModule>();
        Debug.Assert(_gridModule != null, $"{MODULE_TAG} GameTable doesn't contain GridModule object.");
    }


    public void FillGameTableByItems()
    {
        // fill _gameTableItems
        _gridModule?.InstantiateItems();
    }

    public void AddGameTableItem(GameTableItem item, int col, int row)
    {
        Debug.Assert(_gameTableItems != null, $"{MODULE_TAG} _gameTableItems list is null, first call GameTable.Initialize(col, row);");
        
        if (item != null)
        {
            _gameTableItems[col * RowMax + row] = item;
        }
    }

    public GameTableItem GetTableItem(int col, int row)
    {
        Debug.Assert(_gameTableItems != null, $"{MODULE_TAG} _gameTableItems list is null, first call GameTable.Initialize(col, row);");
        if (col < 0 || row < 0 || col >= ColMax || row >= RowMax)
        {
            throw new ArgumentException($"{MODULE_TAG} Col or Row attributes has wrong value");
        }

        return _gameTableItems[col * RowMax + row];
    }

    public GameTableItem[] GetTableItems()
    {
        return _gameTableItems;
    }

    public bool IsInitialized => _isInitialized;
}
