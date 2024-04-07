using UnityEngine;


// using CommunicationMotionNamespace;
// using CommunicationCollisionNamespace;


public class TranslationSelectionSplineWalker : MonoBehaviour {

	int valueMod = 5;

	public TranslationSelectionBezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

	private float progress;
	private bool goingForward = true;


	bool activeMotion = false;

	bool bool_CheckActiveVisible = false;	
	bool bool_SetActiveVisible = false;

	private void Update () 
	{



		if(Input.GetKeyDown(KeyCode.Tab))
		{
			goingForward = true;
			progress = 0;
			activeMotion = true;
			
			bool_CheckActiveVisible = true;
			bool_SetActiveVisible = true;
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

		
		// Debug.Log(progress);
		


		Vector3 position = spline.GetPoint(progress);
		transform.localPosition = position;


		
		if(progress == 1f)
		{
			bool_CheckActiveVisible = true;
			bool_SetActiveVisible = false;
			activeMotion = false;
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