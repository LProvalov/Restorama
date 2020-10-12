using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class GridModule : MonoBehaviour
{
    public GameObject GameTableItemPrefab;

    public int LeftBorderThickness = 50;
    public int RightBorderThickness = 50;
    public int TopBorderThickness = 50;
    public int BottomBorderThickness = 50;
    public int GridCellBorderThickness = 1;

    private GameTable _gameTable = null;
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

        _gameTable = GetComponentInParent<GameTable>();
        Debug.Assert(_gameTable != null, $"{MODULE_TAG} GameTable component not found.");

        var parentCanvas = GetComponentInParent<Canvas>();
        Debug.Assert(parentCanvas, $"{MODULE_TAG} Parent object doesn't contain Canvas component.");

        _gridModuleRectTransform = GetComponent<RectTransform>();
        Debug.Assert(_gridModuleRectTransform != null, $"{MODULE_TAG} GridModule haven't got a rect transform component.");        
    }
    public void Initialization()
    {
        if (_gridModuleRectTransform.rect.width > LeftBorderThickness + RightBorderThickness)
        {
            _gridCellWidth = (_gridModuleRectTransform.rect.width - LeftBorderThickness - RightBorderThickness) / _gameTable.ColMax;
            _gridCellWidth -= GridCellBorderThickness;
        }

        if (_gridModuleRectTransform.rect.height > TopBorderThickness + BottomBorderThickness)
        {
            _gridCellHeight = (_gridModuleRectTransform.rect.height - TopBorderThickness - BottomBorderThickness) / _gameTable.RowMax;
            _gridCellHeight -= GridCellBorderThickness;
        }

        _gridCellStartWidth = _gridModuleRectTransform.rect.min.x + LeftBorderThickness;
        _gridCellStartHeight = _gridModuleRectTransform.rect.min.y + BottomBorderThickness;

        _IsInitialized = true;
    }

    public void InstantiateItems()
    {
        Debug.Assert(_IsInitialized, "GridModule doesn't initialized. Call 'Initialize(int colMax, int rowMax)' method.");
        var gameTableItems = _gameTable.GetTableItems();

        for (int gameTableItemIndex = 0; gameTableItemIndex < gameTableItems.Length; gameTableItemIndex++)
        {
            if (gameTableItems[gameTableItemIndex] != null)
            {
                int col = gameTableItemIndex % _gameTable.ColMax;
                int row = gameTableItemIndex / _gameTable.ColMax;
                float xInstPos = _gridCellStartWidth + GridCellBorderThickness + (_gridCellWidth + GridCellBorderThickness) * col;
                float yInstPos = _gridCellStartHeight + GridCellBorderThickness + (_gridCellHeight + GridCellBorderThickness) * row;
                var instantiatedItem = Instantiate(GameTableItemPrefab,
                                                       new Vector3(xInstPos, yInstPos, 0),
                                                       Quaternion.identity, this.transform);
                if (instantiatedItem != null)
                {
                    var sr = instantiatedItem.GetComponent<SpriteResolver>();
                    if (sr != null)
                    {
                        switch (gameTableItems[gameTableItemIndex].ItemType)
                        {
                            case GameTableItem.Type.Bacon:
                                sr.SetCategoryAndLabel("Game Table Items", "bacon");
                                break;
                            case GameTableItem.Type.Bread:
                                sr.SetCategoryAndLabel("Game Table Items", "bread");
                                break;
                            case GameTableItem.Type.Meat:
                                sr.SetCategoryAndLabel("Game Table Items", "meat");
                                break;
                            case GameTableItem.Type.Onion:
                                sr.SetCategoryAndLabel("Game Table Items", "onion");
                                break;
                            case GameTableItem.Type.Salad:
                                sr.SetCategoryAndLabel("Game Table Items", "salad");
                                break;
                            case GameTableItem.Type.Tomato:
                                sr.SetCategoryAndLabel("Game Table Items", "tomato");
                                break;
                            default:
                                sr.SetCategoryAndLabel("Game Table Items", "bread");
                                break;
                        }
                    }
                    sr.ResolveSpriteToSpriteRenderer();
                    instantiatedItem.name = $"GameTableItem(C:{col},R:{row})";
                    gameTableItems[gameTableItemIndex].InstantiatedPrefab = instantiatedItem;
                }
            } 
            else
            {
                Debug.LogWarning($"gameTableItems[{gameTableItemIndex}] is null!");
            }
        }
    }
}
