using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;
using UnityEngine.Tilemaps;

public class GridModule : MonoBehaviour
{
    public GameObject GameTableItemPrefab;

    
    private GameTable _gameTable = null;

    private RectTransform _gridModuleRectTransform = null;
    private Grid _grid = null;
    private Tilemap _tilemap = null;

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

        _grid = GetComponent<Grid>();
        Debug.Assert(_grid != null, $"{MODULE_TAG} GridModule haven't got a grid component.");

        _tilemap = GetComponentInParent<Tilemap>();
        Debug.Assert(_tilemap != null, $"{MODULE_TAG} GridModule haven't got a tilemap children component.");
    }
    public void Initialization()
    {
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
                float xInstPos = 0;
                float yInstPos = 0;
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
