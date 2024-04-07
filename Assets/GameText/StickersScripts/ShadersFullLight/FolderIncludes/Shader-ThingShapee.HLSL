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

float3 rotate(float3 src, float pitch, float yaw, float roll)
{
	float3 dst;
	dst.x = src.x * cos(pitch) - src.y * sin(pitch);
	dst.y = src.x * sin(pitch) + src.y * cos(pitch);
	dst.z = src.z;
	
	src = dst;
	
	dst.x = src.x * cos(yaw) - src.z * sin(yaw);
	dst.y = src.y;
	dst.z = src.x * sin(yaw) + src.z * cos(yaw);
	
	src = dst;

	dst.x = src.x;	
	dst.y = src.y * cos(roll) - src.z * sin(roll);
	dst.z = src.y * sin(roll) + src.z * cos(roll);
	
	return dst;
}

float acc_line(float3 a, float3 b, float2 uv)
{	
	float2 dir = normalize(a.xy-b.xy);
	float2 dir2 = a.xy-uv;
	float2 dir3 = uv-b.xy;
	float2 pdir = float2(-dir.y,dir.x);
	
	float d = dot(pdir,dir2);
	
	if ( dot(dir,dir2) < 0.0 ) d = length(uv-a.xy);
	if ( dot(dir,dir3) < 0.0 ) d = length(uv-b.xy);
	
	return max(0.04 - abs(d), 0.0) * 10.0;
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

	         float3 box[8], tbox[8];
	box[0] = float3(0.5,0.5,0.5);
	box[1] = float3(-0.5,0.5,0.5);
	box[2] = float3(-0.5,-0.5,0.5);
	box[3] = float3(-0.5,-0.5,-0.5);
	box[4] = float3(0.5,-0.5,-0.5);
	box[5] = float3(0.5,0.5,-0.5);
	box[6] = float3(0.5,-0.5,0.5);
	box[7] = float3(-0.5,0.5,-0.5);
	
	float2 uv = float2(-0.5,-0.5) + coordinate;
	float c = 0.0;
	float c2 = 0.0;
	
	for(int i=0; i<8; i++)
	{
		tbox[i] = rotate(box[i] * .5,TIME/3.0,TIME,TIME/2.0);
		tbox[i] = tbox[i] * (1.0 + tbox[i].z);
		float2 diff = (uv - tbox[i].xy);
		float len = length(diff) * 10.0 - tbox[i].z / 2.0;
		c = max((5.0 - len) * (0.025 + cos(TIME) / 80.0), c);
	}
	
	c = max(c, acc_line(tbox[0],tbox[1],uv));
	c = max(c, acc_line(tbox[0],tbox[5],uv));
	c = max(c, acc_line(tbox[0],tbox[6],uv));
	
	c = max(c, acc_line(tbox[1],tbox[2],uv));
	c = max(c, acc_line(tbox[1],tbox[7],uv));
	
	c = max(c, acc_line(tbox[2],tbox[6],uv));
	c = max(c, acc_line(tbox[2],tbox[3],uv));
	
	c = max(c, acc_line(tbox[3],tbox[4],uv));
	c = max(c, acc_line(tbox[3],tbox[7],uv));
	
	c = max(c, acc_line(tbox[4],tbox[5],uv));
	c = max(c, acc_line(tbox[4],tbox[6],uv));
	
	c = max(c, acc_line(tbox[5],tbox[7],uv));
	
	float4 fragColor = float4(
		0.0,
		cos(c*30.0 + 3.14),
		c*8.0,
		1.0);

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