using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CommunciationImplementationResetNamespace;

namespace CommunciationImplementationResetNamespace
{
	
	public static class CommunciationImplementationResetClass
	{

		public static bool bool_ActiveResetWords  = false;

	}

}


public class BezierImplementationReset : MonoBehaviour
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


		if(CommunciationImplementationResetClass.bool_ActiveResetWords && bool_CheckActiveCurrentObject )
		{
	
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
		 
			CommunciationImplementationResetClass.bool_ActiveResetWords = false;
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
