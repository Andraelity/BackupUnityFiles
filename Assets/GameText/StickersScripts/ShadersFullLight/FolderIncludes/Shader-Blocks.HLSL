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


#define ANTIALIAS 2

#define ANIMATE


float hash1( float n ) { return frac(sin(n)*43758.5453); }
float2  hash2( float2  p ) { p = float2( dot(p, float2(127.1,311.7)), dot(p,float2(269.5,183.3)) ); return frac(sin(p)*43758.5453); }

float noise( in float2 x )
{
    float2 p = floor(x);
    float2 f = frac(x);
    f = f*f*(3.0-2.0*f);
    float n = p.x + p.y*57.0;
    return lerp(lerp( hash1(n+  0.0), hash1(n+  1.0),f.x),
               lerp( hash1(n+ 57.0), hash1(n+ 58.0),f.x),f.y);
}

float3 texturef( in float2 p )
{
	float2 q = p;
	p = p*float2(6.0,128.0);
	float f = 0.0;
    f += 0.500*noise( p ); 
    p = p*2.02;
    f += 0.250*noise( p ); 
    p = p*2.03;
    f += 0.125*noise( p ); 
    p = p*2.01;
	f /= 0.875;
	
	float3 col = 0.6 + 0.4*sin( f*2.5 + 1.0 + float3(0.0,0.5,1.0) );
	col *= 0.7 + 0.3*noise( 8.0*q.yx );
	col *= 0.8 + 0.2*clamp(2.0*noise(256.0*q.yx ),0.0,1.0);
    col *= float3(1.0,0.65,0.5) * 0.85;
    return col;

}

float4 voronoi( in float2 x, out float2 resUV, out float resOcc )
{
    float2 n = floor( x );
    float2 f = frac( x );

	float2 uv = (0.0);
	float4 m =  ( 8.0 );
	float m2 = 9.0;
    for( int j=-2; j<=2; j++ )
    for( int i=-2; i<=2; i++ )
    {
        float2 g = float2( float(i),float(j) );
        float2 o = hash2( n + g );
		#ifdef ANIMATE
        o = 0.5 + 0.5*sin( 0.5*TIME + 6.2831*o );
        #endif	
		float2 r = g - f + o;

        // distance and tex coordinates		
        float2 u = float2( dot( r, float2(0.5, 0.866) ), 
					   	   dot( r, float2(0.5,-0.866) ) );
		float2 d = float2( -r.y, 1.0 );
		float h = 0.5*abs(r.x)+0.866*r.y;
		if( h > 0.0 ) 
		{ 
			u = float2( h, r.x );
			d = float2( 0.866*abs(r.x)+0.5*r.y, 0.5*step(0.0,r.x) ); 
		}
		
        if( d.x<m.x )
        {
			m2 = m.x;
            m.x = d.x;
            m.y = dot(n+g, float2(7.0,113.0) );
			m.z = d.y;
			m.w = max(r.y,0.0);
			uv = u;
        }
        else if( d.x<m2 )
		{
			m2 = d.x;
        }
			
    }
	resUV = uv;
	resOcc = m2-m.x;
    return m;
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

	float3 tot = (0.0);
    
    #ifdef ANTIALIAS
    for( int j=0; j<ANTIALIAS; j++ )
    for( int i=0; i<ANTIALIAS; i++ )
    {
        float2 off = float2(float(i),float(j))/float(ANTIALIAS);
    #else
        float2 off = float2(0.0);
    #endif        
		
        float2  q = coordinate;
        float2 p = 4.0 *q;
        
        float2 uv = (0.0);

        //vec2 dp = 2.0*vec2( 1.0/iResolution.y, 0.0 );
        float2 dp = float2( 0.004, 0.0 );

        float occ = 0.0;
        float4  c = voronoi( 3.5*p, uv, occ );


        float2 tmp; float tmp2;
        float d = abs(voronoi( 3.5*(p+dp.xy), tmp, tmp2 ).z - voronoi( 3.5*(p-dp.xy), tmp, tmp2 ).z ) + 
                  abs(voronoi( 3.5*(p+dp.yx), tmp, tmp2 ).z - voronoi( 3.5*(p-dp.yx), tmp, tmp2 ).z );

        // color
        c.y = hash1( c.y );
        float3 col = 0.6 + 0.4*sin( c.y*2.5 + 1.0+ float3(0.0,0.5,1.0) );
        col *= 0.4 + 0.6*smoothstep( 0.1,0.25,abs(hash1(c.y+0.413)-0.5) );

        // texture	
        col *= 1.7*pow(texturef( uv ), float3(0.4, 0.4, 0.4) );

        // lighting
        col *= clamp( 0.65 + c.z*0.35, 0.0, 1.0 );
        col *= sqrt(clamp(1.0 - c.x,0.0,1.0));
        col *= clamp(1.0-0.3*c.w, 0.0, 1.0 );

        float operationFloat= sqrt(clamp(8.0*occ,0.0,1.0));
        col *= 0.6 + 0.4 * float3( operationFloat, operationFloat, operationFloat );

        // patern
        if( hash1(c.y)>0.6 )
        {
        	float pa = sin(c.w+(1.0-0.7*c.y)*25.0*uv.y)*sin((1.0-0.7*c.y)*25.0*uv.x);
        	col *= smoothstep( 0.0, 0.3, abs(pa-0.6) );
        	col *= 1.0 - 0.35*smoothstep( 0.6, 0.7, pa );
        }

        // wireframe	
        col *= 1.0 - d;

        // tint 
        col = pow( col, float3(1.0,1.0,0.8) );	

        // vigneting	
        col *= 0.7 + 0.3*pow( 16.0*q.x*q.y*(1.0-q.x)*(1.0-q.y), 0.2 );

        tot += col;
    }
    
    #ifdef ANTIALIAS
    tot /= float(ANTIALIAS*ANTIALIAS);
	#endif
    
	float4 fragColor = float4( tot, 1.0 );

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