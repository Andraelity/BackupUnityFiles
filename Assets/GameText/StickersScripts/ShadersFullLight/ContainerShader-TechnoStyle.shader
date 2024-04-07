Shader "ShaderBloom/ContainerShader-TechnoStyle"
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


#define EPS .001

#define TM TIME * 3.5
#define FT 2.5 * EPS * hashThisShader(TM)
#define SM 50
#define CI float3(1.0, 1.0, 1.0) 
#define CO float3(r, 0, 0)

float hashThisShader(in float n) { return frac(sin(n)*43758.5453123); }

float hashThisShader(float2 p)
{
    return frac(sin(dot(p,float2(127.1,311.7))) * 43758.5453123);
}

float noise(float2 p)
{
    float2 i = floor(p), f = frac(p); 
    f *= f*f*(3.-2.*f);
    
    float2 c = float2(0,1);
    
    return lerp(lerp(hashThisShader(i + c.xx), 
                   hashThisShader(i + c.yx), f.x),
               lerp(hashThisShader(i + c.xy), 
                   hashThisShader(i + c.yy), f.x), f.y);
}

float fbm(in float2 p)
{
    return  .5000 * noise(p)
           +.2500 * noise(p * 2.)
           +.1250 * noise(p * 4.)
           +.0625 * noise(p * 8.);
}

float metaball(float2 p, float r)
{
    return float2(noise(float2(FT,1)/r), noise(float2(FT,1)/r)).x / dot(p, p);
}

float3 blob(float2 p, float t)
{
    float t0 = sin(t * 0.9) * .46;
    float t1 = sin(t * 2.4) * .39;
    float t2 = cos(t * 1.4) * .57;

    float r = metaball(p - float2(t1 * .98, t2 * .36), noise(float2(TM, TM) *.8))
            + metaball(p + float2(t2 * .55, t0 * .27), noise(float2(TM, TM) *.7))
            + metaball(p - float2(t0 * .33, t1 * .52), noise(float2(TM, TM) *.9))
            + metaball(p + float2(t2 * .22, t1 * .23), noise(float2(TM, TM) *.6))
            + metaball(p - float2(t1 * .85, t1 * .55), noise(float2(TM, TM) *.2));
    
    r = max(r, .2);
    
    r *= FT;

    float valueStep = step(0.1, r*r*r);

    float3 valueReturn =  (r > .5) 
                          ? (float3(step(.1, r*r*r), step(.1, r*r*r), step(.1, r*r*r)) * CI)
                          : (r < 1000.9 ? CO : CI);

    return valueReturn;
}

float3 bg(float2 p, float3 c)
{
    return (0.01);
}

float3 texsample(float2 uv, in float2 fragCoord)
{
    if (abs(EPS + uv.y) >= .4) { 
        return float3(0.0, 0.0, 0.0);
    }
        
    float3  c = float3(0.0, 0.0, 0.0);
    
    for (int i = 0; i < SM; ++i) {
        float dt = TM + 30. * fbm(float2(uv + 90.)) / float(i);
        c += blob(uv - noise(float2(uv) * 0.4), dt) / float(SM);
    }
    
    float3 fx = float3(smoothstep(0., .3, TIME) * c);
    
    fx += bg(uv, fx);

    // PostFX
    float noise = hashThisShader((hashThisShader(uv.x) + uv.y) * TIME) * 0.075;
    float fade = smoothstep(EPS, 2.5, TIME);
    
    return fade * (noise + fx);
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
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////
    colBase = 0.0;
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////////


    float2 uv = coordinateScale;
    float2 uv2 = coordinate;

    uv = fmod(uv, 0.5);
    uv2 = fmod(uv2, 0.5) * 2.0 - 0.5 ;

    float4 fragColor = float4(texsample(uv2,coordinate), 1);
    float4 fragColor2 = float4(texsample(coordinateScale,coordinate), 1);

    // fragColor = float4(uv2, 0.0, 1.0);
    fragColor = lerp(1.0, fragColor2, fragColor);
    fragColor = lerp(fragColor, fragColor2, 0.5);
    // return fragColor + fragColor2;



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