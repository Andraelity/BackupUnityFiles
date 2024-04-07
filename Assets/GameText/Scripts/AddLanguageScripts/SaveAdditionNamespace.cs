using System.IO;
using System.Text; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

using UnityEngine;
using TMPro;

using SelectCancel_ButtonCommunicationNamespace;


namespace SaveAdditionNamespace
{
    
    public class SaveAdditionClass : MonoBehaviour
    {

        public GameObject gameObject_CurrentActiveName;

        public GameObject gameobject_InputFieldNameOne;
        public GameObject gameobject_InputFieldNameOne_Placeholder;
        
        public GameObject gameobject_InputFieldNameTwo;
        public GameObject gameobject_InputFieldNameTwo_Placeholder;
        
        public GameObject gameobject_InputFieldContentOne;
        public GameObject gameobject_InputFieldContentOne_Placeholder;

        public GameObject gameobject_InputFieldContentTwo;
        public GameObject gameobject_InputFieldContentTwo_Placeholder;


        // THIS THING IS AN INPUT FIELD THAT MAKES THE DESESLECTION OF THE TEXTFIELD POSSIBLE;
        public GameObject gameobject_dull;

        public GameObject gameobject_WordsOrSentencesText_a;
        public GameObject gameobject_WordsOrSentencesText_b;
        public GameObject gameobject_WordsOrSentencesText_c;

        public GameObject gameobject_BorderEditActive;
        public GameObject gameobject_HolderEKeyPress; 
        public GameObject gameobject_HolderQKeyPress; 


        int int_CurrentAdditionActive;

        string string_CurrentActivePATH_Name_One;
        string string_CurrentActivePATH_Name_Two;
        string string_CurrentActivePATH_Words_One;
        string string_CurrentActivePATH_Words_Two;
        string string_CurrentActivePATH_Sentences_One;
        string string_CurrentActivePATH_Sentences_Two;

        string string_ContentToDisplay_Name_One;
        string string_ContentToDisplay_Name_Two;
        string string_ContentToDisplay_Words_One;
        string string_ContentToDisplay_Words_Two;
        string string_ContentToDisplay_Sentences_One;
        string string_ContentToDisplay_Sentences_Two;



        void Start()
        {   

            // DEFAULT VALUE TO SET THE PATHS ON, 0 IS THE DIRECTOY FOLDER OF ADDITION#0;
            int_CurrentAdditionActive = 0;

            Set_Words_CurrentActiveLanguge_Addition_Int(int_CurrentAdditionActive);
            
            gameObject_CurrentActiveName.GetComponent<TMP_Text>().text = GetCurrentActiveName(int_CurrentAdditionActive);


            if(File.Exists(string_CurrentActivePATH_Name_One) == false)
            {

                string_ContentToDisplay_Name_One = "Null";
                string_ContentToDisplay_Name_Two = "Null";
                string_ContentToDisplay_Words_One = "Null";
                string_ContentToDisplay_Words_Two = "Null";
                string_ContentToDisplay_Sentences_One = "Null";
                string_ContentToDisplay_Sentences_Two = "Null";

            }
            else
            {

                string_ContentToDisplay_Name_One = File.ReadAllText(string_CurrentActivePATH_Name_One);
                string_ContentToDisplay_Name_Two = File.ReadAllText(string_CurrentActivePATH_Name_Two);
                string_ContentToDisplay_Words_One = File.ReadAllText(string_CurrentActivePATH_Words_One);
                string_ContentToDisplay_Words_Two = File.ReadAllText(string_CurrentActivePATH_Words_Two);
                string_ContentToDisplay_Sentences_One = File.ReadAllText(string_CurrentActivePATH_Sentences_One);
                string_ContentToDisplay_Sentences_Two = File.ReadAllText(string_CurrentActivePATH_Sentences_Two);

            }

            gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_One;
            gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_Two;
            gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Sentences_One;
            gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Sentences_Two;
            
        }


        public void Set_Words_CurrentActiveLanguge_Addition_Int(int int_FileAdditionNumber)
        {
            
            string string_PathToFile = Application.persistentDataPath + "/Language_Addition_Files/Language_Addition_" + int_FileAdditionNumber.ToString();

            string string_Path_Name_One = string_PathToFile + "/Language_Addition_" + int_FileAdditionNumber.ToString() +"_Name_0.json";
            string string_Path_Name_Two = string_PathToFile + "/Language_Addition_" + int_FileAdditionNumber.ToString() +"_Name_1.json";
            string string_Path_Words_One = string_PathToFile + "/Language_Addition_" + int_FileAdditionNumber.ToString() +"_Words_0.json";
            string string_Path_Words_Two = string_PathToFile + "/Language_Addition_" + int_FileAdditionNumber.ToString() +"_Words_1.json";
            string string_Path_Sentences_One = string_PathToFile + "/Language_Addition_" + int_FileAdditionNumber.ToString() +"_Sentences_0.json";
            string string_Path_Sentences_Two = string_PathToFile + "/Language_Addition_" + int_FileAdditionNumber.ToString() +"_Sentences_1.json";

            string_CurrentActivePATH_Name_One = string_Path_Name_One;
            string_CurrentActivePATH_Name_Two = string_Path_Name_Two;
            string_CurrentActivePATH_Words_One = string_Path_Words_One;
            string_CurrentActivePATH_Words_Two = string_Path_Words_Two;
            string_CurrentActivePATH_Sentences_One = string_Path_Sentences_One;
            string_CurrentActivePATH_Sentences_Two = string_Path_Sentences_Two;

        }


        public string GetCurrentActiveName(int int_AdditionNumber)
        {

            switch(int_AdditionNumber)
            {
                case 0:
                    return "Addition#0";
                    break;

                case 1:
                    return "Addition#1";
                    break;

                case 2:
                    return "Addition#2";
                    break;

                case 3:
                    return "Addition#3";
                    break;

                case 4:
                    return "Addition#4";
                    break;

                case 5:
                    return "Addition#5";
                    break;

                case 6:
                    return "Addition#6";
                    break;

                case 7:
                    return "Addition#7";
                    break;

                default:
                    return "Addition#0";
                    break; 

            }

        }

        bool bool_StatusEdit = false;

        public void SetStatusEdit()
        {
            bool_StatusEdit = !bool_StatusEdit;
            gameobject_BorderEditActive.gameObject.SetActive(bool_StatusEdit);
        }


        bool bool_SetSaveStatus = false;

        public void SetSaveStatus()
        {
            bool_SetSaveStatus =  true;
        }


        bool bool_WordsOrSentencesStatus = false;

        public void SetWordsOrSentencesStatus()
        {

            bool_WordsOrSentencesStatus = !bool_WordsOrSentencesStatus;


            if(bool_WordsOrSentencesStatus == true)
            {

                // INPUTFIELD INFORMATION
                gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_One;
                gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_Two;
                gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Words_One;
                gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Words_Two;

                //  BUTTON TEXT INFORMATION INFORMATION 
                gameobject_WordsOrSentencesText_a.GetComponent<TMP_Text>().text = "WORDS";
                gameobject_WordsOrSentencesText_b.GetComponent<TMP_Text>().text = "WORDS";
                gameobject_WordsOrSentencesText_c.GetComponent<TMP_Text>().text = "WORDS";

            }
            else
            {

                // INPUTFIELD INFORMATION
                gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_One;
                gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_Two;
                gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Sentences_One;
                gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Sentences_Two;

                //  BUTTON TEXT INFORMATION INFORMATION                 
                gameobject_WordsOrSentencesText_a.GetComponent<TMP_Text>().text = "SENTENCES";
                gameobject_WordsOrSentencesText_b.GetComponent<TMP_Text>().text = "SENTENCES";
                gameobject_WordsOrSentencesText_c.GetComponent<TMP_Text>().text = "SENTENCES";

            }

        }

        void Update()
        {
            

            // Debug.Log("Debug status select true or false ==|||     " + SelectCancel_ButtonCommunicationClass.bool_StatusModified_Select );
            // Debug.Log("Debug status select true or false ==|||     " + SelectCancel_ButtonCommunicationClass.int_AdditionStatusModified );

            if(SelectCancel_ButtonCommunicationClass.bool_StatusModified_Select == true)
            {

                SelectCancel_ButtonCommunicationClass.bool_StatusModified_Select = false;
                


                int_CurrentAdditionActive = SelectCancel_ButtonCommunicationClass.int_AdditionStatusModified;
    
                gameObject_CurrentActiveName.GetComponent<TMP_Text>().text = GetCurrentActiveName(int_CurrentAdditionActive);

                Set_Words_CurrentActiveLanguge_Addition_Int(int_CurrentAdditionActive);

                string_ContentToDisplay_Name_One = File.ReadAllText(string_CurrentActivePATH_Name_One);
                string_ContentToDisplay_Name_Two = File.ReadAllText(string_CurrentActivePATH_Name_Two);
                string_ContentToDisplay_Words_One = File.ReadAllText(string_CurrentActivePATH_Words_One);
                string_ContentToDisplay_Words_Two = File.ReadAllText(string_CurrentActivePATH_Words_Two);
                string_ContentToDisplay_Sentences_One = File.ReadAllText(string_CurrentActivePATH_Sentences_One);
                string_ContentToDisplay_Sentences_Two = File.ReadAllText(string_CurrentActivePATH_Sentences_Two);


                if(bool_WordsOrSentencesStatus == true)
                {

                    // INPUTFIELD INFORMATION
                    gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_One;
                    gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_Two;
                    gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Words_One;
                    gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Words_Two;

                }
                else
                {

                    // INPUTFIELD INFORMATION
                    gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_One;
                    gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Name_Two;
                    gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Sentences_One;
                    gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text = string_ContentToDisplay_Sentences_Two;

                }

            }

            if(bool_StatusEdit == false)
            {

                // EventSystem.current.SetSelectedGameObject(gameobject_InputFieldNameOne.gameObject);
                // EventSystem.current.SetSelectedGameObject(gameobject_InputFieldNameTwo.gameObject);
                // EventSystem.current.SetSelectedGameObject(gameobject_InputFieldContentOne.gameObject);
                // EventSystem.current.SetSelectedGameObject(gameobject_InputFieldContentTwo.gameObject);
                EventSystem.current.SetSelectedGameObject(gameobject_dull.gameObject);

                gameobject_HolderEKeyPress.GetComponent<Michsky.UI.Shift.PressKeyEvent>().enabled = true;
                gameobject_HolderQKeyPress.GetComponent<Michsky.UI.Shift.PressKeyEvent>().enabled = true;
                

            }
            else
            {
                
                gameobject_HolderEKeyPress.GetComponent<Michsky.UI.Shift.PressKeyEvent>().enabled = false;
                gameobject_HolderQKeyPress.GetComponent<Michsky.UI.Shift.PressKeyEvent>().enabled = false;
                
            }

            if(bool_SetSaveStatus)
            {

                bool_SetSaveStatus = false;
                
                if(bool_StatusEdit == true)
                {
                    SetStatusEdit();
                }

                if(bool_WordsOrSentencesStatus == true)
                {

                    // INPUTFIELD INFORMATION
                    string_ContentToDisplay_Name_One = gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text ;
                    string_ContentToDisplay_Name_Two = gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text ;
                    string_ContentToDisplay_Words_One = gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text ;
                    string_ContentToDisplay_Words_Two = gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text ;

                }
                else
                {

                    // INPUTFIELD INFORMATION
                    string_ContentToDisplay_Name_One = gameobject_InputFieldNameOne.GetComponent<TMP_InputField>().text;
                    string_ContentToDisplay_Name_Two = gameobject_InputFieldNameTwo.GetComponent<TMP_InputField>().text;
                    string_ContentToDisplay_Sentences_One = gameobject_InputFieldContentOne.GetComponent<TMP_InputField>().text;
                    string_ContentToDisplay_Sentences_Two = gameobject_InputFieldContentTwo.GetComponent<TMP_InputField>().text;

                }

               
                File.WriteAllText(string_CurrentActivePATH_Name_One, string_ContentToDisplay_Name_One, Encoding.Unicode);
                File.WriteAllText(string_CurrentActivePATH_Name_Two, string_ContentToDisplay_Name_Two, Encoding.Unicode);
                File.WriteAllText(string_CurrentActivePATH_Words_One, string_ContentToDisplay_Words_One, Encoding.Unicode);
                File.WriteAllText(string_CurrentActivePATH_Words_Two, string_ContentToDisplay_Words_Two, Encoding.Unicode);
                File.WriteAllText(string_CurrentActivePATH_Sentences_One, string_ContentToDisplay_Sentences_One, Encoding.Unicode);
                File.WriteAllText(string_CurrentActivePATH_Sentences_Two, string_ContentToDisplay_Sentences_Two, Encoding.Unicode);
 

            }

        }

    }

}
    
