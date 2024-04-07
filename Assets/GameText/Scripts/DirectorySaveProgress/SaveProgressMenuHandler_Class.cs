using System;
using System.IO;
using System.Text; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using SaveProgressNamespace;


public class SaveProgressMenuHandler_Class : MonoBehaviour
{

    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode1;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode2;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode3;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode4;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode5;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode6;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode7;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode8;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode9;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode10;
    [SerializeField]    
    private GameObject gameobject_TextProgressGameMode11;

    
    private string string_FIlePathJSON_SaveProgressData;
    

    private List<SaveProgressData_Class> list_SPD_Class_Holder;      


    void Start()
    {

        string string_PathDevice  = Application.persistentDataPath;
        string string_DirectoryLocation = string_PathDevice + "/User_GameModeProgress_Directory";

        Debug.Log(string_DirectoryLocation);


        if(Directory.Exists(string_DirectoryLocation) == false)
        {
        
            Directory.CreateDirectory(string_DirectoryLocation);

        }


        string string_FilePath = string_DirectoryLocation + "/User_GameModeProgress_Data.json";


        string_FIlePathJSON_SaveProgressData = string_FilePath;


        if (File.Exists(string_FilePath) == false)
        {
            
            SaveProgressData_Class SaveProgressData_Class_Variable;
            
            int int_GameModeType = 0;
            int int_CurrentProgress = 0;
            string string_FullDataToFile_JSON = "";

            for(int i = 0; i < 11; i++)
            {
                
                SaveProgressData_Class_Variable = new SaveProgressData_Class();

                SaveProgressData_Class_Variable.int_GameModeType = i + 1;

                SaveProgressData_Class_Variable.int_CurrentProgress = 0;

                string string_ToWrite = JsonUtility.ToJson(SaveProgressData_Class_Variable) +  Environment.NewLine;

                string_FullDataToFile_JSON += string_ToWrite;

                Debug.Log(string_ToWrite);

            }

            File.WriteAllText(string_FIlePathJSON_SaveProgressData, string_FullDataToFile_JSON, Encoding.UTF8);

        }
        else
        {

            list_SPD_Class_Holder = new List<SaveProgressData_Class>();

            for(int i = 0; i < 11; i++)
            {

                SaveProgressData_Class SPD_Class_Element = new SaveProgressData_Class();
                list_SPD_Class_Holder.Add(SPD_Class_Element);

            }

            string[] string_FullFileToData_JSON = File.ReadAllLines(string_FIlePathJSON_SaveProgressData);

            for(int i = 0 ; i < string_FullFileToData_JSON.Length; i++)
            {

                // Debug.Log(string_FullFileToData_JSON[i]);
                list_SPD_Class_Holder[i] = JsonUtility.FromJson<SaveProgressData_Class>(string_FullFileToData_JSON[i]);

            }

            for(int i = 0 ; i < list_SPD_Class_Holder.Count; i++)
            {

                // Debug.Log(list_SPD_Class_Holder[i].int_GameModeType.ToString() + "   ||||    " +  list_SPD_Class_Holder[i].int_CurrentProgress.ToString());

            }

            Debug.Log("The thing reach this point");

        }
    

        TMP_Text tmp_TextProgressGameMode1 = gameobject_TextProgressGameMode1.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode2 = gameobject_TextProgressGameMode2.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode3 = gameobject_TextProgressGameMode3.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode4 = gameobject_TextProgressGameMode4.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode5 = gameobject_TextProgressGameMode5.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode6 = gameobject_TextProgressGameMode6.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode7 = gameobject_TextProgressGameMode7.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode8 = gameobject_TextProgressGameMode8.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode9 = gameobject_TextProgressGameMode9.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode10 = gameobject_TextProgressGameMode10.GetComponent<TMP_Text>();
        TMP_Text tmp_TextProgressGameMode11 = gameobject_TextProgressGameMode11.GetComponent<TMP_Text>();
        

        tmp_TextProgressGameMode1.text = list_SPD_Class_Holder[0].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode2.text = list_SPD_Class_Holder[1].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode3.text = list_SPD_Class_Holder[2].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode4.text = list_SPD_Class_Holder[3].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode5.text = list_SPD_Class_Holder[4].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode6.text = list_SPD_Class_Holder[5].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode7.text = list_SPD_Class_Holder[6].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode8.text = list_SPD_Class_Holder[7].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode9.text = list_SPD_Class_Holder[8].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode10.text = list_SPD_Class_Holder[9].int_CurrentProgress.ToString();
        tmp_TextProgressGameMode11.text = list_SPD_Class_Holder[10].int_CurrentProgress.ToString();


    }

}
