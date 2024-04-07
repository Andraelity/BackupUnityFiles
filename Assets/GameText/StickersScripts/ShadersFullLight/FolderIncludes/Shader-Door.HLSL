#define PI 3.1415926535897931
#define TIME  _Time.y

sampler2D _TextureSprite;
sampler2D _TextureChannel0;
sampler2D _TextureChannel1;
sampler2D _TextureChannel2;
sampler2D _TextureChannel3;
           
float _OverlaySelection;
float _StickerType;
float _MotionState;
float4 _BorderColor;
float _BorderSizeOne;
float _BorderSizeTwo;
float _BorderBlurriness;
float _RangeSOne_One0; 
float _RangeSOne_One1; 
float _RangeSOne_One2; 
float _RangeSOne_One3; 
float _RangeSTen_Ten0;
float _RangeSTen_Ten1;
float _RangeSTen_Ten2;
float _RangeSTen_Ten3;
float _InVariableTick;
float _InVariableRatioX;
float _InVariableRatioY;
float4 _OutlineColor;
float _OutlineSprite;
float4 _ColorGlowHDR;
float _AlphaColor;
float _GlowFull;

#include "FolderIncludes/SDfs.hlsl"
#include "FolderIncludes/Stickers.hlsl"
#include "FolderIncludes/Sprites.hlsl"
#include "UnityPBSLighting.cginc"
#include "UnityMetaPass.cginc"

struct vertexPoints
{
	float4 vertex : POSITION;
	float2 uv : TEXCOORD0;
	float2 uv2 : TEXCOORD1;
	
};

struct pixelPoints
{
	float4 vertex : SV_POSITION;
	float2 uv : TEXCOORD0;
	float2 uv2 : TEXCOORD1;
};

pixelPoints VERTEXSHADER (vertexPoints VERTEXSPACE)
{
	pixelPoints PIXELSPACE;
	PIXELSPACE.vertex = UnityObjectToClipPos(VERTEXSPACE.vertex);
	PIXELSPACE.uv = VERTEXSPACE.uv;
	PIXELSPACE.uv2 = VERTEXSPACE.uv2;
	return PIXELSPACE;
}

#define Number _FloatNumber
#define NumberOne _FloatVariable


/////////////////////////////////////////////////////////////////////////////////////////////
// Default 
/////////////////////////////////////////////////////////////////////////////////////////////

//Comment this to have an static camera and see the whole scene from a 3rd person view 
#define SUBJETIVE_CAMERA

//Comment this to remove the ilumination of the spark representing the subjective camera
#define SPARK_ILUMINATION

//////////////////////////////////////////
//Common MACROS

#define EPSILON     0.002
#define TIME_SCALE  1.0
#define TIME_OFFSET -0.5

//////////////////////////////////////////
//Fade-In Transition Times
#define FADE_IN_START 0.5
#define FADE_IN_END   1.5

//////////////////////////////////////////
//Ray marching MACROS

#define NUM_RAYMARCH_STEP 150
#define STEP_REDUCTION    0.8
#define NUM_REFLECTIONS   3

//////////////////////////////////////////
//Portal MACROS

#define NUM_PORTAL_RECURSIONS    4
#define PORTAL_EPSILON           0.02
#define PORTAL_RADIUS            1.5
#define PORTAL_SHAPE_INV_SCALE_X 1.7
#define PORTAL_OPEN_SPEED        5.0
#define PORTAL_CLOSE_SPEED       20.0

//////////////////////////////////////////
// Global Definitions

//Portal Ray Properties
static float3 rayDirections[NUM_PORTAL_RECURSIONS]; 
static float3 rayPositions[NUM_PORTAL_RECURSIONS];
static float4 rayColors[NUM_PORTAL_RECURSIONS];
static float distances[NUM_PORTAL_RECURSIONS];
static float3 portal1Norm = float3(0.0,0.0,-1.0); 
static float3 portal1Pos = float3(-8.0,-3.5,-PORTAL_EPSILON);  
static float portal1Radius = 1.5; 
static float3 portal2Norm = float3(0.0,0.0,1.0); 
static float3 portal2Pos = float3(-8.0,-3.5,-10.0+PORTAL_EPSILON);  
static float portal2Radius = 1.5; 
#define NUM_CAMERA_TRAMS 5
static float3 cameraControl0[NUM_CAMERA_TRAMS]; 
static float3 cameraControl1[NUM_CAMERA_TRAMS];
static float3 cameraControl2[NUM_CAMERA_TRAMS];
static float3 cameraControl3[NUM_CAMERA_TRAMS];
static float cameraDuration[NUM_CAMERA_TRAMS];
#define NUM_PORTAL_TRAMS 5
static float3 portal1Normals[NUM_PORTAL_TRAMS];
static float3 portal1Positions[NUM_PORTAL_TRAMS];
static float portal1Duration[NUM_PORTAL_TRAMS];
static float3 portal2Normals[NUM_PORTAL_TRAMS];
static float3 portal2Positions[NUM_PORTAL_TRAMS];
static float portal2Duration[NUM_PORTAL_TRAMS];

////////////////
// DATA SETUP //
////////////////

void InitData()
{
	//Portal 1 - Blue
	portal1Duration [0] = 4.0; 
	portal1Normals  [0] = float3(0.0,0.0,-1.0);
	portal1Positions[0] = float3(-8.0,-3.5,-PORTAL_EPSILON); 
	
	portal1Duration [1] = 2.6; 
	portal1Normals  [1] = float3(0.0,0.0,1.0);
	portal1Positions[1] = float3(8.0,8.0,-10.0+PORTAL_EPSILON); 
	
	portal1Duration [2] = 1.0; 
	portal1Normals  [2] = float3(0.0,0.0,-1.0);
	portal1Positions[2] = float3(8.0,5.0,10.0-PORTAL_EPSILON); 
	
	portal1Duration [3] = 5.0; 
	portal1Normals  [3] = float3(0.0,0.0,1.0);
	portal1Positions[3] = float3(8.0,-3.5,-10.0+PORTAL_EPSILON); 
	
	//Note: portal 4 doesn't have 4rt place
	
	//Portal 2 - Red
	portal2Duration [0] = 1.0; 
	portal2Normals  [0] = float3(0.0,0.0,1.0);
	portal2Positions[0] = float3(-8.0,-3.5,-10.0+PORTAL_EPSILON); 

	portal2Duration [1] = 4.6; 
	portal2Normals  [1] = float3(0.0,0.0,-1.0);
	portal2Positions[1] = float3(8.0,-3.5,-PORTAL_EPSILON); 
	
	portal2Duration [2] = 0.45; 
	portal2Normals  [2] = float3(0.0,1.0,0.0);
	portal2Positions[2] = float3(0.0,-10.0+PORTAL_EPSILON,-5.0);
	
	portal2Duration [3] = 2.0; 
	portal2Normals  [3] = float3(0.0,0.0,-1.0);
	portal2Positions[3] = float3(8.0,1.5,10.0-PORTAL_EPSILON); 	
	
	portal2Duration [4] = 5.0; 
	portal2Normals  [4] = float3(0.0,0.0,1.0);
	portal2Positions[4] = float3(-8.0,-3.5,-10.0+PORTAL_EPSILON); 
	
  	//Camera 
	cameraDuration[0] = 3.0; 
	cameraControl0[0] = portal2Positions[0];
	cameraControl1[0] = portal2Positions[0] + portal2Normals[0]*2.0;
	cameraControl2[0] = portal1Positions[0] + portal1Normals[0]*2.0;
	cameraControl3[0] = portal1Positions[0];
	
	cameraDuration[1] = 2.0; 
	cameraControl0[1] = portal2Positions[1];
	cameraControl1[1] = portal2Positions[1] + portal2Normals[1]*2.0;
	cameraControl2[1] = float3(8.0,-3.5,-5.0); 
	cameraControl3[1] = float3(5.0,-3.5,-5.0);
	
	cameraDuration[2] = 1.0; 
	cameraControl0[2] = cameraControl3[1];
	cameraControl1[2] = cameraControl3[1]-float3(2.0,0.0,0.0);
	cameraControl2[2] = portal2Positions[2] + portal2Normals[2]*5.0;
	cameraControl3[2] = portal2Positions[2];
	
	cameraDuration[3] = 1.5; 
	cameraControl0[3] = portal1Positions[1];
	cameraControl1[3] = portal1Positions[1] + portal1Normals[1]*5.0;
	cameraControl2[3] = portal2Positions[3] + portal2Normals[3]*2.0;
	cameraControl3[3] = portal2Positions[3];
	
	cameraDuration[4] = 1.5; 
	cameraControl0[4] = portal1Positions[2];
	cameraControl1[4] = portal1Positions[2] + portal1Normals[2]*10.0;
	cameraControl2[4] = portal1Positions[3] + portal1Normals[3]*2.0;
	cameraControl3[4] = portal1Positions[3];
}

///////////////////
// WORLD UPDATES //
///////////////////

float3 Bezier(in float3 p0, in float3 p1, in float3 p2, in float3 p3, in float t, out float3 tangent)
{
	// n : being n+1 the number of control points
	// Bezier(t) = SUM[i=0..n]( Bernstein<n,i>(t)*ControlPoint<i> )

	// Bernstein polys for the given factor
	// Berstein<n,i>(t) = (n!/(i!*(n-i)!))*t^i*(1-t)^(n-i)
	
	float t2 = t*t;
	float t3 = t*t*t;

	float minusT = 1.0 - t;
	float minusT2 = minusT * minusT;
	float minusT3 = minusT2 * minusT;

	// Tangent
	
	// derived Bernstein polys for the given factor
	// Berstein<n,i>(t) = (n!/(i!*(n-i)!))*t^i*(1-t)^(n-i)
	
	tangent = normalize((p1-p0)*minusT2 + (p2-p1)*(2.0*t*minusT) + (p3-p2)*t2);
	
	// Position
	
	float b0 = minusT3;		  //(1-t)^3
	float b1 = 3.0*t*minusT2; //3t(1-t)^2
	float b2 = 3.0*t2*minusT; //3t^2(1-t)
	float b3 = t3;			  //t^3

	return (b0*p0)+(b1*p1)+(b2*p2)+(b3*p3);
}

float3 GetCameraPos(in float time, out float3 tangent)
{
	float localTime = time; 	
	for (int i=0;i<NUM_CAMERA_TRAMS;++i)
	{
		if (localTime < cameraDuration[i])
		{
			return Bezier(cameraControl0[i],cameraControl1[i],cameraControl2[i],cameraControl3[i],localTime/cameraDuration[i],tangent);
		}
						
		localTime -= cameraDuration[i];		
	}
	return (0.0);
}

float3 UpdatePortals(in float time)
{
	float localTime1 = time; 	
	float localTime2 = time; 	
	for (int i=0;i<NUM_PORTAL_TRAMS;++i)
	{
		if (localTime1 < portal1Duration[i])
		{
			portal1Norm = portal1Normals[i];
			portal1Pos  = portal1Positions[i];
			float openCloseFactor = min(PORTAL_OPEN_SPEED*localTime1,PORTAL_CLOSE_SPEED*abs(localTime1 - portal1Duration[i])); 
			portal1Radius = PORTAL_RADIUS*min(openCloseFactor,1.0);
			localTime1  = 99999.0; 
		}
		
		if (localTime2 < portal2Duration[i])
		{
			portal2Norm = portal2Normals[i];
			portal2Pos  = portal2Positions[i];
			float openCloseFactor = min(PORTAL_OPEN_SPEED*localTime2,PORTAL_CLOSE_SPEED*abs(localTime2 - portal2Duration[i])); 
			portal2Radius = PORTAL_RADIUS*min(openCloseFactor,1.0);
			localTime2  = 99999.0; 
		}
						
		localTime1 -= portal1Duration[i];		
		localTime2 -= portal2Duration[i];
	}
	return (0.0);
}

////////////////
// MORPHOLOGY //
////////////////

float AnaliticalDistSpark(in float3 ro, in float3 rd, float3 pointThisShader, float distThreshold)
{
	float lambda = dot(-(ro - pointThisShader),rd);
	float dist = length((ro+rd*lambda)-pointThisShader);
	return lerp(9999.0,dist,step(-1.0,lambda)*step(lambda,distThreshold+1.0)); 
}

float DistBox(in float3 p, in float3 dimensions)
{
	return length(max(abs(p) - dimensions,0.0)); 
}

float DistWalls( in float3 p)
{
	return min(min(-p.z+10.0,-p.x+10.0),-p.y+10.0);
}

float Map( in float3 p )
{
	float3 q = float3(abs(p.x),p.y,p.z); 
	
	return  min(
			   DistWalls(float3(q.x,abs(q.y),abs(q.z))),
			   min(DistBox(q-float3(0.0,-10.0,10.0),float3(10.0, 10.0, 10.0)),DistBox(q-float3(15.0,-15.0,0.0),float3(10.0, 10.0, 10.0)))
			   );
}

////////////
// PORTAL //
////////////

float RayPortalIntersection(
	in float3 rayOrigin,
	in float3 rayDir, 
	in float3 portalNorm, 
	in float3 portalLeft, 
	in float3 portalUp,
	in float3 portalPos, 
	in float portalRadius, 
	in float otherRadius,
	out float3 localRayDir,
	out float2 localPos, 
	out float signedDist
)
{
	float t = dot(portalNorm,portalPos-rayOrigin) / (dot(rayDir,portalNorm)); 
	float3 intersectionPos = rayOrigin + t*rayDir; 
	
	float dotRayNorm = dot(rayDir,portalNorm); 
	
	float3 localp = intersectionPos - portalPos;
	localPos = float2(dot(localp,portalLeft),dot(localp,portalUp));
	localRayDir = float3(dot(rayDir,portalLeft),dot(rayDir,portalUp),dotRayNorm); 
					
	//check distance to portal 
	const float2 scale = float2(PORTAL_SHAPE_INV_SCALE_X,1.0); 

	float2 localPosScaled = localPos*scale;
	
	float2 closestBorderPoint = normalize(localPosScaled)*min(portalRadius,otherRadius); 
	float2 borderDeltaVec = localPosScaled - closestBorderPoint; 
	
	signedDist = dot(borderDeltaVec,closestBorderPoint)*length(borderDeltaVec/scale); 
	
	//return data
	float insidePortal = step(dotRayNorm,0.0)*step(EPSILON,t)*step(length(localPosScaled),portalRadius);
	return lerp(9999.0,t,insidePortal);
}

float4 GetPortalColor(in float signedDist, in float3 baseColor)
{
	return float4(baseColor,clamp(1.0+signedDist*20.0,0.0,1.0)); 
}

float PortalCheck(
	in float3 rayOrigin,
	in float3 rayDir, 
	out float3 outRayOrigin,
	out float3 outRayDir,
	out float4 outPortalColor
	)
{
	float3 realUp1 = lerp(float3(0.0,1.0,0.0),float3(-1.0,0.0,0.0),step(0.9,dot(float3(0.0,1.0,0.0),portal1Norm)));
	float3 realUp2 = lerp(float3(0.0,1.0,0.0),float3(-1.0,0.0,0.0),step(0.9,dot(float3(0.0,1.0,0.0),portal2Norm)));
	
	float3 portal1Left = normalize(cross(realUp1,portal1Norm));
	float3 portal1Up = normalize(cross(portal1Norm,portal1Left));
	float3 portal2Left = normalize(cross(realUp2,portal2Norm));
	float3 portal2Up = normalize(cross(portal2Norm,portal2Left));
	
	float2 portal1localPos;
	float3 portal1localRay; 
	float portal1SignedDist; 
	float p1 = RayPortalIntersection(rayOrigin,rayDir,portal1Norm,portal1Left,portal1Up,portal1Pos,portal1Radius,portal2Radius,portal1localRay,portal1localPos,portal1SignedDist); 
	float4 portal1Color = GetPortalColor(portal1SignedDist,float3(0.0,0.0,1.0)); 	
	
	float2 portal2localPos;
	float3 portal2localRay; 
	float portal2SignedDist; 
	float p2 = RayPortalIntersection(rayOrigin,rayDir,portal2Norm,portal2Left,portal2Up,portal2Pos,portal2Radius,portal1Radius,portal2localRay,portal2localPos,portal2SignedDist); 
	float4 portal2Color = GetPortalColor(portal2SignedDist,float3(1.0,0.0,0.0)); 	
	
	float3 outPosp1p2 = portal2Pos - portal1localPos.x*portal2Left + portal1localPos.y*portal2Up;
	float3 outDirp1p2 = -portal1localRay.x*portal2Left + portal1localRay.y*portal2Up - portal1localRay.z*portal2Norm; 	

	float3 outPosp2p1 = portal1Pos - portal2localPos.x*portal1Left + portal2localPos.y*portal1Up;
	float3 outDirp2p1 = -portal2localRay.x*portal1Left + portal2localRay.y*portal1Up - portal2localRay.z*portal1Norm; 	

	float portalSelector = step(p2,p1); // 0 if portal 1 -> portal 2 | 1 if portal 2 -> portal 1 	
	outRayOrigin = 	 lerp(outPosp1p2,outPosp2p1,portalSelector);
	outRayDir = 	 lerp(outDirp1p2,outDirp2p1,portalSelector);
	outPortalColor = lerp(portal1Color,portal2Color,portalSelector);
	return 	lerp(p1,p2,portalSelector);
}

void ComputePortals(in float3 rayOrigin, in float3 rayDir)
{
	rayPositions[0]=rayOrigin;
	rayDirections[0]=rayDir; 
	distances[0]=0.0;
	rayColors[0]=(0.0);
	
	for (int i=1;i<NUM_PORTAL_RECURSIONS;++i)
	{
		distances[i]=distances[i-1] + PortalCheck(rayPositions[i-1],rayDirections[i-1],rayPositions[i],rayDirections[i],rayColors[i]);		
	}
}

float ExtractPortalRay(in float t, out float3 rayOrigin, out float3 rayDir)
{
	float ret = 0.0; 
	rayOrigin = (0.0); 
	rayDir = (0.0);
	
	for (int i=0;i<NUM_PORTAL_RECURSIONS;++i)
	{
		float isCopy = step(distances[i],t);
		float minusIsCopy = 1.0 - isCopy; 
		
		ret = (distances[i]*isCopy)+(ret*minusIsCopy);
		rayOrigin = (rayPositions[i]*isCopy)+(rayOrigin*minusIsCopy);
		rayDir = (rayDirections[i]*isCopy)+(rayDir*minusIsCopy);		
	}
	return ret;
}

float4 ExtractPortalColor(in float t)
{
	float4 ret = (0.0); 
	
	for (int i=0;i<NUM_PORTAL_RECURSIONS;++i)
	{
		float blendFactor = (1.0 - ret.w)*rayColors[i].w*step(distances[i],t); 
		ret.xyz = ret.xyz + blendFactor*rayColors[i].xyz;
		ret.w += blendFactor; 
	}
	return ret;
}

///////////////
// MATERIALS //
///////////////

float4 CalcColor( in float3 pos, in float3 nor)
{
	//ground/ceiling basic Color
	float2 groundtiles = 2.0*(0.5 - abs(0.5-fmod(pos.xz,float2(1.0, 1.0)))); 
	float groundtileBorder = smoothstep(0.0,0.1,min(groundtiles.x,groundtiles.y));
	float4 groundColor = groundtileBorder*float4(0.2,0.2,0.2,0.08); 
	
	//walls
	float3 wallTiles = 2.0*abs(float3(1.0,2.5,1.0)-fmod(pos+float3(0.96,2.5,0.96),float3(2.0,5.0,2.0)));
	float walltileBorder = smoothstep(0.0,0.1,min(min(wallTiles.x,wallTiles.y),wallTiles.z));
	float4 wallColor = walltileBorder*float4(0.05,0.05,0.05,0.02);
		
	return lerp(wallColor,groundColor,abs(dot(nor,float3(0.0,1.0,0.0))));
}

//////////////////////
// MAIN RAY/SHADING //
//////////////////////

float Intersect()
{
	float3 ro; 
	float3 rd; 
	float res = 2.0*EPSILON;
    float t = 0.0;
    for( int i=0; i<NUM_RAYMARCH_STEP; i++ )
    {
		if( abs(res)<EPSILON ) continue;
		float dist = ExtractPortalRay(t,ro,rd);
		res = Map( ro+rd*(t-dist) );
		t += res*STEP_REDUCTION;
    }
	return t;
}

float3 CalcNormal( in float3 pos )
{
    float2 eps = float2(EPSILON,0.0);
	return normalize( float3( Map(pos+eps.xyy) - Map(pos-eps.xyy), Map(pos+eps.yxy) - Map(pos-eps.yxy), Map(pos+eps.yyx) - Map(pos-eps.yyx) ) );
}

//IQ ray-marched ambient occlusion algorithm 
float AmbientOcclusion( in float3 pos, in float3 nor )
{
	float totao = 0.0;
    float sca = 1.0;
    for( int aoi=0; aoi<8; aoi++ )
    {
        float hr = 0.01 + 1.2*pow(float(aoi)/8.0,1.5);
        float3 aopos =  nor * hr + pos;
        float dd = Map( aopos );
        totao += -(dd-hr)*sca;
        sca *= 0.85;
    }
    return clamp( 1.0 - 0.6*totao, 0.0, 1.0 );
}
    

//////////////////////////////////////////////////////////////////////////////////////////////
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////

fixed4 FRAGMENTSHADER (pixelPoints PIXELSPACE) : SV_Target
{
	float2 coordinate = PIXELSPACE.uv;
	float2 coordinateSprite = PIXELSPACE.uv2;
	
	float2 coordinateScale = (PIXELSPACE.uv * 2.0) - 1.0 ;
	
	float2 coordinateShade = coordinateScale/(float2(2.0, 2.0));
	
	float2 coordinateFull = ceil(coordinateShade);
	float3 colBase  = 0.0;  
	float3 coalTexture = float3(coordinateScale.x + coordinateScale.y, coordinateScale.y - coordinateScale.x, pow(coordinate.x,2.0f));

//////////////////////////////////////////////////////////////////////////////////////////////
///	DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////
	colBase = 0.0;
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////



	////////////////
	//Init Data
	InitData();
	
	////////////////
	//Update World
	float localTime = fmod(TIME_OFFSET+TIME*TIME_SCALE,9.0);
	float3 tangent;
	float3 worldCameraPos = GetCameraPos(localTime,tangent);
	UpdatePortals(localTime); 
		
	////////////////
	// Render
    float2 puv = -1.0 + 2.0 * coordinate;
    float2 p = float2(puv.x * 1.0,puv.y);
	
	// Compute Camera
	float2 mousePos = 0.0;
	
#ifdef SUBJETIVE_CAMERA
	
	//move camera when clicking	
	float2 mouseAngles = (lerp(float2(0.0, 0.0),(mousePos*2.0)-1.0,clamp(0.0,0.0,1.0)))*float2(PI,PI*0.5);
	
	float3 camPosition = worldCameraPos;
	float3 camTmpRight = normalize( cross(tangent,float3(0.0,1.0,0.0) ) );
    float3 camTmpUp    = normalize( cross(camTmpRight,tangent));
	
	//apply camera extra rotation
	float3 camFront    = (tangent*cos(mouseAngles.x)+camTmpRight*sin(mouseAngles.x))*cos(mouseAngles.y)+camTmpUp*sin(mouseAngles.y);
	float3 camRight 	 = normalize( cross(camFront,camTmpUp));

#else
	
	//apply permanent camera movement 
	float inputCamAngle = PI-2.0*PI*mousePos.x;
	float3 camPosition    = float3(0.0,2.0,-5.0);
	float3 camTarget	    = camPosition + float3(sin(inputCamAngle), (3.0*mousePos.y)-1.0, cos(inputCamAngle));
	float3 camFront 	    = normalize( camTarget - camPosition );
	float3 camRight 	    = normalize( cross(camFront,float3(0.0,1.0,0.0) ) );
   
#endif
	
	float3 camUp 		 = normalize( cross(camRight,camFront));
    float3 rayDir 	 = normalize( p.x*camRight + p.y*camUp + 2.0*camFront );
	
	// light compute 
	float3 lightPos1 = float3(0.0,5.0,0.0);
	float3 lightColor1 = float3(1.0,1.0,1.0);
				
	// Start Ray
    float3 finalcolor = (0.0);
	float attenuation = 1.0;
	for( int reflectCount=0; reflectCount<NUM_REFLECTIONS; reflectCount++ )
	{
		// Portal logic goes here 
		ComputePortals(camPosition,rayDir);
							
		// Compute color for single ray
    	float t = Intersect();
		
		float3 prevRayDir = rayDir; 
		float3 prevCamPosition = camPosition;
		
		// results extraction
		float4 portalColor = ExtractPortalColor(t);
		float dist = ExtractPortalRay(t,camPosition,rayDir); 
		float localDist = t-dist;
		
		float3 position 	= camPosition + localDist*rayDir;
		float3 normal 	= normalize(CalcNormal(position));
		float3 reflDir 	= reflect(rayDir,normal);
			
		// lights and materials 
		float4 materialColor 	= CalcColor( position, normal );
		
		float ambient  		= 0.7 + 0.3*normal.y;
		float3 ambientColor 	= ambient*materialColor.rgb;
		
		//ambient occlusion
		float occlusion = AmbientOcclusion( position, normal );
		
		//light 1
		float3 lightDir1 	= normalize(lightPos1 - position);
		float diffuse1  = max(dot(normal,lightDir1),0.0);
		float3 diffuseColor = diffuse1*lightColor1*materialColor.rgb;		
		
		//bluelightComponent shining
		float2 lightSource = -(abs(position.xz)-10.0);
		float blueIntensity = pow(smoothstep(2.0,10.0,position.y)*(1.0-smoothstep(0.0,8.0,min(lightSource.x,lightSource.y))),50.0);  
		
		//fluroescentCeiling shining
		lightSource = abs(fmod(position.xz-float2(10.0, 10.0),float2(4.0, 4.0))-float2( 2.0, 2.0));
		float whiteIntensity = smoothstep(9.9,10.0,position.y)*(1.0 - smoothstep(1.0,2.5,lightSource.y))*(1.0 - smoothstep(1.0,2.5,lightSource.x));
			
		float3 lightShine = max(0.0,blueIntensity)*float3(0.0,0.1,0.5)+float3(1.0, 1.0, 1.0)*max(whiteIntensity,0.0);
		
#ifndef SUBJETIVE_CAMERA

		//Render Spark on the subjective Camera position
		float fwdSparkDist = AnaliticalDistSpark(prevCamPosition,prevRayDir,worldCameraPos,dist); 
		float ptlSparkDist = AnaliticalDistSpark(camPosition,rayDir,worldCameraPos,localDist);
				
		finalcolor += attenuation*float3(pow(max(smoothstep(4.0,0.0,min(fwdSparkDist,ptlSparkDist)),0.0),80.0));
		
#ifdef SPARK_ILUMINATION
		//Spark ilumination
		float3 lightDir2 	= worldCameraPos - position;
		float diffuse2  = smoothstep(5.0,1.0,length(lightDir2))*max(dot(normal,normalize(lightDir2)),0.0);
		diffuseColor += diffuse2*materialColor.rgb;		
#endif
		
#endif
		//mixing lights
		finalcolor += attenuation*(lightShine + lerp(lerp(ambientColor,occlusion*diffuseColor,0.8),portalColor.xyz,portalColor.w));
		
		// prepare next ray for reflections 
		rayDir = reflDir;
		attenuation *= 2.0*materialColor.w;
		camPosition = position + EPSILON*normal;
	}
	
	// saturate
	finalcolor = min(finalcolor,float3(1.0, 1.0, 1.0));
	
	// desaturation, gamma correction and simple vignette
	finalcolor = pow(lerp( finalcolor, float3(dot(finalcolor,float3(0.33, 0.33, 0.33)), dot(finalcolor,float3(0.33, 0.33, 0.33)), dot(finalcolor,float3(0.33, 0.33, 0.33))), 0.3 ), float3(0.45, 0.45, 0.45));
	float introTransition = smoothstep(FADE_IN_START,FADE_IN_END,TIME); 
	finalcolor *= introTransition*lerp(1.0,0.0,smoothstep(0.7,2.0,length(puv)));
	
    float4 fragColor = float4( finalcolor,1.0 );

	fragColor= smoothstep(0.0, 1.0, fragColor);

	fragColor *= 2.0;


///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS


float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);

float4 colBackground = outputSmoothed;


bool StickerSprite = (_OverlaySelection == 0)?true:false;

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////LINES OF CODE FOR THE SDFs STICKERS /////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
if(StickerSprite)
{
    if(_GlowFull == 1.0)
    {

		float2 coordUV = coordinate;    
        float dSign = PaintSticker(_StickerType, coordUV, _MotionState, _RangeSOne_One0, _RangeSOne_One1, _RangeSOne_One2, _RangeSOne_One3,
                                                                                    _RangeSTen_Ten0, _RangeSTen_Ten1, _RangeSTen_Ten2, _RangeSTen_Ten3); 
        float4 colorOutputTotal = ColorSign(dSign, colBackground, _BorderColor, 75.5, _BorderSizeTwo, _BorderBlurriness); 
    
    	if(colorOutputTotal.w * -1.0 < 0)
    	{

    		// GetEmission(PIXELSPACE)/3.0
        	return colorOutputTotal + float4( _ColorGlowHDR.xyz / 3.0, _AlphaColor/3.0);
    	}
		else 
		{
			return 0.0;
		}	
    }
    else
    {
		float2 coordUV = coordinate;    
    	float dSign = PaintSticker(_StickerType, coordUV, _MotionState, _RangeSOne_One0, _RangeSOne_One1, _RangeSOne_One2, _RangeSOne_One3,
    	_RangeSTen_Ten0, _RangeSTen_Ten1, _RangeSTen_Ten2, _RangeSTen_Ten3); 
    	float4 colorOutputTotal = ColorSign(dSign, float4(0.0, 0.0, 0.0, 0.0), _BorderColor, 75.5, _BorderSizeTwo, _BorderBlurriness); 
        if(colorOutputTotal.w * -1.0 < 0)
    	{
    	return colorOutputTotal + float4( _ColorGlowHDR.xyz / 3.0, _AlphaColor/3.0);
	    }
		else 
		{
    	float4 colorOutputTotal = ColorSign(dSign, colBackground, float4(0.0, 0.0, 0.0, 0.0), 0.0, 0.0, _BorderBlurriness); 
		return colorOutputTotal;
		}	
	}
 }
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////LINES OF CODE FOR THE SDFs STICKERS /////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////LINES OF CODE FOR THE SPRITES ///////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
else
{
	if(_GlowFull == 1.0)
	{
		float4 colorOutputTotal = PaintSprite(coordinateSprite, colBackground, _TextureSprite, _OutlineColor,
		_InVariableTick, _InVariableRatioX, _InVariableRatioY, _OutlineSprite);

		if(colorOutputTotal.w * -1.0 < 0)
		{
			return colorOutputTotal + float4( _ColorGlowHDR.xyz, _AlphaColor);
		}
		return 0.0;

	}
	else
	{
		float4 colorOutputTotal = PaintSpriteGlow(coordinateSprite, colBackground, _TextureSprite, _OutlineColor,
		_InVariableTick, _InVariableRatioX, _InVariableRatioY, _OutlineSprite);

		if(colorOutputTotal.w * -1.0 < 0)
		{
			return colorOutputTotal ;
		}
	
		return 0.0;
	
	}
}
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////LINES OF CODE FOR THE SPRITES ///////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


// float radio = 0.5;
// float2 pointValue = float2(0.0, 0.0);
// float paintPoint = float2(abs(cos(_Time.y)), abs(sin(_Time.y)));
// float lenghtRadio = length(uv - pointValue);
// if (lenghtRadio < radio)
// {
// return float4(1.0, 1.0, 1.0, 1.0) ;
// return 0.0;
// }
// else
// {
// return 0.0;
// }

}