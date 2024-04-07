using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptComponentTest : MonoBehaviour
{
    // Start is called before the first frame update
    CollisionScriptCode gameObjectComponent;
    void Start()
    {
        gameObjectComponent = GetComponent<CollisionScriptCode>();


    }


    void Update()
    {
    	if(Input.GetKeyDown(KeyCode.RightControl))
		{
			gameObjectComponent.enabled = false;
		}        

    }
}
