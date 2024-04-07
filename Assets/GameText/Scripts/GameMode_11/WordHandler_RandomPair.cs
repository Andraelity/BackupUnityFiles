using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using TMPro;

using LinkCommunicationColoredNamespace;
using CommunicationCamaraShakeNamespace;
using CommunicationCorrectAnswerNamespace;
using CommunciationImplementationResetNamespace;
using CommunciationImplementationBezierRandomPairNamespace;

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class Duple_String_Int
{
	public string string_Word = "";
	public int int_Language = 0;
	public int int_Identifier = 0;

}


public class WordHandler_RandomPair : MonoBehaviour
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

 	List<List<Duple_String_Int>> list_ListDupleWordsRow;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";


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


	int int_RandomNumberOFStrings;

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

		int_RandomNumberOFStrings = randomGeneratorNumber.Next(1, 10);

		// int int_RandomNumberOFStrings = 8;

		// Debug.Log("Printing the number of strings");

		// int int_NumberBeforeComparation = int_RandomNumberOFStrings;
		// int_RandomNumberOFStrings = (int_RandomNumberOFStrings%2 == 0)? int_RandomNumberOFStrings : int_RandomNumberOFStrings++;

		// Debug.Log(int_NumberBeforeComparation + "	elements to check   " + int_RandomNumberOFStrings);

		// Debug.Log("Printing the number of strings");

		// int int_RandomNumberOFStrings = randomGeneratorNumber.Next(1, 8);
		// int_RandomNumberOFStrings = (int_RandomNumberOFStrings%2 == 0)? int_RandomNumberOFStrings : int_RandomNumberOFStrings++;
		
		List<string> list_StringsToPrintEnglish = new List<string>();
		List<string> list_StringsToPrintFrench = new List<string>();

		List<string> list_FullListOfString = new List<string>();
		
		List<Duple_String_Int> list_DupleFullString = new List<Duple_String_Int>();


		// Debug.Log("Print element in this CLASS ");
		// Debug.Log("Print element in this CLASS ");
		// Debug.Log("Print element in this CLASS ");

		// Duple_String_Int duple_Element = new Duple_String_Int();
		// duple_Element.string_Word = "ThisISTheWord";
		// duple_Element.int_Language = 1;

		// Debug.Log(duple_Element.string_Word + "   " + duple_Element.int_Language);
		// Debug.Log("Print element in this CLASS ");
		// Debug.Log("Print element in this CLASS ");
		// Debug.Log("Print element in this CLASS ");


		for(int i = 0; i < int_RandomNumberOFStrings; i++)
		{

			int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);

			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];

			list_StringsToPrintEnglish.Add(string_OneTranslation);
			list_StringsToPrintFrench.Add(string_TwoTranslation);

			list_FullListOfString.Add(string_OneTranslation);
			list_FullListOfString.Add(string_TwoTranslation);

			Duple_String_Int duple_Element = new Duple_String_Int();
			duple_Element.string_Word = string_OneTranslation;
			duple_Element.int_Language = 0;
			duple_Element.int_Identifier = i;
			list_DupleFullString.Add(duple_Element);
			duple_Element = new Duple_String_Int();
			duple_Element.string_Word = string_TwoTranslation;
			duple_Element.int_Language = 1;
			duple_Element.int_Identifier = i;			
			list_DupleFullString.Add(duple_Element);

		}

		// foreach(Duple_String_Int Duple_Value in list_DupleFullString)
		// {

		// 	Debug.Log(Duple_Value.string_Word + "    " + Duple_Value.int_Language);

		// }

		list_DupleFullString = RandomListShuffle(list_DupleFullString);

		list_FullListOfString = RandomListShuffle(list_FullListOfString);


		// Debug.Log(int_RandomNumberOFStrings * 2);

		List<int> list_IntNumberOfStringPerRow = new List<int>();

		for(int i = 0; i < 6; i++)
		{

			list_IntNumberOfStringPerRow.Add(0);
		
		}
				
		for(int i = 0; i < int_RandomNumberOFStrings * 2; i++)
		{
			list_IntNumberOfStringPerRow[i % 6] += 1;
		}

		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");

		foreach(int IntToPrint in list_IntNumberOfStringPerRow)
		{
			Debug.Log(IntToPrint);
		}

		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");

		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");

		for(int i = 0; i < 100; i++)
		{

			int int_randomListPositionShuffle_0 = randomGeneratorNumber.Next(0, 6);
			int int_randomListPositionShuffle_1 = randomGeneratorNumber.Next(0, 6);
			
			if(int_randomListPositionShuffle_0 == int_randomListPositionShuffle_1)
			{
				int_randomListPositionShuffle_1 ++;
				int_randomListPositionShuffle_1 %= 6;
			}

			if(list_IntNumberOfStringPerRow[int_randomListPositionShuffle_0] > 0 && list_IntNumberOfStringPerRow[int_randomListPositionShuffle_1] < 3)
			{
				list_IntNumberOfStringPerRow[int_randomListPositionShuffle_0] --;
				list_IntNumberOfStringPerRow[int_randomListPositionShuffle_1] ++;
			}

		}

		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
		// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");

		// foreach(int IntToPrint in list_IntNumberOfStringPerRow)
		// {
		// 	Debug.Log(IntToPrint);
		// }
		

		list_ListDupleWordsRow = new List<List<Duple_String_Int>>();		
		List<Duple_String_Int> list_DupleWordsRow_0 = new List<Duple_String_Int>();
		List<Duple_String_Int> list_DupleWordsRow_1 = new List<Duple_String_Int>();
		List<Duple_String_Int> list_DupleWordsRow_2 = new List<Duple_String_Int>();
		List<Duple_String_Int> list_DupleWordsRow_3 = new List<Duple_String_Int>();
		List<Duple_String_Int> list_DupleWordsRow_4 = new List<Duple_String_Int>();
		List<Duple_String_Int> list_DupleWordsRow_5 = new List<Duple_String_Int>();
		list_ListDupleWordsRow.Add(list_DupleWordsRow_0);
		list_ListDupleWordsRow.Add(list_DupleWordsRow_1);
		list_ListDupleWordsRow.Add(list_DupleWordsRow_2);
		list_ListDupleWordsRow.Add(list_DupleWordsRow_3);
		list_ListDupleWordsRow.Add(list_DupleWordsRow_4);
		list_ListDupleWordsRow.Add(list_DupleWordsRow_5);

		int int_CounterPositionFullString = 0;

		for(int i = 0 ; i < list_ListDupleWordsRow.Count; i++)
		{

			for(int j = 0; j < list_IntNumberOfStringPerRow[i]; j++)
			{
				
				list_ListDupleWordsRow[i].Add(list_DupleFullString[int_CounterPositionFullString]);
				int_CounterPositionFullString++;

			}

		}


		// Debug.Log("Total number of items added");
		// Debug.Log("Total number of items added");
		// Debug.Log("Total number of items added");
		
		
		// Debug.Log(int_CounterPositionFullString);
		// Debug.Log(int_CounterPositionFullString);
		// Debug.Log(int_CounterPositionFullString);


		// Debug.Log("Total number of items added");
		// Debug.Log("Total number of items added");
		// Debug.Log("Total number of items added");


		// Debug.Log("DEBUG LOG STRING FULL LIST");
		// Debug.Log("DEBUG LOG STRING FULL LIST");
		// Debug.Log("DEBUG LOG STRING FULL LIST");
		// Debug.Log("DEBUG LOG STRING FULL LIST");


		// foreach(string string_Primary in list_FullListOfString)
		// {
		// 	Debug.Log(string_Primary);
		// }

		// Debug.Log("DEBUG LOG STRING FULL LIST");
		// Debug.Log("DEBUG LOG STRING FULL LIST");
		// Debug.Log("DEBUG LOG STRING FULL LIST");
		// Debug.Log("DEBUG LOG STRING FULL LIST");

		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");

		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
		// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");


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

			float float_LastPositionHorizontal = 8.95f;
			
			for(int j = 0; j < list_IntNumberOfStringPerRow[i]; j++)
			{
				
				float_LastPositionHorizontal -= 3.0f;

				if(list_IntNumberOfStringPerRow[i] == 1)
				{
					float_LastPositionHorizontal = ((8.95f - 3.0f) * ((float) randomGeneratorNumber.NextDouble())) - (8.95f - 3.0f);
				}

				if(list_IntNumberOfStringPerRow[i] == 2)
				{
					
					if(j == 0)
					{
						float_LastPositionHorizontal = ((3.0f) * ((float) randomGeneratorNumber.NextDouble())) + 4.0f;
					}
					else
					{
						float_LastPositionHorizontal -= (((float) randomGeneratorNumber.NextDouble()) * 2.0f) + 3.0f;
					}

				}

				if(list_IntNumberOfStringPerRow[i] == 3)
				{
					
					if(j == 0)
					{
						float_LastPositionHorizontal = ((3.0f) * ((float) randomGeneratorNumber.NextDouble())) + 5.0f;
					}
					else if (j == 1)
					{
						float_LastPositionHorizontal -= (((float) randomGeneratorNumber.NextDouble())) + 1.0f;
					}
					else
					{
						float_LastPositionHorizontal -= (((float) randomGeneratorNumber.NextDouble())) + 2.0f;
						
					}

				}


				Vector3 vector3_PositionWord = new Vector3(float_LastPositionHorizontal, list_PositionVeritalStart[i], 10.0f);

				GameObject gameobject_InstanceContainer;

				// int numberOperationRandom = randomGeneratorNumber.Next();
				string string_selectedWord = "";
			
				if(list_ListDupleWordsRow[i][j].int_Language == 0)
				{
					gameobject_InstanceContainer = Instantiate(Container_0, vector3_PositionWord, Quaternion.identity);
					string_selectedWord = list_ListDupleWordsRow[i][j].string_Word;
				}
				else
				{
					gameobject_InstanceContainer = Instantiate(Container_1, vector3_PositionWord, Quaternion.identity);
					string_selectedWord = list_ListDupleWordsRow[i][j].string_Word;
				}

			
				gameobject_InstanceContainer.name = "container_" + i.ToString() + "_" + j.ToString();

				GameObject backgroundShaderObject_0 = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
				GameObject backgroundShaderObject_1 = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
				GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;
				GameObject textObject = gameobject_InstanceContainer.transform.GetChild(3).gameObject;


				TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();
	
	
				textMeshObject.text = string_selectedWord;
				textMeshObject.font = font_asset;
				textMeshObject.fontSize = 15;
	

				float widthText_One   = textMeshObject.preferredWidth;
				float heightText_One  = textMeshObject.preferredHeight;
	
	
				backgroundShaderObject_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShaderObject_0.transform.localScale); 	
				backgroundShaderObject_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShaderObject_1.transform.localScale); 	
				backgroundPlaneObject.transform.localScale = GetScaleBackGround(widthText_One, backgroundPlaneObject.transform.localScale);
				

				backgroundShaderObject_0.transform.position += GetPositionBackGround(widthText_One);
				backgroundShaderObject_1.transform.position += GetPositionBackGround(widthText_One);
				backgroundPlaneObject.transform.position += GetPositionBackGround(widthText_One);

				gameobject_InstanceContainer.transform.GetChild(1).gameObject.SetActive(false);

				list_ListGameObjectHolder[i].Add(gameobject_InstanceContainer);


			}

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


	
	List<Duple_String_Int> RandomListShuffle(List<Duple_String_Int> listToShuffle)
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


	
	List<string> RandomListShuffle(List<string> listToShuffle)
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


    Vector3 GetPositionBackGround(float width)
    {

		float position = (width * 0.16f)/1.15f;
    	Vector3 valueOut = new Vector3(position, 0.0f, 0.0f);
  
    	return valueOut;
    	// return position;
    }




    void ColorCurrentTextWord(int int_Position_i, int int_Position_j, int int_NumberOfCharacters)
    { 
 
		GameObject textObject = list_ListGameObjectHolder[int_Position_i][int_Position_j].transform.GetChild(3).gameObject;
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
 
		GameObject textObject = list_ListGameObjectHolder[int_Position_i][int_Position_j].transform.GetChild(3).gameObject;
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

	int int_CounterNumberOfCorrectAnswers = 0;

	bool bool_IdentifiedRemoveColor = false;

	List<List<int>> list_ListIntPositionToColorBefore = new List<List<int>>();

	int int_FirstCorrectWordLength = 0;

    List<int> list_IntColored = new List<int>{0, 0, 0, 0, 0, 0};

	List<int> list_IntPositionFirstWord = new List<int>();
	List<int> list_IntPositionSecondWord = new List<int>();

	bool bool_SolutionWasFound = false;
	bool bool_FirstWordIdentified = false;
	bool bool_SecondWordIdentified = false;

	bool bool_ProcessIdentifiedPair = false;

    void Update()
    {


		SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;

		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;

	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_11 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

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

		List<int> list_IntMaxColored = new List<int>();

    	string string_FromInputFieldOperational = LinkCommunicationColoredClass.string_InputField;

    	for(int i = 0; i < 6; i++)
    	{

    		for(int j = 0; j < list_ListDupleWordsRow[i].Count; j++)
    		{

    			string string_CurrentOperational = list_ListDupleWordsRow[i][j].string_Word;

    			int int_CounterCharToColor = 0;


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
			

    			if(int_CounterCharToColor > 0)
    			{

					list_IntMaxColored.Add(int_CounterCharToColor);

    				List<int> list_IntIdentified = new List<int>();

    				list_IntIdentified.Add(i);
    				list_IntIdentified.Add(j);
    				list_IntIdentified.Add(int_CounterCharToColor);

    				list_ListIntPositionToColor.Add(list_IntIdentified);

    			}

    		}

    	}



		int int_MaxColored = GetMaxColored(list_IntMaxColored);
    	
    	for(int i = 0; i < list_ListIntPositionToColor.Count; i++)
    	{
			
			if(list_ListIntPositionToColor[i][2] == int_MaxColored)
			{
				ColorCurrentTextWord(list_ListIntPositionToColor[i][0], list_ListIntPositionToColor[i][1], list_ListIntPositionToColor[i][2]);

			}
			else
			{
				ColorCurrentTextWordDefault(list_ListIntPositionToColor[i][0], list_ListIntPositionToColor[i][1], list_ListIntPositionToColor[i][2]);
				
			}
 
    	}


    	bool_IdentifiedRemoveColor = true;


    	if(bool_CheckString && bool_FirstWordIdentified == false)
    	{

    		bool_CheckString = false;
    		bool_ActiveCamaraShake = true;

    		//  list_ListDupleWordsRow.Count == 6 , WOULD ALWAYS BE 6;
    		bool bool_BreakHigherLoop = false;

    		int int_PositionIdentified_i = 0;
    		int int_PositionIdentified_j = 0;

    		//HIGHERLOOP//
    		for(int i = 0 ; i < 6; i ++)
    		{

    			for(int j = 0 ; j < list_ListDupleWordsRow[i].Count; j ++)
    			{

    				if(string_FromInputField == list_ListDupleWordsRow[i][j].string_Word)
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

    			bool_ActiveCamaraShake = false;

				CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

				list_IntPositionFirstWord.Add(int_PositionIdentified_i);
				list_IntPositionFirstWord.Add(int_PositionIdentified_j);

				bool_FirstWordIdentified = true;

				WordsCounterCommunicationClass.int_CurrentNumberWords++;				
				
			}

			bool_IdentifiedRemoveColor = false;

    	}

    	// if(bool_CheckString && bool_SecondWordIdentified == false)
		if(bool_CheckString && bool_FirstWordIdentified && bool_SecondWordIdentified == false)
    	{

    		bool_CheckString = false;
    		bool_ActiveCamaraShake = true;

    		bool bool_SecondWordMatch = false;

			string string_ComparationSecondWord = list_ListDupleWordsRow[list_IntPositionSecondWord[0]][list_IntPositionSecondWord[1]].string_Word; 

			// Debug.Log("STRING THAT WAS IDENTIFIED|| 		" + string_ComparationSecondWord);
			
			
			if(string_FromInputField == string_ComparationSecondWord)
			{
				bool_SecondWordMatch = true;
	
			}


			if(bool_SecondWordMatch)
			{

				CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

    			bool_ActiveCamaraShake = false;

				// list_IntPositionFirstWord.Add(int_PositionIdentified_i);
				// list_IntPositionFirstWord.Add(int_PositionIdentified_j);

				bool_SecondWordIdentified = true;

				WordsCounterCommunicationClass.int_CurrentNumberWords++;				

			}    		
			

			CommunicationCamaraShakeClass.bool_ActiveCamaraShake = bool_ActiveCamaraShake;
			CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = bool_ActiveCamaraShake;
				
			

			bool_IdentifiedRemoveColor = false;

    	}


		
		if(bool_FirstWordIdentified && bool_ProcessIdentifiedPair == false)
		{


			list_ListGameObjectHolder[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]].transform.GetChild(1).gameObject.SetActive(true);

			bool_ProcessIdentifiedPair = true;
			int int_Identifier = list_ListDupleWordsRow[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]].int_Identifier;
			int int_Language = list_ListDupleWordsRow[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]].int_Language;
			Debug.Log("CURRENT IDENTIFIER === " + int_Identifier + "		CURRENT WORD ===  " + list_ListDupleWordsRow[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]].string_Word);

			if(int_Language == 0)
			{
				int_Language = 1;
			}
			else
			{
				int_Language = 0;
			}


			string string_SecondStringComparation = "";
			bool bool_BreakHigherLoop = false;


			for(int i = 0 ; i < list_ListDupleWordsRow.Count; i++)
			{

				for(int j = 0; j < list_ListDupleWordsRow[i].Count; j++)
				{

					if(list_ListDupleWordsRow[i][j].int_Identifier == int_Identifier && list_ListDupleWordsRow[i][j].int_Language == int_Language)
					{
						

						string_SecondStringComparation = list_ListDupleWordsRow[i][j].string_Word; 
						list_IntPositionSecondWord.Add(i);
						list_IntPositionSecondWord.Add(j);
						bool_BreakHigherLoop = true;
						
						Debug.Log("THIS IS THE IDENTIFIER THAT WAS FOUND  		" + list_ListDupleWordsRow[i][j].int_Identifier + " 		AND THE WORD " + list_ListDupleWordsRow[i][j].string_Word);
						
						break;
					
					}

				}

				if(bool_BreakHigherLoop == true)
				{
					break;
				}
				
			}

			Debug.Log("STRING THAT WAS IDENTIFIED|| 		" + string_SecondStringComparation);
			

		}


		if(int_RandomNumberOFStrings == int_CounterNumberOfCorrectAnswers)
		{
			bool_ResetListOfWords = true;
		}

		if(bool_FirstWordIdentified && bool_SecondWordIdentified)
		{
			bool_SolutionWasFound = true;
			int_CounterNumberOfCorrectAnswers ++;
		}

		if(bool_SolutionWasFound)
		{

			bool_SolutionWasFound = false;
			bool_FirstWordIdentified = false;
			bool_SecondWordIdentified = false;

			bool_ProcessIdentifiedPair = false;

			bool_SolutionWasFound = false;

			if(list_IntPositionFirstWord[1] > list_IntPositionSecondWord[1])
			{
	
				Destroy(list_ListGameObjectHolder[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]]);
				list_ListGameObjectHolder[list_IntPositionFirstWord[0]].RemoveAt(list_IntPositionFirstWord[1]);
				list_ListDupleWordsRow[list_IntPositionFirstWord[0]].RemoveAt(list_IntPositionFirstWord[1]);

				Destroy(list_ListGameObjectHolder[list_IntPositionSecondWord[0]][list_IntPositionSecondWord[1]]);
				list_ListGameObjectHolder[list_IntPositionSecondWord[0]].RemoveAt(list_IntPositionSecondWord[1]);
				list_ListDupleWordsRow[list_IntPositionSecondWord[0]].RemoveAt(list_IntPositionSecondWord[1]);


			}
			else
			{

				Destroy(list_ListGameObjectHolder[list_IntPositionSecondWord[0]][list_IntPositionSecondWord[1]]);
				list_ListGameObjectHolder[list_IntPositionSecondWord[0]].RemoveAt(list_IntPositionSecondWord[1]);
				list_ListDupleWordsRow[list_IntPositionSecondWord[0]].RemoveAt(list_IntPositionSecondWord[1]);

				Destroy(list_ListGameObjectHolder[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]]);
				list_ListGameObjectHolder[list_IntPositionFirstWord[0]].RemoveAt(list_IntPositionFirstWord[1]);
				list_ListDupleWordsRow[list_IntPositionFirstWord[0]].RemoveAt(list_IntPositionFirstWord[1]);


			}

			list_IntPositionFirstWord = new List<int>();
			list_IntPositionSecondWord = new List<int>();

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
	
	    		for(int j = 0; j < list_ListDupleWordsRow[i].Count; j++)
	    		{

	    			ColorCurrentTextWordDefault(i, j, list_ListDupleWordsRow[i][j].string_Word.Length);

	    		}
	    		
	    	}

		}



		if(bool_FirstWordIdentified)
		{

			ColorCurrentTextWord(list_IntPositionFirstWord[0], list_IntPositionFirstWord[1], list_ListDupleWordsRow[list_IntPositionFirstWord[0]][list_IntPositionFirstWord[1]].string_Word.Length);

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

			
			string string_ToFontAsset = "Fonts/FORCEDSQUARESDF";
	
			TMP_FontAsset font_asset;
	
			int int_selectedFont = (int)PlayerPrefs.GetFloat("Font" + "SliderValue");
	
			if(int_selectedFont  == 0)
			{
				
				int_selectedFont = 1;
			
			}
	
	        string string_GetNameFont = MenuSettingsFontsClass.GetFontName(int_selectedFont - 1);
	
	        font_asset = (TMP_FontAsset)Resources.Load(string_GetNameFont);
	


			bool_ResetListOfWords = false;

			int_ListColorCountBefore = 0;
			int_ListColorCountCurrent = 0;

			int_CounterNumberOfCorrectAnswers = 0;

			bool_IdentifiedRemoveColor = false;

			list_ListIntPositionToColorBefore = new List<List<int>>();

			int_FirstCorrectWordLength = 0;

		    // list_IntColored = new List<int>{0, 0, 0, 0, 0, 0};

			list_IntPositionFirstWord = new List<int>();
			list_IntPositionSecondWord = new List<int>();

			bool_SolutionWasFound = false;
			bool_FirstWordIdentified = false;
			bool_SecondWordIdentified = false;

			bool_ProcessIdentifiedPair = false;
	



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

			// valuesOne_0.text = string_OneTranslation;
			// valuesOne_1.text = string_TwoTranslation;

			int_RandomNumberOFStrings = randomGeneratorNumber.Next(1, 10);

			// int int_RandomNumberOFStrings = 8;

			// Debug.Log("Printing the number of strings");

			// int int_NumberBeforeComparation = int_RandomNumberOFStrings;
			// int_RandomNumberOFStrings = (int_RandomNumberOFStrings%2 == 0)? int_RandomNumberOFStrings : int_RandomNumberOFStrings++;

			// Debug.Log(int_NumberBeforeComparation + "	elements to check   " + int_RandomNumberOFStrings);

			// Debug.Log("Printing the number of strings");

			// int int_RandomNumberOFStrings = randomGeneratorNumber.Next(1, 8);
			// int_RandomNumberOFStrings = (int_RandomNumberOFStrings%2 == 0)? int_RandomNumberOFStrings : int_RandomNumberOFStrings++;
			
			List<string> list_StringsToPrintEnglish = new List<string>();
			List<string> list_StringsToPrintFrench = new List<string>();

			List<string> list_FullListOfString = new List<string>();
			
			List<Duple_String_Int> list_DupleFullString = new List<Duple_String_Int>();


			// Debug.Log("Print element in this CLASS ");
			// Debug.Log("Print element in this CLASS ");
			// Debug.Log("Print element in this CLASS ");

			// Duple_String_Int duple_Element = new Duple_String_Int();
			// duple_Element.string_Word = "ThisISTheWord";
			// duple_Element.int_Language = 1;

			// Debug.Log(duple_Element.string_Word + "   " + duple_Element.int_Language);
			// Debug.Log("Print element in this CLASS ");
			// Debug.Log("Print element in this CLASS ");
			// Debug.Log("Print element in this CLASS ");


			for(int i = 0; i < int_RandomNumberOFStrings; i++)
			{

				int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);

				string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
				string_TwoTranslation = list_OfStringFrench[int_randomListPosition];

				list_StringsToPrintEnglish.Add(string_OneTranslation);
				list_StringsToPrintFrench.Add(string_TwoTranslation);

				list_FullListOfString.Add(string_OneTranslation);
				list_FullListOfString.Add(string_TwoTranslation);

				Duple_String_Int duple_Element = new Duple_String_Int();
				duple_Element.string_Word = string_OneTranslation;
				duple_Element.int_Language = 0;
				duple_Element.int_Identifier = i;
				list_DupleFullString.Add(duple_Element);
				duple_Element = new Duple_String_Int();
				duple_Element.string_Word = string_TwoTranslation;
				duple_Element.int_Language = 1;
				duple_Element.int_Identifier = i;			
				list_DupleFullString.Add(duple_Element);

			}

			foreach(Duple_String_Int Duple_Value in list_DupleFullString)
			{

				Debug.Log(Duple_Value.string_Word + "    " + Duple_Value.int_Language);

			}

			list_DupleFullString = RandomListShuffle(list_DupleFullString);

			list_FullListOfString = RandomListShuffle(list_FullListOfString);


			// Debug.Log(int_RandomNumberOFStrings * 2);

			List<int> list_IntNumberOfStringPerRow = new List<int>();

			for(int i = 0; i < 6; i++)
			{

				list_IntNumberOfStringPerRow.Add(0);
			
			}
					
			for(int i = 0; i < int_RandomNumberOFStrings * 2; i++)
			{
				list_IntNumberOfStringPerRow[i % 6] += 1;
			}

			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");

			// foreach(int IntToPrint in list_IntNumberOfStringPerRow)
			// {
			// 	Debug.Log(IntToPrint);
			// }

			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS BEFORE THE INCREMENT SHUFFLE");

			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");

			for(int i = 0; i < 100; i++)
			{

				int int_randomListPositionShuffle_0 = randomGeneratorNumber.Next(0, 6);
				int int_randomListPositionShuffle_1 = randomGeneratorNumber.Next(0, 6);
				
				if(int_randomListPositionShuffle_0 == int_randomListPositionShuffle_1)
				{
					int_randomListPositionShuffle_1 ++;
					int_randomListPositionShuffle_1 %= 6;
				}

				if(list_IntNumberOfStringPerRow[int_randomListPositionShuffle_0] > 0 && list_IntNumberOfStringPerRow[int_randomListPositionShuffle_1] < 3)
				{
					list_IntNumberOfStringPerRow[int_randomListPositionShuffle_0] --;
					list_IntNumberOfStringPerRow[int_randomListPositionShuffle_1] ++;
				}

			}

			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");
			// Debug.Log("NUMBERS AFTER THE INCREMENT SHUFFLE");

			// foreach(int IntToPrint in list_IntNumberOfStringPerRow)
			// {
			// 	Debug.Log(IntToPrint);
			// }
			

			list_ListDupleWordsRow = new List<List<Duple_String_Int>>();		
			List<Duple_String_Int> list_DupleWordsRow_0 = new List<Duple_String_Int>();
			List<Duple_String_Int> list_DupleWordsRow_1 = new List<Duple_String_Int>();
			List<Duple_String_Int> list_DupleWordsRow_2 = new List<Duple_String_Int>();
			List<Duple_String_Int> list_DupleWordsRow_3 = new List<Duple_String_Int>();
			List<Duple_String_Int> list_DupleWordsRow_4 = new List<Duple_String_Int>();
			List<Duple_String_Int> list_DupleWordsRow_5 = new List<Duple_String_Int>();
			list_ListDupleWordsRow.Add(list_DupleWordsRow_0);
			list_ListDupleWordsRow.Add(list_DupleWordsRow_1);
			list_ListDupleWordsRow.Add(list_DupleWordsRow_2);
			list_ListDupleWordsRow.Add(list_DupleWordsRow_3);
			list_ListDupleWordsRow.Add(list_DupleWordsRow_4);
			list_ListDupleWordsRow.Add(list_DupleWordsRow_5);

			int int_CounterPositionFullString = 0;

			for(int i = 0 ; i < list_ListDupleWordsRow.Count; i++)
			{

				for(int j = 0; j < list_IntNumberOfStringPerRow[i]; j++)
				{
					
					list_ListDupleWordsRow[i].Add(list_DupleFullString[int_CounterPositionFullString]);
					int_CounterPositionFullString++;

				}

			}


			// Debug.Log("Total number of items added");
			// Debug.Log("Total number of items added");
			// Debug.Log("Total number of items added");
			
			
			// Debug.Log(int_CounterPositionFullString);
			// Debug.Log(int_CounterPositionFullString);
			// Debug.Log(int_CounterPositionFullString);


			// Debug.Log("Total number of items added");
			// Debug.Log("Total number of items added");
			// Debug.Log("Total number of items added");


			// Debug.Log("DEBUG LOG STRING FULL LIST");
			// Debug.Log("DEBUG LOG STRING FULL LIST");
			// Debug.Log("DEBUG LOG STRING FULL LIST");
			// Debug.Log("DEBUG LOG STRING FULL LIST");


			// foreach(string string_Primary in list_FullListOfString)
			// {
			// 	Debug.Log(string_Primary);
			// }

			// Debug.Log("DEBUG LOG STRING FULL LIST");
			// Debug.Log("DEBUG LOG STRING FULL LIST");
			// Debug.Log("DEBUG LOG STRING FULL LIST");
			// Debug.Log("DEBUG LOG STRING FULL LIST");

			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");

			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");
			// Debug.Log("DEBUG LOG STRING SEPARATED LIST LIST");


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

				float float_LastPositionHorizontal = 8.95f;
				
				for(int j = 0; j < list_IntNumberOfStringPerRow[i]; j++)
				{
					
					float_LastPositionHorizontal -= 3.0f;

					if(list_IntNumberOfStringPerRow[i] == 1)
					{
						float_LastPositionHorizontal = ((8.95f - 3.0f) * ((float) randomGeneratorNumber.NextDouble())) - (8.95f - 3.0f);
					}

					if(list_IntNumberOfStringPerRow[i] == 2)
					{
						
						if(j == 0)
						{
							float_LastPositionHorizontal = ((3.0f) * ((float) randomGeneratorNumber.NextDouble())) + 4.0f;
						}
						else
						{
							float_LastPositionHorizontal -= (((float) randomGeneratorNumber.NextDouble()) * 2.0f) + 3.0f;
						}

					}

					if(list_IntNumberOfStringPerRow[i] == 3)
					{
						
						if(j == 0)
						{
							float_LastPositionHorizontal = ((3.0f) * ((float) randomGeneratorNumber.NextDouble())) + 5.0f;
						}
						else if (j == 1)
						{
							float_LastPositionHorizontal -= (((float) randomGeneratorNumber.NextDouble())) + 1.0f;
						}
						else
						{
							float_LastPositionHorizontal -= (((float) randomGeneratorNumber.NextDouble())) + 2.0f;
							
						}

					}


					Vector3 vector3_PositionWord = new Vector3(float_LastPositionHorizontal, list_PositionVeritalStart[i], 10.0f);

					GameObject gameobject_InstanceContainer;

					// int numberOperationRandom = randomGeneratorNumber.Next();
					string string_selectedWord = "";
				
					if(list_ListDupleWordsRow[i][j].int_Language == 0)
					{
						gameobject_InstanceContainer = Instantiate(Container_0, vector3_PositionWord, Quaternion.identity);
						string_selectedWord = list_ListDupleWordsRow[i][j].string_Word;
					}
					else
					{
						gameobject_InstanceContainer = Instantiate(Container_1, vector3_PositionWord, Quaternion.identity);
						string_selectedWord = list_ListDupleWordsRow[i][j].string_Word;
					}

					// int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
				
					gameobject_InstanceContainer.name = "container_" + i.ToString() + "_" + j.ToString();

					GameObject backgroundShaderObject_0 = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
					GameObject backgroundShaderObject_1 = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
					GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;
					GameObject textObject = gameobject_InstanceContainer.transform.GetChild(3).gameObject;


					TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();
		
		
					textMeshObject.text = string_selectedWord;
					textMeshObject.font = font_asset;
					textMeshObject.fontSize = 15;
		

					float widthText_One   = textMeshObject.preferredWidth;
					float heightText_One  = textMeshObject.preferredHeight;
		
		
					backgroundShaderObject_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShaderObject_0.transform.localScale); 	
					backgroundShaderObject_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShaderObject_1.transform.localScale); 	
					backgroundPlaneObject.transform.localScale = GetScaleBackGround(widthText_One, backgroundPlaneObject.transform.localScale);
					
					// vector3_PositionStartBackground_Correct_0 = new Vector3(BoxOne_Correct_0.transform.position.x, BoxOne_Correct_0.transform.position.y, BoxOne_Correct_0.transform.position.z);

					backgroundShaderObject_0.transform.position += GetPositionBackGround(widthText_One);
					backgroundShaderObject_1.transform.position += GetPositionBackGround(widthText_One);
					backgroundPlaneObject.transform.position += GetPositionBackGround(widthText_One);

					gameobject_InstanceContainer.transform.GetChild(1).gameObject.SetActive(false);

					list_ListGameObjectHolder[i].Add(gameobject_InstanceContainer);

					// list_ListDupleWordsRow[i].Add(string_selectedWord);


				}

			}

			CommunciationImplementationBezierRandomPairClass.bool_ActiveResetWords = true;

		// 	CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;



    	}
        
    }

}
