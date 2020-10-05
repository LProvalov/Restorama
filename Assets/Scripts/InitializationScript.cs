using System.Collections;
using System.Collections.Generic;
//using System.Windows.Markup;
using UnityEngine;

public class InitializationScript : MonoBehaviour
{
    public GameTable GameTable;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(GameTable != null, "[Initialization Script] GameTable object in null!");

        GameTable?.Initialize();

    }

}
