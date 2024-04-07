using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;

using TMPro;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//using LinkCommunicationColoredNamespace;
using CommunicationFieldSentenceNamespace;
using CommunicationCamaraShakeNamespace;
using CommunicationCorrectAnswerNamespace;
using CommunciationImplementationResetNamespace;

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_PairSentences : MonoBehaviour
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



 	List<string> list_OfStringEnglish;
 	List<string> list_OfStringFrench;

 	// List<List<string>> list_ListStringWordsRow;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";

	int int_NumberChangeSceneStyleMode = 0;


	public GameObject text_ContainerHeight_0;
	public GameObject text_ContainerHeight_1;
	public GameObject text_ContainerHeight_2;
	
	float float_heightBase_0;
	float float_heightBase_1;
	float float_heightBase_2;


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
		
		int_NumberChangeSceneStyleMode = random_GeneratorNextMode.Next(50, 80); 
		
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


		valuesOne_0.text = string_OneTranslation;
		valuesOne_1.text = string_TwoTranslation;


		float widthText_One   = valuesOne_0.preferredWidth;
		float heightText_One  = valuesOne_0.preferredHeight;


		Debug.Log(widthText_One);
		Debug.Log(heightText_One);


		vector3_PositionStartBackgroundShader_0 = new Vector3(backgroundShader_0.transform.position.x, backgroundShader_0.transform.position.y, backgroundShader_0.transform.position.z);
		vector3_PositionStartBackground_0 = new Vector3(BoxOne_0.transform.position.x, BoxOne_0.transform.position.y, BoxOne_0.transform.position.z);
		
		
		vector3_ScaleStartBackgroundShader_0 = new Vector3(backgroundShader_0.transform.localScale.x, backgroundShader_0.transform.localScale.y, backgroundShader_0.transform.localScale.z);
		vector3_ScaleStartBackground_0 = new Vector3(BoxOne_0.transform.localScale.x, BoxOne_0.transform.localScale.y, BoxOne_0.transform.localScale.z);
		

		text_ContainerHeight_0.GetComponent<TextMeshPro>().font = font_asset;
		text_ContainerHeight_1.GetComponent<TextMeshPro>().font = font_asset;
		text_ContainerHeight_2.GetComponent<TextMeshPro>().font = font_asset;

		text_ContainerHeight_0.GetComponent<TextMeshPro>().fontSize = 15;
		text_ContainerHeight_1.GetComponent<TextMeshPro>().fontSize = 15;
		text_ContainerHeight_2.GetComponent<TextMeshPro>().fontSize = 15;


		float_heightBase_0 = text_ContainerHeight_0.GetComponent<TextMeshPro>().preferredHeight;
		float_heightBase_1 = text_ContainerHeight_1.GetComponent<TextMeshPro>().preferredHeight;
		float_heightBase_2 = text_ContainerHeight_2.GetComponent<TextMeshPro>().preferredHeight;

		float float_RatioHeightPivot = float_heightBase_2 - float_heightBase_1;


		// Debug.Log(widthText_One);
		// Debug.Log(heightText_One);

		if(widthText_One > 53.0f)
		{

			float heightText_One_Operative = (heightText_One) - float_heightBase_0;

			
			heightText_One_Operative = (float)System.Math.Round(heightText_One_Operative/float_RatioHeightPivot,2);


			Debug.Log("Height of the thing |||  " + heightText_One_Operative/float_RatioHeightPivot);

			if(heightText_One_Operative == 1 )
			{
			
				// Debug.Log("Reaching this point");
				backgroundShader_0.transform.localScale = GetScaleVerticalBackGroundShader(2, backgroundShader_0.transform.localScale); 	
				BoxOne_0.transform.localScale = GetScaleVerticalBackGround(2, BoxOne_0.transform.localScale);
				
				backgroundShader_0.transform.position -= GetPositionVerticalBackGround(0.33f);
				BoxOne_0.transform.position -= GetPositionVerticalBackGround(0.33f);

			}

			if(heightText_One_Operative == 2 )
			{
			
				backgroundShader_0.transform.localScale = GetScaleVerticalBackGroundShader(3, backgroundShader_0.transform.localScale); 	
				BoxOne_0.transform.localScale = GetScaleVerticalBackGround(3, BoxOne_0.transform.localScale);

				backgroundShader_0.transform.position -= GetPositionVerticalBackGround(0.66f);
				BoxOne_0.transform.position -= GetPositionVerticalBackGround(0.66f);

			}


			backgroundShader_0.transform.localScale = GetScaleBackGroundShader(53.0f, backgroundShader_0.transform.localScale); 	
			BoxOne_0.transform.localScale = GetScaleBackGround(53.0f, BoxOne_0.transform.localScale);

			backgroundShader_0.transform.position += GetPositionBackGround(53.0f);
			BoxOne_0.transform.position += GetPositionBackGround(53.0f);

		}
		else
		{

			backgroundShader_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_0.transform.localScale); 	
			BoxOne_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_0.transform.localScale);
			
			backgroundShader_0.transform.position += GetPositionBackGround(widthText_One);
			BoxOne_0.transform.position += GetPositionBackGround(widthText_One);

		}


		widthText_One   = valuesOne_1.preferredWidth;
		heightText_One  = valuesOne_1.preferredHeight;

		Debug.Log(widthText_One);
		Debug.Log(heightText_One);
		
		// Debug.Log(widthText_One);
		// Debug.Log(heightText_One);

		vector3_PositionStartBackgroundShader_1 = new Vector3(backgroundShader_1.transform.position.x, backgroundShader_1.transform.position.y, backgroundShader_1.transform.position.z);
		vector3_PositionStartBackground_1 = new Vector3(BoxOne_1.transform.position.x, BoxOne_1.transform.position.y, BoxOne_1.transform.position.z);
		
		
		vector3_ScaleStartBackgroundShader_1 = new Vector3(backgroundShader_1.transform.localScale.x, backgroundShader_1.transform.localScale.y, backgroundShader_1.transform.localScale.z);
		vector3_ScaleStartBackground_1 = new Vector3(BoxOne_1.transform.localScale.x, BoxOne_1.transform.localScale.y, BoxOne_1.transform.localScale.z);
		

		if(widthText_One > 53.0f)
		{

	
			float heightText_One_Operative = (heightText_One) - float_heightBase_0;

			
			heightText_One_Operative = (float)System.Math.Round(heightText_One_Operative/float_RatioHeightPivot,2);

		// Debug.Log(heightText_One_Operative/2.5 == 1);

			if(heightText_One_Operative == 1 )
			{
			
				// Debug.Log("Reaching this point");
				backgroundShader_1.transform.localScale = GetScaleVerticalBackGroundShader(2, backgroundShader_1.transform.localScale); 	
				BoxOne_1.transform.localScale = GetScaleVerticalBackGround(2, BoxOne_1.transform.localScale);
				
				backgroundShader_1.transform.position -= GetPositionVerticalBackGround(0.33f);
				BoxOne_1.transform.position -= GetPositionVerticalBackGround(0.33f);

			}

			if(heightText_One_Operative == 2 )
			{
			
				backgroundShader_1.transform.localScale = GetScaleVerticalBackGroundShader(3, backgroundShader_1.transform.localScale); 	
				BoxOne_1.transform.localScale = GetScaleVerticalBackGround(3, BoxOne_1.transform.localScale);

				backgroundShader_1.transform.position -= GetPositionVerticalBackGround(0.66f);
				BoxOne_1.transform.position -= GetPositionVerticalBackGround(0.66f);

			}

			backgroundShader_1.transform.localScale = GetScaleBackGroundShader(53.0f, backgroundShader_1.transform.localScale); 	
			BoxOne_1.transform.localScale = GetScaleBackGround(53.0f, BoxOne_1.transform.localScale);

			backgroundShader_1.transform.position += GetPositionBackGround(53.0f);
			BoxOne_1.transform.position += GetPositionBackGround(53.0f);

		}
		else
		{

			backgroundShader_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_1.transform.localScale); 	
			BoxOne_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_1.transform.localScale);
			
			backgroundShader_1.transform.position += GetPositionBackGround(widthText_One);
			BoxOne_1.transform.position += GetPositionBackGround(widthText_One);

		}


		list_StringWordOne = GetListOfWordSeparated(string_OneTranslation);
		list_StringWordTwo = GetListOfWordSeparated(string_TwoTranslation);

		int_CounterWordListOne = 0;			
		int_CounterWordListTwo = 0;

		string_CurrentWordOne = list_StringWordOne[int_CounterWordListOne];	
		string_CurrentWordTwo = list_StringWordTwo[int_CounterWordListTwo];	

		

    }



    void LoadStringList()
    {	

		string string_FirstLanguage = LinkCommunicationLanguagesFilesClass.string_CurrentActiveLanguage_Sentences_One;
		string string_SecondLanguage = LinkCommunicationLanguagesFilesClass.string_CurrentActiveLanguage_Sentences_Two;


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
		// float scale = (width * 0.037f)/1.15f ;
		
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
    	// return position;
    }


    Vector3 GetPositionVerticalBackGround(float numberLines)
    {

		float position = numberLines; 
    	Vector3 valueOut = new Vector3(0.0f, position, 0.0f);
  
    	return valueOut;
    	// return position;
    }



	List<string> GetListOfWordSeparated(string string_InFunction)
	{

		string string_OperativeFunction = string_InFunction;

		List<string> list_StringOperative = new List<string>();

		string string_Handle = "";

		for(int i = 0; i < string_OperativeFunction.Length; i++)
		{
	
			if(string_OperativeFunction[i].ToString() == " " )
			{
	
				string_Handle += string_OperativeFunction[i];  			
				list_StringOperative.Add(string_Handle);

				string_Handle = ""; 
				continue;
	
			}
 
			string_Handle += string_OperativeFunction[i];  
	
		}
	
		list_StringOperative.Add(string_Handle);
		

		return list_StringOperative;
	
	}


    void ColorCurrentTextWord_FULLCOLOR_0(int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_0.GetComponent<TMP_Text>();

 
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


    void ColorCurrentTextWord_0(int int_StartCharacter, int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_0.GetComponent<TMP_Text>();

 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
 
        for(int i = 0; i < int_StartCharacter + int_NumberOfCharacters; i++)
        {
 
 
	        int currentCharacter = i ;

	        int characterCount = textInfo.characterCount;

	        Color32[] newVertexColors;
	        Color32 c0 = m_TextComponent.color;
	 
	        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
	        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
	        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
	 
			if(i >= int_StartCharacter)
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
		            c0 = new Color32((255), (255), (255), 0);
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



    void ColorCurrentTextWordDefault_0( int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_0.GetComponent<TMP_Text>();

 
 
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
	            c0 = new Color32((255), (255), (255), 0);
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


    void ColorCurrentTextWord_FULLCOLOR_1(int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_1.GetComponent<TMP_Text>();

 
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




    void ColorCurrentTextWord_1( int int_StartCharacter, int int_NumberOfCharacters)
    { 
 
		TMP_Text m_TextComponent = TextOne_1.GetComponent<TMP_Text>();
 
 
		m_TextComponent.ForceMeshUpdate();
 
        TMP_TextInfo textInfo = m_TextComponent.textInfo;
 	
        for(int i = 0; i < int_StartCharacter + int_NumberOfCharacters; i++)
        {
 
 
	        int currentCharacter = i ;

	        int characterCount = textInfo.characterCount;

	        Color32[] newVertexColors;
	        Color32 c0 = m_TextComponent.color;
	 
	        int materialIndex = textInfo.characterInfo[currentCharacter].materialReferenceIndex;
	        newVertexColors = textInfo.meshInfo[materialIndex].colors32;
	        int vertexIndex = textInfo.characterInfo[currentCharacter].vertexIndex;
	 
			if(i >= int_StartCharacter)
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
		            c0 = new Color32((255), (255), (255), 0);
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
 
		TMP_Text m_TextComponent = TextOne_1.GetComponent<TMP_Text>();
 
 
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
	            c0 = new Color32((255), (255), (255), 0);
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

    int counterColoredCharacters = 0;



    bool bool_ActiveCamaraShake = false;
    
    float float_evaluationTime = 0.0f;
	
	int int_CounterCollisionChar = 0;
	int int_LengthBothWords = 0;



	bool bool_CheckLengthColored = false;
	bool bool_ActiveCamaraCheck = false;
	// bool bool_OneIsActivated 

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
	


    void Update()
    {

		if(Input.GetKeyDown(KeyCode.Tab))
		{

			Debug.Log(text_ContainerHeight_0.GetComponent<TextMeshPro>().preferredHeight);
			Debug.Log(text_ContainerHeight_1.GetComponent<TextMeshPro>().preferredHeight);
			Debug.Log(text_ContainerHeight_2.GetComponent<TextMeshPro>().preferredHeight);

		}


		SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;

		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;


	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_9 = WordsCounterCommunicationClass.int_CurrentNumberWords;
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


		string string_InputField = CommunicationFieldSentenceClass.string_InputFieldSentence;


		int int_CounterColorWordOne = 0;
		
		if(bool_ActiveStringOne)
		{

			if(string_InputField.Length <= string_CurrentWordOne.Length)
			{

				for(int i = 0 ; i < string_InputField.Length; i++)
				{

					if(string_InputField[i] == string_CurrentWordOne[i])
					{

						int_CounterColorWordOne++;

					}
					else
					{
						break;
					}
				}
				
				ColorCurrentTextWord_0(int_CurrentPositionWordOne, int_CounterColorWordOne);
				
			}

		}		



		int int_CounterColorWordTwo = 0;

		if(bool_ActiveStringTwo)
		{

			if(string_InputField.Length <= string_CurrentWordTwo.Length)
			{

				for(int i = 0 ; i < string_InputField.Length; i++)
				{

					if(string_InputField[i] == string_CurrentWordTwo[i])
					{

						int_CounterColorWordTwo++;

					}
					else
					{
						break;
					}
				}
				
				ColorCurrentTextWord_1(int_CurrentPositionWordTwo, int_CounterColorWordTwo);
				
			}
			
		}



		if(bool_CheckLengthColored == false)
		{

			if(int_CounterColorWordOne >= 1 && int_CounterColorWordOne < string_InputField.Length && int_CounterColorWordOne > int_CounterColorWordTwo)
			{
				bool_CheckLengthColored = true;
				bool_ActiveCamaraCheck = true;
			}

		}


		if(bool_CheckLengthColored == false)
		{

			if(int_CounterColorWordTwo >= 1 && int_CounterColorWordTwo < string_InputField.Length && int_CounterColorWordTwo > int_CounterColorWordOne)
			{
				bool_CheckLengthColored = true;
				bool_ActiveCamaraCheck = true;
			}

		}


		if(bool_ActiveCamaraCheck)
		{
			bool_ActiveCamaraCheck = false;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;

		}


		if(int_CounterColorWordOne == string_InputField.Length)
		{
			bool_CheckLengthColored = false;
		}

		if(int_CounterColorWordTwo == string_InputField.Length)
		{
			bool_CheckLengthColored = false;
		}


		if(string_InputField == string_CurrentWordOne && bool_ActiveStringOne) 
		{

			int_CounterWordListOne++;
			int_CurrentPositionWordOne += string_CurrentWordOne.Length;
			if(int_CurrentPositionWordOne == string_OneTranslation.Length)
			{
				bool_ActiveStringOne = false;
				BoxOne_0.SetActive(false);			
			
			}
			ColorCurrentTextWordDefault_0(int_CurrentPositionWordOne);
			CommunicationFieldSentenceClass.string_InputFieldSentence = "";
			CommunicationFieldSentenceClass.bool_ActivateReset = true;			

			if(int_CounterWordListOne < list_StringWordOne.Count)
			{
				string_CurrentWordOne = list_StringWordOne[int_CounterWordListOne];	
			}

			CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

			WordsCounterCommunicationClass.int_CurrentNumberWords++;				
			

		}

		if(bool_ActiveStringOne == false)
		{
			ColorCurrentTextWord_FULLCOLOR_0(int_CurrentPositionWordOne);
		}



		if(string_InputField == string_CurrentWordTwo && bool_ActiveStringTwo)
		{

			int_CounterWordListTwo++;
			int_CurrentPositionWordTwo += string_CurrentWordTwo.Length;
			if(int_CurrentPositionWordTwo == string_TwoTranslation.Length)
			{
				bool_ActiveStringTwo = false;
				BoxOne_1.SetActive(false);			
			}
			ColorCurrentTextWordDefault_1(int_CurrentPositionWordTwo);
			CommunicationFieldSentenceClass.string_InputFieldSentence = "";
			CommunicationFieldSentenceClass.bool_ActivateReset = true;
			// string_CurrentWordTwo = list_StringWordTwo[int_CounterWordListTwo];	

			if(int_CounterWordListTwo < list_StringWordTwo.Count)
			{
				string_CurrentWordTwo = list_StringWordTwo[int_CounterWordListTwo];	
			}
	
			CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

			WordsCounterCommunicationClass.int_CurrentNumberWords++;				

		}


		
		if(bool_ActiveStringTwo == false)
		{
			ColorCurrentTextWord_FULLCOLOR_1(int_CurrentPositionWordTwo);
		}



		if(bool_ActiveStringOne == false && bool_ActiveStringTwo == false)
		{
			
			bool_ResetPairWords = true;

		}



    	if(CommunicationFieldSentenceClass.bool_ActiveEnterPress == true)
    	{	

    		CommunicationFieldSentenceClass.bool_ActiveEnterPress = false;

    		Debug.Log("String In InputField After ENTER  == " + CommunicationFieldSentenceClass.string_InputFieldSentence);

    		string_FromInputField = CommunicationFieldSentenceClass.string_InputFieldSentence;

    		bool_CheckString = true;

    	}



		if(bool_ResetPairWords)
    	{

			BoxOne_0.SetActive(true);
			BoxOne_1.SetActive(true);
	
			bool_ActiveStringOne = true;
			bool_ActiveStringTwo = true;

			int_CurrentPositionWordOne = 0;
			int_CurrentPositionWordTwo = 0;
			string_CurrentWordOne = "";
			string_CurrentWordTwo = "";


			bool_ResetPairWords = false;


			TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
			TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();

			var src = DateTime.Now;
			var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
			int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);


			System.Random randomGeneratorNumber = new System.Random(hashRandom);
			int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];


			valuesOne_0.text = string_OneTranslation;
			valuesOne_1.text = string_TwoTranslation;


			float widthText_One   = valuesOne_0.preferredWidth;
			float heightText_One  = valuesOne_0.preferredHeight;

			// Debug.Log(widthText_One);
			// Debug.Log(heightText_One);
			

			backgroundShader_0.transform.position = vector3_PositionStartBackgroundShader_0;
			BoxOne_0.transform.position = vector3_PositionStartBackground_0;
			
			backgroundShader_0.transform.localScale = vector3_ScaleStartBackgroundShader_0;
			BoxOne_0.transform.localScale = vector3_ScaleStartBackground_0;
			
	
	
			float float_RatioHeightPivot = float_heightBase_2 - float_heightBase_1;
			
	

			if(widthText_One > 53.0f)
			{

			
				float heightText_One_Operative = (heightText_One) - float_heightBase_0;


				heightText_One_Operative = (float)System.Math.Round(heightText_One_Operative/float_RatioHeightPivot,2);


				if(heightText_One_Operative == 1 )
				{
				
					// Debug.Log("Reaching this point");
					backgroundShader_0.transform.localScale = GetScaleVerticalBackGroundShader(2, backgroundShader_0.transform.localScale); 	
					BoxOne_0.transform.localScale = GetScaleVerticalBackGround(2, BoxOne_0.transform.localScale);
					
					backgroundShader_0.transform.position -= GetPositionVerticalBackGround(0.33f);
					BoxOne_0.transform.position -= GetPositionVerticalBackGround(0.33f);

				}

				if(heightText_One_Operative == 2 )
				{
				
					backgroundShader_0.transform.localScale = GetScaleVerticalBackGroundShader(3, backgroundShader_0.transform.localScale); 	
					BoxOne_0.transform.localScale = GetScaleVerticalBackGround(3, BoxOne_0.transform.localScale);

					backgroundShader_0.transform.position -= GetPositionVerticalBackGround(0.66f);
					BoxOne_0.transform.position -= GetPositionVerticalBackGround(0.66f);

				}


				backgroundShader_0.transform.localScale = GetScaleBackGroundShader(53.0f, backgroundShader_0.transform.localScale); 	
				BoxOne_0.transform.localScale = GetScaleBackGround(53.0f, BoxOne_0.transform.localScale);

				backgroundShader_0.transform.position += GetPositionBackGround(53.0f);
				BoxOne_0.transform.position += GetPositionBackGround(53.0f);

			}
			else
			{

				backgroundShader_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_0.transform.localScale); 	
				BoxOne_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_0.transform.localScale);
				
				backgroundShader_0.transform.position += GetPositionBackGround(widthText_One);
				BoxOne_0.transform.position += GetPositionBackGround(widthText_One);

			}



			widthText_One   = valuesOne_1.preferredWidth;
			heightText_One  = valuesOne_1.preferredHeight;
			
			// Debug.Log(widthText_One);
			// Debug.Log(heightText_One);
		

			backgroundShader_1.transform.position = vector3_PositionStartBackgroundShader_1;
			BoxOne_1.transform.position = vector3_PositionStartBackground_1;
			
			backgroundShader_1.transform.localScale = vector3_ScaleStartBackgroundShader_1;
			BoxOne_1.transform.localScale = vector3_ScaleStartBackground_1;
			
	


			if(widthText_One > 53.0f)
			{

				float heightText_One_Operative = (heightText_One) - float_heightBase_0;


				heightText_One_Operative = (float)System.Math.Round(heightText_One_Operative/float_RatioHeightPivot,2);


				// Debug.Log(heightText_One_Operative/2.5 == 1);

				if(heightText_One_Operative == 1 )
				{
				
					// Debug.Log("Reaching this point");
					backgroundShader_1.transform.localScale = GetScaleVerticalBackGroundShader(2, backgroundShader_1.transform.localScale); 	
					BoxOne_1.transform.localScale = GetScaleVerticalBackGround(2, BoxOne_1.transform.localScale);
					
					backgroundShader_1.transform.position -= GetPositionVerticalBackGround(0.33f);
					BoxOne_1.transform.position -= GetPositionVerticalBackGround(0.33f);

				}

				if(heightText_One_Operative == 2 )
				{
				
					backgroundShader_1.transform.localScale = GetScaleVerticalBackGroundShader(3, backgroundShader_1.transform.localScale); 	
					BoxOne_1.transform.localScale = GetScaleVerticalBackGround(3, BoxOne_1.transform.localScale);

					backgroundShader_1.transform.position -= GetPositionVerticalBackGround(0.66f);
					BoxOne_1.transform.position -= GetPositionVerticalBackGround(0.66f);

				}

				backgroundShader_1.transform.localScale = GetScaleBackGroundShader(53.0f, backgroundShader_1.transform.localScale); 	
				BoxOne_1.transform.localScale = GetScaleBackGround(53.0f, BoxOne_1.transform.localScale);

				backgroundShader_1.transform.position += GetPositionBackGround(53.0f);
				BoxOne_1.transform.position += GetPositionBackGround(53.0f);

			}
			else
			{

				backgroundShader_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_1.transform.localScale); 	
				BoxOne_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_1.transform.localScale);
				
				backgroundShader_1.transform.position += GetPositionBackGround(widthText_One);
				BoxOne_1.transform.position += GetPositionBackGround(widthText_One);

			}


			list_StringWordOne = GetListOfWordSeparated(string_OneTranslation);
			list_StringWordTwo = GetListOfWordSeparated(string_TwoTranslation);

			int_CounterWordListOne = 0;			
			int_CounterWordListTwo = 0;

			string_CurrentWordOne = list_StringWordOne[int_CounterWordListOne];	
			string_CurrentWordTwo = list_StringWordTwo[int_CounterWordListTwo];	

			CommunciationImplementationResetClass.bool_ActiveResetWords = true;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = true;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = true;



    	}
        
    }

}
