using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TouchPhaseDisplay : MonoBehaviour
{
    public Text phaseDisplayText;
    public Text touchCoordinatesText;
    public Text touchDeltaCoordinatesText;

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

            if (touchCoordinatesText != null)
            {
                touchCoordinatesText.text = $"X: {(int)theTouch.position.x}, Y: {(int)theTouch.position.y}";
            }

            if (touchDeltaCoordinatesText != null)
            {
                touchDeltaCoordinatesText.text = $"dX: {theTouch.deltaPosition.x}, dY: {theTouch.deltaPosition.y}";
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
