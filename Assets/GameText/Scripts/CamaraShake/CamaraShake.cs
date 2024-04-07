using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

using CommunicationCamaraShakeNamespace;

public class CamaraShake : MonoBehaviour
{
    // Start is called before the first frame update
	[SerializeField]
    private Vector3 posInf = new Vector3(0.25f, 0.25f, 0.25f);
    [SerializeField]
    private Vector3 rotInf = new Vector3(1, 1, 1);
    [SerializeField]
    private float magn = 1;
    [SerializeField]
    private float rough = 1;
    [SerializeField]
    private float fadeIn = 0.1f;
    [SerializeField]
    private float fadeOut = 2;


    // CameraShakeInstance shake;

    // Update is called once per frame
    void Update()
    {

    	if(CommunicationCamaraShakeClass.bool_ActiveCamaraShake)
    	{
         	CommunicationCamaraShakeClass.bool_ActiveCamaraShake = false;   
            CameraShakeInstance c = CameraShaker.Instance.ShakeOnce(magn, rough, fadeIn, fadeOut);
            c.PositionInfluence = posInf;
            c.RotationInfluence = rotInf;

    	}
        
    }
}
