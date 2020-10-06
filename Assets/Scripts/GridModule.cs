using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModule : MonoBehaviour
{
    public GameObject GameTableItemPrefab;

    public int BorderThickness = 50;
    public int TopBorderThickness = 50;
    public int BottomBorderThickness = 50;
    
    private GameTable _gameTabel = null;
    [SerializeField]
    private float _gridCellWidth, _gridCellHeight;
    [SerializeField]
    private float _gridCellStartWidth, _gridCellStartHeight;
    private RectTransform _canvasRectTransform;
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

        _canvasRectTransform = parentCanvas?.GetComponent<RectTransform>();
        Debug.Log($"RectWidth: {_canvasRectTransform?.rect.width}, RectHeight: {_canvasRectTransform?.rect.height}");
        
        _gridModuleRectTransform = GetComponent<RectTransform>();
        Debug.Assert(_gridModuleRectTransform != null, $"{MODULE_TAG} GridModule haven't got a rect transform component.");


        float canvasWidth = !_canvasRectTransform ? 0 : _canvasRectTransform.rect.width;
        float canvasHeight = !_canvasRectTransform ? 0 : _canvasRectTransform.rect.height;
        
        if (canvasWidth > BorderThickness * 2)
        {
            _gridCellWidth = (canvasWidth - BorderThickness * 2) / _gameTabel.ColMax;
        }

        if (canvasHeight > TopBorderThickness + BottomBorderThickness)
        {
            _gridCellHeight = (canvasHeight - TopBorderThickness - BottomBorderThickness) / _gameTabel.RowMax;
        }

        _gridCellStartWidth = _gridModuleRectTransform.position.x - 360f;
        _gridCellStartHeight = _gridModuleRectTransform.position.y - (950f / 2);
    }



    public void InstantiateItems(GameTableItem[] gameTableItems)
    {
        Debug.Assert(_IsInitialized, "GridModule doesn't initialized. Call 'Initialize(int colMax, int rowMax)' method.");

        for (int col = 0; col < _gameTabel.ColMax; col++)
        {
            for (int row = 0; row < _gameTabel.RowMax; row++)
            {
                var instantiatedItem = Instantiate(GameTableItemPrefab,
                                                   new Vector3(_gridCellWidth + _gridCellWidth * col, 
                                                               _gridCellHeight + _gridCellHeight * row,
                                                               0),
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
