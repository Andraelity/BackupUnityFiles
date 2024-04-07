using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;
using UnityEngine.Rendering;


public class ShaderBackgroundOnOff : MonoBehaviour
{

    Renderer plane_Renderer;

    Material material;

    void Start()
    {

        if(PlayerPrefs.GetString("BackgroundShader" + "Switch") == "false")
        {
        
                material = (Material)Resources.Load("BlackMaterial", typeof(Material));

                plane_Renderer = GetComponent<Renderer>();

                plane_Renderer.material = material;

        }

    }

}
