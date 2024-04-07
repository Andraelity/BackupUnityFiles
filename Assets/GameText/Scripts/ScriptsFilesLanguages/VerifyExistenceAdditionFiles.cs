using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text; 

using UnityEngine;

public class VerifyExistenceAdditionFiles : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        string string_PathDevice  = Application.persistentDataPath;

        string string_DirectoryLocation = string_PathDevice + "/Language_Addition_Files";
        
        Debug.Log(string_DirectoryLocation);

        if(Directory.Exists(string_DirectoryLocation) == false)
        {
        
            Directory.CreateDirectory(string_DirectoryLocation);

            string string_DirectoryAddition = string_DirectoryLocation + "/Language_Addition_";

            for(int i = 0; i < 7; i++)
            {

                string string_OperationalDirectoryAddition = string_DirectoryAddition + i.ToString();
                
                Directory.CreateDirectory(string_OperationalDirectoryAddition);

                string string_Operational_Name_File_Addition_One = string_OperationalDirectoryAddition + "/Language_Addition_" + i.ToString() + "_Name_0.json";
                string string_Operational_Name_File_Addition_Two = string_OperationalDirectoryAddition + "/Language_Addition_" + i.ToString() + "_Name_1.json";

                string string_Operational_Words_File_Addition_One = string_OperationalDirectoryAddition + "/Language_Addition_" + i.ToString() + "_Words_0.json";
                string string_Operational_Words_File_Addition_Two = string_OperationalDirectoryAddition + "/Language_Addition_" + i.ToString() + "_Words_1.json";

                string string_Operational_Sentences_File_Addition_One = string_OperationalDirectoryAddition + "/Language_Addition_" + i.ToString() + "_Sentences_0.json";
                string string_Operational_Sentences_File_Addition_Two = string_OperationalDirectoryAddition + "/Language_Addition_" + i.ToString() + "_Sentences_1.json";

                File.WriteAllText(string_Operational_Name_File_Addition_One, "Empty" + i.ToString(), Encoding.Unicode);
                File.WriteAllText(string_Operational_Name_File_Addition_Two, "Empty" + i.ToString(), Encoding.Unicode);

                File.WriteAllText(string_Operational_Words_File_Addition_One, "Words" + i.ToString(), Encoding.Unicode);
                File.WriteAllText(string_Operational_Words_File_Addition_Two, "Words" + i.ToString(), Encoding.Unicode);

                File.WriteAllText(string_Operational_Sentences_File_Addition_One, "Sentences" + i.ToString(), Encoding.Unicode);
                File.WriteAllText(string_Operational_Sentences_File_Addition_Two, "Sentences" + i.ToString(), Encoding.Unicode);

            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
