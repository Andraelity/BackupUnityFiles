Shader "ShaderBloom/ContainerShader-RainbowSpaguetti"
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


static const float i3 = 0.5773502691896258;
static const float r = 0.40824829046386302;
static const float i = 0.3333333333333333;
static const float j = 0.6666666666666666;
static const float lrad = 0.015;
static const float trad = 0.06;
static const float fogv = 0.025;
static const float dmax = 20.0;
static const int rayiter = 60;
static const float wrap = 64.0;
static float3 L = normalize(float3(0.1, 1.0, 0.5));
static const float3 axis = float3(1.0, 1.0, 0.0);//vec3(1.0, 1.0, 1.0);
static const float3 tgt = float3(1.0, 1.7, 1.1);//vec3(-0.0, 0.3, -0.15);
static const float3 cpos = tgt + axis;
static const float3 vel = 0.2*axis;
static const float KEY_G = 71.5/256.0;

float hashThisShader(in float3 x) {
    return frac(87.3*dot(x, float3(0.1, 0.9, 0.7)));
}

float lineThisShader(in float3 p0, in float3 p1, in float3 p) {
    
    float3 dp0 = p-p0;
    float3 d10 = p1-p0;
    
    float u = clamp(dot(dp0, d10)/dot(d10, d10), -5.0, 5.0);
    return distance(lerp(p0, p1, u), p)-0.5*lrad;

}

float2 opU(float2 a, float2 b) {
    return a.x < b.x ? a : b;
}

float hueOf(float3 pos) {
    return cos( 2.0*dot(2.0*pos, float3(0.3, 0.7, 0.4)) ) * 0.49 + 0.5;
}

float3 round2(in float3 x, in float3 a) {
    return 2.0 * floor( 0.5 * (x + 1.0 - a) ) + a;
}

float4 pdist(float3 p, float3 q) {
    float3 pq = p-q;
    return float4(q, dot(pq,pq));
}

float4 pselect(float4 a, float4 b) {
    return a.w < b.w ? a : b;
}

float torus(in float3 a, in float3 b, in float3 pos) {
    pos -= 0.5*(a+b);
    float3 n = normalize(b-a);
    return distance(pos, r*normalize(pos - n*dot(n, pos))) - trad;
}

float4x4 permute(float3 e, float3 f, float3 g, float3 h, float p) {

    float4x4 mat4One = {float4(e,1.0), float4(f,1.0), float4(g, 1.0), float4(h, 1.0)}; 
    float4x4 mat4Two = {float4(e,1.0), float4(g,1.0), float4(f, 1.0), float4(h, 1.0)}; 
    float4x4 mat4Three = {float4(e,1.0), float4(h,1.0), float4(f, 1.0), float4(g, 1.0)};
    return (p < i ? mat4One :
            (p < j ? mat4Two :
             mat4Three));
}

float3 randomBasis(float p) {
    return (p < i ? float3(1.0, 0.0, 0.0) :
            p < j ? float3(0.0, 1.0, 0.0) :
            float3(0.0, 0.0, 1.0));
}

float3 randomPerp(float3 v, float p) {
    return (v.x>0.0 ? (p < 0.5 ? float3(0.0, 1.0, 0.0) : float3(0.0, 0.0, 1.0)) :
            v.y>0.0 ? (p < 0.5 ? float3(1.0, 0.0, 0.0) : float3(0.0, 0.0, 1.0)) :
            (p < 0.5 ? float3(1.0, 0.0, 0.0) : float3(0.0, 1.0, 0.0)));
}


float2 map(in float3 pos) {
        
    float3 orig = pos;
    
    pos = fmod(pos + fmod(TIME*vel, wrap), wrap);
        
    // a, b, c, d are octahedron centers
    // d, e, f, g are tetrahedron vertices
    float3 a = round2(pos, float3(1.0, 1.0, 1.0 ));
    float3 h = round2(pos, float3(0.0, 0.0, 0.0 ));
    
    float3 b = float3(a.x, h.y, h.z);
    float3 c = float3(h.x, a.y, h.z);
    float3 d = float3(h.x, h.y, a.z);
    
    float3 e = float3(h.x, a.y, a.z);
    float3 f = float3(a.x, h.y, a.z);
    float3 g = float3(a.x, a.y, h.z);

    // o is the closest octahedron center
    float3 o = pselect(pselect(pdist(pos, a), pdist(pos, b)),
                     pselect(pdist(pos, c), pdist(pos, d))).xyz;
    
    // t is the closest tetrahedron center
    float3 t = floor(pos) + 0.5;

    // normal points towards o
    // so bd is positive inside octahedron, negative inside tetrahedron
    float bd = dot(pos - o.xyz, (o.xyz-t.xyz)*2.0*i3) + i3; 

    float4x4 m = permute(e,f,g,h,hashThisShader(fmod(t, wrap)));
    
    float t1 = torus(m[0].xyz, m[1].xyz, pos);
    float t2 = torus(m[2].xyz, m[3].xyz, pos);
    
    float p = hashThisShader(fmod(o, wrap));
    float3 b1 = randomBasis(frac(85.17*p));
    float3 b2 = randomPerp(b1, frac(63.61*p+4.2));
    float3 b3 = randomPerp(b1, frac(43.79*p+8.3));

    float3 po = pos-o;
    
    float o1 = torus( b1,  b2, po);
    float o2 = torus( b1, -b2, po);
    float o3 = torus(-b1,  b3, po);
    float o4 = torus(-b1, -b3, po);
                         
    float2 noodle = float2(min(max(bd, min(t1,t2)),
                           max(-bd, min(min(o1, o2), min(o3, o4)))),
                           hueOf(orig+0.5*vel*TIME));
                            
        
        return noodle;
        
    
}

float3 hue(float h) {
    
    float3 c = fmod(h*6.0 + float3(2, 0, 4), 6.0);
    return h > 1.0 ? float3(0.5, 0.5, 0.5) : clamp(min(c, -c+4.0), 0.0, 1.0);
}

float2 castRay( in float3 ro, in float3 rd, in float maxd )
{
    float precis = 0.0001;
    float h=precis*2.0;
    float t = 0.0;
    float m = -1.0;
    for( int i=0; i<rayiter; i++ )
    {
        if( abs(h)<precis||t>maxd ) continue;//break;
        t += h;
        float2 res = map( ro+rd*t );
        h = res.x;
        m = res.y;
    }

    return float2( t, m );
}

float3 calcNormal( in float3 pos )
{
    float3 eps = float3( 0.0001, 0.0, 0.0 );
    float3 nor = float3(
        map(pos+eps.xyy).x - map(pos-eps.xyy).x,
        map(pos+eps.yxy).x - map(pos-eps.yxy).x,
        map(pos+eps.yyx).x - map(pos-eps.yyx).x );
    return normalize(nor);
}

float3 shade( in float3 ro, in float3 rd ) {
    float2 tm = castRay(ro, rd, dmax);
    if (tm.y >= 0.0) {
        float3 n = calcNormal(ro + tm.x * rd);
        float fog = exp(-tm.x*tm.x*fogv);
        float3 color = hue(tm.y) * 0.55 + 0.45;
        float3 diffamb = (0.5*dot(n,L)+0.5) * color;
        float3 R = 2.0*n*dot(n,L)-L;
        float spec = 0.2*pow(clamp(-dot(R, rd), 0.0, 1.0), 6.0);
        return fog * (diffamb + spec);
    } else {
        return float3(1.0, 1.0, 1.0);
    }
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

    const float yscl = 720.0;
    const float f = 900.0;
    
    float2 uv = coordinate * yscl;
    
    float3 up = float3(0.0, 1.0, 0.0);
    
    float3 rz = normalize(tgt - cpos);
    float3 rx = normalize(cross(rz,up));
    float3 ry = cross(rx,rz);
    
    float thetax = 0.0;
    float thetay = 0.0;
    
    float cx = cos(thetax);
    float sx = sin(thetax);
    float cy = cos(thetay);
    float sy = sin(thetay);
    
    float3x3 Rx = {1.0, 0.0, 0.0, 
                   0.0, cx, sx,
                   0.0, -sx, cx};
    
    float3x3 Ry = {cy, 0.0, -sy,
                   0.0, 1.0, 0.0,
                   sy, 0.0, cy};

    float3x3 R = {rx,ry,rz};
    float3x3 Rt = {rx.x, ry.x, rz.x,
                   rx.y, ry.y, rz.y,
                   rx.z, ry.z, rz.z};

    float3 rd = mul(mul(mul(R,Rx),Ry), normalize(float3(uv, f)));
    
    float3 ro = tgt + mul(mul(mul(mul(R,Rx),Ry),Rt),(cpos-tgt));

    float4 fragColor = float4(shade(ro, rd), 1.0);

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