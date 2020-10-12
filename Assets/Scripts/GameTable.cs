using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour
{
    public enum GameTableState
    {
        ReadyToInitialization = 0,
        Initialized,
        FillingByItems,
        FilledByItems
    }

    private GameTableItem[] _gameTableItems;
    private GridModule _gridModule;

    public int ColMax = 0, RowMax = 0;
    public GameObject TableBackground;
    private GameTableState _state = GameTableState.ReadyToInitialization;

    private const string MODULE_TAG = "[GameTable]";

    public void Start()
    {
        Debug.Log($"{MODULE_TAG} start");
        Debug.Assert(TableBackground != null, $"{MODULE_TAG} TableBackground object is null.");

        _gridModule = GetComponentInChildren<GridModule>();
        Debug.Assert(_gridModule != null, $"{MODULE_TAG} GameTable doesn't contain GridModule object.");

        _state = GameTableState.ReadyToInitialization;
    }

    public void Initialization()
    {
        _gameTableItems = new GameTableItem[ColMax * RowMax];
        
        _gridModule?.Initialization();
        
        _state = GameTableState.Initialized;
    }

    public void FillGameTableByItems()
    {
        _state = GameTableState.FillingByItems;

        for(int col = 0; col < ColMax; col++)
        {
            for(int row = 0; row < RowMax; row++)
            {
                if (_gameTableItems[col * RowMax + row] == null)
                {
                    int typeIndex = (int)(UnityEngine.Random.value * 6) + 1;
                    _gameTableItems[col * RowMax + row] = new GameTableItem((GameTableItem.Type)typeIndex);
                }
            }
        }

        if (_gridModule != null)
        {
            _gridModule.InstantiateItems();
            _state = GameTableState.FilledByItems;
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

    public GameTableState State => _state;
}
