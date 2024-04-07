using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommunciationImplementationBezierRandomPairNamespace;

namespace CommunciationImplementationBezierRandomPairNamespace
{
	
	public static class CommunciationImplementationBezierRandomPairClass
	{

		public static bool bool_ActiveResetWords  = false;

	}

}


public class BezierImplementationRandomPair : MonoBehaviour
{

	[SerializeField]
	private List<Vector2> list_coordinateControl;

	bool bool_CheckActiveVisible = false;	
	bool bool_SetActiveVisible = false;

	bool bool_SetActiveMotion = false;
	int int_CounterStatus = 0;

	bool bool_CheckActiveCurrentObject = true;

	float currentTime = 0.0f;
	float float_TimeWhenActive = 0.0f;

	bool bool_SetReset = false;

	List<Vector2> list_IterationPoints = new List<Vector2>();



	private void Start()
	{



	}


	private void Update () 
	{

		currentTime = Time.realtimeSinceStartup;


		if(CommunciationImplementationBezierRandomPairClass.bool_ActiveResetWords && bool_CheckActiveCurrentObject )
		{
			
			System.Random randomGenerator = new System.Random();

			float float_axisX = (float)((randomGenerator.NextDouble() * 17.5) - 8.9);
			float float_axisY = (float)((randomGenerator.NextDouble() * 8.0) - 2.0);

			Vector2 vector2_ToInsert = new Vector2(float_axisX, float_axisY);

			list_coordinateControl.Insert(1, vector2_ToInsert);


			bool_CheckActiveCurrentObject = false;

			list_IterationPoints = BezierCurveImplementation.PointList2(list_coordinateControl);

			bool_CheckActiveVisible = true;
			bool_SetActiveVisible = true;
			float_TimeWhenActive = currentTime;	
			bool_SetActiveMotion = true;

		}


		if(bool_SetActiveMotion)
		{

			transform.position = new Vector3(list_IterationPoints[int_CounterStatus].x, list_IterationPoints[int_CounterStatus].y, 10.1f);
			int_CounterStatus ++;

			if(int_CounterStatus == 99)
			{
				bool_SetActiveMotion = false;
				bool_SetReset = true;
			}
		}

		
		if(bool_SetReset &&  currentTime > float_TimeWhenActive + 3.0f)
		{
			bool_CheckActiveVisible = true;
			bool_SetActiveVisible = false;
			// CommunciationImplementationResetClass.bool_ActiveResetWords = false;
		 
			CommunciationImplementationBezierRandomPairClass.bool_ActiveResetWords = false;
			bool_CheckActiveCurrentObject = true;
			bool_SetActiveMotion = false;
			int_CounterStatus = 0;
 
		}


		if(bool_CheckActiveVisible)
		{

			bool_CheckActiveVisible = false;

			gameObject.transform.GetChild(0).gameObject.SetActive(bool_SetActiveVisible);
			gameObject.transform.GetChild(1).gameObject.SetActive(bool_SetActiveVisible);
			

		}


	}


}
