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

#define ANIMATE

#define M_PI 3.1415926535897932384626433832795

//    0  1  2  3  4  5 
// R  1  1  0  0  0  1
// G  0  1  1  1  0  0
// B  0  0  0  1  1  1
float3 getHueColor(float2 pos)
{
	float theta = fmod(3.0 + 3.0 * atan2(pos.x, pos.y) / M_PI + TIME * 10.0, 6.0);
		
	float3 color = (0.0);
	
	return clamp(abs(fmod(theta + float3(0.0, 4.0, 2.0), 6.0) - 3.0) - 1.0, 0.0, 1.0);
}

		////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////
		////////////////////////////////////////////////////////



#define STEPS 50
#define EPSILON 0.001
#define FLAT_SHADING
#define BORDER_LINES
#define BORDER_THICKNESS 1.0
//#define FLAT_GROUND
#define TRIANGLE_BG

// iq's sSqdSegment and sdTriangle functions from: https://www.shadertoy.com/view/XsXSz4
// squared distance to a segment (and orientation)
float2 sSqdSegment( in float2 a, in float2 b, in float2 p )
{
	float2 pa = p - a;
	float2 ba = b - a;
	float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
	return float2( dot( pa-ba*h, pa-ba*h ), pa.x*ba.y-pa.y*ba.x );
}

// signed distance to a 2D triangle
float sdTriangle( in float2 v1, in float2 v2, in float2 v3, in float2 p )
{
	float2 d = min( min( sSqdSegment( v1, v2, p ), 
					   sSqdSegment( v2, v3, p )), 
				       sSqdSegment( v3, v1, p ));

	return -sqrt(d.x)*sign(d.y);
}

// background
float3 shadeBG( float2 uv, float sp )
{
	float sp2 = 0.0;
	// TRIANGLE BG
#ifdef TRIANGLE_BG
	float2 v2 = float2( -0.12, 0.1 );
	float2 v1 = float2( 0.12, 0.1 );
	float2 v3 = float2( 0.0,  0.4 );
	float vadd1 = sp * 0.1;
	float vadd2 = sp2;
	float triDist1 = sp;//min( 1.0, ( 300.0 * 
						//sdTriangle( v1 + float2( vadd1, -vadd1 ), v2 + float2( -vadd1, -vadd1 ), v3 + float2( 0.0, vadd1 ), uv ) ) );
	// if ( triDist1 < 0.0 ) // todo: optimize branch
	// {
	// 	triDist1 = pow( smoothstep( 0.0, 1.0, abs( triDist1 * 0.05 ) ), 0.1 );
	// }
	// triDist1 = max( 0.6, triDist1 );
	return float3( triDist1, triDist1, triDist1 ) *	 float3( 1.0, 0.95, 0.975 );
#else
	// SPHERE BG
	uv.x *= 1.0;
	uv.y -= 0.25;
	
	float3 color = float3( 1.0, 1.0, 1.0 );
	
	float l1 = pow( max( 0.0, min( 1.0, length( uv ) * 4.0 * ( 1.0 + sp  ) ) ), 10.0 );
	float l2 = pow( max( 0.0, min( 1.0, length( uv ) * 6.0 * ( 1.0 + sp2 ) ) ), 10.0 );
	return color * ( max( 1.0 - l1, l2 ) );
#endif

}

int triIsect( const float3   V1,  // Triangle vertices
			  const float3   V2,
			  const float3   V3,
			  const float3    O,  //Ray origin
			  const float3    D,  //Ray direction
			  out float res )
{
  float3 e1, e2;  //Edge1, Edge2
  float3 P, Q, T;
  float det, inv_det, u, v;
  float t;
 
  e1 = V2 - V1;
  e2 = V3 - V1;

  P = cross( D, e2 );
  det = dot( e1, P );
  if(det > -EPSILON && det < EPSILON) return 0;
  inv_det = 1.0 / det;
 
  T = O - V1;
 
  u = dot(T, P) * inv_det;
  if(u < 0. || u > 1.) return 0;
 
  Q = cross( T, e1 );
 
  v = dot(D, Q) * inv_det;
  if(v < 0. || u + v  > 1.) return 0;
 
  t = dot(e2, Q) * inv_det;
 
  if(t > EPSILON) { //ray intersection
    res = t;
    return 1;
  }
 
  return 0;
}

int triIsectNC( const float3   V1,  // Triangle vertices
			 	const float3   V2,
			 	const float3   V3,
			 	const float3    O,  //Ray origin
			 	const float3    D,  //Ray direction
			 	out float res )
{
  float3 e1, e2;  //Edge1, Edge2
  float3 P, Q, T;
  float det, inv_det, u, v;
  float t;
 
  e1 = V2 - V1;
  e2 = V3 - V1;

  P = cross( D, e2 );
  det = dot( e1, P );
  if(det > -EPSILON && det < EPSILON) return 0;
  inv_det = 1.0 / det;
 
  T = O - V1;
 
  u = dot(T, P) * inv_det;
 
  Q = cross( T, e1 );
 
  v = dot(D, Q) * inv_det;
 
  t = dot(e2, Q) * inv_det;
 
  if(t > EPSILON) { //ray intersection
    res = t;
    return 1;
  }
 
  return 0;
}

float3 polygonalGround( float3 pos, float zshift, float sp )
{
	float gridSize = 1.0;
	pos.z += zshift;
	float2 uv1 = floor( float2(pos.x , pos.z) );
	float2 uv2 = uv1 + float2( gridSize, gridSize );
	float um = 0.2;
	float tm = 200.0;
	float gtm = 10.0;

#ifdef FLAT_GROUND
	float h1 = 0.0;
	float h2 = 0.0;
	float h3 = 0.0;
	float h4 = 0.0;
#else
	float h1 = sin( gtm * TIME + tm * tex2D( _TextureChannel2, um * uv1 ).r );
	float h2 = sin( gtm * TIME + tm * tex2D( _TextureChannel2, um * float2( uv2.x, uv1.y ) ).r );
	float h3 = sin( gtm * TIME + tm * tex2D( _TextureChannel2, um * uv2 ).r );
	float h4 = sin( gtm * TIME + tm * tex2D( _TextureChannel2, um * float2( uv1.x, uv2.y ) ).r );
#endif
	
	float hm = 0.7 * max( 0.3, min( 1.0, -( uv1.y - 26.0 ) * 0.05 ) );
	float3 v1 = float3( uv1.x, h1 * hm + 5.0, uv1.y );
	float3 v2 = float3( uv2.x, h2 * hm + 5.0, uv1.y );
	float3 v3 = float3( uv2.x, h3 * hm + 5.0, uv2.y );
	float3 v4 = float3( uv1.x, h4 * hm + 5.0, uv2.y );
	float t1, t2, border1, border2;
	float3 ro = pos + float3( 0.0, 100.0, 0.0 );
	float3 rd = float3( 0.0, -1.0, 0.0 );
	int tri1res = triIsect( v1, v2, v3, ro, rd, t1 );
	int tri2res = triIsectNC( v1, v3, v4, ro, rd, t2 );
						 
	float h = 0.0;
	if ( tri1res == 1 )
	{
		float3 pt = ro + rd * t1;
		return ( pt );
	}
	float3 pt = ro + rd * t2;
	return pt;
}

float3 mapGround( float3 pos, float sp, float zshift )
{
	float3 res = polygonalGround( pos, zshift, sp );
	float h = res.y;

	return float3( pos.x, h * 1.5 - 4.0, pos.z );
}

float rayMarchGround( float3 ro, float3 rd, float sp, float zshift )
{
	float t = 0.0;
	for( int i = 0; i < STEPS; i++ )
	{
		float3 pt = ro + rd * t;
		float h = abs( pt.y - mapGround( pt, sp, zshift ).y );
		if ( h < 0.05 )
		{
			break;
		}
		t += 0.3 * h;
	}
	return t;
}

float intersectZPlane( float3 ro, float3 rd, float planeZ )
{
	return ( -ro.z + planeZ ) / rd.z;
}


// ground
float3 shadeGround( float3 eye, float3 pt, float3 norm, float3 normReflection, float3 light, float mult, float sp )
{
#ifdef FLAT_SHADING
	pt.xz = pt.xz - fmod( pt.xz, 1.0 ); // flat shading
#endif
	float3 r = normalize( reflect( light, norm ) );
	float3 eyeDir = normalize( pt - eye );
	float dotR = dot( r, eyeDir );
	float diffuseColor = max( 0.0, dotR );
	float ambientColor = 0.7;
	float3 groundColor = float3( diffuseColor + ambientColor, diffuseColor + ambientColor, diffuseColor + ambientColor );
	
	float3 rd = normalize( reflect( -eyeDir, normReflection ) );
	float t = intersectZPlane( pt, rd, 10.0 );
	float3 bgPos = pt + rd * t;
	
	float mixv = max( 0.0, dotR * 2.0 );
	float3 bgColor = shadeBG( abs( bgPos.xy ) * float2( 0.1, -0.2 ) + float2( 0.0,2.0 ), sp );
	return groundColor * mixv + bgColor * ( 1.0 - mixv );
}

float3 colorize( float2 uv )
{
	float3 ro = float3( 0.0, 7.0, 0.0 );
	float3 rd = float3( uv.x, uv.y - 0.4, 1.0 );
	rd = normalize( rd );

	float sp = 0.0;

	float zshift =  0.0;
	
	float t = rayMarchGround( ro, rd, sp, zshift );
	
	// directional light
	float3 lightDir = float3( sin( 0.0 ), 0.6, 0.0 ); 
	lightDir = normalize( lightDir );

	float3 color;
	{
		float3 pt = ro + rd * t;

		float eps = 0.001;
		float3 norm1 = float3( mapGround( pt + float3( eps, 0.0, 0.0 ), sp, zshift ).y - 
						   mapGround( pt, sp, zshift ).y,
						   0.005,
						   mapGround( pt + float3( 0.0, 0.0, eps ), sp, zshift ).y - 
						   mapGround( pt, sp, zshift ).y );
		norm1 = normalize( norm1 );
		float3 norm2 = normalize( norm1 + float3(0.0,8.0,0.0) );
		
		// border calculation
		float modx = abs( fmod( pt.x, 1.0 ) );
		float modz = abs( fmod( pt.z + zshift, 1.0 ) );
		float power = 60.0;
#ifdef BORDER_LINES
		float border = pow(     modx, power )				 // x axis border 
			         + pow( 1.0-modx, power )
			         + pow(     modz, power )				 // z axis border
			         + pow( 1.0-modz, power )				 //
					 + pow( 1.0-abs( modx - modz ), power ); // cross border
		border = max( 0.0, min( 1.0, border * BORDER_THICKNESS * sp ) );
#else
		float border = 0.0;
#endif
		
		float3 diffuseColor = float3( 1.0, 0.95, 0.90 );
		//float3 diffuseColor = float3( 1.0, 0.98, .95 );
		float3 color1 = shadeBG( uv, sp ) * diffuseColor;
		float3 color2 = shadeGround( ro, pt, norm1, norm2, lightDir, -1.0, sp ) * diffuseColor * ( 1.0 - border ) + float3( .5, .5, .5 ) * border;
		float mixv = pow( min( min( 1.0, max( 0.0, -abs( pt.x ) + 7.0 ) ), min( 1.0, max( 0.0, -abs( pt.z ) + 25.0 ) ) ), 2.0 );
		color = color1 * ( 1.0 - mixv ) + color2 * mixv;
	}

	return color;
}

float3 noiseGrain( float2 uv )
{

	float valueForFloat = tex2D( _TextureChannel2, uv * 20.0 + float2( 100.678, 100.317 ) ).r;
	return float3(valueForFloat, valueForFloat,valueForFloat) * 0.2;
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

	float2 uv 	 =  coordinateScale * 1.5;
	float2 mouse = 0.0;	
	float l = length(uv);
	float m = length(mouse);
	
	float4 fragColor = (0.0);

	if (l >= 0.75 && l <= 1.0)
	{
		l = 1.0 - abs((l - 0.875) * 8.0);
		l = clamp(l * 200.0 * 0.0625, 0.0, 1.0); // Antialiasing approximation
		
		fragColor = float4(l * getHueColor(uv), l);
	}

	else if (l < 0.75)
	{
		float3 pickedHueColor;
		
		mouse = float2(sin(TIME), cos(TIME));
		pickedHueColor = getHueColor(mouse);
		
		uv = uv / 0.75;
		mouse = normalize(mouse);
		
		float sat = 1.5 - (dot(uv, mouse) + 0.5); // [0.0,1.5]
		
		if (sat < 1.5)
		{
			float h = sat / sqrt(3.0);
			float2 om = float2(cross(float3(mouse, 0.0), float3(0.0, 0.0, 1.0)).xy);
			// float2 om = 0.0;
			float lum = dot(uv, om);
			
			if (abs(lum) <= h)
			{
				l = clamp((h - abs(lum)) * 200.0 * 0.5, 0.0, 1.0) * clamp((1.5 - sat) / 1.5 * 200.0 * 0.5, 0.0, 1.0); // Fake antialiasing

				float3 valueOperation = l * lerp(pickedHueColor, float3(0.5 * (lum + h) / h, 0.5 * (lum + h) / h, 0.5 * (lum + h) / h), sat / 1.5);
				
				fragColor = float4(valueOperation, l);
			}

		}
	}




	float2 uv2 = coordinate;
	uv2 -= float2( 0.5, 0.5 );
	float dist2 = ( 1.0 - length( uv2 - float2( 0.0, 0.25 ) ) * .5) * 1.0;
	float3 color2 = colorize( uv2 ) * dist2 - noiseGrain( uv2 );

	float4 fragColor2 = float4(color2,1.0);


	fragColor2 += fragColor;// smoothstep(0.0, fragColor2, fragColor);



///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS


float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor2);

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