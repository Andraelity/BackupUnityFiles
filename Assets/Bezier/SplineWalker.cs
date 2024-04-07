using UnityEngine;

using CommunicationMotionNamespace;
using CommunicationCollisionNamespace;



public class SplineWalker : MonoBehaviour {

	int valueMod = 5;

	public BezierSpline spline;

	public float duration;

	public bool lookForward;

	public SplineWalkerMode mode;

	private float progress;
	private bool goingForward = true;


	bool activeMotion = false;

	private bool bool_splineMotion_0 = false;
	private bool bool_splineMotion_1 = false;
	private bool bool_splineMotion_2 = false;
	private bool bool_splineMotion_3 = false;
	private bool bool_splineMotion_4 = false;
	private bool bool_splineMotion_5 = false;


	private bool bool_CheckActiveVisible = false;
	private bool bool_SetActiveVisible = true;

	private bool bool_SetTrailTime = false;
	private	bool bool_IsTrailActive = true;
	private float float_UpdateTime = 0.0f;
	private TrailRenderer component_TrailRenderer;

	private void Start()
	{

		component_TrailRenderer = GetComponent<TrailRenderer>();

	}

	private void Update () 
	{


		// Debug.Log(CommunicationCollisionClass.bool_CheckActiveVisible);
		bool_CheckActiveVisible = CommunicationCollisionClass.bool_CheckActiveVisible;
		if(bool_CheckActiveVisible)
		{
		
			bool_SetActiveVisible = CommunicationCollisionClass.bool_SetActiveVisibleFalse;

		}



		bool_splineMotion_0 = CommunicationMotionClass.spline_0;
		bool_splineMotion_1 = CommunicationMotionClass.spline_1;
		bool_splineMotion_2 = CommunicationMotionClass.spline_2;
		bool_splineMotion_3 = CommunicationMotionClass.spline_3;
		bool_splineMotion_4 = CommunicationMotionClass.spline_4;
		bool_splineMotion_5 = CommunicationMotionClass.spline_5;

		if(bool_splineMotion_0 || bool_splineMotion_1 || bool_splineMotion_2 || bool_splineMotion_3 || bool_splineMotion_4 || bool_splineMotion_5)
		{

			if(bool_splineMotion_0)
			{

				gameObject.SetActive(true);

				bool_splineMotion_0 = false;

				CommunicationMotionClass.spline_0 = false;
				
				// CommunicationCollisionClass.bool_PositionCollider = true;
				
				


				spline.SetPointList(0);
				goingForward = true;
				progress = 0;
				activeMotion = true;
				
				bool_CheckActiveVisible = true;
				bool_SetActiveVisible = true;

			}

			if(bool_splineMotion_1)
			{

				gameObject.SetActive(true);

				bool_splineMotion_1 = false;

				CommunicationMotionClass.spline_1 = false;
				
				// CommunicationCollisionClass.bool_PositionCollider = true;

				

				spline.SetPointList(1);
				goingForward = true;
				progress = 0;
				activeMotion = true;

				bool_CheckActiveVisible = true;
				bool_SetActiveVisible = true;

			}

			if(bool_splineMotion_2)
			{

				gameObject.SetActive(true);

				bool_splineMotion_2 = false;

				CommunicationMotionClass.spline_2 = false;
				
				// CommunicationCollisionClass.bool_PositionCollider = true;

				

				spline.SetPointList(2);
				goingForward = true;
				progress = 0;
				activeMotion = true;

				bool_CheckActiveVisible = true;
				bool_SetActiveVisible = true;

			}

			if(bool_splineMotion_3)
			{

				gameObject.SetActive(true);

				bool_splineMotion_3 = false;

				CommunicationMotionClass.spline_3 = false;
				
				// CommunicationCollisionClass.bool_PositionCollider = true;

				

				spline.SetPointList(3);
				goingForward = true;
				progress = 0;
				activeMotion = true;

				bool_CheckActiveVisible = true;
				bool_SetActiveVisible = true;

			}
			
			if(bool_splineMotion_4)
			{

				gameObject.SetActive(true);

				bool_splineMotion_4 = false;

				CommunicationMotionClass.spline_4 = false;
				
				// CommunicationCollisionClass.bool_PositionCollider = true;

				

				spline.SetPointList(4);
				goingForward = true;
				progress = 0;
				activeMotion = true;

				bool_CheckActiveVisible = true;
				bool_SetActiveVisible = true;

			}
			
			if(bool_splineMotion_5)
			{

				gameObject.SetActive(true);

				bool_splineMotion_5 = false;

				CommunicationMotionClass.spline_5 = false;
				
				// CommunicationCollisionClass.bool_PositionCollider = true;

				

				spline.SetPointList(5);
				goingForward = true;
				progress = 0;
				activeMotion = true;

				bool_CheckActiveVisible = true;
				bool_SetActiveVisible = true;

			}

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


		// if(Input.GetKeyDown(KeyCode.Tab))
		// {


		// 	gameObject.transform.GetChild(0).gameObject.SetActive(false);
		// 	gameObject.transform.GetChild(1).gameObject.SetActive(false);
		// 	// gameObject.SetActive(false);

		// }
		// if(Input.GetKeyDown(KeyCode.F8))
		// {

		// 	// gameObject.SetActive(true);
		// 	gameObject.transform.GetChild(0).gameObject.SetActive(true);
		// 	gameObject.transform.GetChild(1).gameObject.SetActive(true);


		// }

		float currentTime = Time.realtimeSinceStartup;

		// Debug.Log(" bool_SetActiveVisible " + Time.realtimeSinceStartup);
		// Debug.Log(bool_SetActiveVisible);
		// Debug.Log(bool_CheckActiveVisible);
		
		if(bool_CheckActiveVisible && bool_IsTrailActive)
		{
			bool_IsTrailActive = false;
			bool_SetTrailTime = true;
			float_UpdateTime = currentTime;
		}

		if(bool_SetTrailTime && currentTime > float_UpdateTime + 1.0 )
		{
			component_TrailRenderer.enabled = false;
			bool_SetTrailTime = false;

		}


		if(bool_CheckActiveVisible)
		{

			bool_CheckActiveVisible = false;

			gameObject.transform.GetChild(0).gameObject.SetActive(bool_SetActiveVisible);
			gameObject.transform.GetChild(1).gameObject.SetActive(bool_SetActiveVisible);
			if(bool_SetActiveVisible == true)
			{
				bool_IsTrailActive = true;
				component_TrailRenderer.enabled = bool_SetActiveVisible;

			}
			
			CommunicationCollisionClass.bool_CheckActiveVisible = false;			

		}


	}

}