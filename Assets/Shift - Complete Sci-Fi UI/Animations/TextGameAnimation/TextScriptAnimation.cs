using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommunicationTextScriptAnimationNamespace;


namespace CommunicationTextScriptAnimationNamespace
{

    public static class CommunicationTextScriptAnimationClass
    {

        public static bool bool_ActivateAnimationFadeIn = false; 
        public static bool bool_ActivateAnimationFadeOut = false; 

    } 

}



public class TextScriptAnimation : MonoBehaviour
{
    // Start is called before the first frame update


    Animation animation_Holder;
    
    void Start()
    {

        animation_Holder = gameObject.GetComponent<Animation>();

    }

    // Update is called once per frame
    void Update()
    {



        if(CommunicationTextScriptAnimationClass.bool_ActivateAnimationFadeOut)
        {
            
            CommunicationTextScriptAnimationClass.bool_ActivateAnimationFadeOut = false;
            animation_Holder.Play("TextFadeOut");    

        }


        if(CommunicationTextScriptAnimationClass.bool_ActivateAnimationFadeIn)
        {
            
            CommunicationTextScriptAnimationClass.bool_ActivateAnimationFadeIn = false;            
            animation_Holder.Play("TextFadeIn");    

        }


    }
}
