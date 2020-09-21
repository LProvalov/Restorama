﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseDisplay : MonoBehaviour
{
    public Text phaseDisplayText;
    public Text touchCoorditatesText;

    private Touch theTouch;
    private float timeTouchEnded;
    private float displayTouchTime = .5f;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            theTouch = Input.GetTouch(0);
            phaseDisplayText.text = theTouch.phase.ToString();

            if (touchCoorditatesText != null)
            {
                touchCoorditatesText.text = $"X: {theTouch.position.x}, Y: {theTouch.position.y}";
            }

            if (theTouch.phase == TouchPhase.Ended)
            {
                timeTouchEnded = Time.time;
            }
        }
        else if (Time.time - timeTouchEnded > displayTouchTime)
        {
            phaseDisplayText.text = "Display";
        }
    }
}
