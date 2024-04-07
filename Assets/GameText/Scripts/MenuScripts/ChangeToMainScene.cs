using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using StyleModeNamespace;

using SaveProgressNamespace;



public class ChangeToMainScene : MonoBehaviour
{


    void Start()
    {
        
    }

    int int_StyleMode = 0;
    int int_CurrentScene = 0;


	public void MenuToMainSceneChallenge()
    {

        int_StyleMode = 0;
	
        StyleModeClass.int_StyleMode = 0;    

    	SceneManager.LoadScene(sceneBuildIndex:1);
    
    }


	public void MenuToMainSceneFreeMode()
    {

        int_StyleMode = 1;        

        StyleModeClass.int_StyleMode = 1;    

    	SceneManager.LoadScene(sceneBuildIndex:1);
        
    }

    public void MainToMenuGeneral()
    {

        SaveProgressClass.bool_SaveProgresActiveMessage = true;

    	SceneManager.LoadScene(sceneBuildIndex:0);

    }
    

    public void LeftArrowChangeScene()
    {
        
        SaveProgressClass.bool_SaveProgresActiveMessage = true;
        
        int_CurrentScene = StyleModeClass.int_CurrentSceneGeneral - 1;

        StyleModeClass.int_CurrentSceneGeneral = int_CurrentScene;
        
        if(StyleModeClass.int_StyleMode == 1)
        {

            if(int_CurrentScene < 1)
            {
                int_CurrentScene = 1;
                StyleModeClass.int_CurrentSceneGeneral = int_CurrentScene;
        
            }

            SceneManager.LoadScene(sceneBuildIndex:int_CurrentScene);

        }

    }


    public void RightArrowChangeScene()
    {

        SaveProgressClass.bool_SaveProgresActiveMessage = true;        

        int_CurrentScene = StyleModeClass.int_CurrentSceneGeneral + 1;
        StyleModeClass.int_CurrentSceneGeneral = int_CurrentScene;

        if(StyleModeClass.int_StyleMode == 1)
        {

            if(int_CurrentScene > 11)
            {
                int_CurrentScene = 11;
                StyleModeClass.int_CurrentSceneGeneral = int_CurrentScene;

            }

        	SceneManager.LoadScene(sceneBuildIndex:int_CurrentScene);

        }
    
    }


    // public void MainToMenuGeneral()
    // {

    // 	SceneManager.LoadScene(sceneBuildIndex:11);
        
    // }




    // Update is called once per frame
    void Update()
    {
        
    }
}
