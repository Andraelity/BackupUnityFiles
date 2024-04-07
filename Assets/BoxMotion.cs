using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Mathf.Cos(Time.realtimeSinceStartup), 0.0f, 0.0f);
    }
}


//Vector3(1.77970898,1.00152469,1.00152469)