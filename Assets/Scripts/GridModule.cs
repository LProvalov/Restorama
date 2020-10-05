using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridModule : MonoBehaviour
{
    private int _itemStepX, _itemStepY;

    public void Start()
    {
        var parentCanvas = GetComponentInParent<Canvas>();
        Debug.Assert(parentCanvas != null, "[GridModule] Parent object doesn't contain Canvas component.");

        var canvasRectTransform = parentCanvas?.GetComponent<RectTransform>();
        Debug.Log($"RectWidth: {canvasRectTransform?.rect.width}, RectHeigth: {canvasRectTransform?.rect.height}");
    }

    public int ItemStepX
    {
        get { return 1; }
    }

    public int ItemStepY { get { return 1; } }
}
