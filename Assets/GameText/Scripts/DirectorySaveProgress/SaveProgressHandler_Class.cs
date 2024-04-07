using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text; 

using SaveProgressNamespace;



public class SaveProgressData_Class
{

    public int int_GameModeType = 0;
    public int int_CurrentProgress = 0;

}


public class SaveProgressHandler_Class : MonoBehaviour
{

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

                Debug.Log(string_FullFileToData_JSON[i]);
                list_SPD_Class_Holder[i] = JsonUtility.FromJson<SaveProgressData_Class>(string_FullFileToData_JSON[i]);

            }

            // for(int i = 0 ; i < list_SPD_Class_Holder.Count; i++)
            {

                // Debug.Log(list_SPD_Class_Holder[i].int_GameModeType.ToString() + "   ||||    " +  list_SPD_Class_Holder[i].int_CurrentProgress.ToString());

            }

            // ContainerUserInformation_Class ContainerUserInformation_Variable = JsonUtility.FromJson<ContainerUserInformation_Class>(string_ContainerUserInformation_JSON);
            // string_NameOfTheUser = ContainerUserInformation_Variable.string_NameOfTheUser;
            // int_LevelOfUser = ContainerUserInformation_Variable.int_CurrentLevelUser;
            // textmeshpro_InputField.text = string_NameOfTheUser;
            // textmeshpro_PlaceHolder.text = string_NameOfTheUser;
            // textmeshpro_MiddleScreenUserName.text = string_NameOfTheUser;
            // textmeshpro_TextLevel.text = "Level " + int_LevelOfUser.ToString();


        }
    
    }

    private bool bool_OverThressHold = false;

    void Update()
    {

        if(SaveProgressClass.bool_SaveProgresActiveMessage == true)
        {
            SaveProgressClass.bool_SaveProgresActiveMessage = false;

            if(SaveProgressClass.int_SaveProgres_GameMode_1 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[0].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_1;
                SaveProgressClass.int_SaveProgres_GameMode_1 = 0;
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_2 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[1].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_2;
                SaveProgressClass.int_SaveProgres_GameMode_2 = 0;
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_3 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[2].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_3;
                SaveProgressClass.int_SaveProgres_GameMode_3 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_4 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[3].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_4;
                SaveProgressClass.int_SaveProgres_GameMode_4 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_5 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[4].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_5;
                SaveProgressClass.int_SaveProgres_GameMode_5 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_6 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[5].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_6;
                SaveProgressClass.int_SaveProgres_GameMode_6 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_7 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[6].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_7;
                SaveProgressClass.int_SaveProgres_GameMode_7 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_8 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[7].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_8;
                SaveProgressClass.int_SaveProgres_GameMode_8 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_9 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[8].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_9;
                SaveProgressClass.int_SaveProgres_GameMode_9 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_10 >= 2)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[9].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_10;
                SaveProgressClass.int_SaveProgres_GameMode_10 = 0;
            
            }

            if(SaveProgressClass.int_SaveProgres_GameMode_11 >= 10)
            {
                bool_OverThressHold = true;
                list_SPD_Class_Holder[10].int_CurrentProgress += SaveProgressClass.int_SaveProgres_GameMode_11;
                SaveProgressClass.int_SaveProgres_GameMode_11 = 0;
            
            }
                        
        } 

        if(bool_OverThressHold == true)
        {

            bool_OverThressHold = false;

            string string_FullDataToFile_JSON = "";
        
            for(int i = 0 ; i < list_SPD_Class_Holder.Count; i++)
            {
            
                string string_ToWrite = JsonUtility.ToJson(list_SPD_Class_Holder[i]) +  Environment.NewLine;

                string_FullDataToFile_JSON += string_ToWrite;
                Debug.Log(string_ToWrite);
            }

            File.WriteAllText(string_FIlePathJSON_SaveProgressData, string_FullDataToFile_JSON, Encoding.UTF8);
            
            SaveProgressClass.bool_SaveProgressLevelUpdate = true;
            

        }

        
    } 
    
}
