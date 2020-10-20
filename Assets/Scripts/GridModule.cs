using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Tilemaps;

public class GridModule : MonoBehaviour
{
    public GameObject GameTableItemPrefab;
    public GameObject GameTablePallete;

    private GameTable _gameTable = null;

    private RectTransform _gridModuleRectTransform = null;
    private Grid _grid = null;
    private Tilemap _tilemap = null;

    private bool _IsInitialized = false;
    private const string MODULE_TAG = "[GridModule]";

    private int _colOffset;
    private int _rowOffset;

    public void Start()
    {
        Debug.Log($"{MODULE_TAG} start");

        _gameTable = GetComponentInParent<GameTable>();
        Debug.Assert(_gameTable != null, $"{MODULE_TAG} GameTable component not found.");

        var parentCanvas = GetComponentInParent<Canvas>();
        Debug.Assert(parentCanvas, $"{MODULE_TAG} Parent object doesn't contain Canvas component.");

        _gridModuleRectTransform = GetComponent<RectTransform>();
        Debug.Assert(_gridModuleRectTransform != null, $"{MODULE_TAG} GridModule haven't got a rect transform component.");

        _grid = GetComponent<Grid>();
        Debug.Assert(_grid != null, $"{MODULE_TAG} GridModule haven't got a grid component.");

        _tilemap = GetComponentInChildren<Tilemap>();
        Debug.Assert(_tilemap != null, $"{MODULE_TAG} GridModule haven't got a tilemap children component.");
    }
    public void Initialization()
    {
        _colOffset = _gameTable.ColMax / 2;
        _rowOffset = _gameTable.RowMax / 2;
        DrawGridBackground();
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
                
                Vector3Int cellPosition = new Vector3Int(
                    (int)((col - _colOffset) ),
                    (int)((row - _rowOffset) ),
                    (int)_grid.cellSize.z);

                Vector3 cellCenterPosition = _grid.GetCellCenterWorld(cellPosition);

                var instantiatedItem = Instantiate(GameTableItemPrefab,
                                                       new Vector3(cellCenterPosition.x, cellCenterPosition.y, cellCenterPosition.z),
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

    private void DrawGridBackground()
    {
        Tilemap palleteTilemap = GameTablePallete.GetComponentInChildren<Tilemap>();

        TileBase left_up_border_0 = palleteTilemap.GetTile(new Vector3Int(-2, 0, 0));
        TileBase left_up_border_1 = palleteTilemap.GetTile(new Vector3Int(-2, 1, 0));
        TileBase left_up_border_2 = palleteTilemap.GetTile(new Vector3Int(-1, 1, 0));
        TileBase up_border = palleteTilemap.GetTile(new Vector3Int(0, 1, 0));
        TileBase right_up_border_0 = palleteTilemap.GetTile(new Vector3Int(1, 1, 0));
        TileBase right_up_border_1 = palleteTilemap.GetTile(new Vector3Int(2, 1, 0));
        TileBase right_up_border_2 = palleteTilemap.GetTile(new Vector3Int(2, 0, 0));

        _tilemap.ClearAllTiles();
        _tilemap.SetTile(new Vector3Int(-_colOffset - 1, _rowOffset - 1, 0), left_up_border_0);
        _tilemap.SetTile(new Vector3Int(-_colOffset - 1, _rowOffset, 0), left_up_border_1);
        _tilemap.SetTile(new Vector3Int(-_colOffset, _rowOffset, 0), left_up_border_2);

        for (int col = 0; col < _gameTable.ColMax; col++)
        {
            _tilemap.SetTile(new Vector3Int(col - _colOffset, _rowOffset, 0), up_border);
        }

        _tilemap.SetTile(new Vector3Int(_colOffset - 1, _rowOffset, 0), right_up_border_0);
        _tilemap.SetTile(new Vector3Int(_colOffset, _rowOffset, 0), right_up_border_1);
        _tilemap.SetTile(new Vector3Int(_colOffset, _rowOffset -1, 0), right_up_border_2);
    }
}
