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


#define TAU 6.28318530718
#define MOD2 float2(.16632,.17369)
#define MOD3 float3(.16532,.17369,.15787)
static float gTime = TIME;
static float flareUp;

//=================================================================================================
float2 Rotate2axis(float2 p, float a)
{
	float si = sin(a);
	float co = cos(a);
	float2x2 mat2 = {si, co, -co, si };
	return mul(mat2, p);
}

//=================================================================================================
// Linear step, faster than smoothstep...
float LinearStep(float a, float b, float x)
{
	return clamp((x-a)/(b-a), 0.0, 1.0);
}

//=================================================================================================

float Hash(float p)
{
	float2 p2 = frac(float2(p, p) * MOD2);
    p2 += dot(p2.yx, p2.xy+19.19);
	return frac(p2.x * p2.y);
}
 
//=================================================================================================
float EyeNoise( in float x )
{
    float p = floor(x);
    float f = frac(x);
	f = clamp(pow(f, 7.0), 0.0,1.0);
	//f = f*f*(3.0-2.0*f);
    return lerp(Hash(p), Hash(p+1.0), f);
}

//=================================================================================================

//=================================================================================================


float3 hash31(float p)
{
   float3 p3 = frac(float3(p, p, p) * float3(.1031, .1030, .0973));
   p3 += dot(p3, p3.yzx+33.33);
   return frac((p3.xxy+p3.yzz)*p3.zyx);

}
float3 hash32(float2 p)
{
	float3 p3 = frac(float3(p.xyx) * float3(.1031, .1030, .0973));
    p3 += dot(p3, p3.yxz+33.33);
    return frac((p3.xxy+p3.yzz)*p3.zyx);
}
float Noise( in float3 x )
{
    float3 p = floor(x);
    float3 f = frac(x);
	f = f*f*(3.0-2.0*f);
	float2 uv = (p.xy + float2(37.0, 17.0) * p.z) + f.xy;
	float2 rg = hash31(uv);//tex2D( _TextureChannel0, (uv+.5)/256.).yx;
	return lerp(rg.x, rg.y, f.z);
}

//=================================================================================================
float Pupil(float3 p, float r)
{
	// It's just a stretched sphere but the mirrored
	// halves are push together to make a sharper top and bottom.
	p.xz = abs(p.xz)+.25;
	return length(p) - r;
}

//=================================================================================================
float DE_Fire(float3 p)
{
	p *= float3(1.0, 1.0, 1.5);
	float len = length(p);
	float ax = atan2(p.y, p.x)*10.0;
	float ay = atan2(p.y, p.z)*10.0;
	float3 shape = float3(len*.5-gTime*1.2, ax, ay) * 2.0;
	
	shape += 2.5 * (Noise(p * .25) -
				 	Noise(p * 0.5) * .5 +
					Noise(p * 2.0) * .25);

	float f = Noise(shape)*6.0;
	f += (LinearStep(7.30, 8.3+flareUp, len)*LinearStep(12.0+flareUp*2.0, 8.0, len)) * 3.0;
	p *= float3(.75, 1.2, 1.0);
	len = length(p);
	f = lerp(f, 0.0, LinearStep(12.5+flareUp, 16.5+flareUp, len));
	return f;
}

//=================================================================================================

//=================================================================================================
float DE_Pupil(float3 p)
{
	float time = gTime * .5+sin(gTime*.3)*.5;
	float t = EyeNoise(time) * .125 +.125;
	p.yz = Rotate2axis(p.yz, t * TAU);
	p *= float3(1.2-EyeNoise(time+32.5)*.5, .155, 1.0);
	t = EyeNoise(time-31.0) * .125 +.1875;
	p.xz = Rotate2axis(p.xz, t*TAU);
	p += float3(.0, 0.0, 4.);
	
	float  d = Pupil(p, .78);
	return d * max(1.0, abs(p.y*2.5));
}

//=================================================================================================


//=================================================================================================
float4 Raymarch( in float3 ro, in float3 rd, in float2 fragCoord, inout bool hit, out float pupil)
{
	float sum = 0.0;
	// Starting point plus dither to prevent edge banding...
	float t = 14.0 + .1 * tex2D(_TextureChannel0, fragCoord.xy / 100.0).y;
	float3 pos = float3(0.0, 0.0, 0.0);
	float d = 100.0;
	pupil = 0.0;

	// [unroll(100)]
	[fastopt]
	for(int i=0; i < 100; i++)
	{
        if (t > 37.0) break;
		pos = ro + t*rd;
		float3 shape = pos * float3(1.5, .4, 1.5);
	
		// Accumulate pixel denisity depending on the distance to the pupil
		d = DE_Pupil(pos);
		pupil += LinearStep(0.02 +Noise(pos*4.0+gTime)*.3, 0.0, d) * .17;

		// Add fire around pupil...
		sum += LinearStep(1.3, 0.0, d) * .014;
		
		// Search for pillars...
		// d = DE_Pillars(pos);
		if (d < 0.01)
		{
			pos = ro + (t + d) * rd;
            hit = true;
			break;
		}


		sum += max(DE_Fire(pos), 0.0) * .00162;
    	t += max(.1, t*.0057);

	}
	
	return float4(pos, clamp(sum*sum*sum, 0.0, 1.0 ));
}

//=================================================================================================
float3 FlameColour(float f)
{
	f = f*f*(3.0-2.0*f);
	return  min(float3(f+.8, f*f*1.4+.05, f*f*f*.6) * f, 1.0);
}

//=================================================================================================


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
	
	  
    flareUp = max(sin(gTime*.75+3.5), 0.0);
	float2 uv = coordinate;
	float2 p = coordinateScale - float2(0.5, 0.5);

	float3 origin = float3(sin(gTime*.34)*5.0, -10.0 - sin(gTime*.415) * 6.0, -20.0+sin(gTime*.15) * 2.0);
	float3 target = float3( 0.0, 0.0, 0.0 );
	
	// Make camera ray using origin and target positions...
	float3 cw = normalize( target-origin);
	float3 cp = float3(0.0, 1.0, 0.0);
	float3 cu = normalize( cross(cw, cp) );
	float3 cv = ( cross(cu,cw) );
	float3 ray = normalize(p.x*cu + p.y*cv + 1.5 * cw );
	
	bool hit = false;
	float pupil = 0.0;
	float4 ret = Raymarch(origin, ray, coordinate, hit, pupil);
	float3 col = float3(0.0, 0.0, 0.0);

	float3 light = float3(0.0, 4.0, -4.0);
	// Do the lightning flash effect...
	float t = fmod(gTime+3.0, 13.0);
	float flash = smoothstep(0.4, .0, t);
	flash += smoothstep(0.2, .0, abs(t-.6)) * 1.5;
	flash += smoothstep(0.7, .8, t) * smoothstep(1.3, .8, t);
	flash *= 2.2;

	
	col += FlameColour(ret.w);
	col = lerp (col, col, min(pupil, 1.0));
	
	// Contrasts...
	col = sqrt(col);
	col = min(lerp(float3(length(col), length(col), length(col)),col, 1.22), 1.0);
	col += col * .3;
	
	float4 fragColor = float4(min(col, 1.0),1.0);	



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