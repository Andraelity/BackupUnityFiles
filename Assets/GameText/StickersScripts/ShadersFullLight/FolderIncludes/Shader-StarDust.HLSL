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

#define SPEED           (1.7)
#define WARMUP_TIME     (2.0)

// Shadertoy's sound is a bit out of sync every time you run it :(
#define SOUND_OFFSET    (-0.0)

float saturate(float x)
{
    return clamp(x, 0.0, 1.0);
}

float isectPlane(float3 n, float d, float3 org, float3 dir)
{
    float t = -(dot(org, n) + d) / dot(dir, n);

    return t;
}


float3 drawEffect(float2 coord, float time)
{
    time = fmod(time, 55.0);
    float3 clr = (0.0);
    const float far_dist = 10000.0;

    float mtime = SOUND_OFFSET + time * 2.0 / SPEED;
    float2 uv = coord.xy ;

    float3 org = (0.0);
    float3 dir = float3(uv.xy * 2.0 - 1.0, 1.0);

    // Animate tilt
    float ang = sin(time * 0.2) * 0.2;
    float3 odir = dir;
    dir.x = cos(ang) * odir.x + sin(ang) * odir.y;
    dir.y = sin(ang) * odir.x - cos(ang) * odir.y;

    // Animate FOV and aspect ratio
    dir.x *= 1.5 + 0.5 * sin(time * 0.125);
    dir.y *= 1.5 + 0.5 * cos(time * 0.25 + 0.5);

    // Animate view direction
    dir.x += 0.25 * sin(time * 0.3);
    dir.y += 0.25 * sin(time * 0.7);

    // Bend it like this
    dir.xy = lerp(float2(dir.x + 0.2 * cos(dir.y) - 0.1, dir.y), dir.xy,
        smoothstep(0.0, 1.0, saturate(0.5 * abs(mtime - 50.0))));

    // Bend it like that
    dir.xy = lerp(float2(dir.x + 0.1 * sin(4.0 * (dir.x + time)), dir.y), dir.xy,
        smoothstep(0.0, 1.0, saturate(0.5 * abs(mtime - 58.0))));

    // Cycle between long blurry and short sharp particles
    float2 param = lerp(float2(60.0, 0.8), float2(800.0, 3.0),
        pow(0.5 + 0.5 * sin(time * 0.2), 2.0));

    float lt = frac(mtime / 4.0) * 4.0;
    float2 mutes = (0.0);
    
    if (mtime >= 32.0 && mtime < 48.0)
    {
        mutes = max(float2(0.0, 0.0), 1.0 - 4.0 * abs(lt - float2(3.25, 3.50)));
    }
    
    for (int k = 0; k < 2; k++)
    for (int i = 0; i < 64; i++)
    {
        // Draw only few layers during prologue
        if (mtime < 16.0 && i >= 16) break;

        float3 pn = float3(k > 0 ? -1.0 : 1.0, 0.0, 0.0);
        float t = isectPlane(pn, 100.0 + float(i) * 20.0, org, dir);

        if (t <= 0.0 || t >= far_dist) continue;

        float3 p = org + dir * t;
        float3 vdir = normalize(-p);

        // Create particle lanes by quantizing position
        float3 pp = ceil(p / 100.0) * 100.0;

        // Pseudo-random variables
        float n = pp.y + float(i) + float(k) * 123.0;
        float q = frac(sin(n * 123.456) * 234.345);
        float q2= frac(sin(n * 234.123) * 345.234);

        q = sin(p.z * 0.0003 + 1.0 * time * (0.25 + 0.75 * q2) + q * 12.0);

        // Smooth particle edges out
        q = saturate(q * param.x - param.x + 1.0) * param.y;
        q *= saturate(4.0 - 8.0 * abs(-50.0 + pp.y - p.y) / 100.0);

        // Fade out based on distance
        q *= 1.0 - saturate(pow(t / far_dist, 5.0));

        // Fade out based on view angle
        float fn = 1.0 - pow(1.0 - dot(vdir, pn), 2.0);
        q *= 2.0 * smoothstep(0.0, 1.0, fn);

        // Flash fade left or right plane
        q *= 1.0 - 0.9 * (k == 0 ? mutes.x : mutes.y);

        // Cycle palettes
        const float3 orange = float3(1.0, 0.7, 0.4);
        const float3 blue   = float3(0.4, 0.7, 1.0);
        clr += q * lerp(orange, blue, 0.5 + 0.5 * sin(time * 0.5 + q2));

        // Flash some particles in sync with bass drum
        float population = mtime < 16.0 ? 0.0 : 0.97;

        if (mtime >= 8.0 && q2 > population)
        {
            float a = mtime >= 62.0 ? 8.0 : 1.0;
            float b = mtime <  16.0 ? 2.0 : a;

            clr += q * (mtime < 16.0 ? 2.0 : 8.0)
                * max(0.0, frac(-mtime * b) * 2.0 - 1.0);
        }
    }

    clr *= 0.2;

    // Cycle gammas
    clr.r = pow(clr.r, 0.75 + 0.35 * sin(time * 0.5));
    clr.b = pow(clr.b, 0.75 - 0.35 * sin(time * 0.5));

    // Initial fade-in
    clr *= pow(min(mtime / 4.0, 1.0), 2.0);

    // Fade-out shortly after initial fade-in right before drums kick in
    if (mtime < 8.0) clr *= 1.0 - saturate((mtime - 5.0) / 3.0);

    // Flash horizon in sync with snare drum
    if (mtime >= 15.0)
    {
        float h = normalize(dir).x;
        clr *= 1.0 + 2.0 * pow(saturate(1.0 - abs(h)), 8.0)
            * max(0.0, frac(-mtime + 0.5) * 4.0 - 3.0);
    }

    // The end
    if (mtime >= 64.0) clr = float3(0.0, 0.0, 0.0);

    // Initial flash
    if (mtime >= 16.0) clr += max(0.0, 1.0 - (mtime-16.0) * 1.0);

    // Final flash
    if (mtime >= 64.0) clr += max(0.0, 1.0 - (mtime-64.0) * 0.5) * float3(0.8,0.9,1.0);

    // Desaturate prologue
    if (mtime < 16.0) clr = lerp( float3(dot(clr, float3(0.33, 0.33, 0.33)), dot(clr, float3(0.33, 0.33, 0.33)), dot(clr, float3(0.33, 0.33, 0.33))), clr, min(1.0, mtime / 32.0));

    // Vignette in linear space (looks better)
    clr *= clr;
    clr *= 1.4;
    clr *= 1.0 - 1.5 * dot(uv - 0.5, uv - 0.5);
    clr = sqrt(max(float3(0.0, 0.0, 0.0), clr));

    return clr;
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
    
    float time = max(0.0, TIME - WARMUP_TIME);
    float3  clr = (0.0);

    clr = drawEffect(coordinate, time);

    float4 fragColor = float4(clr, 1.0);

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