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
#define FOV           1.2
//#define ITER_FRACTAL  6
#define ITER_MAX      80
#define ITER_DETAIL   0.05
#define SCALE_MIN     2.0
#define SCALE_MAX     2.6

// MANDELBOX

#define fixedRadius2  (1.125*1.125)
#define minRadius2    (0.5*0.5)
#define foldingLimit  (1.0)

void boxFold(inout float3 z, inout float dz) {
	z = clamp(z, -foldingLimit, foldingLimit) * 2.0 - z;
}

void sphereFold(inout float3 z, inout float dz) {
	float r2 = dot(z, z);
	if (r2 < minRadius2) { 
		float temp = (fixedRadius2 / minRadius2);
		z  *= temp;
		dz *= temp;
	} else if (r2 < fixedRadius2) { 
		float temp = (fixedRadius2 / r2);
		z  *= temp;
		dz *= temp;
	}
}

float DE(in float3 z, in float scale) {
	float3 offset = z;
	float dr = 1.0;
	
	// unroll 6 times, add for more detail
	
	boxFold(z, dr);
	sphereFold(z, dr);
 	z = scale * z + offset;
	dr = dr * abs(scale) + 1.0;
	
	boxFold(z, dr);
	sphereFold(z, dr);
 	z = scale * z + offset;
	dr = dr * abs(scale) + 1.0;
	
	boxFold(z, dr);
	sphereFold(z, dr);
 	z = scale * z + offset;
	dr = dr * abs(scale) + 1.0;
	
	boxFold(z, dr);
	sphereFold(z, dr);
 	z = scale * z + offset;
	dr = dr * abs(scale) + 1.0;
	
	boxFold(z, dr);
	sphereFold(z, dr);
 	z = scale * z + offset;
	dr = dr * abs(scale) + 1.0;
	
	boxFold(z, dr);
	sphereFold(z, dr);
 	z = scale * z + offset;
	dr = dr * abs(scale) + 1.0;
	
	/*for (int n = 0 ; n < ITER_FRACTAL ; n++) {
		boxFold(z, dr);
		sphereFold(z, dr);
 		z = scale * z + offset;
        dr = dr * abs(scale) + 1.0;
	}*/
	
	float r = length(z);
	return r / abs(dr);
}

// DONE MANDELBOXING

float2x2 rot(float a) {
	float2x2 mat2 = {cos(a),sin(a),-sin(a),cos(a)};

	return mat2;	
}

float pulse(float x) {
	x = fmod(x, 1.0);
	x -= 1.0;
	x *= x;
	x *= x;
	return x;
}

float pulseSig(float len) {
	float val = pulse(len * -0.5 + TIME * 0.5);
	return val*.6+.4;
}

float4 background(float2 p) {
	float v = sin(length(p)*8.0 - TIME * 1.812)*.3+.7;
	return float4(float3(0.54, 0.12, 0.61)*v, 1.0);
}

//////////////////////////////////////////////////////////////////////////////////////////////
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////

fixed4 FRAGMENTSHADER (pixelPoints PIXELSPACE) : SV_Target
{
	float2 coordinate = PIXELSPACE.uv;
	float2 coordinateSprite = PIXELSPACE.uv2;
	
	float2 coordinateScale = (PIXELSPACE.uv * 2.0) - 0.5 ;
	
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
	

	float2 uv = coordinateScale/2.0 - 0.25;
	
	float3 from = float3(-15, 0, 0);
	float3 dir = normalize(float3(uv * FOV, 1.0));
	dir.xz = mul(rot(3.1415*.5), dir.xz );
	
	float2 mouse=1.0;
	float2x2 rotxz = rot(TIME*0.3242+mouse.x);
	float2x2 rotxy = rot(TIME*0.0629+mouse.y);
	
	from.xz = mul(rotxz, from.xz);
	from.xy = mul(rotxy, from.xy);
	dir.xz  = mul(rotxz, dir.xz );
	dir.xy  = mul(rotxy, dir.xy );

	float scale = lerp(SCALE_MIN, SCALE_MAX, sin(TIME*0.0931)*.5+.5);
	
	bool unset = true;
	float occlu = 0.0;
	float mdist = 3.0;
	float pulse = 1.0;
	
	float totdist = 0.0;

	float4 fragColor;
	for (int steps = 0 ; steps < ITER_MAX ; steps++) {
		if (unset) {
			float3 p = from + totdist * dir;
			float dist = min(DE(p, scale), 3.0);
			totdist += dist;
			mdist = min(mdist, dist);
			if (dist < ITER_DETAIL) {
				unset = false;
				occlu = 1.0 - float(steps) / float(ITER_MAX);
				pulse = pulseSig(length(p));
			}
		}
	}
	
	if (unset) {
		fragColor = background(uv);
		fragColor.rgb += float3(1.0, 0.2, 0.8)*min(1.0-mdist*.333, 1.0);
	} else {
		fragColor = float4(0.6, 0.12, 0.48, 1.0);
		fragColor.rgb += (occlu+pulse)*.48;
	}
	

///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);

float4 colBackground = outputSmoothed;

bool StickerSprite = (_OverlaySelection == 0)?true:false;

// colBackground = float4(uv, 0.0, 1.0);
// return colBackground;

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