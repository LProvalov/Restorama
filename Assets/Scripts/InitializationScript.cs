using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;
using UnityEngine;

public class InitializationScript : MonoBehaviour
{
    public GameTable gameTable;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(gameTable != null, "[Initialization Script] gameTable object in null!");

        gameTable.Initialize();
    }

}
