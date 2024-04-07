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



struct RayThisShader 
{
    float3 position;
    float3 direction;
    bool hit;
};


static const int MAX_STEPS = 16;
static const float3 BOX = float3(1.0, 1.0, 1.0);
static const float THRESHOLD = 0.01;

float udBox(float3 p, float3 b) {
	return length(max(abs(p) - b, 0.0));
}


RayThisShader intersect(RayThisShader ray) {
	for (int i=0; i<MAX_STEPS; i++) 
	{
		float dist = udBox(ray.position, BOX);
		if (dist < THRESHOLD) 
		{
            ray.hit = true;
            return ray;
		}
		ray.position += ray.direction * dist;
	}
    return ray;
}

float3 Rx(float3 arrayVector, float angle) 
{
    float rx = arrayVector.x * cos(angle) + arrayVector.z * sin(angle);
    float rz = arrayVector.z * cos(angle) - arrayVector.x * sin(angle);
    return float3(rx, arrayVector.y, rz);
}

float3 render(RayThisShader ray) {   
    if (ray.hit) {
        return (ray.position + BOX) / (2.0 * BOX);
    } else {
		return float3(0.0, 0.0, 0.0);
    }
}


static const float AaSqrt = 4.;
static const float ShutterSpeed = 1.0/30.0;

#define PI  3.14159265359
#define TAU 6.28318530718

float Saw(float t)
{
	return frac(t);
}

float SmoothSaw(float t)
{
	return smoothstep(0.0, 1.0, frac(t));
}


float SmootherSaw(float t)
{
	return smoothstep(0.0, 1.0, smoothstep(0.0, 1.0, frac(t)));
}
	
float3 Scene(float2 q, float t)
{
	t = SmoothSaw(t/2.);
	
	float r = length(q);
	float a = atan2(q.y,q.x);
	float u = 0.0;
	

	u = 10.*a + 3.*TAU*t*(1.- 4.0*(1. - r)*sin(TAU*t));
	u = 20.0*r*(1.0 + 0.1*cos(u));

	
	u = 0.5 + 0.5*cos(u);
	u = min(floor(2.0*u),1.0);
	
	u *= 1.0-smoothstep(1.,1.+0.00001,r);

	float3 Col1 = (0.00);
	float3 Col2 = (0.95);
	
	return lerp(Col1, Col2, u);
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


	float Ratio = 0.45* 200.0;
	
	const float AaCount = AaSqrt*AaSqrt;

	// Render scene in linear space with motion blur and AA:
	float3 ColSum = (0);

	for(float F=0.0; F<AaCount; F++)
	{
		// AA:
		float2 Off = float2(1. + F/AaSqrt, fmod(1. + F,AaSqrt)) / AaSqrt;
		float2 UV = (coordinate * 200.0 - 100.0) / Ratio;	
		
		// Motion blur:
		float t = TIME + F*ShutterSpeed / AaCount;
		
		// Render:
		ColSum += Scene(UV, t);
	}
	
	ColSum /= AaCount;
	
	ColSum = pow(ColSum, float3(1.0/2.2, 1.0/2.2, 1.0/2.2));
	
	float4 fragValue1 = float4(ColSum, (ColSum.x + ColSum.y + ColSum.z)/3.0);





	float aspect_ratio = 2.0;
	float2 uv = coordinate;
	float2 p = uv * 1.0  - 0.5;//(uv - float2(0.5)) * float2(aspect_ratio, 1.0);
    
    float angle = TIME;
    
    	
    float3 camera_loc = 6.0 * float3(-sin(angle), 0.0, -cos(angle));
    float3 camera_dir = float3(sin(angle), 0.0, cos(angle));
    
    RayThisShader ray;
    ray.position = camera_loc;
    ray.direction = Rx(normalize(float3(p, 1.0)), angle);
    ray.hit = false;
	
	ray = intersect(ray);
    
    float3 col = render(ray);
    
    float4 fragColor2 = float4(col , (col.x + col.y + col.z)/2.0);
	float4 fragColor =  smoothstep(fragValue1, fragColor2 , 0.5);



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