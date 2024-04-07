Shader "ShaderBloom/ContainerShader-TimeCoordinates"
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

float2 rot(float2 p, float a)
{
    float c = cos(a);
    float s = sin(a);
    return float2(p.x*c + p.y*s,
                -p.x*s + p.y*c);
}

float circle(float2 pos, float radius)
{
    return clamp(((1.0-abs(length(pos)-radius))-0.99)*100.0, 0.0, 1.0);
    
}

float circleFill(float2 pos, float radius)
{
    return clamp(((1.0-(length(pos)-radius))-0.99)*100.0, 0.0, 1.0);   
}

// Thanks IÃ±igo Quilez!
float lineThisShader( in float2 p, in float2 a, in float2 b )
{
    float2 pa = -p - a;
    float2 ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    float d = length( pa - ba*h );
    
    return clamp(((1.0 - d)-0.99)*100.0, 0.0, 1.0);
}

// for posterity, the original evil function
float EvilLine(float2 pos, float2 start, float2 finish)
{
    float2 delta = finish - start;
    float2 n = normalize(delta);
    float l = length(delta);
    float d = sign(n.x);
    
    float angle = atan(n.y / n.x);
    float2 t = rot(-pos - start, angle);
    
    float s = d < 0.0 ? 0.0: d*l;
    float f = d < 0.0 ? d*l : 0.0;
    if(t.x > s || t.x < f)return 0.0;

    return clamp(((1.0 - abs(t.y))-0.99)*100.0, 0.0, 1.0);
}

float comb(float2 pos) 
{
    pos.x=frac(pos.x+0.5)-.5;
    pos.y=frac(pos.y+0.5)-0.5;
    
    pos = abs(pos);
    float fm = 1.0 + 0.75*sin(TIME);
    return (max(pos.x+pos.y*0.57735*fm,pos.y)-.5);

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
    
    float aspect = 1.0;
    float2 unipos = coordinate;
    float2 pos = unipos * 10.0;
    pos.x *= aspect;
    float dist = comb(pos);
    float valueD = smoothstep( 0.03, 0.06, abs(dist));
    
    
    float3 clr_a = float3(0.9, 0.6, 0.2) * float3(1.0 - abs(dist * 5.0), 1.0 - abs(dist * 5.0), 1.0 - abs(dist * 5.0));
    float3 clr_b = float3(0.3, 0.2, 0.0);
    float m = 0.5 + dist*6.0*(sin(TIME * 1.125));
    float3 clr_c = float3( m, m*0.6, 0.15 );
    
    float3 clr = lerp(clr_a, clr_b, valueD);
    clr = lerp(clr, clr_c, dist*-sin(valueD));
    float4 fragColor2 = float4( clr, 1.0);


    // return fragColor2;


    float2 uv = coordinate;
    float2 p = coordinateScale;
 
    float3 colour = (0);
    float3 white  = (1);
    
    
    
    float c = circle(p, 0.2);
    c += circle(p, 0.1);
    c += circle(p, 0.18);
    c += circleFill(p, 0.005);

//    c += circle(p, 1.3);
    c += circle(p, 1.0);
    if(p.x > 0.0)c += circle(p, 0.4);
    if(p.x > 0.0)c += circle(p, 0.42);
    if(p.x < 0.0)c += circle(p, 0.47);
    c += circleFill(p+float2(0.47, 0.0), 0.02);
    c += circleFill(p+float2(0.84147*0.47, 0.54030*0.47), 0.02);
    c += circleFill(p+float2(0.84147*0.47, -0.54030*0.47), 0.02);
    c += circleFill(p+float2(0.41614*0.47, 0.90929*0.47), 0.02);
    c += circleFill(p+float2(0.41614*0.47, -0.90929*0.47), 0.02);
    
    float t = TIME * 2.0;
    float t2 = t * -1;
    float t3 = t * 1.7;
    
    float2 angle1 = float2(sin(t), cos(t));
    float2 a = angle1 * 0.7;
    
    t *= 0.5;
    float2 angle2 = float2(sin(t), cos(t));
    float2 b = angle2 * 0.8;
    
    float2 angle3 = float2(sin(t2), cos(t2));
    float2 d = b + angle3* 0.4;

    float2 angle4 = float2(sin(t3), cos(t3));
    float2 e = angle4 * 0.9;

    float2 angle5 = float2(sin(t3+4.0), cos(t3+4.0));
    float2 f = angle5 * 0.8;
    
    float2 angle6 = float2(sin(t*-0.1+5.0), cos(t*-0.1+5.0));
    float2 h = angle6 * 0.8;
    
   
 
    
    
    float tt = t * 1.4;
    
    float tm = fmod(tt, 0.5);
    float tmt = tt - tm;
    if( tm > 0.4) tmt += (tm-0.4)*5.0;
    float2 tangle1 = float2(sin(tmt), cos(tmt));

    tt *= 0.8;
    tm = fmod(tt, 0.6);
    float tmt2 = tt - tm;
    if( tm > 0.2) tmt2 += (tm-0.2)*1.5;
    
    float2 tangle2 = float2(sin(tmt2*-4.0), cos(tmt2*-4.0));
   
    float2 tangle3 = float2(sin(tmt2), cos(tmt2));
    
    tt = t+3.0;
    tm = fmod(tt, 0.2);
    tmt = tt - tm;
    if( tm > 0.1) tmt += (tm-0.1)*2.0;
    float2 tangle4 = float2(sin(-tmt), cos(-tmt)); tmt += 0.9;
    float2 tangle41 = float2(sin(-tmt), cos(-tmt)); tmt += 0.5;
    float2 tangle42 = float2(sin(-tmt), cos(-tmt)); tmt += 0.5;
    float2 tangle43 = float2(sin(-tmt), cos(-tmt)); tmt += 0.5;
    float2 tangle44 = float2(sin(-tmt), cos(-tmt)); tmt += 0.5;
    float2 tangle45 = float2(sin(-tmt), cos(-tmt));

    tt = TIME+0.001 * 2.0;
    tm = fmod(tt, 1.0);
    tmt = tt - tm;
    if( tm > 0.9) tmt += (tm-0.9)*10.0;

    float2 tangle51 = 0.17*float2(sin(-tmt), cos(-tmt)); tmt += 1.0471975511965976;
    float2 tangle52 = 0.17*float2(sin(-tmt), cos(-tmt)); tmt += 1.0471975511965976;
    float2 tangle53 = 0.17*float2(sin(-tmt), cos(-tmt));
    
    c += lineThisShader(p, tangle51, -tangle53);
    c += lineThisShader(p, tangle52, tangle51);
    c += lineThisShader(p, tangle53, tangle52);
    c += lineThisShader(p, -tangle51, tangle53);
    c += lineThisShader(p, -tangle52, -tangle51);
    c += lineThisShader(p, -tangle53, -tangle52);

    c += circleFill(p+tangle51, 0.01);
    c += circleFill(p+tangle52, 0.01);
    c += circleFill(p+tangle53, 0.01);
    c += circleFill(p-tangle51, 0.01);
    c += circleFill(p-tangle52, 0.01);
    c += circleFill(p-tangle53, 0.01);
    
    
    
    c += circle(p+a, 0.2);
    c += circle(p+a, 0.14);
    c += circle(p+a, 0.1);
    c += circleFill(p+a, 0.04);
    c += circleFill(p+a+tangle3*0.2, 0.025);   
    
    
    c += circle(p+a, 0.14);


    c += circle(p+b, 0.2);
    c += circle(p+b, 0.03);
    c += circle(p+b, 0.15);
    c += circle(p+b, 0.45);
    c += circleFill(p+b+tangle1*0.05, 0.01);
    c += circleFill(p+b+tangle1*0.09, 0.02);
    c += circleFill(p+b+tangle1*0.15, 0.03);
    c += circle(p+b+tangle1*-0.15, 0.03);
    c += circle(p+b+tangle1*-0.07, 0.015);

    c += circle(p+d, 0.08);


    c += circle(p+e, 0.08);
    

    c += circle(p+f, 0.12);
    c += circle(p+f, 0.10);
    c += circleFill(p+f+tangle2*0.05, 0.01);
    c += circleFill(p+f+tangle2*0.10, 0.01);
    c += circle(p+f-tangle2*0.03, 0.01);
    c += circleFill(p+f+float2(0.085, 0.085), 0.005);
    c += circleFill(p+f, 0.005);

    
    float2 g = tangle4 * 0.16;
    c += circle(p+h, 0.05);
    c += circle(p+h, 0.1);
    c += circle(p+h, 0.17);
    c += circle(p+h, 0.2);
    c += circleFill(p+h+tangle41 *0.16, 0.01);
    c += circleFill(p+h+tangle42 *0.16, 0.01);
    c += circleFill(p+h+tangle43 *0.16, 0.01);
    c += circleFill(p+h+tangle44 *0.16, 0.01);
    c += circleFill(p+h+tangle45 *0.16, 0.01);
    c += circleFill(p+h+angle1 *0.06, 0.02);
    c += circleFill(p+h+tangle43*-0.16, 0.01);
    
    
    c += lineThisShader(p, float2(0.0, 0.0), a);
    c += circleFill(p+b, 0.005);
    c += circleFill(p+d, 0.005);
    c += circleFill(p+e, 0.005);

    c += lineThisShader(p, b, a);
    c += lineThisShader(p, d, e);
    c += lineThisShader(p, b+tangle1*0.15, e);
    c += lineThisShader(p, e, f+float2(0.085, 0.085));

    c += lineThisShader(p, h+angle1*0.06, f);
    c += lineThisShader(p, h+tangle43*-0.16, d);
    c += lineThisShader(p, h+tangle42*0.16, e);
    
    
    // of course I'd write a lineThisShader function that
    // doesn't handle perfectly vertical lines
    c += lineThisShader(p, float2(0.001, -0.5), float2(0.0001, 0.5));
    c += circleFill(p+float2(0.001, -0.5), 0.005);
    c += circleFill(p+float2(0.001, 0.5), 0.005);
    
    c = clamp(c, 0.0, 1.0);
    colour = white * c;
    

    float4 fragColor = float4(colour, 1.0);

    fragColor = smoothstep(0.0, fragColor2 , fragColor);



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