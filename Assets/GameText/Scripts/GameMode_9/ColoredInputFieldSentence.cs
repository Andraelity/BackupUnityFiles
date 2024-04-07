using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

using CommunicationFieldSentenceNamespace;



public class ColoredInputFieldSentence : MonoBehaviour
{

	[SerializeField] 
	private GameObject inputField;

    void Start()
    {

    }

    bool stateBool = false;

	string string_InputFieldSentence = "";

    void Update()
    {

		 
    	// if(string_InputFieldSentence != "")
    	// {
			// string_InputFieldSentence = inputField.GetComponent<TMP_InputField>().text;
            // LinkCommunicationColoredClass.string_InputField = string_InputFieldSentence;
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

        
		string_InputFieldSentence = inputField.GetComponent<TMP_InputField>().text;
        {
            // Debug.Log("Text Manipulation = " + string_InputFieldSentence);
            CommunicationFieldSentenceClass.string_InputFieldSentence = string_InputFieldSentence;

        }

        if (Input.GetKeyUp(KeyCode.Return))
    	{

          	CommunicationFieldSentenceClass.bool_ActiveEnterPress = true;
            CommunicationFieldSentenceClass.string_InputFieldSentence = string_InputFieldSentence;
        	

			inputField.GetComponent<TMP_InputField>().text = "";
        	string_InputFieldSentence = "";

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

        if(CommunicationFieldSentenceClass.bool_ActivateReset == true)
        {
            
            CommunicationFieldSentenceClass.bool_ActivateReset = false;
            string_InputFieldSentence = ""; 
			inputField.GetComponent<TMP_InputField>().text = "";
        
            

        }



    }

}
