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

using CommunicationCorrectAnswerNamespace;
using CommunicationCamaraShakeNamespace;


using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_WritingList : MonoBehaviour
{

	public GameObject TextOne_0;
	public GameObject TextOne_1;
	public GameObject TextOne_2;
	public GameObject TextOne_3; 
	public GameObject TextOne_4; 
	public GameObject TextOne_5; 
	public GameObject TextOne_6; 
	public GameObject TextOne_7; 

	public GameObject BoxOne_0;
	public GameObject BoxOne_1;
	public GameObject BoxOne_2;
	public GameObject BoxOne_3; 
	public GameObject BoxOne_4; 
	public GameObject BoxOne_5; 
	public GameObject BoxOne_6; 
	public GameObject BoxOne_7; 

	public GameObject PlaneBackgroundOne_0;
	public GameObject PlaneBackgroundOne_1;
	public GameObject PlaneBackgroundOne_2;
	public GameObject PlaneBackgroundOne_3; 
	public GameObject PlaneBackgroundOne_4; 
	public GameObject PlaneBackgroundOne_5; 
	public GameObject PlaneBackgroundOne_6; 
	public GameObject PlaneBackgroundOne_7; 




	List<GameObject> listOfBoxGameObject_One;

	List<Transform> listOfTransform_One;
	List<Vector3> listOfPosition_One;
	List<Quaternion> listOfRotation_One;
	List<Collider> listOfCollider_One;
	List<TextMeshPro> listOfTextMeshPro_One;
	List<GameObject> listOfPlaneGameObject_One;

 	List<string> list_OfStringEnglish;
 	List<string> list_OfStringFrench;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";

	Vector3 vector3_DefaultLocalScalePlane;

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


 		listOfBoxGameObject_One = new List<GameObject>();

 		listOfBoxGameObject_One.Add(BoxOne_0);
 		listOfBoxGameObject_One.Add(BoxOne_1);
 		listOfBoxGameObject_One.Add(BoxOne_2);
 		listOfBoxGameObject_One.Add(BoxOne_3);
 		listOfBoxGameObject_One.Add(BoxOne_4);
 		listOfBoxGameObject_One.Add(BoxOne_5);
 		listOfBoxGameObject_One.Add(BoxOne_6);
 		listOfBoxGameObject_One.Add(BoxOne_7);


		LoadStringList();

		listOfTransform_One = new List<Transform>();

		Transform transformOne_0 = BoxOne_0.GetComponent<Transform>();
		Transform transformOne_1 = BoxOne_1.GetComponent<Transform>();
		Transform transformOne_2 = BoxOne_2.GetComponent<Transform>();
		Transform transformOne_3 = BoxOne_3.GetComponent<Transform>();
		Transform transformOne_4 = BoxOne_4.GetComponent<Transform>();
		Transform transformOne_5 = BoxOne_5.GetComponent<Transform>();
		Transform transformOne_6 = BoxOne_6.GetComponent<Transform>();
		Transform transformOne_7 = BoxOne_7.GetComponent<Transform>();		

		listOfTransform_One.Add(transformOne_0);
		listOfTransform_One.Add(transformOne_1);
		listOfTransform_One.Add(transformOne_2);
		listOfTransform_One.Add(transformOne_3);
		listOfTransform_One.Add(transformOne_4);
		listOfTransform_One.Add(transformOne_5);
		listOfTransform_One.Add(transformOne_6);
		listOfTransform_One.Add(transformOne_7);


		
		listOfPosition_One = new List<Vector3>();

		Vector3 positionOne_0 = BoxOne_0.GetComponent<Transform>().position;
		Vector3 positionOne_1 = BoxOne_1.GetComponent<Transform>().position;
		Vector3 positionOne_2 = BoxOne_2.GetComponent<Transform>().position;
		Vector3 positionOne_3 = BoxOne_3.GetComponent<Transform>().position;
		Vector3 positionOne_4 = BoxOne_4.GetComponent<Transform>().position;
		Vector3 positionOne_5 = BoxOne_5.GetComponent<Transform>().position;
		Vector3 positionOne_6 = BoxOne_6.GetComponent<Transform>().position;
		Vector3 positionOne_7 = BoxOne_7.GetComponent<Transform>().position;		

		listOfPosition_One.Add(positionOne_0);
		listOfPosition_One.Add(positionOne_1);
		listOfPosition_One.Add(positionOne_2);
		listOfPosition_One.Add(positionOne_3);
		listOfPosition_One.Add(positionOne_4);
		listOfPosition_One.Add(positionOne_5);
		listOfPosition_One.Add(positionOne_6);
		listOfPosition_One.Add(positionOne_7);



		listOfRotation_One = new List<Quaternion>();

		Quaternion rotationOne_0 = BoxOne_0.GetComponent<Transform>().rotation;
		Quaternion rotationOne_1 = BoxOne_1.GetComponent<Transform>().rotation;
		Quaternion rotationOne_2 = BoxOne_2.GetComponent<Transform>().rotation;
		Quaternion rotationOne_3 = BoxOne_3.GetComponent<Transform>().rotation;
		Quaternion rotationOne_4 = BoxOne_4.GetComponent<Transform>().rotation;
		Quaternion rotationOne_5 = BoxOne_5.GetComponent<Transform>().rotation;
		Quaternion rotationOne_6 = BoxOne_6.GetComponent<Transform>().rotation;
		Quaternion rotationOne_7 = BoxOne_7.GetComponent<Transform>().rotation;

		listOfRotation_One.Add(rotationOne_0);
		listOfRotation_One.Add(rotationOne_1);
		listOfRotation_One.Add(rotationOne_2);
		listOfRotation_One.Add(rotationOne_3);
		listOfRotation_One.Add(rotationOne_4);
		listOfRotation_One.Add(rotationOne_5);
		listOfRotation_One.Add(rotationOne_6);
		listOfRotation_One.Add(rotationOne_7);



		listOfCollider_One = new List<Collider>();

	    Collider colliderOne_0 = BoxOne_0.GetComponent<Collider>();
	    Collider colliderOne_1 = BoxOne_1.GetComponent<Collider>();
	    Collider colliderOne_2 = BoxOne_2.GetComponent<Collider>();
	    Collider colliderOne_3 = BoxOne_3.GetComponent<Collider>();
	    Collider colliderOne_4 = BoxOne_4.GetComponent<Collider>();
	    Collider colliderOne_5 = BoxOne_5.GetComponent<Collider>();
	    Collider colliderOne_6 = BoxOne_6.GetComponent<Collider>();
	    Collider colliderOne_7 = BoxOne_7.GetComponent<Collider>();

	    listOfCollider_One.Add(colliderOne_0);
	    listOfCollider_One.Add(colliderOne_1);
	    listOfCollider_One.Add(colliderOne_2);
	    listOfCollider_One.Add(colliderOne_3);
	    listOfCollider_One.Add(colliderOne_4);
	    listOfCollider_One.Add(colliderOne_5);
	    listOfCollider_One.Add(colliderOne_6);
	    listOfCollider_One.Add(colliderOne_7);



    	listOfTextMeshPro_One = new List<TextMeshPro>();

		TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_2 = TextOne_2.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_3 = TextOne_3.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_4 = TextOne_4.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_5 = TextOne_5.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_6 = TextOne_6.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_7 = TextOne_7.GetComponent<TextMeshPro>();


		listOfPlaneGameObject_One = new List<GameObject>();

		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_0);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_1);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_2);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_3);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_4);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_5);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_6);
		listOfPlaneGameObject_One.Add(PlaneBackgroundOne_7);



		var src = DateTime.Now;
		var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);

		int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);


		System.Random randomGeneratorNumber = new System.Random(hashRandom);
		int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
		
		string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringFrench[int_randomListPosition];  

		valuesOne_0.text = string_OneTranslation;
		valuesOne_1.text = string_OneTranslation;
		valuesOne_2.text = string_OneTranslation;
		valuesOne_3.text = string_OneTranslation;
		valuesOne_4.text = string_OneTranslation;
		valuesOne_5.text = string_OneTranslation;
		valuesOne_6.text = string_OneTranslation;
		valuesOne_7.text = string_OneTranslation;

		valuesOne_0.font = font_asset;
		valuesOne_1.font = font_asset;
		valuesOne_2.font = font_asset;
		valuesOne_3.font = font_asset;
		valuesOne_4.font = font_asset;
		valuesOne_5.font = font_asset;
		valuesOne_6.font = font_asset;
		valuesOne_7.font = font_asset;

		valuesOne_0.fontSize = 15;
		valuesOne_1.fontSize = 15;
		valuesOne_2.fontSize = 15;
		valuesOne_3.fontSize = 15;
		valuesOne_4.fontSize = 15;
		valuesOne_5.fontSize = 15;
		valuesOne_6.fontSize = 15;
		valuesOne_7.fontSize = 15;

		listOfTextMeshPro_One.Add(valuesOne_0);
		listOfTextMeshPro_One.Add(valuesOne_1);
		listOfTextMeshPro_One.Add(valuesOne_2);
		listOfTextMeshPro_One.Add(valuesOne_3);
		listOfTextMeshPro_One.Add(valuesOne_4);
		listOfTextMeshPro_One.Add(valuesOne_5);
		listOfTextMeshPro_One.Add(valuesOne_6);
		listOfTextMeshPro_One.Add(valuesOne_7);



		vector3_DefaultLocalScalePlane = PlaneBackgroundOne_0.transform.localScale;


		float widthText_One   = valuesOne_0.preferredWidth;

		PlaneBackgroundOne_0.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_0.transform.localScale);
		PlaneBackgroundOne_1.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_1.transform.localScale);
		PlaneBackgroundOne_2.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_2.transform.localScale);
		PlaneBackgroundOne_3.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_3.transform.localScale);
		PlaneBackgroundOne_4.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_4.transform.localScale);
		PlaneBackgroundOne_5.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_5.transform.localScale);
		PlaneBackgroundOne_6.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_6.transform.localScale);
		PlaneBackgroundOne_7.transform.localScale = GetScaleBackGround(widthText_One, PlaneBackgroundOne_7.transform.localScale);
		

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



    Vector3 GetScaleBackGround(float width, Vector3 Current)
    {
		float scale = (width * 0.06f)/1.15f ;
    	Vector3 valueOut = new Vector3(scale, Current.y, Current.z);
  
    	return valueOut;
    }




    void ColorCurrentText(int currentString, int numberOfCharacters)
    { 


		TMP_Text m_TextComponent = listOfTextMeshPro_One[currentString].GetComponent<TMP_Text>();

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



    int int_CurrentOne = 7;

    bool bool_CurrentOne = true;

    bool bool_CheckString = false;

    int counter = 0;

    string string_FromInputField = "";

    int counterColoredCharacters = 0;

    string string_EvaluationColor= "";

    bool setLastActive = false;


    bool setLastActive_motion = false;

    float float_evaluationTime = 0.0f;
	
	float counterPosition = 0.0f;

	bool setActiveRigidLastTwo = false;


    bool tab_press = false;

    int counter_tab = 0;

    void Update()
    {

		SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;


		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;
			

	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F6))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:6);
				StyleModeClass.int_CurrentSceneGeneral = 6;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_4 = WordsCounterCommunicationClass.int_CurrentNumberWords;
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


    	if(Input.GetKeyDown(KeyCode.Tab))
    	{

    		tab_press = true;
    		counter_tab ++;
    	}

    	if(counter_tab%2 == 0 && tab_press == true)
    	{
    		tab_press = false;

    		for(int i = 0; i < listOfTextMeshPro_One.Count; i++)
    		{
    			listOfTextMeshPro_One[i].text = string_OneTranslation;
				float widthText_One = listOfTextMeshPro_One[i].preferredWidth;

				listOfPlaneGameObject_One[i].transform.localScale = vector3_DefaultLocalScalePlane;

				listOfPlaneGameObject_One[i].transform.localScale = GetScaleBackGround(widthText_One, listOfPlaneGameObject_One[i].transform.localScale);

    		}
    		if(int_CurrentOne < 7)
    		{
    			listOfTextMeshPro_One[int_CurrentOne + 1].text = "";
    		}


    	}

    	if(counter_tab%2 == 1 && tab_press == true)
    	{
    		tab_press = false;

    		for(int i = 0; i < listOfTextMeshPro_One.Count; i++)
    		{
    			listOfTextMeshPro_One[i].text = string_TwoTranslation;
				float widthText_One = listOfTextMeshPro_One[i].preferredWidth;

				listOfPlaneGameObject_One[i].transform.localScale = vector3_DefaultLocalScalePlane;

				listOfPlaneGameObject_One[i].transform.localScale = GetScaleBackGround(widthText_One, listOfPlaneGameObject_One[i].transform.localScale);

    		}
    		if(int_CurrentOne < 7)
    		{
				listOfTextMeshPro_One[int_CurrentOne + 1].text = "";
    		}

    	}




        if(setActiveRigidLastTwo)
        {
        	for(int i = 0; i < 2; i++)
        	{

				listOfBoxGameObject_One[i].AddComponent<Rigidbody>();

        	}
        	setActiveRigidLastTwo = false;

        }



        float_CurrentTime = Time.realtimeSinceStartup;

        if(setLastActive_motion  && float_CurrentTime > float_evaluationTime + 1.0f)
        {
        	setLastActive_motion = false;

			string string_UpdateText = "";

			if(counter_tab %2 == 0)
			{
				string_UpdateText = string_OneTranslation;
			}
			else
			{
				string_UpdateText = string_TwoTranslation;
			}



   			listOfTextMeshPro_One[0].text = string_UpdateText;
   			listOfTextMeshPro_One[1].text = string_UpdateText;

			float widthText_One = listOfTextMeshPro_One[0].preferredWidth;
			listOfPlaneGameObject_One[0].transform.localScale = vector3_DefaultLocalScalePlane;
			listOfPlaneGameObject_One[0].transform.localScale = GetScaleBackGround(widthText_One, listOfPlaneGameObject_One[0].transform.localScale);
			
			widthText_One = listOfTextMeshPro_One[1].preferredWidth;
			listOfPlaneGameObject_One[1].transform.localScale = vector3_DefaultLocalScalePlane;
			listOfPlaneGameObject_One[1].transform.localScale = GetScaleBackGround(widthText_One, listOfPlaneGameObject_One[1].transform.localScale);



			Destroy(listOfBoxGameObject_One[0].GetComponent<Rigidbody>());
			Destroy(listOfBoxGameObject_One[1].GetComponent<Rigidbody>());
							

			listOfTransform_One[1].position = listOfPosition_One[1] + new Vector3(0.0f,  counterPosition, 0.0f);
			listOfTransform_One[1].rotation = listOfRotation_One[1];

			counterPosition += 5.0f;
   			listOfCollider_One[1].enabled = true;


			listOfTransform_One[0].position = listOfPosition_One[0] + new Vector3(0.0f,  counterPosition, 0.0f);
			listOfTransform_One[0].rotation = listOfRotation_One[0];

			counterPosition += 5.0f;
   			listOfCollider_One[0].enabled = true;



			setActiveRigidLastTwo = true;

        }

        // if(Input.GetKeyDown(KeyCode.F5))
        // {
        // 	setActiveRigidLastTwo = true;
        // }




        if(setLastActive)
        {

        	for(int i = 2; i < listOfBoxGameObject_One.Count; i++)
        	{

				listOfBoxGameObject_One[i].AddComponent<Rigidbody>();
        	}
			setLastActive = false;

        }


    	string_EvaluationColor = LinkCommunicationColoredClass.string_InputField;

    	// print("DebugLogColor");
    	// print("DebugLogColor");
    	// Debug.Log(string_EvaluationColor);
    	// print("DebugLogColor");
    	// print("DebugLogColor");

    	counterColoredCharacters = 0;



    	for(int i = 0; i < string_EvaluationColor.Length; i++)
    	{
			
    		string string_EvaluationEqualityColor = "";

			if(counter_tab %2 == 0)
			{
				string_EvaluationEqualityColor = string_OneTranslation;
			}
			else
			{
				string_EvaluationEqualityColor = string_TwoTranslation;
			}

    		if(string_EvaluationColor.Length > string_EvaluationEqualityColor.Length)
    		{
    			break;
    		}

    		char color = string_EvaluationColor[i]; 
    		char currentString = string_EvaluationEqualityColor[i];

    		// if(string_EvaluationColor[i] == string_OneTranslation[i])
    		if(color == currentString)
    		{
    			counterColoredCharacters++;
    		}
    		else
    		{
    			break;
    		}

    	}

    	ColorCurrentText(int_CurrentOne, counterColoredCharacters);
		




    	if(bool_CheckString == true)
    	{

    		bool_CheckString = false;
			bool bool_ActiveCamaraShake = true;

    		if(bool_CurrentOne == true)
    		{
    

				string string_EvaluationEquality = "";


				if(counter_tab %2 == 0)
				{
					string_EvaluationEquality = string_OneTranslation;
				}
				else
				{
					string_EvaluationEquality = string_TwoTranslation;
				}
	

    			if(string_FromInputField == string_EvaluationEquality)
    			{
	
					listOfCollider_One[int_CurrentOne].enabled = false;

    				listOfTextMeshPro_One[int_CurrentOne].text = "";

            		LinkCommunicationColoredClass.string_InputField = "";

	    		
	    			bool_CurrentOne = true;
	
	    			int_CurrentOne --;

					WordsCounterCommunicationClass.int_CurrentNumberWords++;

					// setLastActive = true;
					// float_evaluationTime = float_CurrentTime;
	
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

    		Debug.Log("String In WordHandler = " + LinkCommunicationColoredClass.string_InputField);

    		string_FromInputField = LinkCommunicationColoredClass.string_InputField;

    		bool_CheckString = true;

    	}


    	if(int_CurrentOne == -1)
    	{

	        float_CurrentTime = Time.realtimeSinceStartup;

			System.Random randomGeneratorNumber = new System.Random((int) float_CurrentTime);
			int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);


			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];

			string string_UpdateText = "";

			if(counter_tab %2 == 0)
			{
				string_UpdateText = string_OneTranslation;
			}
			else
			{
				string_UpdateText = string_TwoTranslation;
			}


    		for(int i = 2; i < listOfTextMeshPro_One.Count; i++)
    		{

    			listOfTextMeshPro_One[i].text = string_UpdateText;
				float widthText_One = listOfTextMeshPro_One[i].preferredWidth;

				listOfPlaneGameObject_One[i].transform.localScale = vector3_DefaultLocalScalePlane;

				listOfPlaneGameObject_One[i].transform.localScale = GetScaleBackGround(widthText_One, listOfPlaneGameObject_One[i].transform.localScale);

    		}

			for(int i = 2; i < listOfBoxGameObject_One.Count; i++)
			{

				Destroy(listOfBoxGameObject_One[i].GetComponent<Rigidbody>());
			
			}
			
			counterPosition = 0.0f;

    		for(int i = listOfTransform_One.Count - 1; i > 1; i--)
    		{
				

				listOfTransform_One[i].position = listOfPosition_One[i] + new Vector3(0.0f, 8.0f + counterPosition, 0.0f);
				listOfTransform_One[i].rotation = listOfRotation_One[i];

				counterPosition += 5.0f;
    			listOfCollider_One[i].enabled = true;

    		}


    		float_evaluationTime = float_CurrentTime;

    		setLastActive_motion = true;
			setLastActive = true;

    		int_CurrentOne = 7;
		
		    bool_CurrentOne = true;

    	}
        
    }

}
