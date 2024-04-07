#define PI 3.1415926535897931
#define TIME _Time.y

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

float min_dist(float2 v, float2 w, float2 p)
{
    float t = ((p.x - v.x) * (w.x - v.x) + (p.y - v.y) * (w.y - v.y)) /
                    ((w.x - v.x) * (w.x - v.x) + (w.y - v.y) * (w.y - v.y));
    if (t < 0.0)
        return distance(p, v);
    else if (t > 1.0)
        return distance(p, w);
    float2 op = v + t * (w - v);
    return distance(p, op);
}

float rand(float _seed)
{
    return frac(sin(_seed+137.0) * 43758.5453);
}

float ll3(float2 p1, float2 p2, float2 uv)
{
	float2 plast;
		
	float mind = 1000000.0;
	
	plast = p1;
	
	float seed = rand(floor(TIME*40.0));
	
	for (int i=0; i < 2; i++)
	{
		float tt = float(i+1) / 3.0;
		
		float2 pt = p1 + ((p2 - p1) * tt);
		float a = rand(seed) * 3.141592 * 2.0;
		float2 pn = pt + float2(cos(a), sin(a)) * rand(seed) / ((abs(tt-0.5)+1.0)*90.0);
		
		float d = min_dist(plast, pn, uv);
		if (d < mind)
			mind = d;
		
		plast = pn;
		seed = rand(seed);
	}
	
	float d = min_dist(plast, p2, uv);
	if (d < mind)
		return d * 3.0;
	else
		return mind * 5.0;
}


float ll2(float2 p1, float2 p2, float2 uv)
{
	float2 plast;
		
	float mind = 1000000.0;
	
	plast = p1;
	
	float seed = rand(floor(TIME*15.0));
	
	for (int i=0; i < 3; i++)
	{
		float tt = float(i+1) / 4.0;
		
		float2 pt = p1 + ((p2 - p1) * tt);
		float a = rand(seed) * 3.141592 * 2.0;
		float2 pn = pt + float2(cos(a), sin(a)) * rand(seed) / ((abs(tt-0.5)+1.0)*30.0);
		
		float d = min_dist(plast, pn, uv);
		if (d < mind)
			mind = d;

		float2 pn2 = pn + float2(cos(a), sin(a)) * rand(seed) / ((abs(tt-0.5)+1.0)*rand(seed)*30.0);
		d = ll3(pn, pn2, uv);
		if (d < mind)
			mind = d;
		
		plast = pn;
		seed = rand(seed);
	}
	
	float d = min_dist(plast, p2, uv);
	if (d < mind)
		return d * 3.0;
	else
		return mind * 3.0;
}

//////////////////////////////////////////////////////////////////////////////////////////////
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////

fixed4 FRAGMENTSHADER (pixelPoints PIXELSPACE) : SV_Target
{
	float2 coordinateSprite = PIXELSPACE.uv2;
	float2 coordinate = PIXELSPACE.uv;
	
	float2 coordinateScale = (PIXELSPACE.uv - 0.5) * 2.0 ;
	
	float2 coordinateShade = coordinateScale/(float2(2.0, 2.0));
	
	float2 coordinateFull = ceil(coordinateShade);
	float3 colBase  = 0.0;  
	float3 colTexture = float3(coordinateScale.x + coordinateScale.y, coordinateScale.y - coordinateScale.x, pow(coordinate.x,2.0f));
//////////////////////////////////////////////////////////////////////////////////////////////
///	DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////
	colBase = 0.0;
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////

	float2 uv = coordinate - float2(0, 0.2);
	float2 p1 = float2(0.1, 0.3),
		 plast,
		 p2 = float2(0.9, 0.3);
		
	float mind = 1000000.0;
	
	plast = p1;
	
	float seed = rand(floor(TIME*10.0));
	
	for (int i=0; i < 14; i++)
	{
		float tt = float(i+1) / 15.0;
		
		float2 pt = p1 + ((p2 - p1) * tt);
		float a = rand(seed) * 3.141592 * 2.0;
		float2 pn = pt + float2(cos(a), sin(a)) * rand(seed) / ((abs(tt-0.5)+1.0)*9.0);
		
		float d = min_dist(plast, pn, uv);
		if (d < mind)
			mind = d;
		
		float2 pn2 = pn + float2(cos(a), sin(a)) * rand(seed) / ((abs(tt-0.5)+1.0)*rand(seed)*6.0);
		d = ll2(pn, pn2, uv);
		if (d < mind)
			mind = d;
		
		plast = pn;
		seed = rand(seed);
	}
	
	float d = min_dist(plast, p2, uv);
	if (d < mind)
		mind = d;
	
	mind *= 10.0;
	mind += 1.0;
	mind = 1.0 / (mind*mind*mind*mind);

	float4 fragColor = float4(1.0, 1.0, 1.0, 1.0) * mind;
	fragColor.r *= mind;
	fragColor.g *= mind;

	fragColor = fragColor;
	// return 1.0;	
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);
// float4 outputSmoothed = 0.0;
float4 colBackground = outputSmoothed;
// float4 colBackground = 0.0;
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
        float4 colorOutputTotal = ColorSign(dSign, colBackground, _BorderColor, _BorderSizeOne, _BorderSizeTwo, _BorderBlurriness); 
    
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
    	float4 colorOutputTotal = ColorSign(dSign, float4(0.0, 0.0, 0.0, 0.0), _BorderColor, _BorderSizeOne, _BorderSizeTwo, _BorderBlurriness); 
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