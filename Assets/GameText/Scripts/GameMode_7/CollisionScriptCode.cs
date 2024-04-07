using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// using CommunicationCollisionNamespace;
using CommunicationCollisionCharToWordsNamespace;

public class CollisionScriptCode : MonoBehaviour
{

    void Start()
    {
     
    	// objectCollider = GetComponent<BoxCollider>();

    	// Debug.Log(objectCollider);

    }

    // Update is called once per frame
    void Update()
    {

    	// if(Input.GetKeyDown(KeyCode.Tab))
    	// {
			// gameObject.layer = 8;

   //  		LayerMask elementLayer = LayerMask.NameToLayer("TextCharToWords");
   //  		// LayerMask elementLayer = ;
   //  		Debug.Log(elementLayer.value);

   //  		elementLayer = (int)Math.Pow(2, 9);
   //		 // objectCollider.includeLayers = elementLayer; 
   //		 objectCollider.excludeLayers = elementLayer; 


    	// }
     	


    }



	void OnCollisionEnter(Collision coll)
    {
    
    	if(coll.gameObject.name == "PlaneTextTranslationOne_0")
    	{

    		// CommunicationCollisionClass.string_NameOfObjectMessage = ;
			CommunicationCollisionCharToWordsClass.bool_ActiveCollisionDetectionMessage = true;

    		gameObject.transform.parent.gameObject.SetActive(false);
    		// gameObject.SetActive(false);
    		// Debug.Log("Collision Enter Number 0 ");
    		// Debug.Log(gameObject.name);

    	}


    	if(coll.gameObject.name == "PlaneTextTranslationOne_1")
    	{

			CommunicationCollisionCharToWordsClass.bool_ActiveCollisionDetectionMessage = true;
    		// gameObject.SetActive(false);
    		gameObject.transform.parent.gameObject.SetActive(false);
			// Debug.Log("Collision Enter Number 1 ");
			// Debug.Log(gameObject.name);

    	}




	}


}
