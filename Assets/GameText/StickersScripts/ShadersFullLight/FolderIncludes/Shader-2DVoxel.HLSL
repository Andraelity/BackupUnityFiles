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
#define NUM_LAYERS 46.

#define time TIME

float3x3 rotXY( float2 angle ) 
{
	float2 c = cos( angle );
	float2 s = sin( angle );	
	
	float3x3 mat3 = {	c.y      ,  0.0, -s.y,
					 	s.y * s.x,  c.x,  c.y * s.x,
					 	s.y * c.x, -s.x,  c.y * c.x	};

	return mat3;
}

float map( float3 p ) 
{
	//vec3 q = mod(pos,c)-0.5*c;
	p = mul(rotXY(float2(time*.9,time*0.6)), p);
	const float w = 1.5;
	float3 q = abs(p);
	float d = max(max(q.z,q.y),q.x*0.15)-w;
	q*= .7;
	d = min(d,max(max(q.z,q.x),q.y*0.15))-w;
	q*= .5;
	d = min(d,max(max(q.x,q.y),q.z*0.15))-w;
	
	return d;
}


static float3 lgt;

//modified from iq's "Hexagons - distance" (https://www.shadertoy.com/view/Xd2GR3)
//return values: x = trigger, y = voxel shading, z = distance to voxel edge, w = lighting
float4 voxelize( float2 p, float bias ) 
{
	//displace based on layer
	p.x += 0.866025*bias;
	p.y += 0.5*bias;
	
	//setup coord system
	float2 q = float2( p.x*2.0*0.5773503, p.y + p.x*0.5773503 );
	float2 pi = floor(q);
	float2 pf = frac(q);

	float v = fmod(pi.x + pi.y, 3.0);

	float ca = step(1.0,v);
	float cb = step(2.0,v);
	float2  ma = step(pf.xy,pf.yx);
	
    // distance to borders
	float2 bz = 1.0-pf.yx + ca*(pf.x+pf.y-1.0) + cb*(pf.yx-2.0*pf.xy);
	float e = dot( ma, bz );	
	
	//voxel shading
	float top = cb*ma.y+clamp((1.-(ca+ma.y)),0.,1.);
	float left = 0.5+step(ca,cb)*0.75;
	
	float2 j = pi + ca - cb*ma;
	float sdf = map(float3(j,bias));
	
	//faked light (using the normal only, actual lighting gets pretty heavy)
	float nl = max(dot(normalize(lgt),normalize(float3(j,bias))),0.);
	return float4( step(sdf,.01),left+top, e, nl*2. );
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

  lgt =  float3(4.+sin(time*0.4)*20.,4.+sin(time*.1)*10.,5.);
    float2 p = coordinate/2  - 0.25;
	float2 bp = p;
	p *= 100.+sin(time*0.5)*80.;
	p.x += sin(time)*10.;
	p.y += cos(time*0.9+0.5)*4.;
	
	float st = sin(time*0.2)*0.3+1.;
	float3 col = (0.);
	for (float i=-NUM_LAYERS/2.;i<NUM_LAYERS/2.;i++) 
	{
		float4 rz = voxelize(p,i);
		//vec3 colx = (.95 + 0.8*sin( hash11(rz.x+i)*1.5 + 2.0 + vec3(1.5+i*0.2, st*1.5+i*0.1, 1.0+i*0.01) ))*rz.x*4.;
		float3 colx = float3(0.15+cos(time+0.1)*0.1,0.2,0.3+sin(time)*0.1)*rz.x*3.;
		//borders
		//colx *= smoothstep(0., 0.1, rz.z);
		//simple shading
		colx *= .4+rz.z*.9;
		//voxel shading
		colx *= rz.y*.5;
		//faked lighting
		colx *= rz.w*0.5+0.5;
		//painter's algo
		col = col*step(colx,float3(0.0, 0.0, 0.0))+colx;
		//max blending (transparency! :P)
		//col += colx*0.4;
	}
	
	float d = distance(lgt,float3(p,1.));
	col += 1.-smoothstep(0.92,1.,(d*.24));
	
	col *= 1.3;

	float4 fragColor = float4( col, (col.x + col.y + col.z)/3.0 );

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