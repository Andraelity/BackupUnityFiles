Shader "ShaderBloom/ContainerShader-GlassPolyhedron"
{

	Properties {


	    //_ColorOperation ("ColorForSomething", Color) = (1.0, 1.0, 1.0, 1.0)

        _TextureSprite ("_TextureSprite", 2D)     = "green" {}
        _TextureChannel0 ("_TextureChannel0", 2D) = "green" {}
        _TextureChannel1 ("_TextureChannel1", 2D) = "green" {}
        _TextureChannel2 ("_TextureChannel2", 2D) = "green" {}
        _TextureChannel3 ("_TextureChannel3", 2D) = "green" {}


        _OverlaySelection("_OverlaySelection", Range(0.0, 1.0)) = 1.0

        _StickerType("_StickerType", Float) = 1.0
        _MotionState("_MotionState", Float) = 1.0
        _BorderColor("_BorderColor", Color) = (1.0, 1.0, 1.0, 1.0)
        _BorderSizeOne("_BorderSizeOne", Float) = 1.0
        _BorderSizeTwo("_BorderSizeTwo", Float) = 1.0
        _BorderBlurriness("_BorderBlurriness", Float) = 1.0
        _RangeSOne_One0("_RangeSOne_One0", Float) = 1.0
        _RangeSOne_One1("_RangeSOne_One1", Float) = 1.0
        _RangeSOne_One2("_RangeSOne_One2", Float) = 1.0
        _RangeSOne_One3("_RangeSOne_One3", Float) = 1.0
        _RangeSTen_Ten0("_RangeSTen_Ten0", Float) = 1.0
        _RangeSTen_Ten1("_RangeSTen_Ten1", Float) = 1.0
        _RangeSTen_Ten2("_RangeSTen_Ten2", Float) = 1.0
        _RangeSTen_Ten3("_RangeSTen_Ten3", Float) = 1.0

        _InVariableTickY(" _InVariableTickY", Float) = 1.0
        _InVariableRatioX("_InVariableRatioX", Float) = 1.0
        _InVariableRatioY("_InVariableRatioY", Float) = 1.0
        _OutlineColor("_OutlineColor", Color) = (1.0, 1.0, 1.0, 1.0)
        _OutlineSprite("_OutlineSprite", Float) = 1.0

        _GlowFull("_GlowFull", Range(0.0, 1.0)) = 0.0


	}

	SubShader 
	{

        LOD 500

        Pass
        {
			Tags { "RenderType"="Transparent" "Queue" = "Transparent" "DisableBatching" ="true" }
            
            ZWrite Off
            Cull off
            Blend SrcAlpha OneMinusSrcAlpha
            
            HLSLPROGRAM

            #pragma vertex VERTEXSHADER
            #pragma fragment FRAGMENTSHADER
            
            #pragma multi_compile_instancing
            

            #include "UnityCG.cginc"

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

#define DELTA               0.001
#define RAY_COUNT           7
#define RAY_LENGTH_MAX      100.0
#define RAY_STEP_MAX        100
#define LIGHT               float3  (1.0, 1.0, -1.0)
#define REFRACT_FACTOR      0.6
#define REFRACT_INDEX       1.6
#define AMBIENT             0.2
#define SPECULAR_POWER      3.0
#define SPECULAR_INTENSITY  0.5
#define FADE_POWER          1.0
#define M_PI                3.1415926535897932384626433832795
#define GLOW_FACTOR         1.5
#define LUMINOSITY_FACTOR   2.0

float3x3 mRotate (in float3  angle) {
    float c = cos (angle.x);
    float s = sin (angle.x);
    float3x3 rx =  {1.0, 0.0, 0.0, 0.0, c, s, 0.0, -s, c};

    c = cos (angle.y);
    s = sin (angle.y);
    float3x3 ry =  {c, 0.0, -s, 0.0, 1.0, 0.0, s, 0.0, c};

    c = cos (angle.z);
    s = sin (angle.z);
    float3x3 rz = {c, s, 0.0, -s, c, 0.0, 0.0, 0.0, 1.0};

    return mul(rz ,mul(ry ,rx));
}

static float3 k;

float getDistance (in float3  p) {
    float repeat = 20.0;
    float3  q = p + repeat * 0.5;
    k = floor (q / repeat);
    q -= repeat * (k + 0.5);
    p = mul(mRotate (k), q);

    float top = p.y - 3.0;
    float angleStep = M_PI / max (2.0, abs (k.x + 2.0 * k.y + 4.0 * k.z));
    float angle = angleStep * (0.5 + floor (atan2 (p.x, p.z) / angleStep));
    float side = cos (angle) * p.z + sin (angle) * p.x - 2.0;
    float bottom = -p.y - 3.0;

    return max (top, max (side, bottom));
}

float3  getFragmentColor (in float3  origin, in float3  direction) {
    float3  lightDirection = normalize (LIGHT);
    float2 delta = float2 (DELTA, 0.0);

    float3  fragColor = float3  (0.0, 0.0, 0.0);
    float intensity = 1.0;

    float distanceFactor = 1.0;
    float refractionRatio = 1.0 / REFRACT_INDEX;
    float rayStepCount = 0.0;
    for (int rayIndex = 0; rayIndex < RAY_COUNT; ++rayIndex) {

        // Ray marching
        float dist = RAY_LENGTH_MAX;
        float rayLength = 0.0;
        for (int rayStep = 0; rayStep < RAY_STEP_MAX; ++rayStep) {
            dist = distanceFactor * getDistance (origin);
            float distMin = max (dist, DELTA);
            rayLength += distMin;
            if (dist < 0.0 || rayLength > RAY_LENGTH_MAX) {
                break;
            }
            origin += direction * distMin;
            ++rayStepCount;
        }

        // Check whether we hit something
        float3  backColor = float3  (0.0, 0.0, 0.1 + 0.2 * max (0.0, dot (-direction, lightDirection)));
        if (dist >= 0.0) {
            fragColor = fragColor * (1.0 - intensity) + backColor * intensity;
            break;
        }

        // Get the normal
        float3  normal = normalize (distanceFactor * float3  (
            getDistance (origin + delta.xyy) - getDistance (origin - delta.xyy),
            getDistance (origin + delta.yxy) - getDistance (origin - delta.yxy),
            getDistance (origin + delta.yyx) - getDistance (origin - delta.yyx)));

        // Basic lighting
        float3  reflection = reflect (direction, normal);
        if (distanceFactor > 0.0) {
            float relfectionDiffuse = max (0.0, dot (normal, lightDirection));
            float relfectionSpecular = pow (max (0.0, dot (reflection, lightDirection)), SPECULAR_POWER) * SPECULAR_INTENSITY;
            float fade = pow (1.0 - rayLength / RAY_LENGTH_MAX, FADE_POWER);

            float3  localColor = max (sin (k * k), 0.2);
            localColor = (AMBIENT + relfectionDiffuse) * localColor + relfectionSpecular;
            localColor = lerp (backColor, localColor, fade);

            fragColor = fragColor * (1.0 - intensity) + localColor * intensity;
            intensity *= REFRACT_FACTOR;
        }

        // Next ray...
        float3  refraction = refract (direction, normal, refractionRatio);
        if (dot (refraction, refraction) < DELTA) {
            direction = reflection;
            origin += direction * DELTA * 2.0;
        }
        else {
            direction = refraction;
            distanceFactor = -distanceFactor;
            refractionRatio = 1.0 / refractionRatio;
        }
    }

    // Return the fragment color
    return fragColor * LUMINOSITY_FACTOR + GLOW_FACTOR * rayStepCount / float (RAY_STEP_MAX * RAY_COUNT);
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
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////
    colBase = 0.0;
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
    
// Define the ray corresponding to this fragment
    float2 frag = coordinateScale;
    float3 direction = normalize (float3 (frag, 2.0));

    // Set the camera
    float3 origin = float3 ((15.0 * cos (TIME * 0.1)), 10.0 * sin (TIME * 0.2), 15.0 * sin (TIME * 0.1));
    float3 forward = -origin;
    float3 up = float3 (sin (TIME * 0.3), 2.0, 0.0);
    float3x3 rotation;
    rotation [2] = normalize (forward);
    rotation [0] = normalize (cross (up, forward));
    rotation [1] = cross (rotation [2], rotation [0]);
    direction = mul(rotation , direction);

    // Set the fragment color
    float4 fragColor = float4 (getFragmentColor (origin, direction), 1.0);

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
			ENDHLSL
		}


		 // Pass
        // {
			// Tags { "RenderType"="Transparent" "Queue" = "Transparent" "DisableBatching" ="true" }
            // 
            // ZWrite Off
            // Cull off
            // Blend SrcAlpha OneMinusSrcAlpha
            
            // HLSLPROGRAM

            // #pragma vertex VERTEXSHADER
            // #pragma fragment FRAGMENTSHADER
            // 
            // #pragma multi_compile_instancing
            

            // #include "UnityCG.cginc"
            // #include "FolderIncludes/TEMPLATESHADER.HLSL"

			// ENDHLSL
		// }


	}

}