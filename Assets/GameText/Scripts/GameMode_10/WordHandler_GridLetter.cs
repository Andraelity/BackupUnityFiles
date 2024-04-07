using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using CommunicationFieldGridLetterNamespace;
using CommunicationCamaraShakeNamespace;
using CommunicationCorrectAnswerNamespace;
using CommunciationImplementationResetNamespace;

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_GridLetter : MonoBehaviour
{


	public GameObject Container_0;
	public GameObject Container_1;

	public GameObject backgroundShader_0;
	public GameObject backgroundShader_1;

	public GameObject BoxOne_0;
	public GameObject BoxOne_1;

	public GameObject TextOne_0;
	public GameObject TextOne_1;

	Vector3 vector3_PositionStartBackgroundShader_0;
	Vector3 vector3_PositionStartBackground_0;
	Vector3 vector3_PositionStartBackgroundShader_1;
	Vector3 vector3_PositionStartBackground_1;

	Vector3 vector3_ScaleStartBackgroundShader_0;
	Vector3 vector3_ScaleStartBackground_0;
	Vector3 vector3_ScaleStartBackgroundShader_1;
	Vector3 vector3_ScaleStartBackground_1;


	public GameObject Container_Letter;
	public GameObject backgroundShader_Letter_UnCheck;
	public GameObject backgroundShader_Letter_Check;
	public GameObject BoxOne_Letter;
	public GameObject TextOne_Letter;

	public GameObject Container_Pointer;
	public GameObject backgroundShader_Pointer;

	
	List<GameObject> list_FullObjectsInstanced;


	public GameObject gameobject_PlaneBlur_Correct;
	public GameObject gameobject_Shader_Correct;

	public GameObject Container_Correct_0;
	public GameObject Container_Correct_1;

	public GameObject backgroundShader_Correct_0;
	public GameObject backgroundShader_Correct_1;

	public GameObject BoxOne_Correct_0;
	public GameObject BoxOne_Correct_1;

	public GameObject TextOne_Correct_0;
	public GameObject TextOne_Correct_1;



	Vector3 vector3_PositionStartBackgroundShader_Correct_0;
	Vector3 vector3_PositionStartBackground_Correct_0;
	Vector3 vector3_PositionStartBackgroundShader_Correct_1;
	Vector3 vector3_PositionStartBackground_Correct_1;

	Vector3 vector3_ScaleStartBackgroundShader_Correct_0;
	Vector3 vector3_ScaleStartBackground_Correct_0;
	Vector3 vector3_ScaleStartBackgroundShader_Correct_1;
	Vector3 vector3_ScaleStartBackground_Correct_1;



 	List<string> list_OfStringEnglish;
 	List<string> list_OfStringFrench;

 	// List<List<string>> list_ListStringWordsRow;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";

	float float_PositionBaseY = -2.0f;
	float float_PositionBaseIncrementY = 1.5f;
	float float_PositionBaseX = -7.0f;
	float float_PositionBaseIncrementX = 1.0f;
	
	List<string> list_StringCharWordOne;
	List<string> list_StringCharWordTwo;


	List<string> list_StringCombinationTwoWords;


	List<int> list_IntCurrentAdded_One;
	List<int> list_IntCurrentAdded_Two;


	List<int> list_IntCurrentAdded_Combination;

	List<string> list_StringFullGridCharacter;

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
		
		int_NumberChangeSceneStyleMode = random_GeneratorNextMode.Next(2,5); 
		
		WordsCounterCommunicationClass.int_CurrentNumberWords = 0;
		WordsCounterCommunicationClass.int_TargetNumberWords = int_NumberChangeSceneStyleMode;



 		list_OfStringEnglish = new List<string>();
 		list_OfStringFrench = new List<string>();

		LoadStringList();

		
		

		var src = DateTime.Now;
		var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
		int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

		System.Random randomGeneratorNumber = new System.Random(hashRandom);
		int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
		string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringFrench[int_randomListPosition];


		TextMeshPro valuesOne_Correct_0 = TextOne_Correct_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_Correct_1 = TextOne_Correct_1.GetComponent<TextMeshPro>();

		valuesOne_Correct_0.text = string_OneTranslation;
		valuesOne_Correct_1.text = string_TwoTranslation;

		valuesOne_Correct_0.font = font_asset;
		valuesOne_Correct_1.font = font_asset;
		valuesOne_Correct_0.fontSize = 20;
		valuesOne_Correct_1.fontSize = 20;
		

		float widthText_One   = valuesOne_Correct_0.preferredWidth;
		float heightText_One  = valuesOne_Correct_0.preferredHeight;

		backgroundShader_Correct_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_0.transform.localScale); 	
		BoxOne_Correct_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_0.transform.localScale);

		widthText_One   = valuesOne_Correct_1.preferredWidth;
		heightText_One  = valuesOne_Correct_1.preferredHeight;

		backgroundShader_Correct_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_0.transform.localScale); 	
		BoxOne_Correct_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_0.transform.localScale);


		gameobject_PlaneBlur_Correct.SetActive(false);
		gameobject_Shader_Correct.SetActive(false);
		Container_Correct_0.SetActive(false);
		Container_Correct_1.SetActive(false);


		TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();

		valuesOne_0.text = string_OneTranslation;
		valuesOne_1.text = string_TwoTranslation;

		valuesOne_0.font = font_asset;
		valuesOne_1.font = font_asset;

		valuesOne_0.fontSize = 20;;
		valuesOne_1.fontSize = 20;;


		widthText_One   = valuesOne_0.preferredWidth;
		heightText_One  = valuesOne_0.preferredHeight;

		backgroundShader_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_0.transform.localScale); 	
		BoxOne_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_0.transform.localScale);

		widthText_One   = valuesOne_1.preferredWidth;
		heightText_One  = valuesOne_1.preferredHeight;

		backgroundShader_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_1.transform.localScale); 	
		BoxOne_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_1.transform.localScale);


		
		list_StringCombinationTwoWords = new List<string>();

		list_StringCharWordOne = new List<string>();
		list_StringCharWordTwo = new List<string>();

		list_IntCurrentAdded_One = new List<int>();
		list_IntCurrentAdded_Two = new List<int>();

		list_IntCurrentAdded_Combination = new List<int>();


		// Debug.Log("String before the shuffle");
		// Debug.Log("String before the shuffle");
		// Debug.Log("String before the shuffle");
		

		for(int i = 0 ; i < string_OneTranslation.Length; i++)
		{

			list_StringCharWordOne.Add(string_OneTranslation[i].ToString());
			
			list_StringCombinationTwoWords.Add(string_OneTranslation[i].ToString());

			list_IntCurrentAdded_One.Add(0);
			
			list_IntCurrentAdded_Combination.Add(0);

		}

		for(int i = 0 ; i < string_TwoTranslation.Length; i++)
		{

			list_StringCharWordTwo.Add(string_TwoTranslation[i].ToString());
			
			list_StringCombinationTwoWords.Add(string_TwoTranslation[i].ToString());

			list_IntCurrentAdded_Two.Add(0);

			list_IntCurrentAdded_Combination.Add(0);
			
		}

		// foreach(string stringToPrint in list_StringCombinationTwoWords)
		// {
		// 	Debug.Log(stringToPrint);
		// }

		// Debug.Log(list_StringCombinationTwoWords.Count);


		List<Vector2> list_Vector2PositionGrid = new List<Vector2>();

		float float_PositionGridX = (float) randomGeneratorNumber.Next(0, 15);
		float float_PositionGridY = (float) randomGeneratorNumber.Next(0, 5);

		Vector2 vector2_PositionToAdd = new Vector2(float_PositionGridX,  float_PositionGridY);

		list_Vector2PositionGrid.Add(vector2_PositionToAdd);

		int int_LengthBothString = list_StringCharWordOne.Count + list_StringCharWordTwo.Count;

		// Debug.Log(int_LengthBothString);


		for(int i = 0; i < int_LengthBothString - 1; i++)
		{
		
			bool bool_WhileLoopRandom = true;
			while(bool_WhileLoopRandom)
			{

				float_PositionGridX = (float) randomGeneratorNumber.Next(0, 15);
				float_PositionGridY = (float) randomGeneratorNumber.Next(0, 5);

				bool bool_StatusComparation = false;
		
				for(int j = 0; j < list_Vector2PositionGrid.Count; j++)
				{

					bool bool_SimpleAssignation_0 = list_Vector2PositionGrid[j].x == float_PositionGridX;  
					bool bool_SimpleAssignation_1 = list_Vector2PositionGrid[j].y == float_PositionGridY; 

					// Debug.Log(bool_SimpleAssignation_0 && bool_SimpleAssignation_1);
									
					if(bool_SimpleAssignation_0 && bool_SimpleAssignation_1)
					{
						
						float_PositionGridX ++;
						float_PositionGridY ++;

						if(float_PositionGridX > 14 || float_PositionGridY > 4)
						{
						
							bool_StatusComparation = true;
						
						}

					}

				}		
				
				if(bool_StatusComparation == true)
				{
					// Debug.Log(i )
					continue;
				}

				for(int j = 0; j < list_Vector2PositionGrid.Count; j++)
				{

					bool bool_SimpleAssignation_0 = list_Vector2PositionGrid[i].x == float_PositionGridX;  
					bool bool_SimpleAssignation_1 = list_Vector2PositionGrid[i].y == float_PositionGridY; 
					
					if(bool_SimpleAssignation_0 && bool_SimpleAssignation_1)
					{
						
						bool_StatusComparation = true;

					}

				}		
			
				if(bool_StatusComparation == true)
				{
					continue;
				}

				if(bool_WhileLoopRandom == true)
				{
					bool_WhileLoopRandom = false;
				}

				vector2_PositionToAdd = new Vector2(float_PositionGridX,  float_PositionGridY);

				list_Vector2PositionGrid.Add(vector2_PositionToAdd);

			}

		}


		// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
		// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
		// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
		
		// int int_CounterPositionSecureVectorList2 = 0;
		// foreach(Vector2 vectorToPrint in list_Vector2PositionGrid)
		// {
		// 	Debug.Log(int_CounterPositionSecureVectorList2.ToString() + "       " + vectorToPrint.x.ToString() + "   " +  vectorToPrint.y.ToString());
		// 	int_CounterPositionSecureVectorList2++;
		// }

		// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
		// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
		// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");


		// // List<Vector2> list_Vector2FullGridCharacterPosition = new List<Vector2>(); 

		// Debug.Log(list_Vector2PositionGrid.Count);
		// Debug.Log(list_StringCombinationTwoWords.Count);


		list_StringFullGridCharacter = new List<string>();


		for(int i = 0; i < 5 ; i++)
		{

			for(int j = 0; j < 15; j++)
			{
				
				// randomGeneratorNumber = new System.Random(hashRandom * (i));
				int int_PositionListTwoWords = randomGeneratorNumber.Next(0, list_StringCombinationTwoWords.Count);

				// Debug.Log(int_PositionListTwoWords);

				string string_CharacterOnGrid = list_StringCombinationTwoWords[int_PositionListTwoWords];

				
				list_StringFullGridCharacter.Add(string_CharacterOnGrid);

			}

		}

		// Debug.Log("REACH THIS POINT");
		// Debug.Log("REACH THIS POINT");
		// Debug.Log("REACH THIS POINT");
		// Debug.Log("REACH THIS POINT");

		for(int k = 0 ; k < list_Vector2PositionGrid.Count; k++)
		{
			
			Vector2 vector2_SecurePosition = list_Vector2PositionGrid[k];
			
			int int_SecurePosition_x = (int) vector2_SecurePosition.x;
			int int_SecurePosition_y = (int) vector2_SecurePosition.y;
			
			// Debug.Log(int_SecurePosition_x.ToString() + "       " + int_SecurePosition_y.ToString());
			string string_CharacterOnGrid = list_StringCombinationTwoWords[k];

			// Debug.Log(string_CharacterOnGrid);
			list_StringFullGridCharacter[((int_SecurePosition_y * 15) + int_SecurePosition_x) ] = string_CharacterOnGrid;
			
			// Debug.Log(((int_SecurePosition_y * 15) + int_SecurePosition_x).ToString() + "     " +  string_CharacterOnGrid);
			// Debug.Log(list_StringFullGridCharacter[((int_SecurePosition_y * 15) + int_SecurePosition_x) ]);
		
		}



		// Debug.Log("String after the shuffle");
		// Debug.Log("String after the shuffle");
		// Debug.Log("String after the shuffle");

		// foreach(string stringToPrint in list_StringCharWordOne)
		// {
		// 	print(stringToPrint);
		// }

		// foreach(string stringToPrint in list_StringCharWordTwo)
		// {
		// 	print(stringToPrint);
		// }




		// float_PositionBaseY = -2.0f;
		// float_PositionBaseIncrementY = 1.5f;
		// float_PositionBaseX = -7.0f;
		// float_PositionBaseIncrementX = 1.0f;

		list_FullObjectsInstanced = new List<GameObject>();

		
		for(int i = 0 ; i < 5; i ++)
		{
			
			for(int j = 0; j < 15; j ++)
			{
				
				// float float_PositionX = (((float)randomGeneratorNumber.NextDouble()) * 18f) - 9f;
				float float_PositionX = float_PositionBaseX + ((float)j) * float_PositionBaseIncrementX;
				// float float_PositionY = (((float)randomGeneratorNumber.NextDouble()) * 8f) - 2f;
				float float_PositionY = float_PositionBaseY + ((float)i) * float_PositionBaseIncrementY;
				

				Vector3 vector3_Position = new Vector3(float_PositionX, float_PositionY, 10);
				GameObject gameobject_InstanceContainer = Instantiate(Container_Letter, vector3_Position , Quaternion.identity);


				gameobject_InstanceContainer.name = "container_" + i.ToString() + "_" + j.ToString();


				GameObject gameobject_BackgroundShader_UnCheck = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
				GameObject gameobject_BackgroundShader_Check = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
				GameObject gameobject_BackgroundPlane = gameobject_InstanceContainer.transform.GetChild(2).gameObject;
				GameObject gameobject_Text = gameobject_InstanceContainer.transform.GetChild(3).gameObject;
				

				TextMeshPro textMeshObject = gameobject_Text.GetComponent<TextMeshPro>();
				textMeshObject.text = list_StringFullGridCharacter[(i * 15) + j];
				textMeshObject.font = font_asset;
				textMeshObject.fontSize = 20;

				widthText_One   = 2.0f;
				heightText_One  = textMeshObject.preferredHeight;


				gameobject_BackgroundShader_UnCheck.transform.localScale =  GetScaleBackGroundShader(widthText_One	, gameobject_BackgroundShader_UnCheck.transform.localScale); 	
				gameobject_BackgroundShader_Check.transform.localScale =  GetScaleBackGroundShader(widthText_One	, gameobject_BackgroundShader_Check.transform.localScale); 	
				gameobject_BackgroundPlane.transform.localScale  =        GetScaleBackGround(widthText_One, gameobject_BackgroundPlane.transform.localScale);

				gameobject_BackgroundShader_Check.SetActive(false);
				
				list_FullObjectsInstanced.Add(gameobject_InstanceContainer);

			}


		}

		{

			int int_PositionPointerStart_X = randomGeneratorNumber.Next(0, 15);
			int int_PositionPointerStart_Y = randomGeneratorNumber.Next(0, 5);
			int_CurrentPositionPointerX = int_PositionPointerStart_X;
			int_CurrentPositionPointerY = int_PositionPointerStart_Y;

			float float_PositionX = float_PositionBaseX + ((float)int_PositionPointerStart_X) * float_PositionBaseIncrementX;
			float float_PositionY = float_PositionBaseY + ((float)int_PositionPointerStart_Y) * float_PositionBaseIncrementY;
			
			Vector3 vector3_Position = new Vector3(float_PositionX, float_PositionY, 10);
			
			Container_Pointer.transform.position = vector3_Position;
			
					
			float widthText_Pointer  = 2.0f;
			
			backgroundShader_Pointer.transform.localScale =  GetScaleBackGroundShader(widthText_Pointer , backgroundShader_Pointer.transform.localScale); 	

		}
		
		bool_ActivateInputFieldOnEnter = false;
		list_Vector2PositionIdentifySuccessfull = new List<Vector2>();
		

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


	
	List<string> GenerateRandomListString(List<string> listToShuffle)
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
	
		float scale = (width * 0.037f)/1.15f ;
    	Vector3 valueOut = new Vector3(scale, Current.y, Current.z);
  
    	return valueOut;
    
	}

    Vector3 GetScaleBackGround(float width, Vector3 Current)
    {
		float scale = (width * 0.033f)/1.15f ;
    	Vector3 valueOut = new Vector3(scale, Current.y , Current.z);
  
    	return valueOut;
    }

    Vector3 GetScaleVerticalBackGroundShader(int NumberLines, Vector3 Current)
    {
		
		float float_NumberLines = (float)NumberLines * 0.8f * Current.z;
    	Vector3 valueOut = new Vector3(Current.x, Current.y, float_NumberLines);
  
    	return valueOut;

    }

    Vector3 GetScaleVerticalBackGround(int NumberLines, Vector3 Current)
    {

		float float_NumberLines = (float)NumberLines * 1.13f* Current.z;
    	Vector3 valueOut = new Vector3(Current.x, Current.y, float_NumberLines);
  
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
    }


    Vector3 GetPositionVerticalBackGround(float numberLines)
    {

		float position = numberLines; 
    	Vector3 valueOut = new Vector3(0.0f, position, 0.0f);
  
    	return valueOut;
    }




    void ColorCurrentTextWord_FULLCOLOR_0(int int_StartCharacter, int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_Correct_0.GetComponent<TMP_Text>();

 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < int_NumberOfCharacters; i++)
        {
  
	        int currentCharacter = i ;

	        int characterCount = textInfo.characterCount;

	        Color32[] newVertexColors;
	        Color32 c0 = m_TextComponent.color;
	 
	        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
	        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
	        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
	 
	
			if(i < int_StartCharacter)
			{

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
			else
			{
				
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

	}


    void ColorCurrentTextWord_0(List<int> list_CharactersToColor)
    { 
 
		TMP_Text m_TextComponent = TextOne_0.GetComponent<TMP_Text>();

 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < list_CharactersToColor.Count; i++)
        {
 
			
	        int currentCharacter = i ;

	        int characterCount = textInfo.characterCount;

	        Color32[] newVertexColors;
	        Color32 c0 = m_TextComponent.color;
	 
	        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
	        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
	        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
	 
			if(list_CharactersToColor[i] == 1)
			{
		
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

			else
			{

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
		 
	}



    void ColorCurrentTextWordDefault_0(int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_Correct_0.GetComponent<TMP_Text>();

 
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


    void ColorCurrentTextWord_FULLCOLOR_1(int int_StartCharacter, int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_Correct_1.GetComponent<TMP_Text>();

 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < int_NumberOfCharacters; i++)
        {
  
	        int currentCharacter = i ;

	        int characterCount = textInfo.characterCount;

	        Color32[] newVertexColors;
	        Color32 c0 = m_TextComponent.color;
	 
	        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
	        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
	        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
	 
			if(i < int_StartCharacter)
			{

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
			else
			{
					
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

	}




    void ColorCurrentTextWord_1( List<int> list_CharactersToColor)
    { 
 
		TMP_Text m_TextComponent = TextOne_1.GetComponent<TMP_Text>();

 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < list_CharactersToColor.Count; i++)
        {
 
			
	        int currentCharacter = i ;

	        int characterCount = textInfo.characterCount;

	        Color32[] newVertexColors;
	        Color32 c0 = m_TextComponent.color;
	 
	        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
	        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
	        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
	 
			if(list_CharactersToColor[i] == 1)
			{
		
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

			else
			{

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
		 
	}



    void ColorCurrentTextWordDefault_1(int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_Correct_1.GetComponent<TMP_Text>();
 
 
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


    bool bool_ResetPairWords = false;

	bool bool_CheckString = false;

    string string_FromInputField = "";

    bool bool_ActiveCamaraShake = false;

	bool bool_ActivateInputFieldOnEnter = false;

	bool bool_ActiveMiniGame = false;
	

    float float_evaluationTime = 0.0f;
	int int_CounterCollisionChar = 0;
	int int_LengthBothWords = 0;
	bool bool_CheckLengthColored = false;
	bool bool_ActiveCamaraCheck = false;
	bool bool_ActiveStringOne = true;
	bool bool_ActiveStringTwo = true;
	int int_CurrentPositionWordOne = 0;
	int int_CurrentPositionWordTwo = 0;
	string string_CurrentWordOne = "";
	string string_CurrentWordTwo = "";
	List<string> list_StringWordOne;
	List<string> list_StringWordTwo;
	int int_CounterWordListOne = 0;
	int int_CounterWordListTwo = 0;

	
	int int_CurrentPositionPointerX;
	int int_CurrentPositionPointerY;
	bool bool_UpdatePositionPointer = false;
	bool bool_IsFullIdentified = true;
    bool bool_ActivateMiniGameObjects = false;

	bool bool_WordMatchOne = false;
	bool bool_WordMatchTwo = false;


	List<Vector2> list_Vector2PositionIdentifySuccessfull;

	List<int> list_IntCharacterToColor_One = new List<int>();
	List<int> list_IntCharacterToColor_Two = new List<int>();


    void Update()
    {

		SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
		

		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;
			

	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_10 = WordsCounterCommunicationClass.int_CurrentNumberWords;
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
		
		if(bool_ActiveMiniGame == false)
		{
			
			if(Input.GetKeyDown(KeyCode.UpArrow))
			{
				if(int_CurrentPositionPointerY < 4)
				{
					int_CurrentPositionPointerY++;
					bool_UpdatePositionPointer = true;
				}
			}
			
			if(Input.GetKeyDown(KeyCode.DownArrow))
			{

				if(int_CurrentPositionPointerY > 0)
				{
					
					int_CurrentPositionPointerY--;
					bool_UpdatePositionPointer = true;

				}

			}
			
			if(Input.GetKeyDown(KeyCode.RightArrow))
			{
				if(int_CurrentPositionPointerX < 14)
				{
					int_CurrentPositionPointerX++;
					bool_UpdatePositionPointer = true;

				}
			}
			
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			{
				
				if(int_CurrentPositionPointerX > 0)
				{
					
					int_CurrentPositionPointerX--;
					bool_UpdatePositionPointer = true;

				}

			}

			if(bool_UpdatePositionPointer)
			{

				bool_UpdatePositionPointer = false;
				float float_UpdatePointer_X = (float) int_CurrentPositionPointerX * float_PositionBaseIncrementX + float_PositionBaseX;
				float float_UpdatePointer_Y = (float) int_CurrentPositionPointerY * float_PositionBaseIncrementY + float_PositionBaseY;

				Vector3 vector3_PositionUpdatePointer = new Vector3(float_UpdatePointer_X, float_UpdatePointer_Y, 10.0f);
				Container_Pointer.transform.position = 	vector3_PositionUpdatePointer;

			}




			// list_StringCharWordOne
			// list_StringCharWordTwo

			if(Input.GetKeyDown(KeyCode.Return))
			{
			
				bool bool_IsPositionAlreadyIdentified = false;

				for(int i = 0; i < list_Vector2PositionIdentifySuccessfull.Count; i++)
				{

					int int_IdentifiedPosition_X = (int)list_Vector2PositionIdentifySuccessfull[i].x;
					int int_IdentifiedPosition_Y = (int)list_Vector2PositionIdentifySuccessfull[i].y;
					
					if(int_IdentifiedPosition_X == int_CurrentPositionPointerX && int_IdentifiedPosition_Y == int_CurrentPositionPointerY)
					{

						bool_IsPositionAlreadyIdentified = true;
						break;

					}

				}	

				if(bool_IsPositionAlreadyIdentified == false)
				{

					string string_CharGrid = list_StringFullGridCharacter[((int_CurrentPositionPointerY * 15) + int_CurrentPositionPointerX)];

					// Debug.Log(string_CharGrid);
					
					bool bool_NewPositionWasAdded = false;

					for(int i = 0; i < list_IntCurrentAdded_Combination.Count; i++)
					{

						if(string_CharGrid == list_StringCombinationTwoWords[i] && list_IntCurrentAdded_Combination[i] == 0)
						{
							// Debug.Log(list_StringCombinationTwoWords[i] + "    		VALUE IDENTIFIED CHAR");
							// Debug.Log(list_IntCurrentAdded_Combination[i].ToString() + "    		VALUE CHAR IDENTIFIED");

							Vector2 vector2_NewPositionIdentified = new Vector2((float)int_CurrentPositionPointerX, (float)int_CurrentPositionPointerY);
							
							list_IntCurrentAdded_Combination[i] = 1;

							list_Vector2PositionIdentifySuccessfull.Add(vector2_NewPositionIdentified);

							bool_NewPositionWasAdded = true;

							break;
							
						}

					}

					if(bool_NewPositionWasAdded)
					{
						list_FullObjectsInstanced[((int_CurrentPositionPointerY * 15) + int_CurrentPositionPointerX)].transform.GetChild(0).gameObject.SetActive(false);					
						list_FullObjectsInstanced[((int_CurrentPositionPointerY * 15) + int_CurrentPositionPointerX)].transform.GetChild(1).gameObject.SetActive(true);
						CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

					}
					else
					{
						CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
			            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;

					}

				}


				// Debug.Log("PRINT NEW LIST");
				// Debug.Log("PRINT NEW LIST");
				// Debug.Log("PRINT NEW LIST");
				// foreach(Vector2 vectorToPrint in list_Vector2PositionIdentifySuccessfull)
				// {

				// 	Debug.Log(vectorToPrint);

				// }

				for(int i = 0; i < list_IntCurrentAdded_Combination.Count; i++)
				{

					if(i < list_IntCurrentAdded_One.Count)
					{
					
						list_IntCurrentAdded_One[i] = list_IntCurrentAdded_Combination[i];
					
					}
					else
					{

						list_IntCurrentAdded_Two[i - list_IntCurrentAdded_One.Count] = list_IntCurrentAdded_Combination[i];

					}

				}

			}


			ColorCurrentTextWord_0(list_IntCurrentAdded_One);
			ColorCurrentTextWord_1(list_IntCurrentAdded_Two);


			for(int i = 0; i < list_IntCurrentAdded_Combination.Count; i++)
			{

				if(list_IntCurrentAdded_Combination[i] != 1)
				{
					bool_IsFullIdentified = false;
					break;
				}
				else
				{
					
					bool_IsFullIdentified = true;

				}

			}
	
		}

		if(Input.GetKeyDown(KeyCode.Tab))
		{
			bool_IsFullIdentified = true;
		}

		if(bool_ActivateInputFieldOnEnter == false)
		{
			CommunicationFieldGridLetterClass.bool_ActiveEnterPress = false;
		}

		if(bool_ActivateInputFieldOnEnter == true)
		{
			CommunicationFieldGridLetterClass.bool_ActiveEnterPress = true;
		}


		if(bool_IsFullIdentified == true)
		{

			bool_ActiveMiniGame = true;
			bool_ActivateMiniGameObjects = true;
		
		}


		if(bool_ActivateMiniGameObjects)
		{
		
			gameobject_PlaneBlur_Correct.SetActive(true);
			gameobject_Shader_Correct.SetActive(true);
			Container_Correct_0.SetActive(true);  
			Container_Correct_1.SetActive(true);  

			bool_ActivateInputFieldOnEnter = true;
			CommunicationFieldGridLetterClass.bool_ActiveEnterPress = true;
			
		}


		if(bool_ActiveMiniGame)
		{
			
			if(bool_CheckString)
			{
				bool_CheckString = false;
				bool bool_AnswerFound = false;

				if(string_FromInputField == string_OneTranslation && bool_WordMatchOne == false)
				{
					bool_WordMatchOne = true;
					bool_AnswerFound = true;

				}

				if(string_FromInputField == string_TwoTranslation && bool_WordMatchTwo == false)
				{
					bool_WordMatchTwo = true;
					bool_AnswerFound = true;
				}


				if(bool_AnswerFound == true)
				{
					CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;					
					WordsCounterCommunicationClass.int_CurrentNumberWords++;				

				}
				else
				{
					CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
		            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;

				}

			}

			string string_InputField = CommunicationFieldGridLetterClass.string_InputFieldGridLetter;

			int int_CounterCharToColor_One = 0;
			if(string_InputField.Length <= string_OneTranslation.Length)
			{

				for(int i = 0; i < string_InputField.Length; i++)
				{
					if(string_OneTranslation[i] == string_InputField[i])
					{
						int_CounterCharToColor_One ++;
						continue;
					}
					else
					{
						break;
					}

				}
			}

			ColorCurrentTextWord_FULLCOLOR_0(int_CounterCharToColor_One, string_OneTranslation.Length);

			if(bool_WordMatchOne)
			{
				ColorCurrentTextWordDefault_0(string_OneTranslation.Length);
			}

			string_InputField = CommunicationFieldGridLetterClass.string_InputFieldGridLetter;

			int int_CounterCharToColor_Two = 0;
			if(string_InputField.Length <= string_TwoTranslation.Length)
			{

				for(int i = 0; i < string_InputField.Length; i++)
				{
					if(string_TwoTranslation[i] == string_InputField[i])
					{
						int_CounterCharToColor_Two ++;
						continue;
					}
					else
					{
						break;
					}

				}
			}

			ColorCurrentTextWord_FULLCOLOR_1(int_CounterCharToColor_Two, string_TwoTranslation.Length);

			if(bool_WordMatchTwo)
			{
				ColorCurrentTextWordDefault_1(string_TwoTranslation.Length);
			}


			if(bool_WordMatchOne && bool_WordMatchTwo)
			{

				bool_ResetPairWords = true;

				gameobject_PlaneBlur_Correct.SetActive(false);
				gameobject_Shader_Correct.SetActive(false);
				Container_Correct_0.SetActive(false);  
				Container_Correct_1.SetActive(false);  

			}

		}




    	if(CommunicationFieldGridLetterClass.bool_ActiveEnterPressMessage == true)
    	{	

			CommunicationFieldGridLetterClass.bool_ActiveEnterPressMessage = false;
    		// CommunicationFieldGridLetterClass.bool_ActiveEnterPress = false;

    		Debug.Log("String In InputField After ENTER  == " + CommunicationFieldGridLetterClass.string_InputFieldGridLetter);

    		string_FromInputField = CommunicationFieldGridLetterClass.string_InputFieldGridLetter;

    		bool_CheckString = true;

    	}

		// if(bool_IsFullIdentified)
		if(bool_ResetPairWords)
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
	


			bool_ResetPairWords = false;


			bool_IsFullIdentified = false;

			bool_WordMatchOne = false;
			bool_WordMatchTwo = false;

			bool_ActiveMiniGame = false;
			bool_ActivateMiniGameObjects = false;



			for(int i = 0; i < list_FullObjectsInstanced.Count; i++)
			{
				Destroy(list_FullObjectsInstanced[i]);
			}


			var src = DateTime.Now;
			var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
			int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

			System.Random randomGeneratorNumber = new System.Random(hashRandom);
			int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];


			TextMeshPro valuesOne_Correct_0 = TextOne_Correct_0.GetComponent<TextMeshPro>();
			TextMeshPro valuesOne_Correct_1 = TextOne_Correct_1.GetComponent<TextMeshPro>();

			valuesOne_Correct_0.text = string_OneTranslation;
			valuesOne_Correct_1.text = string_TwoTranslation;

			float widthText_One   = valuesOne_Correct_0.preferredWidth;
			float heightText_One  = valuesOne_Correct_0.preferredHeight;

			backgroundShader_Correct_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_0.transform.localScale); 	
			BoxOne_Correct_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_0.transform.localScale);

			widthText_One   = valuesOne_Correct_1.preferredWidth;
			heightText_One  = valuesOne_Correct_1.preferredHeight;

			backgroundShader_Correct_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_0.transform.localScale); 	
			BoxOne_Correct_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_0.transform.localScale);


			gameobject_PlaneBlur_Correct.SetActive(false);
			gameobject_Shader_Correct.SetActive(false);
			Container_Correct_0.SetActive(false);
			Container_Correct_1.SetActive(false);


			TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
			TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();

			valuesOne_0.text = string_OneTranslation;
			valuesOne_1.text = string_TwoTranslation;

			widthText_One   = valuesOne_0.preferredWidth;
			heightText_One  = valuesOne_0.preferredHeight;

			backgroundShader_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_0.transform.localScale); 	
			BoxOne_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_0.transform.localScale);

			widthText_One   = valuesOne_1.preferredWidth;
			heightText_One  = valuesOne_1.preferredHeight;

			backgroundShader_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_1.transform.localScale); 	
			BoxOne_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_1.transform.localScale);


			
			list_StringCombinationTwoWords = new List<string>();

			list_StringCharWordOne = new List<string>();
			list_StringCharWordTwo = new List<string>();

			list_IntCurrentAdded_One = new List<int>();
			list_IntCurrentAdded_Two = new List<int>();

			list_IntCurrentAdded_Combination = new List<int>();


			// Debug.Log("String before the shuffle");
			// Debug.Log("String before the shuffle");
			// Debug.Log("String before the shuffle");
			

			for(int i = 0 ; i < string_OneTranslation.Length; i++)
			{

				list_StringCharWordOne.Add(string_OneTranslation[i].ToString());
				
				list_StringCombinationTwoWords.Add(string_OneTranslation[i].ToString());

				list_IntCurrentAdded_One.Add(0);
				
				list_IntCurrentAdded_Combination.Add(0);

			}

			for(int i = 0 ; i < string_TwoTranslation.Length; i++)
			{

				list_StringCharWordTwo.Add(string_TwoTranslation[i].ToString());
				
				list_StringCombinationTwoWords.Add(string_TwoTranslation[i].ToString());

				list_IntCurrentAdded_Two.Add(0);

				list_IntCurrentAdded_Combination.Add(0);
				
			}

			// foreach(string stringToPrint in list_StringCombinationTwoWords)
			// {
			// 	Debug.Log(stringToPrint);
			// }

			// Debug.Log(list_StringCombinationTwoWords.Count);

			List<Vector2> list_Vector2PositionGrid = new List<Vector2>();

			float float_PositionGridX = (float) randomGeneratorNumber.Next(0, 15);
			float float_PositionGridY = (float) randomGeneratorNumber.Next(0, 5);

			Vector2 vector2_PositionToAdd = new Vector2(float_PositionGridX,  float_PositionGridY);

			list_Vector2PositionGrid.Add(vector2_PositionToAdd);

			int int_LengthBothString = list_StringCharWordOne.Count + list_StringCharWordTwo.Count;

			// Debug.Log(int_LengthBothString);


			for(int i = 0; i < int_LengthBothString - 1; i++)
			{
			
				bool bool_WhileLoopRandom = true;

				while(bool_WhileLoopRandom)
				{

					float_PositionGridX = (float) randomGeneratorNumber.Next(0, 15);
					float_PositionGridY = (float) randomGeneratorNumber.Next(0, 5);

					bool bool_StatusComparation = false;
			
					for(int j = 0; j < list_Vector2PositionGrid.Count; j++)
					{

						bool bool_SimpleAssignation_0 = list_Vector2PositionGrid[j].x == float_PositionGridX;  
						bool bool_SimpleAssignation_1 = list_Vector2PositionGrid[j].y == float_PositionGridY; 

						// Debug.Log(bool_SimpleAssignation_0 && bool_SimpleAssignation_1);
										
						if(bool_SimpleAssignation_0 && bool_SimpleAssignation_1)
						{
							
							float_PositionGridX ++;
							float_PositionGridY ++;

							if(float_PositionGridX > 14 || float_PositionGridY > 4)
							{
							
								bool_StatusComparation = true;
							
							}

						}

					}		
					
					if(bool_StatusComparation == true)
					{
						// Debug.Log(i )
						continue;
					}

					for(int j = 0; j < list_Vector2PositionGrid.Count; j++)
					{

						bool bool_SimpleAssignation_0 = list_Vector2PositionGrid[i].x == float_PositionGridX;  
						bool bool_SimpleAssignation_1 = list_Vector2PositionGrid[i].y == float_PositionGridY; 
						
						if(bool_SimpleAssignation_0 && bool_SimpleAssignation_1)
						{
							
							bool_StatusComparation = true;

						}

					}		
				
					if(bool_StatusComparation == true)
					{
						continue;
					}

					if(bool_WhileLoopRandom == true)
					{
						bool_WhileLoopRandom = false;
					}

					vector2_PositionToAdd = new Vector2(float_PositionGridX,  float_PositionGridY);

					list_Vector2PositionGrid.Add(vector2_PositionToAdd);

				}

			}


			// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
			// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
			// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
			
			// int int_CounterPositionSecureVectorList2 = 0;
			// foreach(Vector2 vectorToPrint in list_Vector2PositionGrid)
			// {
			// 	Debug.Log(int_CounterPositionSecureVectorList2.ToString() + "       " + vectorToPrint.x.ToString() + "   " +  vectorToPrint.y.ToString());
			// 	int_CounterPositionSecureVectorList2++;
			// }

			// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
			// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");
			// Debug.Log("POSITIONS CREATED RANDOM SECURE POINT");

			// // List<Vector2> list_Vector2FullGridCharacterPosition = new List<Vector2>(); 

			// Debug.Log(list_Vector2PositionGrid.Count);
			// Debug.Log(list_StringCombinationTwoWords.Count);


			list_StringFullGridCharacter = new List<string>();


			for(int i = 0; i < 5 ; i++)
			{

				for(int j = 0; j < 15; j++)
				{
					
					// randomGeneratorNumber = new System.Random(hashRandom * (i));
					int int_PositionListTwoWords = randomGeneratorNumber.Next(0, list_StringCombinationTwoWords.Count);

					// Debug.Log(int_PositionListTwoWords);

					string string_CharacterOnGrid = list_StringCombinationTwoWords[int_PositionListTwoWords];

					
					list_StringFullGridCharacter.Add(string_CharacterOnGrid);

				}

			}

			// Debug.Log("REACH THIS POINT");
			// Debug.Log("REACH THIS POINT");
			// Debug.Log("REACH THIS POINT");
			// Debug.Log("REACH THIS POINT");

			for(int k = 0 ;k < list_Vector2PositionGrid.Count; k++)
			{
				
				Vector2 vector2_SecurePosition = list_Vector2PositionGrid[k];
				
				int int_SecurePosition_x = (int) vector2_SecurePosition.x;
				int int_SecurePosition_y = (int) vector2_SecurePosition.y;
				
				// Debug.Log(int_SecurePosition_x.ToString() + "       " + int_SecurePosition_y.ToString());
				string string_CharacterOnGrid = list_StringCombinationTwoWords[k];

				// Debug.Log(string_CharacterOnGrid);
				list_StringFullGridCharacter[((int_SecurePosition_y * 15) + int_SecurePosition_x)] = string_CharacterOnGrid;
				
				// Debug.Log(((int_SecurePosition_y * 15) + int_SecurePosition_x).ToString() + "     " +  string_CharacterOnGrid);
				// Debug.Log(list_StringFullGridCharacter[((int_SecurePosition_y * 15) + int_SecurePosition_x) ]);
			
			}

			// Debug.Log("String after the shuffle");
			// Debug.Log("String after the shuffle");
			// Debug.Log("String after the shuffle");

			// foreach(string stringToPrint in list_StringCharWordOne)
			// {
			// 	print(stringToPrint);
			// }

			// foreach(string stringToPrint in list_StringCharWordTwo)
			// {
			// 	print(stringToPrint);
			// }


			// float_PositionBaseY = -2.0f;
			// float_PositionBaseIncrementY = 1.5f;
			// float_PositionBaseX = -7.0f;
			// float_PositionBaseIncrementX = 1.0f;

			list_FullObjectsInstanced = new List<GameObject>();

			
			for(int i = 0 ; i < 5; i ++)
			{
				

				for(int j = 0; j < 15; j ++)
				{
					
					float float_PositionX = float_PositionBaseX + ((float)j) * float_PositionBaseIncrementX;
					float float_PositionY = float_PositionBaseY + ((float)i) * float_PositionBaseIncrementY;
					

					Vector3 vector3_Position = new Vector3(float_PositionX, float_PositionY, 10);
					GameObject gameobject_InstanceContainer = Instantiate(Container_Letter, vector3_Position , Quaternion.identity);


					gameobject_InstanceContainer.name = "container_" + i.ToString() + "_" + j.ToString();


					GameObject gameobject_BackgroundShader_UnCheck = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
					GameObject gameobject_BackgroundShader_Check = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
					GameObject gameobject_BackgroundPlane = gameobject_InstanceContainer.transform.GetChild(2).gameObject;
					GameObject gameobject_Text = gameobject_InstanceContainer.transform.GetChild(3).gameObject;
					
					TextMeshPro textMeshObject = gameobject_Text.GetComponent<TextMeshPro>();
					textMeshObject.text = list_StringFullGridCharacter[(i * 15) + j];
					textMeshObject.font = font_asset;
					textMeshObject.fontSize = 20;

					widthText_One   = 2.0f;
					heightText_One  = textMeshObject.preferredHeight;


					gameobject_BackgroundShader_UnCheck.transform.localScale =  GetScaleBackGroundShader(widthText_One	, gameobject_BackgroundShader_UnCheck.transform.localScale); 	
					gameobject_BackgroundShader_Check.transform.localScale =  GetScaleBackGroundShader(widthText_One	, gameobject_BackgroundShader_Check.transform.localScale); 	
					gameobject_BackgroundPlane.transform.localScale  =        GetScaleBackGround(widthText_One, gameobject_BackgroundPlane.transform.localScale);

					gameobject_BackgroundShader_Check.SetActive(false);
					
					list_FullObjectsInstanced.Add(gameobject_InstanceContainer);

				}


			}

			{

				int int_PositionPointerStart_X = randomGeneratorNumber.Next(0, 15);
				int int_PositionPointerStart_Y = randomGeneratorNumber.Next(0, 5);
				int_CurrentPositionPointerX = int_PositionPointerStart_X;
				int_CurrentPositionPointerY = int_PositionPointerStart_Y;

				float float_PositionX = float_PositionBaseX + ((float)int_PositionPointerStart_X) * float_PositionBaseIncrementX;
				float float_PositionY = float_PositionBaseY + ((float)int_PositionPointerStart_Y) * float_PositionBaseIncrementY;
				
				Vector3 vector3_Position = new Vector3(float_PositionX, float_PositionY, 10);
				
				Container_Pointer.transform.position = vector3_Position;
				
						
				float widthText_Pointer  = 2.0f;
				
				backgroundShader_Pointer.transform.localScale =  GetScaleBackGroundShader(widthText_Pointer , backgroundShader_Pointer.transform.localScale); 	

			}

			bool_ActivateInputFieldOnEnter = false;
			list_Vector2PositionIdentifySuccessfull = new List<Vector2>();


			CommunciationImplementationResetClass.bool_ActiveResetWords = true;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;



    	}
        
    }

}
