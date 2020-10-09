using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModule : MonoBehaviour
{
    public GameObject GameTableItemPrefab;

    public int LeftBorderThickness = 50;
    public int RightBorderThickness = 50;
    public int TopBorderThickness = 50;
    public int BottomBorderThickness = 50;
    public int GridCellBorderThickness = 1;
    
    private GameTable _gameTabel = null;
    [SerializeField]
    private float _gridCellWidth, _gridCellHeight;
    [SerializeField]
    private float _gridCellStartWidth, _gridCellStartHeight;

    private RectTransform _gridModuleRectTransform = null;

    private bool _IsInitialized = false;
    private const string MODULE_TAG = "[GridModule]";

    public void Start()
    {
        Debug.Log($"{MODULE_TAG} start");

        _gameTabel = GetComponentInParent<GameTable>();
        Debug.Assert(_gameTabel != null, $"{MODULE_TAG} GameTable component not found.");

        var parentCanvas = GetComponentInParent<Canvas>();
        Debug.Assert(parentCanvas, $"{MODULE_TAG} Parent object doesn't contain Canvas component.");
        
        _gridModuleRectTransform = GetComponent<RectTransform>();
        Debug.Assert(_gridModuleRectTransform != null, $"{MODULE_TAG} GridModule haven't got a rect transform component.");

        
        if (_gridModuleRectTransform.rect.width > LeftBorderThickness + RightBorderThickness)
        {
            _gridCellWidth = (_gridModuleRectTransform.rect.width - LeftBorderThickness - RightBorderThickness) / _gameTabel.ColMax;
            _gridCellWidth -= GridCellBorderThickness;
        }

        if (_gridModuleRectTransform.rect.height > TopBorderThickness + BottomBorderThickness)
        {
            _gridCellHeight = (_gridModuleRectTransform.rect.height - TopBorderThickness - BottomBorderThickness) / _gameTabel.RowMax;
            _gridCellHeight -= GridCellBorderThickness;
        }

        _gridCellStartWidth = _gridModuleRectTransform.rect.min.x + GridCellBorderThickness;
        _gridCellStartHeight = _gridModuleRectTransform.rect.min.y + GridCellBorderThickness;
    }



    public void InstantiateItems()
    {
        Debug.Assert(_IsInitialized, "GridModule doesn't initialized. Call 'Initialize(int colMax, int rowMax)' method.");
        var gameTableItems = _gameTabel.GetTableItems();

        for (int col = 0; col < _gameTabel.ColMax; col++)
        {
            for (int row = 0; row < _gameTabel.RowMax; row++)
            {
                float xInstPos = _gridCellStartWidth + _gridCellWidth * col;
                float yInstPos = _gridCellStartHeight + _gridCellHeight * row;
                var instantiatedItem = Instantiate(GameTableItemPrefab,
                                                   new Vector3(xInstPos, yInstPos, 0),
                                                   Quaternion.identity, this.transform);
                if (instantiatedItem != null)
                {
                    instantiatedItem.name = $"GameTableItem(C:{col},R:{row})";
                    var instantiatedGameTableItem =
                        (instantiatedItem.GetComponent(typeof(GameTableItem)) as GameTableItem);
                    if (instantiatedGameTableItem != null)
                    {
                        gameTableItems[col * _gameTabel.RowMax + row].InstantiatedPrefab = instantiatedItem;
                    }
                }
            }
        }
    }
}
