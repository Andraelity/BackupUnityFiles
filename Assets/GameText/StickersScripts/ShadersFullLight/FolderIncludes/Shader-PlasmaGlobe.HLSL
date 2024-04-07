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

#define NUM_RAYS float2(13.0, 0.0)

#define VOLUMETRIC_STEPS 19

#define MAX_ITER 35
#define FAR 6.

#define time TIME*1.1


float2x2 mm2(float a)
{
    float c = cos(a);
    float s = sin(a);
    float2x2 matOut = {c,-s,s,c};
    return matOut;
}

float noise( in float x ){return tex2D(_TextureChannel0, float2(x*.01,1.)).x;}

float hashThisShader( float n ){return frac(sin(n)*43758.5453);}

float noise(in float3 p)
{
    float3 ip = floor(p);
    float3 fp = frac(p);
    fp = fp*fp*(3.0-2.0*fp);
    
    float2 tap = (ip.xy+float2(37.0,17.0)*ip.z) + fp.xy;
    float2 rg = tex2D( _TextureChannel0, (tap + 0.5)/256.0).yx;
    return lerp(rg.x, rg.y, fp.z);
}

static float3x3 m3 = { 0.00,  0.80,  0.60,
                      -0.80,  0.36, -0.48,
                      -0.60, -0.48,  0.64 };


//See: https://www.shadertoy.com/view/XdfXRj
float flow( float3 p,  float t)
{
    float z=2.;
    float rz = 0.;
    float3 bp = p;
    for (float i= 1.;i < 5.;i++ )
    {
        p += time*.1;
        rz+= (sin(noise(p+t*0.8)*6.)*0.5+0.5) /z;
        p = lerp(bp,p,0.6);
        z *= 2.;
        p *= 2.01;
        p = mul( m3, p);
    }
    return rz;  
}

//could be improved
float sins(in float x)
{
    float rz = 0.;
    float z = 2.;
    for (float i= 0.;i < 3.;i++ )
    {
        rz += abs(frac(x*1.4)-0.5)/z;
        x *= 1.3;
        z *= 1.15;
        x -= time*.65*z;
    }
    return rz;
}

float segm( float3 p, float3 a, float3 b)
{
    float3 pa = p - a;
    float3 ba = b - a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1. );  
    return length( pa - ba*h )*.5;
}


float3 path(in float i, in float d)
{
    float3 en = float3(0.,0.,20.0);
    float sns2 = sins(d + i * 2.0 )*0.5;
    float sns =  sins(d + i * 2.0) *0.5;
    en.xz = mul(mm2((hashThisShader(i * 10.0)-0.5) * 1.0 + sns2),en.xz +  2.24*abs(sin(TIME))); //700.0;
    en.xy = 0.25*mul(mm2((hashThisShader(i * 2.0 )-0.5) * 5.0 + sns ),en.xy +  3.683*abs(sin(TIME))); //100.0;
    return en;
}


float2 map(float3 p, float i)
{
    float lp = length(p);
    float3 bg = (0.0, 0.0, 0.0);   
    float3 en = path(i,lp);
    
    float ins = smoothstep(0.11,.46,lp);
    float outs = .15+smoothstep(.0,.15,abs(lp-1.));
    p *= ins*outs;
    float id = ins*outs;
    
    float rz = segm(p, bg, en)-0.011;
    return float2(rz,id);
}


float march(in float3 ro, in float3 rd, in float startf, in float maxd, in float j)
{
    float precis = 0.001;
    float h=0.5;
    float d = startf;
    for( int i=0; i<MAX_ITER; i++ )
    {
        if( abs(h)<precis||d>maxd ) break;
        d += h*1.2;
        float res = map(ro+rd*d, j).x;
        h = res;
    }
    return d;
}


float3 vmarch(in float3 ro, in float3 rd,  in float j,  in float3 orig)
{   
    float3 p = ro;
    float2 r = (0.0);
    float3 sum = (0.0);
    float w = 0.0;
    for( int i=0; i<VOLUMETRIC_STEPS; i++ )
    {
        r = map(p,j);
        p += rd*.03;
        float lp = length(p);
        
        float3 col = sin(float3(0.1,0.5,0.2)*10.94+r.y)*.85+0.4;
        col.rgb *= smoothstep(.0,.015,-r.x);
        col *= smoothstep(0.04,.2,abs(lp-1.1));
        col *= smoothstep(0.1,.34,lp);
        // sum += abs(col)*5. * (1.2-noise(lp*2.+j*13.+time*5.)*1.1) / (log(distance(p,orig)-2.)+.75);
        sum += abs(col)*5. * (1.2-0.7*1.1) / (log(distance(p,orig)-2.)+.75);
    }
    return sum;
}


float2 iSphere2(in float3 ro, in float3 rd)
{
    float3 oc = ro;
    float b = dot(oc, rd);
    float c = dot(oc,oc) - 1.;
    float h = b*b - c;
    if(h <0.0) return float2(-1.0, -1.0);
    else return float2((-b - sqrt(h)), (-b + sqrt(h)));
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
    float2 p = coordinate -0.5;
    float2 um = 0.0;
    //camera
    float3 ro = float3(0.,0.,6.9);
    float3 rd = normalize(float3(p*.7,-1.5));
    float2x2 mx = mm2(time*.4+um.x*6.);
    float2x2 my = mm2(time*0.3+um.y*6.); 
    // ro.xz = mul( mx, ro.xz );
    // rd.xz = mul( mx, rd.xz );
    // ro.xy = mul( my, ro.xy );
    // rd.xy = mul( my, rd.xy );
    
    float3 bro = ro;
    float3 brd = rd;
    
    float3 col = float3(0.0125,0.,0.025);
    // #if 1
    for (float j = 1.;j<14+1.;j++)
    {
        ro = bro;
        rd = brd;
        float2x2 mm = mm2(TIME);
        // ro.xy = mul( mm, ro.xy );
        // rd.xy = mul( mm, rd.xy );
        // ro.xz = mul( mm, ro.xz );
        // rd.xz = mul( mm, rd.xz );
        float rz = march(ro,rd,2.5,FAR,j);
        if ( rz >= FAR)continue;
        float3 pos = ro+rz*rd;
        col = max(col,vmarch(pos,rd,j, bro));
    }
    // #endif
    
    ro = bro;
    rd = brd;
    float2 sph = iSphere2(ro,rd);
    
    if (sph.x > 0.)
    {
        float3 pos = ro+rd*sph.x;
        float3 pos2 = ro+rd*sph.y;
        float3 rf = reflect( rd, pos );
        float3 rf2 = reflect( rd, pos2 );
        float nz = (-log(abs(flow(rf*1.2,time)-.01)));
        float nz2 = (-log(abs(flow(rf2*1.2,-time)-.01)));
        col += (nz*nz* float3(0.12,0.12,.2) + 0.5*nz2*nz2*float3(0.24,0.2,.24));
    }
    
    float4 fragColor = float4(col*1.4, ((col.x + col.y + col.z )*1.0)/ 3.0);

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