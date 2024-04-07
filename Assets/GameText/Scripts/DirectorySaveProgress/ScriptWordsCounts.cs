using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

using WordsCounterCommunicationNamespace;


namespace WordsCounterCommunicationNamespace
{
    public static class WordsCounterCommunicationClass
    {
    
        public static int int_CurrentNumberWords = 0;
        public static int int_TargetNumberWords = 0;

    }
}


public class ScriptWordsCounts : MonoBehaviour
{
    
    [SerializeField]
    private GameObject gameobject_CurrentNumberWords; 

    [SerializeField]
    private GameObject gameobject_TargetNumberWords;


    private TMP_Text tmp_CurrentNumberWords;  
    private TMP_Text tmp_TargetNumberWords;  

    // // Start is called before the first frame update
    void Start()
    {
    
        tmp_CurrentNumberWords = gameobject_CurrentNumberWords.GetComponent<TMP_Text>();
        tmp_TargetNumberWords = gameobject_TargetNumberWords.GetComponent<TMP_Text>();
        
        // Debug.Log(gameobject_CurrentNumberWords.GetComponent<TMP_Text>().text);
    }

    // Update is called once per frame
    void Update()
    {
        
        tmp_CurrentNumberWords.text = WordsCounterCommunicationClass.int_CurrentNumberWords.ToString();

        // Debug.Log(gameobject_CurrentNumberWords.GetComponent<TextMeshPro>().text);


        if( WordsCounterCommunicationClass.int_TargetNumberWords == -1)
        {
            tmp_TargetNumberWords.text = "-";     
        }
        else
        {
            tmp_TargetNumberWords.text = WordsCounterCommunicationClass.int_TargetNumberWords.ToString();

        }
    }

}
