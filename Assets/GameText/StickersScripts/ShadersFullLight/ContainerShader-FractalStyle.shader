Shader "ShaderBloom/ContainerShader-FractalStyle"
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

float3 sunDir  = normalize( float3(  0.35, 0.1,  0.3 ) );
const float3 sunColour = float3(1.0, .95, .8);


#define SCALE 2.8
#define MINRAD2 .25
static float minRad2 = clamp(MINRAD2, 1.0e-9, 1.0);
#define scale (float4(SCALE, SCALE, SCALE, abs(SCALE)) / minRad2)
static float absScalem1 = abs(SCALE - 1.0);
static float AbsScaleRaisedTo1mIters = pow(abs(SCALE), float(1-10));
static float3 surfaceColour1 = float3(.8, .0, 0.);
static float3 surfaceColour2 = float3(.4, .4, 0.5);
static float3 surfaceColour3 = float3(.5, 0.3, 0.00);
static float3 fogCol = float3(0.4, 0.4, 0.4);
static float gTime;


//----------------------------------------------------------------------------------------
float Noise( in float3 x )
{
    float3 p = floor(x);
    float3 f = frac(x);
    f = f*f*(3.0-2.0*f);
    
    float2 uv = (p.xy+float2(37.0,17.0)*p.z) + f.xy;
    float2 rg = tex2D( _TextureChannel0, (uv+ 0.5)/256.0 ).yx;
    return lerp( rg.x, rg.y, f.z );
}

//----------------------------------------------------------------------------------------
float Map(float3 pos) 
{
    
    float4 p = float4(pos,1);
    float4 p0 = p;  // p.w is the distance estimate

    for (int i = 0; i < 9; i++)
    {
        p.xyz = clamp(p.xyz, -1.0, 1.0) * 2.0 - p.xyz;

        float r2 = dot(p.xyz, p.xyz);
        p *= clamp(max(minRad2/r2, minRad2), 0.0, 1.0);

        // scale, translate
        p = p*scale + p0;
    }
    return ((length(p.xyz) - absScalem1) / p.w - AbsScaleRaisedTo1mIters);
}

//----------------------------------------------------------------------------------------
float3 Colour(float3 pos, float sphereR) 
{
    float3 p = pos;
    float3 p0 = p;
    float trap = 1.0;
    
    for (int i = 0; i < 6; i++)
    {
        
        p.xyz = clamp(p.xyz, -1.0, 1.0) * 2.0 - p.xyz;
        float r2 = dot(p.xyz, p.xyz);
        p *= clamp(max(minRad2/r2, minRad2), 0.0, 1.0);

        p = p*scale.xyz + p0.xyz;
        trap = min(trap, r2);
    }
    // |c.x|: log final distance (fractional iteration count)
    // |c.y|: spherical orbit trap at (0,0,0)
    float2 c = clamp(float2( 0.3333*log(dot(p,p))-1.0, sqrt(trap) ), 0.0, 1.0);

    float t = fmod(length(pos) - gTime*150., 16.0);
    surfaceColour1 = lerp( surfaceColour1, float3(.4, 3.0, 5.), pow(smoothstep(0.0, .3, t) * smoothstep(0.6, .3, t), 10.0));
    return lerp(lerp(surfaceColour1, surfaceColour2, c.y), surfaceColour3, c.x);
}


//----------------------------------------------------------------------------------------
float3 GetNormal(float3 pos, float distance)
{
    distance *= 0.001+.0001;
    float2 eps = float2(distance, 0.0);
    float3 nor = float3(
        Map(pos+eps.xyy) - Map(pos-eps.xyy),
        Map(pos+eps.yxy) - Map(pos-eps.yxy),
        Map(pos+eps.yyx) - Map(pos-eps.yyx));
    return normalize(nor);
}

//----------------------------------------------------------------------------------------
float GetSky(float3 pos)
{
    pos *= 2.3;
    float t = Noise(pos);
    t += Noise(pos * 2.1) * .5;
    t += Noise(pos * 4.3) * .25;
    t += Noise(pos * 7.9) * .125;
    return t;
}

//----------------------------------------------------------------------------------------
float BinarySubdivision(in float3 rO, in float3 rD, float2 t)
{
    float halfwayT;
  
    for (int i = 0; i < 6; i++)
    {

        halfwayT = dot(t, float2(0.5, 0.5));
        float d = Map(rO + halfwayT*rD); 
        //if (abs(d) < 0.001) break;
        t = lerp(float2(t.x, halfwayT), float2(halfwayT, t.y), step(0.0005, d));

    }

    return halfwayT;
}

//----------------------------------------------------------------------------------------
float2 Scene(in float3 rO, in float3 rD, in float2 fragCoord)
{
    float t = .05 + 0.05 * tex2D(_TextureChannel0, fragCoord.xy / 256).y;
    float3 p = (0.0);
    float oldT = 0.0;
    bool hit = false;
    float glow = 0.0;
    float2 dist;
    for( int j=0; j < 100; j++ )
    {
        if (t > 12.0) break;
        p = rO + t*rD;
       
        float h = Map(p);
        
        if(h  <0.0005)
        {
            dist = float2(oldT, t);
            hit = true;
            break;
        }
        glow += clamp(.05-h, 0.0, .4);
        oldT = t;
        t +=  h + t*0.001;
    }
    if (!hit)
        t = 1000.0;
    else       t = BinarySubdivision(rO, rD, dist);
    return float2(t, clamp(glow*.25, 0.0, 1.0));

}

//----------------------------------------------------------------------------------------
float Hash(float2 p)
{
    return frac(sin(dot(p, float2(12.9898, 78.233))) * 33758.5453)-.5;
} 


//----------------------------------------------------------------------------------------
float3 PostEffects(float3 rgb, float2 xy)
{
    // Gamma first...
    

    // Then...
    #define CONTRAST 1.08
    #define SATURATION 1.5
    #define BRIGHTNESS 1.5
    rgb = lerp(float3(0.5, 0.5, 0.5), lerp(float3(dot(float3(.2125, .7154, .0721), rgb*BRIGHTNESS), dot(float3(.2125, .7154, .0721), rgb*BRIGHTNESS), dot(float3(.2125, .7154, .0721), rgb*BRIGHTNESS)), rgb*BRIGHTNESS, SATURATION), CONTRAST);
    // Noise...
    //rgb = clamp(rgb+Hash(xy*iTime)*.1, 0.0, 1.0);
    // Vignette...
    rgb *= .5 + 0.5*pow(20.0*xy.x*xy.y*(1.0-xy.x)*(1.0-xy.y), 0.2); 

    rgb = pow(rgb, float3(0.47, 0.47, 0.47));
    return rgb;
}

//----------------------------------------------------------------------------------------
float Shadow( in float3 ro, in float3 rd)
{
    float res = 1.0;
    float t = 0.05;
    float h;
    
    for (int i = 0; i < 8; i++)
    {
        h = Map( ro + rd*t );
        res = min(6.0*h / t, res);
        t += h;
    }
    return max(res, 0.0);
}

//----------------------------------------------------------------------------------------
float3x3 RotationMatrix(float3 axis, float angle)
{
    axis = normalize(axis);
    float s = sin(angle);
    float c = cos(angle);
    float oc = 1.0 - c;
    
    float3x3 matOut = 
    {
                oc * axis.x * axis.x + c,           oc * axis.x * axis.y - axis.z * s,  oc * axis.z * axis.x + axis.y * s,
                oc * axis.x * axis.y + axis.z * s,  oc * axis.y * axis.y + c,           oc * axis.y * axis.z - axis.x * s,
                oc * axis.z * axis.x - axis.y * s,  oc * axis.y * axis.z + axis.x * s,  oc * axis.z * axis.z + c
    };

    return matOut;
}

//----------------------------------------------------------------------------------------
float3 LightSource(float3 spotLight, float3 dir, float dis)
{
    float g = 0.0;
    if (length(spotLight) < dis)
    {
        float a = max(dot(normalize(spotLight), dir), 0.0);
        g = pow(a, 500.0);
        g +=  pow(a, 5000.0)*.2;
    }
   
    return float3(0.6, 0.6, 0.6) * g;
}

//----------------------------------------------------------------------------------------
float3 CameraPath( float t )
{
    float3 p = float3(-.78 + 3. * sin(2.14*t),.05+2.5 * sin(.942*t+1.3),.05 + 3.5 * cos(3.594*t) );
    return p;
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

    float m = 0.0;
    gTime = (TIME+m)*.01 + 15.00;
    float2 xy = coordinate;
    float2 uv = (-1.0 + 2.0 * xy) ;
    
    
    #ifdef STEREO
    float isRed = fmod(fragCoord.x + fmod(fragCoord.y, 2.0),2.0);
    #endif

    float3 cameraPos    = CameraPath(gTime);
    float3 camTar       = CameraPath(gTime + .01);

    float roll = 13.0*sin(gTime*.5+.4);
    float3 cw = normalize(camTar-cameraPos);

    float3 cp = float3(sin(roll), cos(roll),0.0);
    float3 cu = normalize(cross(cw,cp));

    float3 cv = normalize(cross(cu,cw));
    cw = mul(RotationMatrix(cv, sin(-gTime*20.0)*.7) , cw);
    float3 dir = normalize(uv.x*cu + uv.y*cv + 1.3*cw);

    #ifdef STEREO
    cameraPos += .008*cu*isRed; // move camera to the right
    #endif

    float3 spotLight = CameraPath(gTime + .03) + float3(sin(gTime*18.4), cos(gTime*17.98), sin(gTime * 22.53))*.2;
    float3 col = 0.0;
    float3 sky = float3(0.03, .04, .05) * GetSky(dir);
    float2 ret = Scene(cameraPos, dir,coordinate);
    
    if (ret.x < 900.0)
    {
        float3 p = cameraPos + ret.x*dir; 
        float3 nor = GetNormal(p, ret.x);
        
        float3 spot = spotLight - p;
        float atten = length(spot);

        spot /= atten;
        
        float shaSpot = Shadow(p, spot);
        float shaSun = Shadow(p, sunDir);
        
        float bri = max(dot(spot, nor), 0.0) / pow(atten, 1.5) * .25;
        float briSun = max(dot(sunDir, nor), 0.0) * .2;
        
       col = Colour(p, ret.x);
       col = (col * bri * shaSpot) + (col * briSun* shaSun);
        
       float3 ref = reflect(dir, nor);
       col += pow(max(dot(spot,  ref), 0.0), 10.0) * 2.0 * shaSpot * bri;
       col += pow(max(dot(sunDir, ref), 0.0), 10.0) * 2.0 * shaSun * briSun;
    }
    
    col = lerp(sky, col, min(exp(-ret.x+1.5), 1.0));
    col += float3(pow(abs(ret.y), 2.), pow(abs(ret.y), 2.), pow(abs(ret.y), 2.)) * float3(.02, .04, .1);

    col += LightSource(spotLight-cameraPos, dir, ret.x);
    col = PostEffects(col, xy); 

    
    #ifdef STEREO   
    col *= float3( isRed, 1.0-isRed, 1.0-isRed );   
    #endif
    
    float4 fragColor=float4(col,1.0);
    fragColor= smoothstep(0.0, 1.0, fragColor);


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