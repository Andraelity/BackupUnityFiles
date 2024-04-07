using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using LinkCommunicationColoredNamespace;
using CommunicationCamaraShakeNamespace;
using CommunicationCorrectAnswerNamespace;
using CommunciationImplementationResetNamespace;

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_RunWords : MonoBehaviour
{


	public GameObject Container_0;
	public GameObject Container_1;

	public GameObject backgroundShader_0;
	public GameObject backgroundShader_1;

	public GameObject BoxOne_0;
	public GameObject BoxOne_1;

	public GameObject TextOne_0;
	public GameObject TextOne_1;

	Vector3 vector3_PositionStartBackground_0;
	Vector3 vector3_PositionStartBackground_1;



	List<GameObject> list_GameObjectInstanciated_0;
	List<GameObject> list_GameObjectInstanciated_1;
	List<GameObject> list_GameObjectInstanciated_2;
	List<GameObject> list_GameObjectInstanciated_3;
	List<GameObject> list_GameObjectInstanciated_4;
	List<GameObject> list_GameObjectInstanciated_5;

	List<List<GameObject>> list_ListGameObjectHolder;
		

	// List<float> list_FloatHorizontalLastPositionInstanced;


 	List<string> list_OfStringEnglish;
 	List<string> list_OfStringFrench;

 	List<List<string>> list_ListStringWordsRow;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";


	// List<int> list_PositionCheck;

	float float_horizontalPosition_0 = -9.93f;

	List<float> list_PositionVeritalStart = new List<float>()
	{

		 4.9f,
		 3.6f,
		 2.3f,
		 1.0f,
		-0.3f,
		-1.6f

	};

	List<float> list_LastPositionWordWidth = new List<float>
	{

		0.0f,
		0.0f,
		0.0f,
		0.0f,
		0.0f,
		0.0f

	};

	List<float> list_LastPositionWord = new List<float>
	{

		-9.93f,
		-9.93f,	
		-9.93f,
		-9.93f,
		-9.93f,
		-9.93f

	};

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



 		list_OfStringEnglish = new List<string>();
 		list_OfStringFrench = new List<string>();


		LoadStringList();


		TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();

		valuesOne_0.font = font_asset; 		
		valuesOne_1.font = font_asset; 
		valuesOne_0.fontSize = 15; 
		valuesOne_1.fontSize = 15; 

		var src = DateTime.Now;
		var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
		int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

		System.Random randomGeneratorNumber = new System.Random(hashRandom);
		int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
		string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
		
		// string_OneTranslation = "HelloBaby";
		// string_TwoTranslation = "BonjourBaby";
		
		// int_LengthBothWords = string_OneTranslation.Length + string_TwoTranslation.Length;

		valuesOne_0.text = string_OneTranslation;
		valuesOne_1.text = string_TwoTranslation;




		list_ListStringWordsRow = new List<List<string>>();		
		List<string> list_StringWordsRow_0 = new List<string>();
		List<string> list_StringWordsRow_1 = new List<string>();
		List<string> list_StringWordsRow_2 = new List<string>();
		List<string> list_StringWordsRow_3 = new List<string>();
		List<string> list_StringWordsRow_4 = new List<string>();
		List<string> list_StringWordsRow_5 = new List<string>();
		list_ListStringWordsRow.Add(list_StringWordsRow_0);
		list_ListStringWordsRow.Add(list_StringWordsRow_1);
		list_ListStringWordsRow.Add(list_StringWordsRow_2);
		list_ListStringWordsRow.Add(list_StringWordsRow_3);
		list_ListStringWordsRow.Add(list_StringWordsRow_4);
		list_ListStringWordsRow.Add(list_StringWordsRow_5);



		// list_LastPositionWordWidth = new List<float>();

		list_ListGameObjectHolder = new List<List<GameObject>>();
		list_GameObjectInstanciated_0 = new List<GameObject>();
		list_GameObjectInstanciated_1 = new List<GameObject>();
		list_GameObjectInstanciated_2 = new List<GameObject>();
		list_GameObjectInstanciated_3 = new List<GameObject>();
		list_GameObjectInstanciated_4 = new List<GameObject>();
		list_GameObjectInstanciated_5 = new List<GameObject>();
		list_ListGameObjectHolder.Add(list_GameObjectInstanciated_0);
		list_ListGameObjectHolder.Add(list_GameObjectInstanciated_1);
		list_ListGameObjectHolder.Add(list_GameObjectInstanciated_2);
		list_ListGameObjectHolder.Add(list_GameObjectInstanciated_3);
		list_ListGameObjectHolder.Add(list_GameObjectInstanciated_4);
		list_ListGameObjectHolder.Add(list_GameObjectInstanciated_5);




		for(int i = 0 ; i < 6; i++)
		{

			Vector3 vector3_PositionWord = new Vector3(float_horizontalPosition_0, list_PositionVeritalStart[i], 10.0f);

			GameObject gameobject_InstanceContainer;

			int numberOperationRandom = randomGeneratorNumber.Next();

			int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
		
			string string_selectedWord = "";

			if(numberOperationRandom % 2 == 0)
			{
			
				gameobject_InstanceContainer = Instantiate(Container_0, vector3_PositionWord, Quaternion.identity);
				string_selectedWord = list_OfStringEnglish[int_randomListPosition];
			
			}
			else
			{

				gameobject_InstanceContainer = Instantiate(Container_1, vector3_PositionWord, Quaternion.identity);
				string_selectedWord = list_OfStringFrench[int_randomListPosition];

			}

					
			gameobject_InstanceContainer.name = "container_0_" + i.ToString();

			GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
			GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
			GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;


			TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();
	
			textMeshObject.font = font_asset;
			
			textMeshObject.fontSize = 15;


 
			textMeshObject.text = string_selectedWord;
 

			float widthText_One   = textMeshObject.preferredWidth;
			float heightText_One  = textMeshObject.preferredHeight;
 
 
			backgroundShaderObject.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShaderObject.transform.localScale); 	
			backgroundPlaneObject.transform.localScale = GetScaleBackGround(widthText_One, backgroundPlaneObject.transform.localScale);
	 		

			// vector3_PositionStartBackground_Correct_0 = new Vector3(BoxOne_Correct_0.transform.position.x, BoxOne_Correct_0.transform.position.y, BoxOne_Correct_0.transform.position.z);


			backgroundShaderObject.transform.position += GetPositionBackGround(widthText_One);
			backgroundPlaneObject.transform.position += GetPositionBackGround(widthText_One);

			list_ListStringWordsRow[i].Add(string_selectedWord);
			list_ListGameObjectHolder[i].Add(gameobject_InstanceContainer);

			list_LastPositionWordWidth[i] = GetPositionBackGround(widthText_One).x;

		}


		for(int i = 0; i < list_SpeedHorizontal.Count; i++)
		{

			list_SpeedHorizontal[i] = (0.6f *  ((float)randomGeneratorNumber.NextDouble())) + 0.4f;

		}

    }



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
				list_OfStringEnglish.Add(lines[i]);

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
				list_OfStringFrench.Add(lines2[i]);
			
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
				list_OfStringEnglish.Add(lines[i]);

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
				list_OfStringFrench.Add(lines2[i]);
			
			}

		}	

    }



	
	List<int> GenerateRandomLoop(List<int> listToShuffle)
	{


		var src = DateTime.Now;
		var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);

		int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

		
		System.Random _rand = new System.Random(95397 + hashRandom);


	    for (int i = listToShuffle.Count - 1; i > 0; i--)
	    {
	        var k = _rand.Next(i + 1);
	        var valueList = listToShuffle[k];
	        listToShuffle[k] = listToShuffle[i];
	        listToShuffle[i] = valueList;
	    }
	    return listToShuffle;
	}


    Vector3 GetScaleBackGroundShader(float width, Vector3 Current)
    {
		float scale = (width * 0.047f)/1.15f ;
    	Vector3 valueOut = new Vector3(scale, Current.y, Current.z);
  
    	return valueOut;
    }


    Vector3 GetScaleBackGround(float width, Vector3 Current)
    {
		float scale = (width * 0.035f)/1.15f ;
    	Vector3 valueOut = new Vector3(scale, Current.y, Current.z);
  
    	return valueOut;
    }

    // 10 = 2
    // x  = 1

    // 0.16 = 1.15
    // position = width;


    Vector3 GetPositionBackGround(float width)
    {


		float position = (width * 0.16f)/1.15f ; 
    	Vector3 valueOut = new Vector3(position, 0.0f, 0.0f);
  
    	return valueOut;
    	// return position;
    }



    void ColorCurrentTextWord(int int_Position_i, int int_Position_j, int int_NumberOfCharacters)
    { 
 
		GameObject textObject = list_ListGameObjectHolder[int_Position_i][int_Position_j].transform.GetChild(2).gameObject;
		// TMP_Text m_TextComponent = list_ListGameObjectHolder[int_Position_i][int_Position_j].GetComponent<TMP_Text>();
		TMP_Text m_TextComponent = textObject.GetComponent<TMP_Text>();

 
 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < int_NumberOfCharacters; i++)
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



    void ColorCurrentTextWordDefault(int int_Position_i, int int_Position_j, int int_NumberOfCharacters)
    { 
 
		GameObject textObject = list_ListGameObjectHolder[int_Position_i][int_Position_j].transform.GetChild(2).gameObject;
		// TMP_Text m_TextComponent = list_ListGameObjectHolder[int_Position_i][int_Position_j].GetComponent<TMP_Text>();
		TMP_Text m_TextComponent = textObject.GetComponent<TMP_Text>();

 
 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < int_NumberOfCharacters; i++)
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
	            c0 = new Color32((255), (255), (255), 255);
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



    bool bool_ResetListOfWords = false;


	bool bool_CheckString = false;

    string string_FromInputField = "";

    int counterColoredCharacters = 0;



    bool bool_ActiveCamaraShake = false;
    
    float float_evaluationTime = 0.0f;
	
	int int_CounterCollisionChar = 0;
	int int_LengthBothWords = 0;


    bool tab_press = false;

    int counter_tab = 0;




	List<float> list_SpeedHorizontal = new List<float>
	{
	
		0.5f,
		0.7f,
		1.0f,
		0.6f,
		0.9f,
		0.8f

	};

	List<float> list_TimeUpdatedRow = new List<float>
	{

		0.0f,
		0.0f,
		0.0f,
		0.0f,
		0.0f,
		0.0f
	
	};

	int int_ListColorCountBefore = 0;
	int int_ListColorCountCurrent = 0;

	bool bool_IdentifiedRemoveColor = false;

	List<List<int>> list_ListIntPositionToColorBefore = new List<List<int>>();

	int int_FirstCorrectWordLength = 0;

    List<int> list_IntColored = new List<int>{0, 0, 0, 0, 0, 0};



	public int GetMaxColored(List<int> list)
	{
	 
	    int maxAge = 0;

	    foreach (int valueList in list)
	    {
	        if (valueList > maxAge)
	        {
	            maxAge = valueList;
	        }
	    }

	    return maxAge;

	}



    void Update()
    {

		SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;


		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;


	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{


				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_8 = WordsCounterCommunicationClass.int_CurrentNumberWords;
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



    	float_CurrentTime = Time.realtimeSinceStartup;


    	List<List<int>> list_ListIntPositionToColor = new List<List<int>>();

    	string string_FromInputFieldOperational = LinkCommunicationColoredClass.string_InputField;

    	// list_ListStringWordsRow.Count == 6, WOULD ALWAYS BE 6
    	for(int i = 0; i < 6; i++)
    	{

    		for(int j = 0; j < list_ListStringWordsRow[i].Count; j++)
    		{

    			string string_CurrentOperational = list_ListStringWordsRow[i][j];

    			int int_CounterCharToColor = 0;

    			// if(string_CurrentOperational.Length <= string_FromInputFieldOperational.Length)
    			// {

	    			for(int k = 0 ; k < string_FromInputFieldOperational.Length; k++)
	    			{
	
	    				if(string_FromInputFieldOperational.Length > string_CurrentOperational.Length)
	    				{

	    					break;

	    				}

	    				if(string_CurrentOperational[k] == string_FromInputFieldOperational[k])
	    				{
						
							int_CounterCharToColor++;
							continue;	
	    				
	    				}

	    				break;
	
	    			}
			
    			// }

    			if(int_CounterCharToColor > 0)
    			{

    				List<int> list_IntIdentified = new List<int>();

    				list_IntIdentified.Add(i);
    				list_IntIdentified.Add(j);
    				list_IntIdentified.Add(int_CounterCharToColor);

    				list_ListIntPositionToColor.Add(list_IntIdentified);

    			}

    		}

    	}


    	
    	for(int i = 0; i < list_ListIntPositionToColor.Count; i++)
    	{
 
			ColorCurrentTextWord(list_ListIntPositionToColor[i][0], list_ListIntPositionToColor[i][1], list_ListIntPositionToColor[i][2]);
 
    	}

		// ColorCurrentTextWord(list_ListIntPositionToColor[i][0], list_ListIntPositionToColor[i][1], list_ListIntPositionToColor[i][2]);
		// ColorCurrentTextWord(0, 0, 3);




		// list_ListGameObjectHolder[i].Count == 6, would always be 6
    	for(int i = 0; i < 6 ; i++)
    	{

    		if(float_CurrentTime > list_TimeUpdatedRow[i] + list_SpeedHorizontal[i])
    		{

	 			list_TimeUpdatedRow[i] = float_CurrentTime;	

	    		for(int j = 0; j < list_ListGameObjectHolder[i].Count; j++)
				{
		

					list_ListGameObjectHolder[i][j].transform.position += new Vector3(0.5f, 0.0f, 0.0f);

				}
				
				list_LastPositionWord[i] += 0.5f;

    		}

    	}


		// list_LastPositionWord.Count == 6, WOULD ALWAYS BE 6
    	for(int i = 0; i < 6; i++)
    	{

			if(list_LastPositionWord[i] - list_LastPositionWordWidth[i] >  float_horizontalPosition_0 + 3.0)
	    	{
				
				Vector3 vector3_PositionWord = new Vector3(float_horizontalPosition_0, list_PositionVeritalStart[i], 10.0f);

				var src = DateTime.Now;
				var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
				int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);
		
				System.Random randomGeneratorNumber = new System.Random(hashRandom);



				GameObject gameobject_InstanceContainer;
	
				int numberOperationRandom = randomGeneratorNumber.Next();
	
				int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
			
				string string_selectedWord = "";
	
				if(numberOperationRandom % 2 == 0)
				{
				
					gameobject_InstanceContainer = Instantiate(Container_0, vector3_PositionWord, Quaternion.identity);
					string_selectedWord = list_OfStringEnglish[int_randomListPosition];

				}
				else
				{
	
					gameobject_InstanceContainer = Instantiate(Container_1, vector3_PositionWord, Quaternion.identity);
					string_selectedWord = list_OfStringFrench[int_randomListPosition];
	
				}


				
				gameobject_InstanceContainer.name = "container_0_" + i.ToString();

				GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
				GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
				GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;
	
	
				TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();
	 
	 
				textMeshObject.text = string_selectedWord;
	 
				float widthText_One   = textMeshObject.preferredWidth;
				float heightText_One  = textMeshObject.preferredHeight;
	 
	 	
				backgroundShaderObject.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShaderObject.transform.localScale); 		
				backgroundPlaneObject.transform.localScale = GetScaleBackGround(widthText_One, backgroundPlaneObject.transform.localScale);			
		 
				// vector3_PositionStartBackground_Correct_0 = new Vector3(BoxOne_Correct_0.transform.position.x, BoxOne_Correct_0.transform.position.y, BoxOne_Correct_0.transform.position.z);
		 
				backgroundShaderObject.transform.position += GetPositionBackGround(widthText_One);
				backgroundPlaneObject.transform.position += GetPositionBackGround(widthText_One);
	
				list_ListStringWordsRow[i].Add(string_selectedWord);
				list_ListGameObjectHolder[i].Add(gameobject_InstanceContainer);
				
				list_LastPositionWordWidth[i] = GetPositionBackGround(widthText_One).x;
				list_LastPositionWord[i] = float_horizontalPosition_0; 

	    	}
   
    	}
		

    	// ColorCurrentTextWordOne(list_IntCharToColorOne);
    	// ColorCurrentTextWordTwo(list_IntCharToColorTwo);
    	bool_IdentifiedRemoveColor = true;


    	if(bool_CheckString)
    	{

    		bool_CheckString = false;
    		bool_ActiveCamaraShake = true;

    		//  list_ListStringWordsRow.Count == 6 , WOULD ALWAYS BE 6;
    		bool bool_BreakHigherLoop = false;

    		int int_PositionIdentified_i = 0;
    		int int_PositionIdentified_j = 0;

    		//HIGHERLOOP//
    		for(int i = 0 ; i < 6; i ++)
    		{

    			for(int j = 0 ; j < list_ListStringWordsRow[i].Count; j ++)
    			{

    				if(string_FromInputField == list_ListStringWordsRow[i][j])
    				{
    					int_PositionIdentified_i = i;
    					int_PositionIdentified_j = j;
    					bool_BreakHigherLoop = true;

    					break;
    				}

    			}

    			if(bool_BreakHigherLoop)
    			{

    				break;

    			}

    		}


			if(bool_BreakHigherLoop)
			{

				WordsCounterCommunicationClass.int_CurrentNumberWords++;				

				CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

    			bool_ActiveCamaraShake = false;

				Destroy(list_ListGameObjectHolder[int_PositionIdentified_i][int_PositionIdentified_j]);

				list_ListGameObjectHolder[int_PositionIdentified_i].RemoveAt(int_PositionIdentified_j);
				list_ListStringWordsRow[int_PositionIdentified_i].RemoveAt(int_PositionIdentified_j);

			}    		

			bool_IdentifiedRemoveColor = false;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = bool_ActiveCamaraShake;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = bool_ActiveCamaraShake;

    	}

    	int_ListColorCountBefore = list_ListIntPositionToColorBefore.Count;
    	int_ListColorCountCurrent = list_ListIntPositionToColor.Count;
  	

		if(bool_IdentifiedRemoveColor)
		{

			if(int_ListColorCountBefore > int_ListColorCountCurrent)
			{

				for(int i = 0; i < list_ListIntPositionToColorBefore.Count; i++)
				{

					ColorCurrentTextWordDefault(list_ListIntPositionToColorBefore[i][0], list_ListIntPositionToColorBefore[i][1], list_ListIntPositionToColorBefore[i][2]);

				}

			}
		
		}



		if(bool_IdentifiedRemoveColor == false || LinkCommunicationColoredClass.string_InputField == "")
		{

		  	for(int i = 0; i < 6; i++)
	    	{
	
	    		for(int j = 0; j < list_ListStringWordsRow[i].Count; j++)
	    		{

	    			ColorCurrentTextWordDefault(i, j, list_ListStringWordsRow[i][j].Length);

	    		}
	    		
	    	}

		}


    	// list_ListGameObjectHolder == 6, WOULD ALWAYS BE 6
   		for(int i = 0; i < 6; i++)
    	{
    		bool bool_PositionIdentified = false;

    		for(int j = 0; j < list_ListGameObjectHolder[i].Count; j++)
    		{

				if(list_ListGameObjectHolder[i][j].transform.position.x  >  14.0f)
		    	{
					
					bool_ResetListOfWords = true;

					bool_PositionIdentified = true;
					break;

				}
    
    		}

    		if(bool_PositionIdentified == true)
    		{

    			break;

    		}

		}



    	if(LinkCommunicationColoredClass.bool_ActiveStatus == true)
    	{	

    		LinkCommunicationColoredClass.bool_ActiveStatus = false;

    		Debug.Log("String In InputField After ENTER  == " + LinkCommunicationColoredClass.string_InputField);

    		string_FromInputField = LinkCommunicationColoredClass.string_InputField;

    		bool_CheckString = true;

    	}

		if(bool_ResetListOfWords)
    	{
		
			
			bool_ResetListOfWords = false;
			// CommunicationCollisionClass.bool_ResetListOfWords = false;    		


			for(int i = 0; i < list_ListGameObjectHolder.Count; i++)
			{
				for(int j = 0; j < list_ListGameObjectHolder[i].Count; j ++)
				{

					 Destroy(list_ListGameObjectHolder[i][j]);
				
				}
			}


    		bool_ResetListOfWords = false;
	        float_CurrentTime = Time.realtimeSinceStartup;



	
			TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
			TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();
	
	
	 
			var src = DateTime.Now;
			var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
			int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);
	
			System.Random randomGeneratorNumber = new System.Random(hashRandom);
			int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
			

			// string_OneTranslation = "HelloBaby";
			// string_TwoTranslation = "BonjourBaby";
			
			// int_LengthBothWords = string_OneTranslation.Length + string_TwoTranslation.Length;
	
			valuesOne_0.text = string_OneTranslation;
			valuesOne_1.text = string_TwoTranslation;



			list_ListStringWordsRow = new List<List<string>>();		
			List<string> list_StringWordsRow_0 = new List<string>();
			List<string> list_StringWordsRow_1 = new List<string>();
			List<string> list_StringWordsRow_2 = new List<string>();
			List<string> list_StringWordsRow_3 = new List<string>();
			List<string> list_StringWordsRow_4 = new List<string>();
			List<string> list_StringWordsRow_5 = new List<string>();
			list_ListStringWordsRow.Add(list_StringWordsRow_0);
			list_ListStringWordsRow.Add(list_StringWordsRow_1);
			list_ListStringWordsRow.Add(list_StringWordsRow_2);
			list_ListStringWordsRow.Add(list_StringWordsRow_3);
			list_ListStringWordsRow.Add(list_StringWordsRow_4);
			list_ListStringWordsRow.Add(list_StringWordsRow_5);
	
		
			// list_LastPositionWordWidth = new List<float>();

			list_ListGameObjectHolder = new List<List<GameObject>>();
			list_GameObjectInstanciated_0 = new List<GameObject>();
			list_GameObjectInstanciated_1 = new List<GameObject>();
			list_GameObjectInstanciated_2 = new List<GameObject>();
			list_GameObjectInstanciated_3 = new List<GameObject>();
			list_GameObjectInstanciated_4 = new List<GameObject>();
			list_GameObjectInstanciated_5 = new List<GameObject>();
			list_ListGameObjectHolder.Add(list_GameObjectInstanciated_0);
			list_ListGameObjectHolder.Add(list_GameObjectInstanciated_1);
			list_ListGameObjectHolder.Add(list_GameObjectInstanciated_2);
			list_ListGameObjectHolder.Add(list_GameObjectInstanciated_3);
			list_ListGameObjectHolder.Add(list_GameObjectInstanciated_4);
			list_ListGameObjectHolder.Add(list_GameObjectInstanciated_5);
	
	
	

	
			for(int i = 0; i < list_SpeedHorizontal.Count; i++)
			{
	
				list_SpeedHorizontal[i] = (0.6f *  ((float)randomGeneratorNumber.NextDouble())) + 0.4f;
	
				// Debug.Log(list_SpeedHorizontal[i]);
			}

	


			list_IntColored = new List<int>{0, 0, 0, 0, 0, 0};


    		float_evaluationTime = float_CurrentTime;

			CommunciationImplementationResetClass.bool_ActiveResetWords = true;

			CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;



    	}
        
    }

}
