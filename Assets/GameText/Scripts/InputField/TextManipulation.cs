using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

using LinkCommunicationNamespace;



public class TextManipulation : MonoBehaviour
{

	[SerializeField] 
	private GameObject inputField;

    void Start()
    {

    }

    bool stateBool = false;

    void Update()
    {

        

        
        if (Input.GetKeyUp(KeyCode.Return))
        {

		    string text = inputField.GetComponent<TMP_InputField>().text;

            if(text != "")
            {
                Debug.Log("Text Manipulation = " + text);
                LinkCommunicationClass.string_InputField = text;
                LinkCommunicationClass.bool_ActiveStatus = true;
            }


            Debug.Log("Return key was pressed.");

			inputField.GetComponent<TMP_InputField>().text = "";
			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
			stateBool = true;

        }

        if(stateBool == true)
        {
        
        	stateBool = false;
			inputField.GetComponent<TMP_InputField>().ActivateInputField();
        
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {

			inputField.GetComponent<TMP_InputField>().text = "";

			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);

        }

    }

}
