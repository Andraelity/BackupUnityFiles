using UnityEngine;

// using CommunicationMotionNamespace;
// using CommunicationCollisionNamespace;

using CommunicationCorrectAnswerNamespace;


public class TranslationBlocksSplineWalker : MonoBehaviour {


	int valueMod = 5;

	public TranslationBlocksBezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

	private float progress;
	private bool goingForward = true;


	bool activeMotion = false;

	bool bool_CheckActiveVisible = false;	
	bool bool_SetActiveVisible = false;

	bool bool_CheckActiveCurrentObject = true;

	float currentTime = 0.0f;
	float float_TimeWhenActive = 0.0f;


	private void Update () 
	{

		// if(Input.GetKeyDown(KeyCode.Tab))
		// {
		// 	CommunicationCorrectAnswerClass.bool_ActiveResetWords = true;

		// }	

		currentTime = Time.realtimeSinceStartup;


		if(CommunicationCorrectAnswerClass.bool_ActiveResetWords && bool_CheckActiveCurrentObject )
		// if(Input.GetKeyDown(KeyCode.Tab))
		{
			bool_CheckActiveCurrentObject = false;

			// CommunicationCorrectAnswerClass.bool_ActiveResetWords = false;
			goingForward = true;
			progress = 0;
			activeMotion = true;
			
			bool_CheckActiveVisible = true;
			bool_SetActiveVisible = true;
			float_TimeWhenActive = currentTime;	

		}

		

		if(activeMotion)
		{


			if (goingForward) 
			{
				// Time.realtimeSinceStartup%5;
				progress += Time.deltaTime / duration;
				// progress += (Time.realtimeSinceStartup%0.01f )/ duration;
				if (progress > 1f) 
				{
					if (mode == SplineWalkerMode.Once) 
					{
						// progress = Time.realtimeSinceStartup%5;
						progress = 1;
					}
					else if (mode == SplineWalkerMode.Loop) 
					{
						progress -= 1f;
					}
					else 
					{
						progress = 2f - progress;
						goingForward = false;
					}
				}
			}
			else 
			{

				progress -= Time.deltaTime / duration;
				if (progress < 0f) 
				{
					progress = -progress;
					goingForward = true;
				}
			}
	
		}


		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;


		
		if(progress == 1f && currentTime > float_TimeWhenActive + 3.0f)
		{
			bool_CheckActiveVisible = true;
			bool_SetActiveVisible = false;
			activeMotion = false;
		
			CommunicationCorrectAnswerClass.bool_ActiveResetWords = false;
			bool_CheckActiveCurrentObject = true;
		}


		if(bool_CheckActiveVisible)
		{

			bool_CheckActiveVisible = false;

			gameObject.transform.GetChild(0).gameObject.SetActive(bool_SetActiveVisible);
			gameObject.transform.GetChild(1).gameObject.SetActive(bool_SetActiveVisible);
			
			// CommunicationCollisionClass.bool_CheckActiveVisible = false;			

		}


	}


}