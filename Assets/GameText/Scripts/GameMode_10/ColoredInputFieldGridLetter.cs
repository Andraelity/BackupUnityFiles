using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

using CommunicationFieldGridLetterNamespace;



public class ColoredInputFieldGridLetter : MonoBehaviour
{

	[SerializeField] 
	private GameObject inputField;

    void Start()
    {

    }

    bool stateBool = false;


	string string_InputFieldGridLetter = "";


    void Update()
    {


    	// if(string_InputFieldGridLetter != "")
    	// {
			// string_InputFieldGridLetter = inputField.GetComponent<TMP_InputField>().text;
            // LinkCommunicationColoredClass.string_InputField = string_InputFieldGridLetter;
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

        
		string_InputFieldGridLetter = inputField.GetComponent<TMP_InputField>().text;

        CommunicationFieldGridLetterClass.string_InputFieldGridLetter = string_InputFieldGridLetter;


        if(CommunicationFieldGridLetterClass.bool_ActiveEnterPress == true)
        {
            if (Input.GetKeyUp(KeyCode.Return))
            {
                
                CommunicationFieldGridLetterClass.bool_ActiveEnterPressMessage = true;
                CommunicationFieldGridLetterClass.string_InputFieldGridLetter = string_InputFieldGridLetter;
                

                inputField.GetComponent<TMP_InputField>().text = "";
                string_InputFieldGridLetter = "";

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

        }

        if(CommunicationFieldGridLetterClass.bool_ActiveEnterPress == false)
        {

			inputField.GetComponent<TMP_InputField>().text = "";

			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);

        }  

        // Debug.Log(CommunicationFieldGridLetterClass.bool_ActiveEnterPress);

        if (Input.GetKeyUp(KeyCode.Escape))
        {

			inputField.GetComponent<TMP_InputField>().text = "";

			EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);

        }  


    }

}
