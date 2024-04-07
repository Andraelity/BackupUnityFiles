using System; 
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text; 
using UnityEngine;


using CommunicationButtonLanguageNamespace;
using LinkCommunicationLanguagesFilesNamespace;

public class LanguageSelectionData_Class
{

    public int int_CurrentLanguageSelection = 0;

}

public class HandlerLanguageButtonsClass : MonoBehaviour
{

    [SerializeField]
    private List<GameObject> list_GameObjectLanguageSelectionField;


    private bool bool_ButtonSetLanguageToFrench = false;
    private bool bool_ButtonSetLanguageToPortuguese = false;
    private bool bool_ButtonSetLanguageToSpanish = false;
    private bool bool_ButtonSetLanguageToAddition0 = false;
    private bool bool_ButtonSetLanguageToAddition1 = false;
    private bool bool_ButtonSetLanguageToAddition2 = false;
    private bool bool_ButtonSetLanguageToAddition3 = false;
    private bool bool_ButtonSetLanguageToAddition4 = false;
    private bool bool_ButtonSetLanguageToAddition5 = false;
    private bool bool_ButtonSetLanguageToAddition6 = false;

    private bool bool_ConfirmChangeLanguage = false; 

    private string string_FilePathJSON_ContainerLanguagePersistance;

    private int int_CurrentLanguageSelection = 0;

    private bool bool_SetActiveLanguageCurrentSave = false;



    public void ChangeLanguageSelectionFrench()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToFrench = true;
        bool_ButtonSetLanguageToFrench = true;

    }
    
    public void ChangeLanguageSelectionPortuguese()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToPortuguese = true;
        bool_ButtonSetLanguageToPortuguese = true;
        
    }

    public void ChangeLanguageSelectionSpanish()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToSpanish = true;
    
    }

    public void ChangeLanguageSelectionAddition0()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition0 = true;
    
    }

    public void ChangeLanguageSelectionAddition1()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition1 = true;
    
    }

    public void ChangeLanguageSelectionAddition2()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition2 = true;
    
    }
    
    public void ChangeLanguageSelectionAddition3()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition3 = true;
    
    }

    public void ChangeLanguageSelectionAddition4()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition4 = true;
    
    }

    public void ChangeLanguageSelectionAddition5()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition5 = true;
    
    }

    public void ChangeLanguageSelectionAddition6()
    {

        // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;
        bool_ButtonSetLanguageToAddition6 = true;
    
    }

    public void ConfirmChangeLanguage()
    {

        bool_ConfirmChangeLanguage = true;

    }

    public void CancelOperation()
    {

        bool_ButtonSetLanguageToFrench = false;
        bool_ButtonSetLanguageToPortuguese = false;
        bool_ButtonSetLanguageToSpanish = false;
        bool_ButtonSetLanguageToAddition0 = false;
        bool_ButtonSetLanguageToAddition1 = false;
        bool_ButtonSetLanguageToAddition2 = false;
        bool_ButtonSetLanguageToAddition3 = false;
        bool_ButtonSetLanguageToAddition4 = false;
        bool_ButtonSetLanguageToAddition5 = false;
        bool_ButtonSetLanguageToAddition6 = false;

    }


    void Start()
    {

        List<GameObject> list_GameObjectLanguageSelection = list_GameObjectLanguageSelectionField;

        for(int i = 0; i < list_GameObjectLanguageSelection.Count; i++)
        {
            list_GameObjectLanguageSelection[i].transform.GetChild(0).gameObject.SetActive(true);
            list_GameObjectLanguageSelection[i].transform.GetChild(1).gameObject.SetActive(false);
        }
    

        string string_PathDevice  = Application.persistentDataPath;

        string string_DirectoryLocation = string_PathDevice + "/Language_Selection_Persistance";
        
        Debug.Log(string_DirectoryLocation);
        

  
        if(Directory.Exists(string_DirectoryLocation) == false)
        {
        
            Directory.CreateDirectory(string_DirectoryLocation);
        
        }

        
        string string_FilePath = string_DirectoryLocation + "/Language_Selection_Data.json";

        string_FilePathJSON_ContainerLanguagePersistance = string_FilePath;

        if (File.Exists(string_FilePath) == false)
        {
            
            LanguageSelectionData_Class LanguageSelectionData_Variable = new LanguageSelectionData_Class();

            int_CurrentLanguageSelection = LanguageSelectionData_Variable.int_CurrentLanguageSelection;

            string string_ToWrite = JsonUtility.ToJson(LanguageSelectionData_Variable);

            File.WriteAllText(string_FilePath, string_ToWrite, Encoding.UTF8);


        }
        else
        {
 
            string string_LanguageSelectionData_JSON = File.ReadAllText(string_FilePath);

            LanguageSelectionData_Class LanguageSelectionData_Variable = JsonUtility.FromJson<LanguageSelectionData_Class>(string_LanguageSelectionData_JSON);

            int_CurrentLanguageSelection = LanguageSelectionData_Variable.int_CurrentLanguageSelection;
            
        }

        list_GameObjectLanguageSelection[int_CurrentLanguageSelection].transform.GetChild(1).gameObject.SetActive(true);
       
        // gameobject_IconHolderFrench.transform.GetChild(0).gameObject.SetActive(true);
        // gameobject_IconHolderPortuguese.transform.GetChild(0).gameObject.SetActive(true);
        // gameobject_IconHolderSpanish.transform.GetChild(0).gameObject.SetActive(true);

        bool_SetActiveLanguageCurrentSave = true;
        bool_ConfirmChangeLanguage = true;

    }

    bool bool_ReUpdateFiles = false;

    void Update()
    {

        if(Input.GetKeyDown(KeyCode.Tab))
        {
            
            list_GameObjectLanguageSelectionField[3].transform.GetChild(1).gameObject.SetActive(false);

        }
        
        if(bool_ConfirmChangeLanguage)
        {

            bool_ConfirmChangeLanguage = false;

            List<GameObject> list_GameObjectLanguageSelection = list_GameObjectLanguageSelectionField;

            for(int i = 0; i < list_GameObjectLanguageSelection.Count; i++)
            {
                list_GameObjectLanguageSelection[i].transform.GetChild(0).gameObject.SetActive(true);
                list_GameObjectLanguageSelection[i].transform.GetChild(1).gameObject.SetActive(false);
                Debug.Log(list_GameObjectLanguageSelection[i].transform.parent.transform.parent.transform.parent);
            }


            LanguageSelectionData_Class LanguageSelectionData_Variable = new LanguageSelectionData_Class();

            LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;


            
            if(bool_ButtonSetLanguageToFrench || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 0))
            {
                
                bool_SetActiveLanguageCurrentSave = false;

                // CommunicationButtonLanguageClass.bool_SetLanguageToFrench = true;

                Debug.Log("    LANGUAGE SET TO FRENCH");

                bool_ButtonSetLanguageToFrench = false;

                list_GameObjectLanguageSelection[0].transform.GetChild(1).gameObject.SetActive(true);

                int_CurrentLanguageSelection = 0;
    

                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;            

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = true;
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(0);

            }
            
            if(bool_ButtonSetLanguageToPortuguese || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 1))
            {
                
                bool_SetActiveLanguageCurrentSave = false;

                // CommunicationButtonLanguageClass.bool_SetLanguageToPortuguese = true;
                
                Debug.Log("    LANGUAGE SET TO PORTUGUESE");

                bool_ButtonSetLanguageToPortuguese = false;
                
                list_GameObjectLanguageSelection[1].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 1;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = true;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(1);

            }

            if(bool_ButtonSetLanguageToSpanish || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 2))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO SPANISH");

                bool_ButtonSetLanguageToSpanish = false;

                list_GameObjectLanguageSelection[2].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 2;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = true;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(2);

            }

            if(bool_ButtonSetLanguageToAddition0 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 3))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION0");
                
                list_GameObjectLanguageSelection[3].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 3;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(3);

            }

            if(bool_ButtonSetLanguageToAddition1 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 4))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION1");

                list_GameObjectLanguageSelection[4].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 4;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(4);

            }

            if(bool_ButtonSetLanguageToAddition2 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 5))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION2");
                
                list_GameObjectLanguageSelection[5].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 5;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(5);

            }

            if(bool_ButtonSetLanguageToAddition3 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 6))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION3");

                list_GameObjectLanguageSelection[6].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 6;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;
                
                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(6);

            }

            if(bool_ButtonSetLanguageToAddition4 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 7))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION4");

                list_GameObjectLanguageSelection[7].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 7;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(7);

            }

            if(bool_ButtonSetLanguageToAddition5 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 8))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION5");
                
                list_GameObjectLanguageSelection[8].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 8;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(8);

            }

            if(bool_ButtonSetLanguageToAddition6 || (bool_SetActiveLanguageCurrentSave && int_CurrentLanguageSelection == 9))
            {
                
                bool_SetActiveLanguageCurrentSave = false;                

                // CommunicationButtonLanguageClass.bool_SetLanguageToSpanish = true;

                Debug.Log("    LANGUAGE SET TO ADDITION6");
                
                list_GameObjectLanguageSelection[9].transform.GetChild(1).gameObject.SetActive(true);
                
                int_CurrentLanguageSelection = 9;
    
                LanguageSelectionData_Variable.int_CurrentLanguageSelection = int_CurrentLanguageSelection;

                bool_ReUpdateFiles = true;

                LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile = false;                
                LinkCommunicationLanguagesFilesClass.SetCurrentActiveLanguge_MAIN(9);

            }

            if(bool_ReUpdateFiles)
            {
                
                bool_ReUpdateFiles = false;

                int_CurrentLanguageSelection = LanguageSelectionData_Variable.int_CurrentLanguageSelection;

                string string_ToWrite = JsonUtility.ToJson(LanguageSelectionData_Variable);

                File.WriteAllText(string_FilePathJSON_ContainerLanguagePersistance, string_ToWrite, Encoding.UTF8);

            }

            bool_ButtonSetLanguageToFrench = false;
            bool_ButtonSetLanguageToPortuguese = false;
            bool_ButtonSetLanguageToSpanish = false;
            bool_ButtonSetLanguageToAddition0 = false;
            bool_ButtonSetLanguageToAddition1 = false;
            bool_ButtonSetLanguageToAddition2 = false;
            bool_ButtonSetLanguageToAddition3 = false;
            bool_ButtonSetLanguageToAddition4 = false;
            bool_ButtonSetLanguageToAddition5 = false;
            bool_ButtonSetLanguageToAddition6 = false;
            

        }

    }

}
