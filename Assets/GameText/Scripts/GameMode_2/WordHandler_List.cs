using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using TMPro;

using CommunicationCorrectAnswerNamespace;
using CommunicationCamaraShakeNamespace;


using LinkCommunicationColoredNamespace;

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_List : MonoBehaviour
{

	public GameObject PlanePointer;

	Vector3 vector3_FirstPositionPlanePointer;


	public GameObject BackgroundOne_0;
	public GameObject BackgroundOne_1;
	public GameObject BackgroundOne_2;
	public GameObject BackgroundOne_3; 
	public GameObject BackgroundOne_4; 
	public GameObject BackgroundOne_5; 
	public GameObject BackgroundOne_6; 

	public GameObject BackgroundTwo_0;
	public GameObject BackgroundTwo_1;
	public GameObject BackgroundTwo_2;
	public GameObject BackgroundTwo_3;
	public GameObject BackgroundTwo_4;
	public GameObject BackgroundTwo_5;
	public GameObject BackgroundTwo_6;
	
	List<GameObject> list_BackGround_One;
	List<GameObject> list_BackGround_Two;



	public GameObject TextOne_0;
	public GameObject TextOne_1;
	public GameObject TextOne_2;
	public GameObject TextOne_3; 
	public GameObject TextOne_4;
	public GameObject TextOne_5;
	public GameObject TextOne_6;


	public GameObject TextTwo_0;
	public GameObject TextTwo_1;
	public GameObject TextTwo_2;
	public GameObject TextTwo_3;
	public GameObject TextTwo_4;
	public GameObject TextTwo_5;
	public GameObject TextTwo_6;
	
	
	List<TextMeshPro> listOfTextMeshPro_One;
	List<TextMeshPro> listOfTextMeshPro_Two;

 	List<string> list_OfStringWordEnglish;
 	List<string> list_OfStringWordFrench;

 	List<string> list_OfStringScreenEnglish;
 	List<string> list_OfStringScreenFrench;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";

	int int_NumberChangeSceneStyleMode = 0;


    void Start()	
    {	


		string string_ToFontAsset = "Fonts/FORCEDSQUARESDF";

		TMP_FontAsset font_asset;

		int int_selectedFont = (int)PlayerPrefs.GetFloat("Font" + "SliderValue");

		if(int_selectedFont  == 0)
		{
			
			int_selectedFont = 1;
		
		}

        string string_GetNameFont = MenuSettingsFontsClass.GetFontName(int_selectedFont - 1);

        font_asset = (TMP_FontAsset)Resources.Load(string_GetNameFont);
		


		var dateRandom = DateTime.Now;
		var dateRandom_Construtor = new DateTime(dateRandom.Year, dateRandom.Month, dateRandom.Day, dateRandom.Hour, dateRandom.Minute, dateRandom.Second);

		int int_hashRandom_SetNextMode = (dateRandom_Construtor.Hour + dateRandom_Construtor.Year + dateRandom_Construtor.Month + dateRandom_Construtor.Day + dateRandom_Construtor.Minute + dateRandom_Construtor.Second);


		System.Random random_GeneratorNextMode = new System.Random(int_hashRandom_SetNextMode);
		
		int_NumberChangeSceneStyleMode = random_GeneratorNextMode.Next(15,20); 
		
		WordsCounterCommunicationClass.int_CurrentNumberWords = 0;
		WordsCounterCommunicationClass.int_TargetNumberWords = int_NumberChangeSceneStyleMode;



		vector3_FirstPositionPlanePointer = PlanePointer.transform.position;

		list_BackGround_One = new List<GameObject>();
    	list_BackGround_Two = new List<GameObject>();

		list_BackGround_One.Add(BackgroundOne_0);
		list_BackGround_One.Add(BackgroundOne_1);
		list_BackGround_One.Add(BackgroundOne_2);
		list_BackGround_One.Add(BackgroundOne_3);
		list_BackGround_One.Add(BackgroundOne_4);
		list_BackGround_One.Add(BackgroundOne_5);
		list_BackGround_One.Add(BackgroundOne_6);

		list_BackGround_Two.Add(BackgroundTwo_0);
		list_BackGround_Two.Add(BackgroundTwo_1);
		list_BackGround_Two.Add(BackgroundTwo_2);
		list_BackGround_Two.Add(BackgroundTwo_3);
		list_BackGround_Two.Add(BackgroundTwo_4);
		list_BackGround_Two.Add(BackgroundTwo_5);
		list_BackGround_Two.Add(BackgroundTwo_6);


		var src = DateTime.Now;
		var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);


		int HashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);



 		list_OfStringWordEnglish = new List<string>();
 		list_OfStringWordFrench = new List<string>();

		list_OfStringScreenEnglish = new List<string>();
		list_OfStringScreenFrench = new List<string>();
		LoadStringList();

    	listOfTextMeshPro_One = new List<TextMeshPro>();
    	listOfTextMeshPro_Two = new List<TextMeshPro>();

		TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_2 = TextOne_2.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_3 = TextOne_3.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_4 = TextOne_4.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_5 = TextOne_5.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_6 = TextOne_6.GetComponent<TextMeshPro>();
	
		TextMeshPro valuesTwo_0 = TextTwo_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesTwo_1 = TextTwo_1.GetComponent<TextMeshPro>();
		TextMeshPro valuesTwo_2 = TextTwo_2.GetComponent<TextMeshPro>();
		TextMeshPro valuesTwo_3 = TextTwo_3.GetComponent<TextMeshPro>();
		TextMeshPro valuesTwo_4 = TextTwo_4.GetComponent<TextMeshPro>();
		TextMeshPro valuesTwo_5 = TextTwo_5.GetComponent<TextMeshPro>();
		TextMeshPro valuesTwo_6 = TextTwo_6.GetComponent<TextMeshPro>();

		System.Random randomGeneratorNumber = null;
		
		int int_randomListPosition = 0;


		randomGeneratorNumber = new System.Random(HashRandom + 5830);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_0.text = string_OneTranslation;
		valuesTwo_0.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);

		randomGeneratorNumber = new System.Random(HashRandom + 5583);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_1.text = string_OneTranslation;
		valuesTwo_1.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);
		

		randomGeneratorNumber = new System.Random(HashRandom + 5085);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_2.text = string_OneTranslation;
		valuesTwo_2.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);

		
		randomGeneratorNumber = new System.Random(HashRandom + 3085);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_3.text = string_OneTranslation;
		valuesTwo_3.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);

		
		randomGeneratorNumber = new System.Random(HashRandom + 9537);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_4.text = string_OneTranslation;
		valuesTwo_4.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);
		
		randomGeneratorNumber = new System.Random(HashRandom + 3485);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_5.text = string_OneTranslation;
		valuesTwo_5.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);
		
		randomGeneratorNumber = new System.Random(HashRandom + 9355);
		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
		string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
		valuesOne_6.text = string_OneTranslation;
		valuesTwo_6.text = string_TwoTranslation;
		list_OfStringScreenEnglish.Add(string_OneTranslation);
		list_OfStringScreenFrench.Add(string_TwoTranslation);


		listOfTextMeshPro_One.Add(valuesOne_0);
		listOfTextMeshPro_One.Add(valuesOne_1);
		listOfTextMeshPro_One.Add(valuesOne_2);
		listOfTextMeshPro_One.Add(valuesOne_3);
		listOfTextMeshPro_One.Add(valuesOne_4);
		listOfTextMeshPro_One.Add(valuesOne_5);
		listOfTextMeshPro_One.Add(valuesOne_6);

		listOfTextMeshPro_Two.Add(valuesTwo_0);
		listOfTextMeshPro_Two.Add(valuesTwo_1);
		listOfTextMeshPro_Two.Add(valuesTwo_2);
		listOfTextMeshPro_Two.Add(valuesTwo_3);
		listOfTextMeshPro_Two.Add(valuesTwo_4);
		listOfTextMeshPro_Two.Add(valuesTwo_5);
		listOfTextMeshPro_Two.Add(valuesTwo_6);

		for(int i = 0; i < listOfTextMeshPro_One.Count; i++)
		{
			listOfTextMeshPro_One[i].font = font_asset;
			listOfTextMeshPro_Two[i].font = font_asset;
			
			listOfTextMeshPro_One[i].fontSize = 15;
			listOfTextMeshPro_Two[i].fontSize = 15;
			
		}


    }


    // void LoadStringList()
    // {


	// 	string string_FirstLanguage = LinkCommunicationLanguagesFilesClass.string_CurrentActiveLanguage_Words_One;
	// 	string string_SecondLanguage = LinkCommunicationLanguagesFilesClass.string_CurrentActiveLanguage_Words_Two;


	// 	TextAsset asset = (TextAsset)Resources.Load(string_FirstLanguage);
	// 	string string_FileLines = asset.ToString();

	// 	string[] lines = string_FileLines.Split(
	//     // new string[] { "\r\n", "\r", "\n" },
	//     new string[] { "\r\n" },
	//     StringSplitOptions.None
	// 	);

	// 	for(int i = 0; i < lines.Length; i++)
	// 	{
	// 		// Debug.Log(lines[i] + "  " + lines[i].Length.ToString());
	// 		list_OfStringWordEnglish.Add(lines[i]);

	// 	}


	// 	// asset = (TextAsset)Resources.Load("WordListFrench");
	// 	asset = (TextAsset)Resources.Load(string_SecondLanguage);
	// 	string_FileLines = asset.ToString();

	// 	string[] lines2 = string_FileLines.Split(
	//     // new string[] { "\r\n", "\r", "\n" },
	//     new string[] { "\r\n" },
	//     StringSplitOptions.None
	// 	);

	// 	for(int i = 0; i < lines2.Length; i++)
	// 	{
	// 		// Debug.Log(lines2[i] + "  " + (lines2[i].Length).ToString());
	// 		list_OfStringWordFrench.Add(lines2[i]);
		
	// 	}


    // }
	
    void LoadStringList()
    {

		string string_FirstLanguage = LinkCommunicationLanguagesFilesClass.string_CurrentActiveLanguage_Words_One;
		string string_SecondLanguage = LinkCommunicationLanguagesFilesClass.string_CurrentActiveLanguage_Words_Two;

		if(LinkCommunicationLanguagesFilesClass.bool_IsResourcesFolderFile == true)
		{

			// TextAsset asset = (TextAsset)Resources.Load("WordListEnglish");
			TextAsset asset = (TextAsset)Resources.Load(string_FirstLanguage);
			string string_FileLines = asset.ToString();

			string[] lines = string_FileLines.Split(
			// new string[] { "\r\n", "\r", "\n" },
			new string[] { "\r\n" },
			StringSplitOptions.None
			);

			for(int i = 0; i < lines.Length; i++)
			{
				// Debug.Log(lines[i] + "  " + lines[i].Length.ToString());
				list_OfStringWordEnglish.Add(lines[i]);

			}

			// asset = (TextAsset)Resources.Load("WordListFrench");
			asset = (TextAsset)Resources.Load(string_SecondLanguage);
			string_FileLines = asset.ToString();

			string[] lines2 = string_FileLines.Split(
			// new string[] { "\r\n", "\r", "\n" },
			new string[] { "\r\n" },
			StringSplitOptions.None
			);

			for(int i = 0; i < lines2.Length; i++)
			{
				// Debug.Log(lines2[i] + "  " + (lines2[i].Length).ToString());
				list_OfStringWordFrench.Add(lines2[i]);
			
			}

		}	
		else
		{
			
			string asset = File.ReadAllText(string_FirstLanguage);

			string string_FileLines = asset;

			string[] lines = string_FileLines.Split(
			new string[] { "\r\n", "\r", "\n" },
			// new string[] { "\r\n" },
			StringSplitOptions.None
			);

			for(int i = 0; i < lines.Length; i++)
			{
				// Debug.Log(lines[i] + "  " + lines[i].Length.ToString());
				list_OfStringWordEnglish.Add(lines[i]);

			}

			// asset = (TextAsset)Resources.Load("WordListFrench");
			asset = File.ReadAllText(string_SecondLanguage);
			string_FileLines = asset;

			string[] lines2 = string_FileLines.Split(
			new string[] { "\r\n", "\r", "\n" },
			// new string[] { "\r\n" },
			StringSplitOptions.None
			);

			for(int i = 0; i < lines2.Length; i++)
			{
				// Debug.Log(lines2[i] + "  " + (lines2[i].Length).ToString());
				list_OfStringWordFrench.Add(lines2[i]);
			
			}

		}	

    }



	
    void ColorCurrentTextList_One(bool bool_SetList, int currentList, int numberOfCharacters)
    { 

		TMP_Text m_TextComponent;

		if(bool_SetList == true)
		{
			m_TextComponent = listOfTextMeshPro_One[currentList].GetComponent<TMP_Text>();

		}
		else
		{
			m_TextComponent = listOfTextMeshPro_Two[currentList].GetComponent<TMP_Text>();
		}

		m_TextComponent.ForceMeshUpdate();


        TMP_TextInfo textInfo = m_TextComponent.textInfo;

        if(numberOfCharacters > 0)
        {

	        for(int i = 0; i < numberOfCharacters; i++)
	        {
		
		        int currentCharacter = i;
		        
		        int characterCount = textInfo.characterCount;
		
		        Color32[] newVertexColors;
		        Color32 c0 = m_TextComponent.color;
		
		        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
		        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
		        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
		
		
		
		        if (textInfo.characterInfo[currentCharacter].isVisible)
		        {
		            c0 = new Color32((byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), (byte)UnityEngine.Random.Range(0, 255), 255);
		            newVertexColors[vertexIndex + 0] = c0;
		            newVertexColors[vertexIndex + 1] = c0;
		            newVertexColors[vertexIndex + 2] = c0;
		            newVertexColors[vertexIndex + 3] = c0;
		            // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
		            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
		            // This last process could be done to only update the vertex data that has changed as opposed to all of the vertex data but it would require extra steps and knowing what type of renderer is used.
		            // These extra steps would be a performance optimization but it is unlikely that such optimization will be necessary.
		        }
	
		    }

        }


        if(numberOfCharacters == 0)
        {

	        for(int i = 0; i < numberOfCharacters; i++)
	        {
		
		        int currentCharacter = i;
		        
		        int characterCount = textInfo.characterCount;
		
		        Color32[] newVertexColors;
		        Color32 c0 = m_TextComponent.color;
		
		        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
		        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
		        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
		
		
		
		        if (textInfo.characterInfo[currentCharacter].isVisible)
		        {
		            c0 = new Color32((byte)255, (byte)255, (byte)255, 255);
		            newVertexColors[vertexIndex + 0] = c0;
		            newVertexColors[vertexIndex + 1] = c0;
		            newVertexColors[vertexIndex + 2] = c0;
		            newVertexColors[vertexIndex + 3] = c0;
		            // New function which pushes (all) updated vertex data to the appropriate meshes when using either the Mesh Renderer or CanvasRenderer.
		            m_TextComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
		            // This last process could be done to only update the vertex data that has changed as opposed to all of the vertex data but it would require extra steps and knowing what type of renderer is used.
		            // These extra steps would be a performance optimization but it is unlikely that such optimization will be necessary.
		        }
	
		    }

		}

    }


    int int_CurrentOne = 0;
    int int_CurrentTwo = 0;

    bool bool_CurrentOne = true;
    bool bool_CurrentTwo = false;

    bool bool_CheckString = false;

    int counter = 0;

    string string_FromInputField = "";



    void Update()
    {

		SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;



		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;			

	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_2 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:11);
				StyleModeClass.int_CurrentSceneGeneral = 11;

	    	}

		}
		else
		{
			
			if(WordsCounterCommunicationClass.int_CurrentNumberWords == int_NumberChangeSceneStyleMode)
			{

				SaveProgressClass.bool_SaveProgresActiveMessage = true;			

				var src = DateTime.Now;
				var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);

				int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

				System.Random randomGeneratorNumber = new System.Random(hashRandom);

				int int_OperativeChangeSceneStyleMode = randomGeneratorNumber.Next(1,12);

				StyleModeClass.int_CurrentSceneGeneral = int_OperativeChangeSceneStyleMode;
				SceneManager.LoadScene (sceneBuildIndex:int_OperativeChangeSceneStyleMode);


			}

		}


		string string_OperativeInputField = LinkCommunicationColoredClass.string_InputField;
		
		if(bool_CurrentOne)
		{

			// Debug.Log("Print elements ||   " + string_OperativeInputField);

			string string_OperativeListEnglish = list_OfStringScreenEnglish[int_CurrentOne];

			int int_CounterToColorChar = 0;

			if(string_OperativeInputField.Length <= string_OperativeListEnglish.Length)
			{				
				for(int i = 0; i < string_OperativeInputField.Length; i++)
				{
					if(string_OperativeInputField[i] == string_OperativeListEnglish[i])
					{
						int_CounterToColorChar++;
					}
					else
					{
						break;
					}
				}
			}
			// Debug.Log("Int COUNTER COLOR 	||||	" + int_CounterToColorChar);
			
			ColorCurrentTextList_One(true, int_CurrentOne, int_CounterToColorChar);

		}

		if(bool_CurrentTwo)
		{

			// Debug.Log("Print elements ||   " + string_OperativeInputField);

			string string_OperativeListFrench = list_OfStringScreenFrench[int_CurrentTwo];

			int int_CounterToColorChar = 0;

			if(string_OperativeInputField.Length <= string_OperativeListFrench.Length)
			{				
				for(int i = 0; i < string_OperativeInputField.Length; i++)
				{
					if(string_OperativeInputField[i] == string_OperativeListFrench[i])
					{
						int_CounterToColorChar++;
					}
					else
					{
						break;
					}
				}
			}
			// Debug.Log("Int COUNTER COLOR 	||||	" + int_CounterToColorChar);
			
			ColorCurrentTextList_One(false, int_CurrentTwo, int_CounterToColorChar);

		}


    	if(bool_CheckString == true)
    	{

    		bool_CheckString = false;

			bool bool_ActiveCamaraShake = true;			

    		if(bool_CurrentOne == true)
    		{
    			if(string_FromInputField == list_OfStringScreenEnglish[int_CurrentOne])
    			{
	
					Vector3 vector3_OperativePositionHandler = PlanePointer.transform.position;
					PlanePointer.transform.position = new Vector3(vector3_OperativePositionHandler.x + 8.0f, vector3_OperativePositionHandler.y, vector3_OperativePositionHandler.z);

    				listOfTextMeshPro_One[int_CurrentOne].text = "";
					list_BackGround_One[int_CurrentOne].SetActive(false);


	    			bool_CurrentOne = false;
	    			bool_CurrentTwo = true;
	
	    			int_CurrentOne ++;

					WordsCounterCommunicationClass.int_CurrentNumberWords++;				
					
					CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;
	   				bool_ActiveCamaraShake = false;


    			}

    		}

    		if(bool_CurrentTwo == true)
    		{

    			if(string_FromInputField == list_OfStringScreenFrench[int_CurrentTwo])
    			{
					
					Vector3 vector3_OperativePositionHandler = PlanePointer.transform.position;
					PlanePointer.transform.position = new Vector3(vector3_OperativePositionHandler.x - 8.0f, vector3_OperativePositionHandler.y - 1.1f, vector3_OperativePositionHandler.z);

    				listOfTextMeshPro_Two[int_CurrentTwo].text = "";
					list_BackGround_Two[int_CurrentTwo].SetActive(false);

    				bool_CurrentOne = true;
    				bool_CurrentTwo = false;	

    				int_CurrentTwo ++;

					WordsCounterCommunicationClass.int_CurrentNumberWords++;				

					CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;
	   				bool_ActiveCamaraShake = false;

	   			}    			

    		}

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = bool_ActiveCamaraShake;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = bool_ActiveCamaraShake;


    	}


    	if(LinkCommunicationColoredClass.bool_ActiveStatus == true)
    	{

    		LinkCommunicationColoredClass.bool_ActiveStatus = false;

    		Debug.Log("String In WordHandler_List = " + LinkCommunicationColoredClass.string_InputField);

    		string_FromInputField = LinkCommunicationColoredClass.string_InputField;


    		bool_CheckString = true;

    	}


    	if(int_CurrentTwo == 7)
    	{
	        float_CurrentTime = Time.realtimeSinceStartup;

			// System.Random randomGeneratorNumber = new System.Random((int) float_CurrentTime);
			// int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
			
			PlanePointer.transform.position = vector3_FirstPositionPlanePointer;
			
			System.Random randomGeneratorNumber;
			int int_randomListPosition ;

    		for(int i = 0; i < listOfTextMeshPro_One.Count; i++)
    		{

				randomGeneratorNumber = new System.Random(i + (int) float_CurrentTime);
				int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringWordEnglish.Count);
				string_OneTranslation = list_OfStringWordEnglish[int_randomListPosition];
				string_TwoTranslation = list_OfStringWordFrench[int_randomListPosition];  
				listOfTextMeshPro_One[i].text = string_OneTranslation;
				listOfTextMeshPro_Two[i].text = string_TwoTranslation;
				list_OfStringScreenEnglish[i] = string_OneTranslation;
				list_OfStringScreenFrench[i] = string_TwoTranslation;

				list_BackGround_One[i].SetActive(true);
				list_BackGround_Two[i].SetActive(true);

    		}

    		int_CurrentOne = 0;
		    int_CurrentTwo = 0;
		
		    bool_CurrentOne = true;
		    bool_CurrentTwo = false;

    	}
        
    }

}
