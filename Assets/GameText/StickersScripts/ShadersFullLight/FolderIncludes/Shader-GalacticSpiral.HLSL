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


static const float RETICULATION = 3.;  // strenght of dust texture
static const float NB_ARMS = 2.;       // number of arms

static const float COMPR = .9;         // compression in arms
static const float SPEED = .1;
static const float GALAXY_R = 1./2.;
static const float BULB_R = 1./4.;
static const float3 GALAXY_COL = float3(.9,.9,1.); //(1.,.8,.5);
static const float3 BULB_COL   = float3(1.,.8,.8);
static const float3 SKY_COL    = .5*float3(.1,.3,.5);
		
#define Pi PI
static float t;
static float2 FragCoord;
static float4 FragColor;


#define ANIM true

static float scale = 1.;

// --- noise functions from https://www.shadertoy.com/view/XslGRr
// Created by inigo quilez - iq/2013
// License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.

//mat3 m = mat3( 0.00,  0.80,  0.60,
//              -0.80,  0.36, -0.48,
//              -0.60, -0.48,  0.64 );
static float3x3 m = {-0.60, -0.48,  0.64,
              		 -0.80,  0.36, -0.48,
               		 0.00,  0.80,  0.60 };

float hashGalactic( float n )
{
    return frac(sin(n)*43758.5453);
}

float noise( in float3 x )
{
    float3 p = floor(x);
    float3 f = frac(x);

    f = f*f*(3.0-2.0*f);

    float n = p.x + p.y*57.0 + 113.0*p.z;

    float res = lerp(lerp(lerp( hashGalactic(n+  0.0), hashGalactic(n+  1.0),f.x),
                          lerp( hashGalactic(n+ 57.0), hashGalactic(n+ 58.0),f.x),f.y),
                     lerp(lerp( hashGalactic(n+113.0), hashGalactic(n+114.0),f.x),
                          lerp( hashGalactic(n+170.0), hashGalactic(n+171.0),f.x),f.y),f.z);
    return res;
}

float fbm( float3 p )
{
    float f;
    f  = 0.5000*noise( p ); 

    p = mul(m, p*2.02);
    
    f += 0.2500*noise( p ); 

    p = mul(m, p*2.03);
    
    f += 0.1250*noise( p ); 

    p = mul(m, p*2.01);
    
    f += 0.0625*noise( p );

    return f;
}
// --- End of Created by inigo quilez


float fbmm( float3 p )
{
    float f;
    f  =    noise( p ); 

    p = mul(m, p*2.02);
    
    f *= 2.*noise( p ); 

    p = mul(m, p*2.03);
    
    f *= 2.*noise( p ); 

    p = mul(m, p*2.01);
    
    f *= 2.*noise( p ); 

    p = mul(m, p*2.03);
    
    f *= 2.*noise( p ); 

    p = mul(m, p*2.01);
    
    f *= 2.*noise( p );
    return f;
}

#define noises(p) (2.*noise(p)-1.)
float fbms( float3 p )
{
    float f;
    f  = 0.5000*noises( p ); 

    p = mul(m, p*2.02);
    
    f += 0.2500*noises( p ); 

    p = mul(m, p*2.03);
    
    f += 0.1250*noises( p ); 

    p = mul(m, p*2.01);
    
    f += 0.0625*noises( p );
    return f;
}

static float scaleW = 0.0;
#define warp1(p) p

float fbmms( float3 p )
{
    float f;
	p = p;
    f  = 1.+scale*noises( warp1(p) ); 
    
    p = mul(m, p*2.02);
    
    f *= 1.+scale*noises( warp1(p) ); 
    
    p = mul(m, p*2.03);
    
    f *= 1.+scale*noises( warp1(p) ); 
    
    p = mul(m, p*2.01);
    
    f *= 1.+scale*noises( warp1(p) ); 
    
    p = mul(m, p*2.03);
    
    f *= 1.+scale*noises( warp1(p) ); 
    
    p = mul(m, p*2.01);
    
    f *= 1.+scale*noises( warp1(p) );

    return .5*f;
}

float fbma( float3 p )
{
    float f;
    f  = 0.5000*abs(noise( p )); 

    p = mul(m, p*2.02);
    
    f += 0.2500*abs(noise( p )); 

    p = mul(m, p*2.03);

    f += 0.1250*abs(noise( p )); 
    
    p = mul(m, p*2.01);

    f += 0.0625*abs(noise( p ));

    return f;
}
float fbmsa( float3 p )
{
    float f;
    f  = 0.5000*abs(noises( p )); 

    p = mul(m, p*2.02);
    
    f += 0.2500*abs(noises( p )); 

    p = mul(m, p*2.03);
    
    f += 0.1250*abs(noises( p )); 

    p = mul(m, p*2.01);
    
    f += 0.0625*abs(noises( p ));
    return f;
}
float fbmsma( float3 p )
{
    float f;
    f  = 0.5000*(1.-abs(noises( p ))); 

    p = mul(m, p*2.02);
    
    f += 0.2500*(1.-abs(noises( p ))); 

    p = mul(m, p*2.03);
    
    f += 0.1250*(1.-abs(noises( p ))); 

    p = mul(m, p*2.01);
    
    f += 0.0625*(1.-abs(noises( p )));
    return f;
}

// gives a random 1..N integer for index i
#define rnd(i,N) int(1.+float(N)*hashGalactic(float(i)))

// gives a random 1..N integer every T seconds, starting at i.
#define rndT(i,N,T) int(1.+float(N)*hashGalactic(float(i)+floor(t/(T))))

float2 noise2_2( float2 p )     // 2 noise channels from 2D position
{
	float3 pos = float3(p,.5);
	if (ANIM) pos.z += t;
	pos = mul(m, pos);
    float fx = noise(pos);
    float fy = noise(pos+float3(1345.67,0,45.67));
    return float2(fx,fy);
}



//////////////////////////////////////////////////////////////////////////////////////////////
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////

fixed4 FRAGMENTSHADER (pixelPoints PIXELSPACE) : SV_Target
{
	float2 coordinateSprite = PIXELSPACE.uv2;
	float2 coordinate = PIXELSPACE.uv;
	
	float2 coordinateScale = (PIXELSPACE.uv - 0.5) * 2.0 ;
	
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
	
	t = TIME * 20.0;
	FragColor = 0.0;	
	float2 uv = coordinate;
	uv = 2.*(uv - float2(1.0,1.0));

	
	float3 pos = float3(5.*uv,0.5);
	float3 col;
	float shape, noiz;

	// spiral stretching with distance
	float rho = length(uv); // polar coords
	float ang = atan2(uv.y,uv.x);
	float shear = 2.*log(rho); // logarythmic spiral
	float c = cos(shear), s=sin(shear);
	float2x2 R = {c,-s,s,c};

	// galaxy profile
	float r; // disk
	r = rho/GALAXY_R; float dens = exp(-r*r);
	r = rho/BULB_R;	  float bulb = exp(-r*r);
	float phase = NB_ARMS*(ang-shear);
	// arms = spirals compression
	//ang = ang-.5*COMPR*cos(phase)+SPEED*t; // .1/rho
    ang = ang-COMPR/NB_ARMS*cos(phase)+SPEED*t;
	uv = rho*float2(cos(ang),sin(ang));
	// stretched texture must be darken by d(new_ang)/d(ang)
	float spires = 1.+NB_ARMS*COMPR*sin(phase);
	// pires = mix(1.,sin(phase),ARM);

	dens = 1.+COMPR*sin(phase);
	//dens *= .7*spires;	
	
	// gaz texture
	float gaz = fbmms(float3(100.*.09*1.2* mul(R,uv),0.5));
	float gaz_trsp = pow(clamp(1.-gaz*dens,0.,1.),2.);
	float3 stars = (0.);
	

	col = float3(1.-dens*gaz,1.-dens*gaz,1.-dens*gaz);
		
	
	
	
	float4 fragColor = float4(col,1.0);

///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);
// float4 outputSmoothed = 0.0;
float4 colBackground = outputSmoothed;
// float4 colBackground = 0.0;
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
        float4 colorOutputTotal = ColorSign(dSign, colBackground, _BorderColor, _BorderSizeOne, _BorderSizeTwo, _BorderBlurriness); 
    
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
    	float4 colorOutputTotal = ColorSign(dSign, float4(0.0, 0.0, 0.0, 0.0), _BorderColor, _BorderSizeOne, _BorderSizeTwo, _BorderBlurriness); 
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