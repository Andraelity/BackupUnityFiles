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

using LinkCommunicationLanguagesFilesNamespace;

using StyleModeNamespace;

using WordsCounterCommunicationNamespace;

using SaveProgressNamespace;

using MenuSettingsFontsNamespace;


public class WordHandler_Blocks : MonoBehaviour
{

	public GameObject TextOne_0;
	public GameObject TextOne_1;
	public GameObject TextOne_2;
	public GameObject TextOne_3; 
	public GameObject TextOne_4; 
	public GameObject TextOne_5; 

	public GameObject BoxOne_0;
	public GameObject BoxOne_1;
	public GameObject BoxOne_2;
	public GameObject BoxOne_3; 
	public GameObject BoxOne_4; 
	public GameObject BoxOne_5; 

	public GameObject backgroundShader_0;
	public GameObject backgroundShader_1;
	public GameObject backgroundShader_2;
	public GameObject backgroundShader_3;
	public GameObject backgroundShader_4;
	public GameObject backgroundShader_5;

	List<GameObject> list_OfGameObjectBackgroundShader;
	List<Transform> listOfTransformBackgroundShader;

	List<GameObject> listOfBoxGameObject_One;

	List<Transform> listOfTransform_One;
	List<Vector3> listOfPosition_One;
	List<Quaternion> listOfRotation_One;
	List<Collider> listOfCollider_One;
	List<TextMeshPro> list_OfTextMeshPro;

 	List<string> list_OfStringEnglish;
 	List<string> list_OfStringFrench;

 	float float_CurrentTime;

    string string_OneTranslation = "Goal";
    string string_TwoTranslation = "Goalition";

	List<int> list_PositionCheck;

    int int_PositionSelected = 0;
    int int_PositionList = 0;

    // Vector3 updatedPositionOne = new Vector3(0.0f, 0.0f, 0.0f);

	List<string> list_OfWordsAfterSuffle; 
	List<int> list_OfIntOrderPair;

    // Vector3 coordinateBase = new Vector3(-7f, -1.75f, 0.0f);
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

		
		// transformFocus = BoxFocus.GetComponent<Transform>();
		// scaleFocus = BoxFocus.GetComponent<Transform>().localScale;
		// positionFocus = BoxFocus.GetComponent<Transform>().position;
		// rotationFocus = BoxFocus.GetComponent<Transform>().rotation;
		// colliderFocus = BoxFocus.GetComponent<Collider>();
		// textMeshProFocus = TextFocus.GetComponent<TextMeshPro>();

		list_OfGameObjectBackgroundShader = new List<GameObject>();

		list_OfGameObjectBackgroundShader.Add(backgroundShader_0);
		list_OfGameObjectBackgroundShader.Add(backgroundShader_1);
		list_OfGameObjectBackgroundShader.Add(backgroundShader_2);
		list_OfGameObjectBackgroundShader.Add(backgroundShader_3);
		list_OfGameObjectBackgroundShader.Add(backgroundShader_4);
		list_OfGameObjectBackgroundShader.Add(backgroundShader_5);



		listOfTransformBackgroundShader = new List<Transform>();

		Transform transforBackgroundShader_0 = backgroundShader_0.GetComponent<Transform>();
		Transform transforBackgroundShader_1 = backgroundShader_1.GetComponent<Transform>();
		Transform transforBackgroundShader_2 = backgroundShader_2.GetComponent<Transform>();
		Transform transforBackgroundShader_3 = backgroundShader_3.GetComponent<Transform>();
		Transform transforBackgroundShader_4 = backgroundShader_4.GetComponent<Transform>();
		Transform transforBackgroundShader_5 = backgroundShader_5.GetComponent<Transform>();

		listOfTransformBackgroundShader.Add(transforBackgroundShader_0);
		listOfTransformBackgroundShader.Add(transforBackgroundShader_1);
		listOfTransformBackgroundShader.Add(transforBackgroundShader_2);
		listOfTransformBackgroundShader.Add(transforBackgroundShader_3);
		listOfTransformBackgroundShader.Add(transforBackgroundShader_4);
		listOfTransformBackgroundShader.Add(transforBackgroundShader_5);





 		listOfBoxGameObject_One = new List<GameObject>();

 		listOfBoxGameObject_One.Add(BoxOne_0);
 		listOfBoxGameObject_One.Add(BoxOne_1);
 		listOfBoxGameObject_One.Add(BoxOne_2);
 		listOfBoxGameObject_One.Add(BoxOne_3);
 		listOfBoxGameObject_One.Add(BoxOne_4);
 		listOfBoxGameObject_One.Add(BoxOne_5);




		listOfTransform_One = new List<Transform>();

		Transform transformOne_0 = BoxOne_0.GetComponent<Transform>();
		Transform transformOne_1 = BoxOne_1.GetComponent<Transform>();
		Transform transformOne_2 = BoxOne_2.GetComponent<Transform>();
		Transform transformOne_3 = BoxOne_3.GetComponent<Transform>();
		Transform transformOne_4 = BoxOne_4.GetComponent<Transform>();
		Transform transformOne_5 = BoxOne_5.GetComponent<Transform>();

		listOfTransform_One.Add(transformOne_0);
		listOfTransform_One.Add(transformOne_1);
		listOfTransform_One.Add(transformOne_2);
		listOfTransform_One.Add(transformOne_3);
		listOfTransform_One.Add(transformOne_4);
		listOfTransform_One.Add(transformOne_5);



		
		listOfPosition_One = new List<Vector3>();

		Vector3 positionOne_0 = BoxOne_0.GetComponent<Transform>().position;
		Vector3 positionOne_1 = BoxOne_1.GetComponent<Transform>().position;
		Vector3 positionOne_2 = BoxOne_2.GetComponent<Transform>().position;
		Vector3 positionOne_3 = BoxOne_3.GetComponent<Transform>().position;
		Vector3 positionOne_4 = BoxOne_4.GetComponent<Transform>().position;
		Vector3 positionOne_5 = BoxOne_5.GetComponent<Transform>().position;

		listOfPosition_One.Add(positionOne_0);
		listOfPosition_One.Add(positionOne_1);
		listOfPosition_One.Add(positionOne_2);
		listOfPosition_One.Add(positionOne_3);
		listOfPosition_One.Add(positionOne_4);
		listOfPosition_One.Add(positionOne_5);




		listOfRotation_One = new List<Quaternion>();

		Quaternion rotationOne_0 = BoxOne_0.GetComponent<Transform>().rotation;
		Quaternion rotationOne_1 = BoxOne_1.GetComponent<Transform>().rotation;
		Quaternion rotationOne_2 = BoxOne_2.GetComponent<Transform>().rotation;
		Quaternion rotationOne_3 = BoxOne_3.GetComponent<Transform>().rotation;
		Quaternion rotationOne_4 = BoxOne_4.GetComponent<Transform>().rotation;
		Quaternion rotationOne_5 = BoxOne_5.GetComponent<Transform>().rotation;

		listOfRotation_One.Add(rotationOne_0);
		listOfRotation_One.Add(rotationOne_1);
		listOfRotation_One.Add(rotationOne_2);
		listOfRotation_One.Add(rotationOne_3);
		listOfRotation_One.Add(rotationOne_4);
		listOfRotation_One.Add(rotationOne_5);




		listOfCollider_One = new List<Collider>();

	    Collider colliderOne_0 = BoxOne_0.GetComponent<Collider>();
	    Collider colliderOne_1 = BoxOne_1.GetComponent<Collider>();
	    Collider colliderOne_2 = BoxOne_2.GetComponent<Collider>();
	    Collider colliderOne_3 = BoxOne_3.GetComponent<Collider>();
	    Collider colliderOne_4 = BoxOne_4.GetComponent<Collider>();
	    Collider colliderOne_5 = BoxOne_5.GetComponent<Collider>();

	    listOfCollider_One.Add(colliderOne_0);
	    listOfCollider_One.Add(colliderOne_1);
	    listOfCollider_One.Add(colliderOne_2);
	    listOfCollider_One.Add(colliderOne_3);
	    listOfCollider_One.Add(colliderOne_4);
	    listOfCollider_One.Add(colliderOne_5);




    	list_OfTextMeshPro = new List<TextMeshPro>();

		TextMeshPro valuesOne_0 = TextOne_0.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_1 = TextOne_1.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_2 = TextOne_2.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_3 = TextOne_3.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_4 = TextOne_4.GetComponent<TextMeshPro>();
		TextMeshPro valuesOne_5 = TextOne_5.GetComponent<TextMeshPro>();


		valuesOne_0.font = font_asset;
		valuesOne_1.font = font_asset;
		valuesOne_2.font = font_asset;
		valuesOne_3.font = font_asset;
		valuesOne_4.font = font_asset;
		valuesOne_5.font = font_asset;

		valuesOne_0.fontSize = 20;
		valuesOne_1.fontSize = 20;
		valuesOne_2.fontSize = 20;
		valuesOne_3.fontSize = 20;
		valuesOne_4.fontSize = 20;
		valuesOne_5.fontSize = 20;


		var src = DateTime.Now;
		var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);

		int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

		
		// string_TwoTranslation = list_OfStringFrench[int_randomListPosition];  



		List<string> list_OfWordsCurrent = new List<string>();
		list_PositionCheck = new List<int>();

		System.Random randomGeneratorNumber = new System.Random(hashRandom);
		int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);

		// list_PositionCheck.Add(int_randomListPosition);
		// string_OneTranslation = "OPERACIONESWOWMORE";//list_OfStringEnglish[int_randomListPosition];
		
		string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
		list_OfWordsCurrent.Add(string_OneTranslation);
		list_OfWordsCurrent.Add(string_TwoTranslation);


		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
		list_PositionCheck.Add(int_randomListPosition);
		
		string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
		list_OfWordsCurrent.Add(string_OneTranslation);
		list_OfWordsCurrent.Add(string_TwoTranslation);


		int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
		list_PositionCheck.Add(int_randomListPosition);

		string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
		string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
		list_OfWordsCurrent.Add(string_OneTranslation);
		list_OfWordsCurrent.Add(string_TwoTranslation);


		list_OfIntOrderPair = new List<int>();
		
		list_OfIntOrderPair.Add(0);
		list_OfIntOrderPair.Add(0);
		list_OfIntOrderPair.Add(1);
		list_OfIntOrderPair.Add(1);
		list_OfIntOrderPair.Add(2);
		list_OfIntOrderPair.Add(2);

		list_OfIntOrderPair = GenerateRandomLoop(list_OfIntOrderPair);
		

		list_OfWordsAfterSuffle = new List<string>();

		list_OfWordsAfterSuffle.Add("");
		list_OfWordsAfterSuffle.Add("");
		list_OfWordsAfterSuffle.Add("");
		list_OfWordsAfterSuffle.Add("");
		list_OfWordsAfterSuffle.Add("");
		list_OfWordsAfterSuffle.Add("");

		int int_counterPair = 0;
		int int_CounterDuality = 0;
		int int_CounterFind = 0;


		Debug.Log("CurrentWords");
		Debug.Log("CurrentWords");
		Debug.Log("CurrentWords");

		foreach(string valueWord in list_OfWordsCurrent)
		{
			Debug.Log(valueWord);
		}

		Debug.Log("CurrentWords");
		Debug.Log("CurrentWords");
		Debug.Log("CurrentWords");
		
		Debug.Log("CurrentIntOrder");
		Debug.Log("CurrentIntOrder");
		Debug.Log("CurrentIntOrder");

		foreach(int valueInt in list_OfIntOrderPair)
		{
			Debug.Log(valueInt);
		}
		Debug.Log("CurrentIntOrder");
		Debug.Log("CurrentIntOrder");
		Debug.Log("CurrentIntOrder");
		



		for(int i = 0; i < 3; i++)
		{
			
			for(int j = 0; j < list_OfIntOrderPair.Count; j++)
			{
				if(list_OfIntOrderPair[j] == i  && int_CounterFind == 0)
				{
					list_OfWordsAfterSuffle[j] = list_OfWordsCurrent[i * 2];  
					int_CounterFind ++;
					continue;
				}

				if(list_OfIntOrderPair[j] == i  && int_CounterFind == 1)
				{
					
					list_OfWordsAfterSuffle[j] = list_OfWordsCurrent[i * 2  + 1];  

					int_CounterFind = 0;
					break;
				}

			}

		}


		foreach(string valueWord in list_OfWordsAfterSuffle)
		{

			Debug.Log(valueWord);
		
		}


		list_OfTextMeshPro.Add(valuesOne_0);
		list_OfTextMeshPro.Add(valuesOne_1);
		list_OfTextMeshPro.Add(valuesOne_2);
		list_OfTextMeshPro.Add(valuesOne_3);
		list_OfTextMeshPro.Add(valuesOne_4);
		list_OfTextMeshPro.Add(valuesOne_5);
		
		for (int i = 0; i < list_OfWordsAfterSuffle.Count; i++)
		{
			list_OfTextMeshPro[i].text = list_OfWordsAfterSuffle[i];
		}


		for(int i = 0; i < list_OfTextMeshPro.Count; i++)
		{

			float widthText_One   = list_OfTextMeshPro[i].preferredWidth;
			float heightText_One  = list_OfTextMeshPro[i].preferredHeight;
	
	
			listOfTransform_One[i].localScale = GetScaleBackGround(widthText_One, listOfTransform_One[i].localScale);
			listOfTransformBackgroundShader[i].localScale = GetScaleBackGroundShader(widthText_One, listOfTransformBackgroundShader[i].localScale); 	
		}


		foreach(GameObject valueObject in list_OfGameObjectBackgroundShader)
		{
			valueObject.SetActive(false);
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


		float position = (width * 0.212f)/1.15f ; 
    	Vector3 valueOut = new Vector3(position, 0.0f, 0.0f);
  
    	return valueOut;
    	// return position;
    }




    void ColorCurrentTextList(int currentList, int numberOfCharacters)
    { 



		TMP_Text m_TextComponent = list_OfTextMeshPro[currentList].GetComponent<TMP_Text>();

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


    string GetCurrentEqualityWord(int currentFindPosition)
    {
    	int currentNumber = list_OfIntOrderPair[currentFindPosition];

    	string valuePairWord = "";

    	for(int i = 0; i < list_OfIntOrderPair.Count;i++)
    	{
    		if(i == currentFindPosition)
    		{
    			continue;
    		}
    		if(list_OfIntOrderPair[i] == currentNumber)
    		{
    			valuePairWord = list_OfWordsAfterSuffle[i];
    			break;
    		}
    	}

    	return valuePairWord;
    }


    int GetCurrentPositionEqualityWord(int currentFindPosition)
    {
    	int currentNumber = list_OfIntOrderPair[currentFindPosition];

    	int valuePairWord = 0;

    	for(int i = 0; i < list_OfIntOrderPair.Count;i++)
    	{
    		if(i == currentFindPosition)
    		{
    			continue;
    		}
    		if(list_OfIntOrderPair[i] == currentNumber)
    		{
    			valuePairWord = i;
    			break;
    		}
    	}

    	return valuePairWord;
    }



    int int_CounterCorrectPairWords = 0;

    int int_CounterTranslation = 1;

    int int_CounterPositionListWords = 0;

    bool bool_ToCheckWord = true;
	bool bool_ListWords = false;

    bool bool_CheckString = false;

    bool bool_ResetListOfWords = false;


    string string_FromInputField = "";

    int counterColoredCharacters = 0;

    string string_EvaluationColor = "";


    bool bool_ActiveCamaraShake = false;
    
    float float_evaluationTime = 0.0f;
	



    bool tab_press = false;

    int counter_tab = 0;



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

		SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;


		if(StyleModeClass.int_StyleMode == 1)
		{

			WordsCounterCommunicationClass.int_TargetNumberWords = -1;
			

	    	if(Input.GetKeyDown(KeyCode.F1))
	    	{
				
				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:1);
				StyleModeClass.int_CurrentSceneGeneral = 1;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F2))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:2);
				StyleModeClass.int_CurrentSceneGeneral = 2;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F3))
	    	{
				
				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:3);
				StyleModeClass.int_CurrentSceneGeneral = 3;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F4))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:4);
				StyleModeClass.int_CurrentSceneGeneral = 4;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F5))
	    	{
				
				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:5);
				StyleModeClass.int_CurrentSceneGeneral = 5;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F7))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:7);
				StyleModeClass.int_CurrentSceneGeneral = 7;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F8))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:8);
				StyleModeClass.int_CurrentSceneGeneral = 8;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F9))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:9);
				StyleModeClass.int_CurrentSceneGeneral = 9;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F10))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
				SaveProgressClass.bool_SaveProgresActiveMessage = true;			
				SceneManager.LoadScene (sceneBuildIndex:10);
				StyleModeClass.int_CurrentSceneGeneral = 10;

	    	}

	    	if(Input.GetKeyDown(KeyCode.F11))
	    	{

				SaveProgressClass.int_SaveProgres_GameMode_6 = WordsCounterCommunicationClass.int_CurrentNumberWords;
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


    	string_EvaluationColor = LinkCommunicationColoredClass.string_InputField;

    	counterColoredCharacters = 0;



    	if(int_CounterTranslation == 1)
  		{


  			for(int iUp = 0; iUp < list_OfTextMeshPro.Count; iUp++)
  			{

  				counterColoredCharacters = 0;

		    	string string_EvaluationEqualityColor = list_OfTextMeshPro[iUp].text;

				

		    	for(int i = 0; i < string_EvaluationColor.Length; i++)
		    	{

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
		    			list_IntColored[iUp] = counterColoredCharacters;
		    		}
		    		else
		    		{
		    			break;
		    		}
		
		    	}
  	
  			}


  			List<int> valuesCharacters = new List<int>();
  			int MaxIntColored = GetMaxColored(list_IntColored);

  			counterColoredCharacters = 0;

  			for(int i = 0; i < list_IntColored.Count; i++)
  			{
  				if(MaxIntColored > list_IntColored[i])
  				{
  					counterColoredCharacters = 0;
    				ColorCurrentTextList(i, counterColoredCharacters);
  				}
  				else
  				{

  					counterColoredCharacters = list_IntColored[i];
  					ColorCurrentTextList(i, counterColoredCharacters);	
  				}

  			}

 		}  	

		
		list_IntColored = new List<int>{0, 0, 0, 0, 0, 0};



		if(bool_ListWords)
		{

			ColorCurrentTextList(int_CounterPositionListWords, int_FirstCorrectWordLength);
		
		}


    	if(bool_CheckString == true)
    	{

    		bool_CheckString = false;

    		bool_ActiveCamaraShake = true;


    		if(bool_ToCheckWord == true)
    		{


				for(int i = 0; i < list_OfWordsAfterSuffle.Count; i++)
				{

					string string_EvaluationEquality = list_OfWordsAfterSuffle[i];


	    			if(string_FromInputField == string_EvaluationEquality && list_OfTextMeshPro[i].text != "")
	    			{


						CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;
						
						int_FirstCorrectWordLength = string_EvaluationEquality.Length;


						list_OfGameObjectBackgroundShader[i].SetActive(true);
	    				int_CounterPositionListWords = i;
		    			bool_ToCheckWord = false;
						
						bool_ListWords = true;
						
	    				bool_ActiveCamaraShake = false;
						
						WordsCounterCommunicationClass.int_CurrentNumberWords++;
						

						break;
	
	    			}
	
				}

    		}


			if(bool_ListWords == true)
			{


				string string_EvaluationEquality = GetCurrentEqualityWord(int_CounterPositionListWords);

    			if(string_FromInputField == string_EvaluationEquality)
    			{
	
    				// CommunicationMotionClass.SetSplineTrue(int_PositionList); 

					CommunicationCorrectAnswerClass.bool_ActiveCorrectAnswerShader = true;
    				

    				list_OfTextMeshPro[GetCurrentPositionEqualityWord(int_CounterPositionListWords)].text = "";
					list_OfTextMeshPro[int_CounterPositionListWords].text = "";

					list_OfGameObjectBackgroundShader[GetCurrentPositionEqualityWord(int_CounterPositionListWords)].SetActive(true);
	    			
	    			int_CounterPositionListWords = 0;
	    			int_FirstCorrectWordLength = 0;

	    			bool_ToCheckWord = true;

	    			bool_ListWords = false;

	    			int_CounterCorrectPairWords++;
	    			// bool_ToCheckWord = true;
					
					// bool_ListWords = false;
	    			// int_CounterTranslation ++;

    				bool_ActiveCamaraShake = false;

					WordsCounterCommunicationClass.int_CurrentNumberWords++;

    			}


			}

            LinkCommunicationColoredClass.string_InputField = "";

            CommunicationCamaraShakeClass.bool_ActiveCamaraShake = bool_ActiveCamaraShake;
            CommunicationCamaraShakeClass.bool_ActiveCamaraShakeShader = bool_ActiveCamaraShake;

    	}

    	if(int_CounterCorrectPairWords == 3)
    	{

			bool_ResetListOfWords = true;
			int_CounterCorrectPairWords = 0;
    	}



    	if(LinkCommunicationColoredClass.bool_ActiveStatus == true)
    	{	

    		LinkCommunicationColoredClass.bool_ActiveStatus = false;

    		Debug.Log("String In WordHandler = " + LinkCommunicationColoredClass.string_InputField);

    		string_FromInputField = LinkCommunicationColoredClass.string_InputField;

    		bool_CheckString = true;

    	}

		// if(Input.GetKeyDown(KeyCode.Tab))
		// {
		// 	CommunicationCorrectAnswerClass.bool_ActiveResetWords = true;

		// }	


		// if(CommunicationCollisionClass.bool_ResetListOfWords)
		if(bool_ResetListOfWords)
    	{
			// CommunicationCollisionClass.bool_ResetListOfWords = false;    		

    		bool_ResetListOfWords = false;
	        float_CurrentTime = Time.realtimeSinceStartup;



			var src = DateTime.Now;
			var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);

			int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

			
			// string_TwoTranslation = list_OfStringFrench[int_randomListPosition];  


			List<string> list_OfWordsCurrent = new List<string>();

			System.Random randomGeneratorNumber = new System.Random(hashRandom);
			int int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);

			// list_PositionCheck.Add(int_randomListPosition);
			// string_OneTranslation = "OPERACIONESWOWMORE";//list_OfStringEnglish[int_randomListPosition];
			
			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
			list_OfWordsCurrent.Add(string_OneTranslation);
			list_OfWordsCurrent.Add(string_TwoTranslation);


			int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);
			
			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
			list_OfWordsCurrent.Add(string_OneTranslation);
			list_OfWordsCurrent.Add(string_TwoTranslation);


			int_randomListPosition = randomGeneratorNumber.Next(0, list_OfStringEnglish.Count);

			string_OneTranslation = list_OfStringEnglish[int_randomListPosition];
			string_TwoTranslation = list_OfStringFrench[int_randomListPosition];
			list_OfWordsCurrent.Add(string_OneTranslation);
			list_OfWordsCurrent.Add(string_TwoTranslation);


			list_OfIntOrderPair = new List<int>();
			
			list_OfIntOrderPair.Add(0);
			list_OfIntOrderPair.Add(0);
			list_OfIntOrderPair.Add(1);
			list_OfIntOrderPair.Add(1);
			list_OfIntOrderPair.Add(2);
			list_OfIntOrderPair.Add(2);

			list_OfIntOrderPair = GenerateRandomLoop(list_OfIntOrderPair);
			

			list_OfWordsAfterSuffle = new List<string>();

			list_OfWordsAfterSuffle.Add("");
			list_OfWordsAfterSuffle.Add("");
			list_OfWordsAfterSuffle.Add("");
			list_OfWordsAfterSuffle.Add("");
			list_OfWordsAfterSuffle.Add("");
			list_OfWordsAfterSuffle.Add("");

			int int_counterPair = 0;
			int int_CounterDuality = 0;
			int int_CounterFind = 0;


			Debug.Log("CurrentWords");
			Debug.Log("CurrentWords");
			Debug.Log("CurrentWords");

			foreach(string valueWord in list_OfWordsCurrent)
			{
				Debug.Log(valueWord);
			}

			Debug.Log("CurrentWords");
			Debug.Log("CurrentWords");
			Debug.Log("CurrentWords");
			
			Debug.Log("CurrentIntOrder");
			Debug.Log("CurrentIntOrder");
			Debug.Log("CurrentIntOrder");

			foreach(int valueInt in list_OfIntOrderPair)
			{
				Debug.Log(valueInt);
			}
			Debug.Log("CurrentIntOrder");
			Debug.Log("CurrentIntOrder");
			Debug.Log("CurrentIntOrder");
			



			for(int i = 0; i < 3; i++)
			{
				
				for(int j = 0; j < list_OfIntOrderPair.Count; j++)
				{
					if(list_OfIntOrderPair[j] == i  && int_CounterFind == 0)
					{
						list_OfWordsAfterSuffle[j] = list_OfWordsCurrent[i * 2];  
						int_CounterFind ++;
						continue;
					}

					if(list_OfIntOrderPair[j] == i  && int_CounterFind == 1)
					{
						
						list_OfWordsAfterSuffle[j] = list_OfWordsCurrent[i * 2  + 1];  

						int_CounterFind = 0;
						break;
					}

				}

			}


			foreach(string valueWord in list_OfWordsAfterSuffle)
			{

				Debug.Log(valueWord);
			
			}

			// int_PositionList = randomGeneratorNumber.Next(0, list_PositionCheck.Count);


			// int_PositionSelected = list_PositionCheck[int_PositionList];


			// Debug.Log("Int positionSelected");
			// Debug.Log(int_PositionSelected);
			// Debug.Log("Int positionSelected");



			// string_OneTranslation = list_OfStringEnglish[int_PositionSelected];		
			// string_TwoTranslation = list_OfStringFrench[int_PositionSelected];		

			// textMeshProFocus.text = string_TwoTranslation;

			
			for (int i = 0; i < list_OfWordsAfterSuffle.Count; i++)
			{
				list_OfTextMeshPro[i].text = list_OfWordsAfterSuffle[i];
			}


			for(int i = 0; i < list_OfTextMeshPro.Count; i++)
			{

				float widthText_One   = list_OfTextMeshPro[i].preferredWidth;
				float heightText_One  = list_OfTextMeshPro[i].preferredHeight;
		
		
				listOfTransform_One[i].localScale = GetScaleBackGround(widthText_One, listOfTransform_One[i].localScale);
				listOfTransformBackgroundShader[i].localScale = GetScaleBackGroundShader(widthText_One, listOfTransformBackgroundShader[i].localScale); 	

		
			}


			foreach(GameObject valueObject in list_OfGameObjectBackgroundShader)
			{
				valueObject.SetActive(false);
			}


			list_IntColored = new List<int>{0, 0, 0, 0, 0, 0};


    		float_evaluationTime = float_CurrentTime;

			CommunicationCorrectAnswerClass.bool_ActiveResetWords = true;

    		// int_CounterTranslation = 1;
		
		 	// bool_ToCheckWord = true;

	
			// float widthText_One = textMeshProFocus.preferredWidth;
			// float heightText_One  = textMeshProFocus.preferredHeight;
	
			// // Transform transform_BackOne = BackGroundOne_0.transform;
			// // TextFocus.transform.position = transformFocus.position;

			// transformFocus.position = basePositionBox;

			// transformFocus.localScale = GetScaleBackGround(widthText_One, transformFocus.localScale);
	
	
			// updatedPositionOne = GetPositionBackGround(widthText_One);
	
			// transformFocus.position +=  updatedPositionOne;
	
	
			// BoxFocus.transform.position = transformFocus.position;
	


    	}
        
    }

}
