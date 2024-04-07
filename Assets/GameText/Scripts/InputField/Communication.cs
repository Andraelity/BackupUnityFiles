using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommunicationCollisionNamespace
{

	static public class CommunicationCollisionClass
	{
	    
		static public bool bool_PositionCollider = false;

		static public bool bool_CheckActiveVisible = false;
		static public bool bool_SetActiveVisibleFalse = false;
		static public bool bool_ResetListOfWords = false;



	}

}


namespace CommunicationMotionNamespace
{
	static public class CommunicationMotionClass
	{

		static public bool spline_0 = false;
		static public bool spline_1 = false;
		static public bool spline_2 = false;
		static public bool spline_3 = false;
		static public bool spline_4 = false;
		static public bool spline_5 = false;

		static public void SetSplineTrue(int numberSpline)
		{
			switch (numberSpline) 
			{

			  case 0:
			    spline_0 = true;
			    Debug.Log(spline_0 + "	>>> ValueSpline");
			    break;

			  case 1:
			    spline_1 = true;
			    Debug.Log(spline_1 + "	>>> ValueSpline");
			    break;	
			  
			  case 2:
			    spline_2 = true;
			    Debug.Log(spline_2 + "	>>> ValueSpline");
			    break;
			  
			  case 3:
			    spline_3 = true;
			    Debug.Log(spline_3 + "	>>> ValueSpline");
			    break;
			  
			  case 4:
			    spline_4 = true;
			    Debug.Log(spline_4 + "	>>> ValueSpline");
			    break;
			  
			  case 5:
			    spline_5 = true;
			    Debug.Log(spline_5 + "	>>> ValueSpline");
			    break;

			}			

		}

	}
}


namespace CommunicationCamaraShakeNamespace
{

	static public class CommunicationCamaraShakeClass
	{

		static public bool bool_ActiveCamaraShake = false; 
		static public bool bool_ActiveCamaraShakeShader = false; 

	}

}




namespace CommunicationCorrectAnswerNamespace
{

	static public class CommunicationCorrectAnswerClass
	{

		static public bool bool_ActiveCorrectAnswerShader = false; 
		static public bool bool_ActiveResetWords = false;
	}

}

