using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommunicationCollisionNamespace;



public class ArrowCollision : MonoBehaviour
{
    
	private bool bool_PositionColliderStatus = false;


	private bool bool_ActualCollision_0 = false;
	private bool bool_ActualCollision_1 = false;
	private bool bool_ActualCollision_2 = false;
	private bool bool_ActualCollision_3 = false;
	private bool bool_ActualCollision_4 = false;
	private bool bool_ActualCollision_5 = false;




    // Update is called once per frame
    void Update()
    {

    	//This scripts allows us to modify the position of the arrow after the collision had entered.
		// bool_PositionColliderStatus = CommunicationCollisionClass.bool_PositionCollider;

		
		// if(bool_PositionColliderStatus)
		// {
		// 	gameObject.transform.position = new Vector3(-10.0f,1.61000001f,10.1000004f);
		// 	CommunicationCollisionClass.bool_PositionCollider = false;
			
		// }


    }

	void OnCollisionEnter(Collision coll)
    {
    
    	if(coll.gameObject.name == "PlaneTextTranslationOne_0")
    	{

    		Debug.Log("Collision Enter Number 0 ");
			CommunicationCollisionClass.bool_CheckActiveVisible = true;    		
			CommunicationCollisionClass.bool_ResetListOfWords = true;   


    	}

    	if(coll.gameObject.name == "PlaneTextTranslationOne_1")
    	{

    		Debug.Log("Collision Enter  Number 1 ");
			CommunicationCollisionClass.bool_CheckActiveVisible = true;    		
			CommunicationCollisionClass.bool_ResetListOfWords = true;    		

    	}

    	if(coll.gameObject.name == "PlaneTextTranslationOne_2")
    	{

    		Debug.Log("Collision Enter  Number 2 ");
			CommunicationCollisionClass.bool_CheckActiveVisible = true;    		
			CommunicationCollisionClass.bool_ResetListOfWords = true;    		

    	}

    	if(coll.gameObject.name == "PlaneTextTranslationOne_3")
    	{

    		Debug.Log("Collision Enter  Number 3 ");
			CommunicationCollisionClass.bool_CheckActiveVisible = true;    		
			CommunicationCollisionClass.bool_ResetListOfWords = true;    		

    	}

    	if(coll.gameObject.name == "PlaneTextTranslationOne_4")
    	{

    		Debug.Log("Collision Enter  Number 4 ");
			CommunicationCollisionClass.bool_CheckActiveVisible = true;    		
			CommunicationCollisionClass.bool_ResetListOfWords = true;    		

    	}

    	if(coll.gameObject.name == "PlaneTextTranslationOne_5")
    	{

    		Debug.Log("Collision Enter  Number 5 ");
			CommunicationCollisionClass.bool_CheckActiveVisible = true;    		
			CommunicationCollisionClass.bool_ResetListOfWords = true;    		

    	}

    }
    


}
