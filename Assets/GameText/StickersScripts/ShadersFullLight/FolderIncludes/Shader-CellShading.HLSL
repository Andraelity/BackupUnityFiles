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



static const float epsilon = 1e-6;
static const float pov = PI/10.0;
static const float maxdistance = 5.0;
static const float3 light = float3(0.0, 0.0, 1.0);
static const float celShades = 5.0;
static const float3 lightColor = float3(1.0, 0.0, 0.0);
static const float3 eye = float3(0.0, 0.0, 4.0);
static const float delta = 0.0001;
static const float3 dx = float3(delta, 0.0, 0.0);
static const float3 dy = float3(0.0, delta, 0.0);
static const float3 dz = float3(0.0, 0.0, delta);
static const float delta2 = 0.01;
static const float3 dx2 = float3(delta2, 0.0, 0.0);
static const float3 dy2 = float3(0.0, delta2, 0.0);
static const float3 dz2 = float3(0.0, 0.0, delta2);

static float c;
static float s;

float smin( float a, float b)
{
	float k = 0.2;
    float h = clamp( 0.5+0.5*(b-a)/k, 0.0, 1.0 );
    return lerp( b, a, h ) - k*h*(1.0-h);
}

float sdTorus(float3 p, float2 t)
{
  float2 q = float2(length(p.xz)-t.x,p.y);
  return length(q)-t.y;
}

float sdBox( float3 p, float3 b )
{
  float3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) +
         length(max(d,0.0));
}

float scene(float3 p)
{
    float3x3 m1 ={c, 0, -s,
                  0, 1, 0,
                  s, 0, c};
    float3x3 m2 ={c, -s, 0,
                  s, c, 0,
                  0, 0, 1};
    p = mul(mul(m1,m2), p);
    float size = (abs(c) + abs(s))*0.5;
    //return sdTorus(p, vec2(size, size/2.0));
    //return sdBox(p, float3(size));
    return smin(sdTorus(p, float2(size, size/2.0)),
                sdBox(p, float3(0.6, 0.6, 0.6)));
}

float4 march(float3 ray)
{
    float4 color = float4(0.0, 0.0, 0.0, 0.0);
    float t = 0.0;
    float mindistance = maxdistance;
    for (int i  = 0; i < 256; ++i)
    {
        float3 p = eye + ray * t;
        float d = scene(p);
        if (d < mindistance)
            mindistance = d;
        if(abs(d) < epsilon)
        {
            float3 n1 = normalize(float3(scene(p+dx2)-d, scene(p+dy2)-d, scene(p+dz2)-d));
            float3 n2 = normalize(float3(d-scene(p-dx2), d-scene(p-dy2), d-scene(p-dz2)));

            if (abs(dot(n1,n2)) > 0.99)
            {
                float3 normal = normalize(float3(
                    scene(p+dx)-scene(p-dx),
                    scene(p+dy)-scene(p-dy),
                    scene(p+dz)-scene(p-dz)));
                float intensity = dot(normal, light);
                intensity = ceil(intensity * celShades) / celShades;
                color = float4(lightColor * intensity, 1.0);
            }
            else
                color = float4(0.0, 0.0, 0.0, 1.0);
            break;
        }
        t += d;
        if (t > maxdistance)
        {
            if (mindistance < 0.02)
                color = float4(0.0, 0.0, 0.0, 1.0);
            break;
        }
    }
    return color;
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
	float3 colTexture = float3(coordinateScale.x + coordinateScale.y, coordinateScale.y - coordinateScale.x, pow(coordinate.x,2.0f));
//////////////////////////////////////////////////////////////////////////////////////////////
///	DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////
	colBase = 0.0;
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
	
	c = cos(TIME );
	s = sin(TIME );
    float ar  = 1.0;
    float2 uv = coordinate * 1.5 - 1.5/2.0;
    float3 ray = normalize(float3(uv, eye.z - 0.5/atan(pov)) - eye);
    float4 fragColor = float4(march(ray));

    // fragColor = float4(fragColor.xyz * 2.0, (fragColor.x + fragColor.y + fragColor.z)/3.0);


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