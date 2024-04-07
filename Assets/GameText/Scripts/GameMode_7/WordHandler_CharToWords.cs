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
using CommunicationBezierCharToWordsNamespace;
using CommunicationCollisionCharToWordsNamespace;

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_CharToWords : MonoBehaviour
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



	public GameObject PlaneBackground_correct;
	public GameObject PlaneShader_correct;

	public GameObject Container_Correct_0;
	public GameObject Container_Correct_1;

	public GameObject backgroundShader_Correct_0;
	public GameObject backgroundShader_Correct_1;

	public GameObject BoxOne_Correct_0;
	public GameObject BoxOne_Correct_1;

	public GameObject TextOne_Correct_0;
	public GameObject TextOne_Correct_1;

	Vector3 vector3_PositionStartBackground_Correct_0;
	Vector3 vector3_PositionStartBackground_Correct_1;



	List<GameObject> list_GameObjectInstanciated_0;
	List<GameObject> list_GameObjectInstanciated_1;
		
    List<int> list_IntPositionMark_0;
    List<int> list_IntPositionMark_1;

    List<string> list_OneTranslationOperational;
    List<string> list_TwoTranslationOperational;



 	List<string> list_OfStringEnglish;
 	List<string> list_OfStringFrench;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";
    string string_OneTranslationOperational = "Goal";
    string string_TwoTranslationOperational = "Goalition";


	List<int> list_IntCharToColorOne;
	List<int> list_IntCharToColorTwo;
    

	List<int> list_PositionCheck;

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
		
		int_LengthBothWords = string_OneTranslation.Length + string_TwoTranslation.Length;

		valuesOne_0.text = string_OneTranslation;
		valuesOne_1.text = string_TwoTranslation;

		string_OneTranslationOperational = string_OneTranslation;
		string_TwoTranslationOperational = string_TwoTranslation;

		list_OneTranslationOperational = new List<string>();
		list_TwoTranslationOperational = new List<string>(); 

		list_IntCharToColorOne = new List<int>(); 		
		list_IntCharToColorTwo = new List<int>();		


		for(int i = 0; i < string_OneTranslation.Length; i++)
		{
		
			list_OneTranslationOperational.Add(string_OneTranslation[i].ToString());
			list_IntCharToColorOne.Add(0);

		}

		for(int i = 0; i < string_TwoTranslation.Length; i++)
		{

			list_TwoTranslationOperational.Add(string_TwoTranslation[i].ToString());
			list_IntCharToColorTwo.Add(0);

		}


		float widthText_One   = valuesOne_0.preferredWidth;
		float heightText_One  = valuesOne_0.preferredHeight;


		backgroundShader_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_0.transform.localScale); 	
		BoxOne_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_0.transform.localScale);

		vector3_PositionStartBackground_0 = new Vector3(BoxOne_0.transform.position.x, BoxOne_0.transform.position.y, BoxOne_0.transform.position.z);

		backgroundShader_0.transform.position += GetPositionBackGround(widthText_One);
		BoxOne_0.transform.position += GetPositionBackGround(widthText_One);


		widthText_One   = valuesOne_1.preferredWidth;
		heightText_One  = valuesOne_1.preferredHeight;


		backgroundShader_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_1.transform.localScale); 	
		BoxOne_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_1.transform.localScale);

		vector3_PositionStartBackground_1 = new Vector3(BoxOne_1.transform.position.x, BoxOne_1.transform.position.y, BoxOne_1.transform.position.z);

		backgroundShader_1.transform.position += GetPositionBackGround(widthText_One);
		BoxOne_1.transform.position += GetPositionBackGround(widthText_One);


		int int_LengthOne = string_OneTranslation.Length;
		int int_LengthTwo = string_TwoTranslation.Length;






		TextMeshPro valuesOne_Correct_0 = TextOne_Correct_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_Correct_1 = TextOne_Correct_1.GetComponent<TextMeshPro>();

		valuesOne_Correct_0.font = font_asset;
		valuesOne_Correct_1.font = font_asset;

		valuesOne_Correct_0.fontSize = 15;
		valuesOne_Correct_1.fontSize = 15;

		valuesOne_Correct_0.text = string_OneTranslation;
		valuesOne_Correct_1.text = string_TwoTranslation;

		widthText_One   = valuesOne_Correct_0.preferredWidth;
		heightText_One  = valuesOne_Correct_0.preferredHeight;


		backgroundShader_Correct_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_0.transform.localScale); 	
		BoxOne_Correct_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_0.transform.localScale);

		vector3_PositionStartBackground_Correct_0 = new Vector3(BoxOne_Correct_0.transform.position.x, BoxOne_Correct_0.transform.position.y, BoxOne_Correct_0.transform.position.z);

		backgroundShader_Correct_0.transform.position += GetPositionBackGround(widthText_One);
		BoxOne_Correct_0.transform.position += GetPositionBackGround(widthText_One);


		widthText_One   = valuesOne_Correct_1.preferredWidth;
		heightText_One  = valuesOne_Correct_1.preferredHeight;


		backgroundShader_Correct_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_1.transform.localScale); 	
		BoxOne_Correct_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_1.transform.localScale);

		vector3_PositionStartBackground_Correct_1 = new Vector3(BoxOne_Correct_1.transform.position.x, BoxOne_Correct_1.transform.position.y, BoxOne_Correct_1.transform.position.z);

		backgroundShader_Correct_1.transform.position += GetPositionBackGround(widthText_One);
		BoxOne_Correct_1.transform.position += GetPositionBackGround(widthText_One);


		PlaneBackground_correct.SetActive(false);
		PlaneShader_correct.SetActive(false);
		Container_Correct_0.SetActive(false);
		Container_Correct_1.SetActive(false);




		// System.Random randomGeneratorNumber = new System.Random(hashRandom);


		// x = -8.65 min corners
		// x = 1.55  max corners
		// y = -2.74 min corners
		// y = 6.0  max corners


		// x = -8.15 min corners correctect / inner square;
		// x = 1.05  max corners correctect / inner square;
		// y = -2.54 min corners correctect  / inner square;
		// y = 5.5  max corners correctect / inner square;


		// x = 9.2;
		// y = 8.04;

		list_GameObjectInstanciated_0 = new List<GameObject>();
		list_GameObjectInstanciated_1 = new List<GameObject>();

		list_IntPositionMark_0 = new List<int>(); 
		list_IntPositionMark_1 = new List<int>(); 

		List<Vector3> list_Vector3Addition = new List<Vector3>();


		float float_randomPosition_x;
		float float_randomPosition_y;


		for(int i = 0; i < int_LengthOne; i++)
		{

			bool bool_Verifyaddition = true;
			
			Vector3 vector3_NewPositionInstance = new Vector3(0f, 0f, 0f);
	
			for(int j = 0; j < 100; j++)
			// while(bool_Verifyaddition)
			{

				float_randomPosition_x = 9.2f * (float)randomGeneratorNumber.NextDouble();
				float_randomPosition_y = 8.04f * (float)randomGeneratorNumber.NextDouble();
				
				vector3_NewPositionInstance = new Vector3(-6.15f + float_randomPosition_x, -2.54f + float_randomPosition_y,  10f);

				for(int n = 0; n < list_Vector3Addition.Count; n++)
				{

					Vector3 vector3_ValidatePosition = vector3_NewPositionInstance - list_Vector3Addition[n];

					if(vector3_ValidatePosition.magnitude < 1.0f)
					{
						break;

					}
			
					bool_Verifyaddition = false;

				}

				if(bool_Verifyaddition == false)
				{
					j = 100;
				}

			}
			

			list_Vector3Addition.Add(vector3_NewPositionInstance);



			GameObject gameobject_InstanceContainer = Instantiate(Container_0, Vector3.zero, Quaternion.identity);

			gameobject_InstanceContainer.name = "container_0_" + i.ToString();

			GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
			GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
			GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;


			backgroundShaderObject.GetComponent<Plane_Renderer_CharToWords_0>().bool_ActivePredefinedShader = true;

			backgroundPlaneObject.name = "containerPlane_0_" + i.ToString();
			backgroundPlaneObject.layer = 8;
			backgroundPlaneObject.AddComponent<Rigidbody>();
			backgroundPlaneObject.GetComponent<Rigidbody>().useGravity = false;
    		LayerMask elementLayer = LayerMask.NameToLayer("Arrow");
    		// Debug.Log(elementLayer.value);

			backgroundPlaneObject.GetComponent<BoxCollider>().excludeLayers = (int) Math.Pow(2, elementLayer.value);


			gameobject_InstanceContainer.transform.position = new Vector3(vector3_NewPositionInstance.x, vector3_NewPositionInstance.y, vector3_NewPositionInstance.z); 


			TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();

			textMeshObject.font = font_asset;
			textMeshObject.fontSize = 15;

			textMeshObject.text = string_OneTranslation[i].ToString();

			// float float_OperationalSpaceWidth = (string_OneTranslation[i].ToString() == " ")

			widthText_One   = (string_OneTranslation[i].ToString() == " ")? 1.0f: textMeshObject.preferredWidth ;
			heightText_One  = textMeshObject.preferredHeight;
			Debug.Log(widthText_One);


			backgroundShaderObject.transform.localScale =  GetScaleBackGroundShader(widthText_One * 1.5f, backgroundShaderObject.transform.localScale); 	
			backgroundPlaneObject.transform.localScale  =        GetScaleBackGround(widthText_One * 1.5f, backgroundPlaneObject.transform.localScale);

			// vector3_PositionStartBackground_0 //= new Vector3(backgroundPlaneObject.transform.position.x, backgroundPlaneObject.transform.position.y, backgroundPlaneObject.transform.position.z);

			backgroundShaderObject.transform.position -= GetPositionBackGround(valuesOne_0.preferredWidth);
			backgroundPlaneObject.transform.position  -= GetPositionBackGround(valuesOne_0.preferredWidth);

			backgroundShaderObject.transform.position += GetPositionBackGround(widthText_One);
			backgroundPlaneObject.transform.position  += GetPositionBackGround(widthText_One);

			backgroundPlaneObject.GetComponent<CollisionScriptCode>().enabled = true;



			list_GameObjectInstanciated_0.Add(gameobject_InstanceContainer);
			list_IntPositionMark_0.Add(0);

		}



		for(int i = 0; i < int_LengthTwo; i++)
		{

			bool bool_Verifyaddition = true;
			
			Vector3 vector3_NewPositionInstance = new Vector3(0f, 0f, 0f);
	
			// while(bool_Verifyaddition)
			for(int j = 0; j < 100; j++)
			{

				float_randomPosition_x = 9.2f * (float)randomGeneratorNumber.NextDouble();
				float_randomPosition_y = 8.04f * (float)randomGeneratorNumber.NextDouble();
				
				vector3_NewPositionInstance = new Vector3(-6.15f + float_randomPosition_x, -2.54f + float_randomPosition_y,  10f);


				// foreach(Vector3 vector3_PositionsAdded in list_Vector3Addition)
				// {

				// 	Vector3 vector3_ValidatePosition = vector3_NewPositionInstance - vector3_PositionsAdded;

				for(int n = 0; n < list_Vector3Addition.Count; n++)
				{

					Vector3 vector3_ValidatePosition = vector3_NewPositionInstance - list_Vector3Addition[n];

					if(vector3_ValidatePosition.magnitude < 1.0f)
					{
						break;

					}
			
					bool_Verifyaddition = false;

				}

				if(bool_Verifyaddition == false)
				{
					j = 100;
				}

			}

			
			list_Vector3Addition.Add(vector3_NewPositionInstance);



			GameObject gameobject_InstanceContainer = Instantiate(Container_1, Vector3.zero, Quaternion.identity);

			gameobject_InstanceContainer.name = "container_1_" + i.ToString();

			GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
			GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
			GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;

			backgroundShaderObject.GetComponent<Plane_Renderer_CharToWords_1>().bool_ActivePredefinedShader = true;

			backgroundPlaneObject.name = "containerPlane_0_" + i.ToString();
			backgroundPlaneObject.layer = 8;
			backgroundPlaneObject.AddComponent<Rigidbody>();
			backgroundPlaneObject.GetComponent<Rigidbody>().useGravity = false;
    		LayerMask elementLayer = LayerMask.NameToLayer("Arrow");
    		// Debug.Log(elementLayer.value);

			backgroundPlaneObject.GetComponent<BoxCollider>().excludeLayers = (int) Math.Pow(2, elementLayer.value);


			gameobject_InstanceContainer.transform.position = new Vector3(vector3_NewPositionInstance.x, vector3_NewPositionInstance.y, vector3_NewPositionInstance.z); 


			// GameObject gameobject_InstanceContainer = Instantiate(Container_1, vector3_NewPositionInstance, Quaternion.identity);

			// gameobject_InstanceContainer.name = "container_1_" + i.ToString();		

			// GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
			// GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
			// GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;

			TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();

			textMeshObject.text = string_TwoTranslation[i].ToString();

			// widthText_One   = textMeshObject.preferredWidth;

			widthText_One   = (string_TwoTranslation[i].ToString() == " ")? 1.0f: textMeshObject.preferredWidth ;
			heightText_One  = textMeshObject.preferredHeight;


			backgroundShaderObject.transform.localScale =  GetScaleBackGroundShader(widthText_One * 1.5f, backgroundShaderObject.transform.localScale); 	
			backgroundPlaneObject.transform.localScale  =        GetScaleBackGround(widthText_One * 1.5f, backgroundPlaneObject.transform.localScale);

			// vector3_PositionStartBackground_0 //= new Vector3(backgroundPlaneObject.transform.position.x, backgroundPlaneObject.transform.position.y, backgroundPlaneObject.transform.position.z);


			backgroundShaderObject.transform.position -= GetPositionBackGround(valuesOne_1.preferredWidth);
			backgroundPlaneObject.transform.position  -= GetPositionBackGround(valuesOne_1.preferredWidth);

			backgroundShaderObject.transform.position += GetPositionBackGround(widthText_One);
			backgroundPlaneObject.transform.position  += GetPositionBackGround(widthText_One);

			backgroundPlaneObject.GetComponent<CollisionScriptCode>().enabled = true;

			// backgroundPlaneObject.GetComponent<Rigidbody>().useGravity = false;

			// Debug.Log(gameobject_InstanceContainer.transform.GetChild(1).gameObject);


			list_GameObjectInstanciated_1.Add(gameobject_InstanceContainer);
			list_IntPositionMark_1.Add(0);

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


    Vector3 GetPositionBackGround(float width)
    {


		float position = (width * 0.16f)/1.15f ; 
    	Vector3 valueOut = new Vector3(position, 0.0f, 0.0f);
  
    	return valueOut;
    	// return position;
    }



    void ColorCurrentTextWordOne(List<int> list_CharToColor)
    { 

		TMP_Text m_TextComponent = TextOne_0.GetComponent<TMP_Text>();


		m_TextComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = m_TextComponent.textInfo;


        for(int i = 0; i < list_CharToColor.Count; i++)
        {

			if(list_CharToColor[i] == 1)
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

			else
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
		            c0 = new Color32(0, 0, 0, 0);
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

    void ColorCurrentTextWordTwo(List<int> list_CharToColor)
    { 

		TMP_Text m_TextComponent = TextOne_1.GetComponent<TMP_Text>();


		m_TextComponent.ForceMeshUpdate();

        TMP_TextInfo textInfo = m_TextComponent.textInfo;


        for(int i = 0; i < list_CharToColor.Count; i++)
        {

			if(list_CharToColor[i] == 1)
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

			else
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
		            c0 = new Color32(0, 0, 0, 0);
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

	
    void ColorCurrentTextPair(bool bool_SetList, int numberOfCharacters)
    { 

		TMP_Text m_TextComponent;

		if(bool_SetList == true)
		{
			m_TextComponent = TextOne_Correct_0.GetComponent<TMP_Text>();

		}
		else
		{
			m_TextComponent = TextOne_Correct_1.GetComponent<TMP_Text>();
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






    int int_CounterCorrectPairWords = 0;

    int int_CounterTranslation = 1;

    int int_CounterPositionListWords = 0;

    bool bool_FirstString = true;
	bool bool_SecondString = true;

    bool bool_CheckString = false;

    int int_PositionContainerFound_0 = -1; 

    int int_PositionContainerFound_1 = -1; 
    
    bool bool_AnswerFound = false;

	bool bool_ActiveCorrectAnswerMiniGame = false;
	bool bool_ActiveMiniGameStatus = false;

	bool bool_SolutionMiniGameOne = false;
	bool bool_SolutionMiniGameTwo = false;

    bool bool_ResetListOfWords = false;




    string string_FromInputField = "";

    int counterColoredCharacters = 0;

    string string_EvaluationColor = "";


    bool bool_ActiveCamaraShake = false;
    
    float float_evaluationTime = 0.0f;
	
	int int_CounterCollisionChar = 0;
	int int_LengthBothWords = 0;

	 bool bool_ColoredIdentified_One = false;
	 bool bool_ColoredIdentified_Two = false;




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


		SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;

		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;
			

	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{


				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_7 = WordsCounterCommunicationClass.int_CurrentNumberWords;
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



    	ColorCurrentTextWordOne(list_IntCharToColorOne);
    	ColorCurrentTextWordTwo(list_IntCharToColorTwo);



    	if(bool_CheckString && bool_ActiveMiniGameStatus == false)
    	{

    		bool_CheckString = false;

    		bool_ActiveCamaraShake = true;

    		bool_SecondString = true;

    		if(string_FromInputField.Length == 1)
    		{

    			int int_LastIndexCurrent = string_OneTranslationOperational.LastIndexOf(string_FromInputField); 


				if(int_LastIndexCurrent != -1)
				{

					for(int i = 0; i < list_OneTranslationOperational.Count; i++)
					{


						if(list_IntPositionMark_0[i] == -1)
						{
							continue;
						}

						// string string_comparation = list_GameObjectInstanciated_0[i].transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text;
						string string_comparation = list_OneTranslationOperational[i];



						if(string_comparation == string_FromInputField)
						{

							

							list_GameObjectInstanciated_0[i].transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = "";
							list_IntPositionMark_0[i] = -1;
							list_IntCharToColorOne[i] = 1;
							string_OneTranslationOperational = string_OneTranslationOperational.Remove(int_LastIndexCurrent, 1);

							bool_SecondString = false;

							bool_AnswerFound = true;
							int_PositionContainerFound_0 = i;


							bool_ActiveCamaraShake = false;
							CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

							break;

						}

					}
					
				}


    			int_LastIndexCurrent = string_TwoTranslationOperational.LastIndexOf(string_FromInputField); 



				if(bool_SecondString == true && int_LastIndexCurrent != -1)
				{

					for(int i = 0; i < list_TwoTranslationOperational.Count; i++)
					{

						if(list_IntPositionMark_1[i] == -1)
						{
							continue;
						}


						string string_comparation = list_TwoTranslationOperational[i];


						if(string_comparation == string_FromInputField)
						{

							list_GameObjectInstanciated_1[i].transform.GetChild(2).gameObject.GetComponent<TextMeshPro>().text = "";
							list_IntPositionMark_1[i] = -1;
							list_IntCharToColorTwo[i] = 1;

							string_TwoTranslationOperational = string_TwoTranslationOperational.Remove(int_LastIndexCurrent, 1);
							
							bool_SecondString = false;

							bool_AnswerFound = true;
							int_PositionContainerFound_1 = i;

							bool_ActiveCamaraShake = false;
							CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

							break;

						}

					}

				}

    		}

    		if(string_FromInputField.Length > 1)
    		{


				if(string_FromInputField == string_OneTranslation || string_FromInputField == string_TwoTranslation)
				{

					int_CounterCollisionChar = 0;
					bool_ActiveCorrectAnswerMiniGame = true;
					string_FromInputField = "";

					bool_ActiveCamaraShake = false;
					CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;
				
				}
	

    		}


            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = bool_ActiveCamaraShake;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = bool_ActiveCamaraShake;


    	}


    	if(bool_AnswerFound == true)
    	{
    		bool_AnswerFound = false;

    		if(int_PositionContainerFound_0 >= 0)
    		{


    			Vector3 vector3_operational_0 = new Vector3(list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.GetChild(0).gameObject.transform.position.x, list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.position.y, list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.position.z);
    			list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.position = vector3_operational_0;



				// Vector3 vector3_operational = list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.GetChild(0).gameObject.transform.position;

				list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.GetChild(0).gameObject.transform.position = vector3_operational_0;//new Vector3(0.0f, vector3_operational.y, vector3_operational.z);
				list_GameObjectInstanciated_0[int_PositionContainerFound_0].transform.GetChild(1).gameObject.transform.position = vector3_operational_0;//new Vector3( 	0.0f, vector3_operational.y, vector3_operational.z);


    			CommunicationBezierCharToWords.string_NameObjectMessage = list_GameObjectInstanciated_0[int_PositionContainerFound_0].name;
    			CommunicationBezierCharToWords.bool_ActiveMotionMessage = true;
				CommunicationBezierCharToWords.int_OneOrTwoMessage = 1;

    			int_PositionContainerFound_0 = -1;
    		
    		}

    		if(int_PositionContainerFound_1 >= 0)
    		{

    			Vector3 vector3_operational_1 = new Vector3(list_GameObjectInstanciated_1[int_PositionContainerFound_1].transform.GetChild(0).gameObject.transform.position.x, list_GameObjectInstanciated_1[int_PositionContainerFound_1].transform.position.y, list_GameObjectInstanciated_1[int_PositionContainerFound_1].transform.position.z);
    			list_GameObjectInstanciated_1[int_PositionContainerFound_1].transform.position = vector3_operational_1;



				// Vector3 vector3_operational = list_GameObjectInstanciated_0[int_PositionContainerFound_1].transform.GetChild(0).gameObject.transform.position;

				list_GameObjectInstanciated_1[int_PositionContainerFound_1].transform.GetChild(0).gameObject.transform.position = vector3_operational_1;//new Vector3(0.0f, vector3_operational.y, vector3_operational.z);
				list_GameObjectInstanciated_1[int_PositionContainerFound_1].transform.GetChild(1).gameObject.transform.position = vector3_operational_1;//new Vector3( 	0.0f, vector3_operational.y, vector3_operational.z);



    			CommunicationBezierCharToWords.string_NameObjectMessage = list_GameObjectInstanciated_1[int_PositionContainerFound_1].name;
    			CommunicationBezierCharToWords.bool_ActiveMotionMessage = true;
				CommunicationBezierCharToWords.int_OneOrTwoMessage = 2;


    			int_PositionContainerFound_1 = -1;

    		}
    		

    	}


    	// print(string_OneTranslationOperational);
    	// print(string_TwoTranslationOperational);



    	if(LinkCommunicationColoredClass.bool_ActiveStatus == true)
    	{	

    		LinkCommunicationColoredClass.bool_ActiveStatus = false;

    		Debug.Log("String In InputField After ENTER  == " + LinkCommunicationColoredClass.string_InputField);

    		string_FromInputField = LinkCommunicationColoredClass.string_InputField;

    		bool_CheckString = true;

    	}

		// if(Input.GetKeyDown(KeyCode.Tab))
		// {
		// 	CommunicationCorrectAnswerClass.bool_ActiveResetWords = true;

		// }

		if(CommunicationCollisionCharToWordsClass.bool_ActiveCollisionDetectionMessage)
		{

			CommunicationCollisionCharToWordsClass.bool_ActiveCollisionDetectionMessage = false;
			int_CounterCollisionChar ++;

		}


		if(int_CounterCollisionChar == int_LengthBothWords)
		{

			int_CounterCollisionChar = 0;

			bool_ActiveCorrectAnswerMiniGame = true;

			// bool_ResetListOfWords = true;
		
		}


		if(bool_ActiveCorrectAnswerMiniGame == true)
		{

			bool_ActiveCorrectAnswerMiniGame = false;
			PlaneBackground_correct.SetActive(true);
			PlaneShader_correct.SetActive(true);
			Container_Correct_0.SetActive(true);
			Container_Correct_1.SetActive(true);

			bool_ActiveMiniGameStatus = true;

		}


		
		string string_OperativeInputField = LinkCommunicationColoredClass.string_InputField;
		
		if(bool_SolutionMiniGameOne == false)
		{

			// Debug.Log("Print elements ||   " + string_OperativeInputField);


			int int_CounterToColorChar = 0;

			if(string_OperativeInputField.Length <= string_OneTranslation.Length)
			{				
				for(int i = 0; i < string_OperativeInputField.Length; i++)
				{
					if(string_OperativeInputField[i] == string_OneTranslation[i])
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
			
			ColorCurrentTextPair(true, int_CounterToColorChar);

		}

		if(bool_SolutionMiniGameTwo == false)
		{

			// Debug.Log("Print elements ||   " + string_OperativeInputField);

			int int_CounterToColorChar = 0;

			if(string_OperativeInputField.Length <= string_TwoTranslation.Length)
			{				
				for(int i = 0; i < string_OperativeInputField.Length; i++)
				{
					if(string_OperativeInputField[i] == string_TwoTranslation[i])
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
			
			ColorCurrentTextPair(false, int_CounterToColorChar);

		}



		if(bool_ActiveMiniGameStatus && bool_CheckString)
		{
			bool_CheckString = false;

			bool_ActiveCamaraShake = true;

			if(string_FromInputField == string_OneTranslation && bool_SolutionMiniGameOne == false)
			{
			
				bool_SolutionMiniGameOne = true;

				TextMeshPro operationalTextOne = TextOne_Correct_0.GetComponent<TextMeshPro>();
				
				BoxOne_Correct_0.SetActive(false);

				bool_ColoredIdentified_One = true;
				// operationalTextOne.text = "";
				
				CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

				bool_ActiveCamaraShake = false;

				WordsCounterCommunicationClass.int_CurrentNumberWords++;
			
			}


			if(string_FromInputField == string_TwoTranslation && bool_SolutionMiniGameTwo == false)
			{
			
				bool_SolutionMiniGameTwo = true;
				
				TextMeshPro operationalTextTwo = TextOne_Correct_1.GetComponent<TextMeshPro>();

				BoxOne_Correct_1.SetActive(false);
				
				bool_ColoredIdentified_Two = true;
				
				// operationalTextTwo.text = "";

				CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;

				bool_ActiveCamaraShake = false;

				WordsCounterCommunicationClass.int_CurrentNumberWords++;

			}

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = bool_ActiveCamaraShake;

            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = bool_ActiveCamaraShake;


		}


		if(bool_ColoredIdentified_One)
		{
		
			ColorCurrentTextPair(true, string_OneTranslation.Length);
		
		}


		if(bool_ColoredIdentified_Two)
		{
		
			ColorCurrentTextPair(false, string_TwoTranslation.Length);
		
		}



		if(bool_SolutionMiniGameOne && bool_SolutionMiniGameTwo)
		{
		
			bool_SolutionMiniGameOne = false;
			bool_SolutionMiniGameTwo = false;
			
			bool_ActiveMiniGameStatus = false;

			bool_ColoredIdentified_One = false;
			bool_ColoredIdentified_Two = false;
			
			bool_ResetListOfWords = true;
		
		}


		// if(CommunicationCollisionClass.bool_ResetListOfWords)
		if(bool_ResetListOfWords)
    	{
			// CommunicationCollisionClass.bool_ResetListOfWords = false;    		
			BoxOne_Correct_0.SetActive(true);
			BoxOne_Correct_1.SetActive(true);
			

    		for(int i = 0; i < list_GameObjectInstanciated_0.Count; i++)
    		{
    			Destroy(list_GameObjectInstanciated_0[i]);

    		}

    		for(int i = 0; i < list_GameObjectInstanciated_1.Count; i++)
    		{
    			Destroy(list_GameObjectInstanciated_1[i]);

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
			
			int_LengthBothWords = string_OneTranslation.Length + string_TwoTranslation.Length;
	
			valuesOne_0.text = string_OneTranslation;
			valuesOne_1.text = string_TwoTranslation;
	
			string_OneTranslationOperational = string_OneTranslation;
			string_TwoTranslationOperational = string_TwoTranslation;
	
			list_OneTranslationOperational = new List<string>();
			list_TwoTranslationOperational = new List<string>(); 

			list_IntCharToColorOne = new List<int>(); 		
			list_IntCharToColorTwo = new List<int>();		
	

			for(int i = 0; i < string_OneTranslation.Length; i++)
			{
			
				list_OneTranslationOperational.Add(string_OneTranslation[i].ToString());
				list_IntCharToColorOne.Add(0);
	
			}
	
			for(int i = 0; i < string_TwoTranslation.Length; i++)
			{
	
				list_TwoTranslationOperational.Add(string_TwoTranslation[i].ToString());
				list_IntCharToColorTwo.Add(0);
	
			}
	
	

			float widthText_One   = valuesOne_0.preferredWidth;
			float heightText_One  = valuesOne_0.preferredHeight;
	
	
			backgroundShader_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_0.transform.localScale); 	
			BoxOne_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_0.transform.localScale);
	
			backgroundShader_0.transform.position = new Vector3(vector3_PositionStartBackground_0.x, vector3_PositionStartBackground_0.y, vector3_PositionStartBackground_0.z); 
			BoxOne_0.transform.position = new Vector3(vector3_PositionStartBackground_0.x, vector3_PositionStartBackground_0.y, vector3_PositionStartBackground_0.z - 0.05f); 

			backgroundShader_0.transform.position += GetPositionBackGround(widthText_One);
			BoxOne_0.transform.position += GetPositionBackGround(widthText_One);
	
	
			widthText_One   = valuesOne_1.preferredWidth;
			heightText_One  = valuesOne_1.preferredHeight;
	
	
			backgroundShader_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_1.transform.localScale); 	
			BoxOne_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_1.transform.localScale);
	
			backgroundShader_1.transform.position = new Vector3(vector3_PositionStartBackground_1.x, vector3_PositionStartBackground_1.y, vector3_PositionStartBackground_1.z); 
			BoxOne_1.transform.position = new Vector3(vector3_PositionStartBackground_1.x, vector3_PositionStartBackground_1.y, vector3_PositionStartBackground_1.z - 0.05f); 

	
			backgroundShader_1.transform.position += GetPositionBackGround(widthText_One);
			BoxOne_1.transform.position += GetPositionBackGround(widthText_One);
	
	
	
			int int_LengthOne = string_OneTranslation.Length;
			int int_LengthTwo = string_TwoTranslation.Length;
	


	
			TextMeshPro valuesOne_Correct_0 = TextOne_Correct_0.GetComponent<TextMeshPro>();
			TextMeshPro valuesOne_Correct_1 = TextOne_Correct_1.GetComponent<TextMeshPro>();
	
	
			valuesOne_Correct_0.text = string_OneTranslation;
			valuesOne_Correct_1.text = string_TwoTranslation;
	
			widthText_One   = valuesOne_Correct_0.preferredWidth;
			heightText_One  = valuesOne_Correct_0.preferredHeight;
	
	
			backgroundShader_Correct_0.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_0.transform.localScale); 	
			BoxOne_Correct_0.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_0.transform.localScale);
	
			// vector3_PositionStartBackground_Correct_0 = new Vector3(BoxOne_Correct_0.transform.position.x, BoxOne_Correct_0.transform.position.y, BoxOne_Correct_0.transform.position.z);

			backgroundShader_Correct_0.transform.position = new Vector3(vector3_PositionStartBackground_Correct_0.x, vector3_PositionStartBackground_Correct_0.y, vector3_PositionStartBackground_Correct_0.z); 
			BoxOne_Correct_0.transform.position = new Vector3(vector3_PositionStartBackground_Correct_0.x, vector3_PositionStartBackground_Correct_0.y, vector3_PositionStartBackground_Correct_0.z - 0.05f); 

	
			backgroundShader_Correct_0.transform.position += GetPositionBackGround(widthText_One);
			BoxOne_Correct_0.transform.position += GetPositionBackGround(widthText_One);
	
	
			widthText_One   = valuesOne_Correct_1.preferredWidth;
			heightText_One  = valuesOne_Correct_1.preferredHeight;
	
	
			backgroundShader_Correct_1.transform.localScale = GetScaleBackGroundShader(widthText_One, backgroundShader_Correct_1.transform.localScale); 	
			BoxOne_Correct_1.transform.localScale = GetScaleBackGround(widthText_One, BoxOne_Correct_1.transform.localScale);
	
			// vector3_PositionStartBackground_Correct_1 = new Vector3(BoxOne_Correct_1.transform.position.x, BoxOne_Correct_1.transform.position.y, BoxOne_Correct_1.transform.position.z);
	
			backgroundShader_Correct_1.transform.position = new Vector3(vector3_PositionStartBackground_Correct_1.x, vector3_PositionStartBackground_Correct_1.y, vector3_PositionStartBackground_Correct_1.z); 
			BoxOne_Correct_1.transform.position = new Vector3(vector3_PositionStartBackground_Correct_1.x, vector3_PositionStartBackground_Correct_1.y, vector3_PositionStartBackground_Correct_1.z - 0.05f); 

			backgroundShader_Correct_1.transform.position += GetPositionBackGround(widthText_One);
			BoxOne_Correct_1.transform.position += GetPositionBackGround(widthText_One);
	
	
			PlaneBackground_correct.SetActive(false);
			PlaneShader_correct.SetActive(false);
			Container_Correct_0.SetActive(false);
			Container_Correct_1.SetActive(false);
	


	
			// System.Random randomGeneratorNumber = new System.Random(hashRandom);
	
	
			// x = -8.65 min corners
			// x = 1.55  max corners
			// y = -2.74 min corners
			// y = 6.0  max corners
	
	
			// x = -8.15 min corners correctect / inner square;
			// x = 1.05  max corners correctect / inner square;
			// y = -2.54 min corners correctect  / inner square;
			// y = 5.5  max corners correctect / inner square;
	
	
			// x = 9.2;
			// y = 8.04;
	
			list_GameObjectInstanciated_0 = new List<GameObject>();
			list_GameObjectInstanciated_1 = new List<GameObject>();
	
			list_IntPositionMark_0 = new List<int>(); 
			list_IntPositionMark_1 = new List<int>(); 
	
			List<Vector3> list_Vector3Addition = new List<Vector3>();
	
	
			float float_randomPosition_x;
			float float_randomPosition_y;
	
	
			for(int i = 0; i < int_LengthOne; i++)
			{
	
				bool bool_Verifyaddition = true;
				
				Vector3 vector3_NewPositionInstance = new Vector3(0f, 0f, 0f);
		
				for(int j = 0; j < 100; j++)
				// while(bool_Verifyaddition)
				{
	
					float_randomPosition_x = 9.2f * (float)randomGeneratorNumber.NextDouble();
					float_randomPosition_y = 8.04f * (float)randomGeneratorNumber.NextDouble();
					
					vector3_NewPositionInstance = new Vector3(-6.15f + float_randomPosition_x, -2.54f + float_randomPosition_y,  10f);
	
					for(int n = 0; n < list_Vector3Addition.Count; n++)
					{
	
						Vector3 vector3_ValidatePosition = vector3_NewPositionInstance - list_Vector3Addition[n];
	
						if(vector3_ValidatePosition.magnitude < 1.0f)
						{
							break;
	
						}
				
						bool_Verifyaddition = false;
	
					}
	
					if(bool_Verifyaddition == false)
					{
						j = 100;
					}
	
				}
				
	
				list_Vector3Addition.Add(vector3_NewPositionInstance);
	
	
	
				GameObject gameobject_InstanceContainer = Instantiate(Container_0, Vector3.zero, Quaternion.identity);
	
				gameobject_InstanceContainer.name = "container_0_" + i.ToString();
	
				GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
				GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
				GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;

				backgroundShaderObject.GetComponent<Plane_Renderer_CharToWords_0>().bool_ActivePredefinedShader = true;
	
	
				backgroundPlaneObject.name = "containerPlane_0_" + i.ToString();
				backgroundPlaneObject.layer = 8;
				backgroundPlaneObject.AddComponent<Rigidbody>();
				backgroundPlaneObject.GetComponent<Rigidbody>().useGravity = false;
	    		LayerMask elementLayer = LayerMask.NameToLayer("Arrow");
	    		// Debug.Log(elementLayer.value);
	
				backgroundPlaneObject.GetComponent<BoxCollider>().excludeLayers = (int) Math.Pow(2, elementLayer.value);
	
	
				gameobject_InstanceContainer.transform.position = new Vector3(vector3_NewPositionInstance.x, vector3_NewPositionInstance.y, vector3_NewPositionInstance.z); 
	
	
				TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();
	
	
				textMeshObject.text = string_OneTranslation[i].ToString();
	
				// widthText_One   = textMeshObject.preferredWidth;
				widthText_One   = (string_OneTranslation[i].ToString() == " ")? 1.0f: textMeshObject.preferredWidth ;				
				heightText_One  = textMeshObject.preferredHeight;
	
	
				backgroundShaderObject.transform.localScale =  GetScaleBackGroundShader(widthText_One * 1.5f, backgroundShaderObject.transform.localScale); 	
				backgroundPlaneObject.transform.localScale  =        GetScaleBackGround(widthText_One * 1.5f, backgroundPlaneObject.transform.localScale);
	
				// vector3_PositionStartBackground_0 //= new Vector3(backgroundPlaneObject.transform.position.x, backgroundPlaneObject.transform.position.y, backgroundPlaneObject.transform.position.z);

				backgroundShaderObject.transform.position -= GetPositionBackGround(valuesOne_0.preferredWidth);
				backgroundPlaneObject.transform.position  -= GetPositionBackGround(valuesOne_0.preferredWidth);

				backgroundShaderObject.transform.position += GetPositionBackGround(widthText_One);
				backgroundPlaneObject.transform.position  += GetPositionBackGround(widthText_One) + new Vector3(0.0f, 0.0f, -0.1f);
				textObject.transform.position += new Vector3(0.0f, 0.0f, -0.11f);
	

				backgroundPlaneObject.GetComponent<CollisionScriptCode>().enabled = true;
	
	
	
				list_GameObjectInstanciated_0.Add(gameobject_InstanceContainer);
				list_IntPositionMark_0.Add(0);
	
			}
	
	
	
			for(int i = 0; i < int_LengthTwo; i++)
			{
	
				bool bool_Verifyaddition = true;
				
				Vector3 vector3_NewPositionInstance = new Vector3(0f, 0f, 0f);
		
				// while(bool_Verifyaddition)
				for(int j = 0; j < 100; j++)
				{
	
					float_randomPosition_x = 9.2f * (float)randomGeneratorNumber.NextDouble();
					float_randomPosition_y = 8.04f * (float)randomGeneratorNumber.NextDouble();
					
					vector3_NewPositionInstance = new Vector3(-6.15f + float_randomPosition_x, -2.54f + float_randomPosition_y,  10f);
	
	
					// foreach(Vector3 vector3_PositionsAdded in list_Vector3Addition)
					// {
	
					// 	Vector3 vector3_ValidatePosition = vector3_NewPositionInstance - vector3_PositionsAdded;
	
					for(int n = 0; n < list_Vector3Addition.Count; n++)
					{
	
						Vector3 vector3_ValidatePosition = vector3_NewPositionInstance - list_Vector3Addition[n];
	
						if(vector3_ValidatePosition.magnitude < 1.0f)
						{
							break;
	
						}
				
						bool_Verifyaddition = false;
	
					}
	
					if(bool_Verifyaddition == false)
					{
						j = 100;
					}
	
				}
	
				
				list_Vector3Addition.Add(vector3_NewPositionInstance);
	
	
	
				GameObject gameobject_InstanceContainer = Instantiate(Container_1, Vector3.zero, Quaternion.identity);
	
				gameobject_InstanceContainer.name = "container_1_" + i.ToString();
	
				GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
				GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
				GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;

				backgroundShaderObject.GetComponent<Plane_Renderer_CharToWords_1>().bool_ActivePredefinedShader = true;

	
				backgroundPlaneObject.name = "containerPlane_1_" + i.ToString();
				backgroundPlaneObject.layer = 8;
				backgroundPlaneObject.AddComponent<Rigidbody>();
				backgroundPlaneObject.GetComponent<Rigidbody>().useGravity = false;
	    		LayerMask elementLayer = LayerMask.NameToLayer("Arrow");
	    		// Debug.Log(elementLayer.value);
	
				backgroundPlaneObject.GetComponent<BoxCollider>().excludeLayers = (int) Math.Pow(2, elementLayer.value);
	
	
	
				gameobject_InstanceContainer.transform.position = new Vector3(vector3_NewPositionInstance.x, vector3_NewPositionInstance.y, vector3_NewPositionInstance.z); 
	
	
				// GameObject gameobject_InstanceContainer = Instantiate(Container_1, vector3_NewPositionInstance, Quaternion.identity);
	
				// gameobject_InstanceContainer.name = "container_1_" + i.ToString();		
	
				// GameObject backgroundShaderObject = gameobject_InstanceContainer.transform.GetChild(0).gameObject;
				// GameObject backgroundPlaneObject = gameobject_InstanceContainer.transform.GetChild(1).gameObject;
				// GameObject textObject = gameobject_InstanceContainer.transform.GetChild(2).gameObject;
	
				TextMeshPro textMeshObject = textObject.GetComponent<TextMeshPro>();
	
				textMeshObject.text = string_TwoTranslation[i].ToString();
	
				// widthText_One   = textMeshObject.preferredWidth;
				widthText_One   = (string_TwoTranslation[i].ToString() == " ")? 1.0f: textMeshObject.preferredWidth ;
				heightText_One  = textMeshObject.preferredHeight;
	
	
				backgroundShaderObject.transform.localScale =  GetScaleBackGroundShader(widthText_One * 1.5f, backgroundShaderObject.transform.localScale); 	
				backgroundPlaneObject.transform.localScale  =        GetScaleBackGround(widthText_One * 1.5f, backgroundPlaneObject.transform.localScale);
	
				// vector3_PositionStartBackground_0 //= new Vector3(backgroundPlaneObject.transform.position.x, backgroundPlaneObject.transform.position.y, backgroundPlaneObject.transform.position.z);
	
	
				backgroundShaderObject.transform.position -= GetPositionBackGround(valuesOne_1.preferredWidth);
				backgroundPlaneObject.transform.position  -= GetPositionBackGround(valuesOne_1.preferredWidth);
				
				backgroundShaderObject.transform.position += GetPositionBackGround(widthText_One);
				backgroundPlaneObject.transform.position  += GetPositionBackGround(widthText_One) +  new Vector3(0.0f, 0.0f, -0.1f);
				textObject.transform.position += new Vector3(0.0f, 0.0f, -0.11f);
		
				backgroundPlaneObject.GetComponent<CollisionScriptCode>().enabled = true;
	
				// backgroundPlaneObject.GetComponent<Rigidbody>().useGravity = false;
	
				// Debug.Log(gameobject_InstanceContainer.transform.GetChild(1).gameObject);
	
	
				list_GameObjectInstanciated_1.Add(gameobject_InstanceContainer);
				list_IntPositionMark_1.Add(0);
	
			}
	




    		float_evaluationTime = float_CurrentTime;

			CommunicationCorrectAnswerClass.bool_ActiveResetWords = true;


    	}
        
    }

}
