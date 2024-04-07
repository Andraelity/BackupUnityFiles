using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

using CommunicationBezierCharToWordsNamespace;

public class UpdatePositionBezier : MonoBehaviour
{

	List<Vector2> list_IterationPoints;
	List<Vector2> list_ControlPoints;

	Vector3 vector3_InitialPosition;

	string string_NameObject;

	Vector3 vector3_FinalPositionOne = new Vector3(3.55f, 3.92f, 10f);

	Vector3 vector3_FinalPositionTwo = new Vector3(3.55f, -0.45f, 10f);


    void Start()
    {

        list_ControlPoints = new List<Vector2>();
        list_ControlPoints.Add(new Vector2(0.0f, 0.0f));
        list_ControlPoints.Add(new Vector2(0.0f, 0.0f));
        list_ControlPoints.Add(new Vector2(0.0f, 0.0f));
    	string_NameObject = gameObject.name;

    }

    // Update is called once per frame
    float currentTime = 0f;
    float updatedTime = 0f;
    int int_CounterPosition = 0;


    bool bool_ActiveMotion = false;
    int int_PositionOneOrTwo = 0;


    void Update()
    {

    	currentTime = Time.realtimeSinceStartup;

    	if(CommunicationBezierCharToWords.string_NameObjectMessage == string_NameObject)
    	{

            Debug.Log("string name of object ==  " + string_NameObject);
	    	if(CommunicationBezierCharToWords.bool_ActiveMotionMessage)
	    	{
	           
                CommunicationBezierCharToWords.string_NameObjectMessage = "";      

	    		CommunicationBezierCharToWords.bool_ActiveMotionMessage = false;
	
	    		vector3_InitialPosition = gameObject.transform.position;
	    		int_CounterPosition = 0;
	    		bool_ActiveMotion = true;
    			
    			int_PositionOneOrTwo = CommunicationBezierCharToWords.int_OneOrTwoMessage;

    			CommunicationBezierCharToWords.int_OneOrTwoMessage = 0;

                Debug.Log("INT POSITION ONEorTWO === " + int_PositionOneOrTwo.ToString());


                var src = DateTime.Now;
                var hm = new DateTime(src.Year, src.Month, src.Day, src.Hour, src.Minute, src.Second);
                int hashRandom = (hm.Hour + hm.Year + hm.Month + hm.Day + hm.Minute + hm.Second);

                System.Random randomGenerator = new System.Random(hashRandom);

	    		if(int_PositionOneOrTwo == 1)
	    		{

                    Debug.Log("Reach this point one one one one ");
                    // x = 9.2;
                    // y = 8.04;

                    Vector2 vector2_SetControlPoint = new Vector2(((float)randomGenerator.NextDouble()) * 9.2f - 8.15f, ((float)randomGenerator.NextDouble()) * 8.04f - 2.54f);

    				list_ControlPoints[0] = gameObject.transform.position;
                    list_ControlPoints[1] = vector2_SetControlPoint * 2.50f;
    				list_ControlPoints[2] = vector3_FinalPositionOne;
    				list_IterationPoints = BezierCurveImplementation.PointList2(list_ControlPoints);

	    		}
				else
				{

                    Debug.Log("Reach this point two two two two ");

                    Vector2 vector2_SetControlPoint = new Vector2(((float)randomGenerator.NextDouble()) * 9.2f - 8.15f, ((float)randomGenerator.NextDouble()) * 8.04f - 2.54f);

    				list_ControlPoints[0] = gameObject.transform.position;
                    list_ControlPoints[1] = vector2_SetControlPoint * 2.5f;
                    list_ControlPoints[2] = vector3_FinalPositionTwo;
    				list_IterationPoints = BezierCurveImplementation.PointList2(list_ControlPoints);

				}

                foreach(Vector2 controlElement in list_ControlPoints)
                {
                    Debug.Log(controlElement);
                }

	    	}

    	}



    	if(bool_ActiveMotion == true)
    	{
    		updatedTime = currentTime;

    		gameObject.transform.position = new Vector3(list_IterationPoints[int_CounterPosition].x, list_IterationPoints[int_CounterPosition].y, 10f);
    		
    		int_CounterPosition ++;
    		
    		if(int_CounterPosition == 100)
    		{
    			bool_ActiveMotion = false;
    		}

    	}

       
    }
}
