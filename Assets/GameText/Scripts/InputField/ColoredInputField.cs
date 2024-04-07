using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

using LinkCommunicationColoredNamespace;



public class ColoredInputField : MonoBehaviour
{

	[SerializeField] 
	private GameObject inputField;

    void Start()
    {

    }

    bool stateBool = false;

	string text_InputField = "";

    void Update()
    {

		 
    	// if(text_InputField != "")
    	// {
			// text_InputField = inputField.GetComponent<TMP_InputField>().text;
            // LinkCommunicationColoredClass.string_InputField = text_InputField;
    	// }



        if(Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Backspace))
        {

            string string_Main = inputField.GetComponent<TMP_InputField>().text;

            if(string_Main.LastIndexOf(" ") == -1)
            {

                string_Main = "";

            }
            else 
            {

                string_Main = string_Main.TrimEnd();
                string_Main = string_Main.Substring(0, string_Main.LastIndexOf(" ") + 1);

            }


            inputField.GetComponent<TMP_InputField>().text = string_Main;
    
        }

        
		text_InputField = inputField.GetComponent<TMP_InputField>().text;
        {
            // Debug.Log("Text Manipulation = " + text_InputField);
            LinkCommunicationColoredClass.string_InputField = text_InputField;

        }

        if (Input.GetKeyUp(KeyCode.Return))
    	{

          	LinkCommunicationColoredClass.bool_ActiveStatus = true;
            LinkCommunicationColoredClass.string_InputField = text_InputField;
        	

			inputField.GetComponent<TMP_InputField>().text = "";
        	text_InputField = "";

            Debug.Log("Return key was pressed.");
            // LinkCommunicationColoredClass.string_InputField = "";

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
