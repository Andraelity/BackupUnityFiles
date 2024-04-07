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

// ray marching
static const int max_iterations = 128;
static const float stop_threshold = 0.001;
static const float grad_step = 0.01;
static const float clip_far = 1000.0;
static const int   ao_iterations = 5;
static const float ao_step = 0.2;
static const float ao_scale = 1.46;
static const float DEG_TO_RAD = PI / 180.0;
static const float GOLDEN = 1.6180339887499;
static const float r = 1.0 * 2.0 * 1.414214 / 1.732051;
static const float2 h = float2( r, 0.01 );
static const float2 h2 = h * float2( 0.73, 4.0 );
static const float3 cc0 = float3( 0.333333, -0.333333, -0.333333 );
static const float3 cx0 = float3( 0.707107, 0.000000, 0.707107 );
static const float3 cy0 = float3( 0.408248, 0.816496, -0.408248 );
static const float3 cz0 = float3( -0.577350, 0.577350, 0.577350 );
static const float3 cc1 = float3( -0.333333, -0.333333, 0.333333 );
static const float3 cx1 = float3( 0.707107, 0.000000, 0.707107 );
static const float3 cy1 = float3( -0.408248, 0.816496, 0.408248 );
static const float3 cz1 = float3( -0.577350, -0.577350, 0.577350 );
static const float3 cc2 = float3( -0.333333, 0.333333, -0.333333 );
static const float3 cx2 = float3( 0.707107, 0.707107, 0.000000 );
static const float3 cy2 = float3( -0.408248, 0.408248, 0.816496 );
static const float3 cz2 = float3( 0.577350, -0.577350, 0.577350 );
static const float3 cc3 = float3( 0.333333, 0.333333, 0.333333 );
static const float3 cx3 = float3( 0.000000, 0.707107, -0.707107 );
static const float3 cy3 = float3( -0.816496, 0.408248, 0.408248 );
static const float3 cz3 = float3( 0.577350, 0.577350, 0.577350 );
static const float3 c0  = float3( 0.333333, 0.333333, -0.333333 );
static const float3 x0  = float3( 0.572061, 0.218508, 0.790569 );
static const float3 y0  = float3( -0.582591, 0.786715, 0.204124 );
static const float3 z0  = float3( -0.577350, -0.577350, 0.577350 );
static const float3 c1  = float3( 0.206011, -0.539344, 0.000000 );
static const float3 x1  = float3( 0.572061, 0.218508, 0.790569 );
static const float3 y1  = float3( -0.738528, -0.282093, 0.612372 );
static const float3 z1  = float3( 0.356822, -0.934172, 0.000000 );
static const float3 c2  = float3( -0.539344, 0.000000, -0.206011 );
static const float3 x2  = float3( -0.218508, 0.790569, 0.572061 );
static const float3 y2  = float3( -0.282093, -0.612372, 0.738528 );
static const float3 z2  = float3( 0.934172, 0.000000, 0.356822 );
static const float3 c3  = float3( 0.000000, 0.206011, 0.539344 );
static const float3 x3  = float3( -0.790569, 0.572061, -0.218508 );
static const float3 y3  = float3( -0.612372, -0.738528, 0.282093 );
static const float3 z3  = float3( -0.000000, 0.356822, 0.934172 );

// distance function

// iq's Signed Triangular Prism distance function
float dist_triXY( float3 p, float2 h ) {
    float3 q = abs(p);
    return max(q.z-h.y,max(q.x*0.866025+p.y*0.5,-p.y)-h.x*0.5);
}

float dist_tri( float3 v, float3 c, float3 x, float3 y, float3 z ) {
	v -= c;	
	v = float3( dot( v, x ), dot( v, y ), dot( v, z ) );
	return max( dist_triXY( v, h  ), -dist_triXY( v, h2 ) );
}

float dist_field( float3 v ) {
	float b0, b1, b2, b3, b4;
	
	// cube
	{
		float d0 = dist_tri( v, cc0, cx0, cy0, cz0 );
		float d1 = dist_tri( v, cc1, cx1, cy1, cz1 );
		float d2 = dist_tri( v, cc2, cx2, cy2, cz2 );
		float d3 = dist_tri( v, cc3, cx3, cy3, cz3 );
		b0 = min( min( d0, d1 ), min( d2, d3 ) );
	}
	
	// xyz
	{
		float d0 = dist_tri( v, c0, x0, y0, z0 );
		float d1 = dist_tri( v, c1, x1, y1, z1 );
		float d2 = dist_tri( v, c2, x2, y2, z2 );
		float d3 = dist_tri( v, c3, x3, y3, z3 );
		b1 = min( min( d0, d1 ), min( d2, d3 ) );
	}
	
	// zx
	{
		v.zx = -v.zx;		
		float d0 = dist_tri( v, c0, x0, y0, z0 );
		float d1 = dist_tri( v, c1, x1, y1, z1 );
		float d2 = dist_tri( v, c2, x2, y2, z2 );
		float d3 = dist_tri( v, c3, x3, y3, z3 );
		v.zx = -v.zx;		
		b2 = min( min( d0, d1 ), min( d2, d3 ) );
	}
	
	// yz
	{
		v.yz = -v.yz;
		float d0 = dist_tri( v, c0, x0, y0, z0 );
		float d1 = dist_tri( v, c1, x1, y1, z1 );
		float d2 = dist_tri( v, c2, x2, y2, z2 );
		float d3 = dist_tri( v, c3, x3, y3, z3 );	
		v.yz = -v.yz;		
		b3 = min( min( d0, d1 ), min( d2, d3 ) );
	}

	// xy
	{
		v.xy = -v.xy;
		float d0 = dist_tri( v, c0, x0, y0, z0 );
		float d1 = dist_tri( v, c1, x1, y1, z1 );
		float d2 = dist_tri( v, c2, x2, y2, z2 );
		float d3 = dist_tri( v, c3, x3, y3, z3 );
		v.xy = -v.xy;		
		b4 = min( min( d0, d1 ), min( d2, d3 ) );
	}
	
	return min( b0, min( min( b1, b2 ), min( b3, b4 ) ) );
}

// ao
float ao( float3 v, float3 n ) {
	float sum = 0.0;
	float att = 1.0;
	float len = ao_step;
	for ( int i = 0; i < ao_iterations; i++ ) {
		sum += ( len - dist_field( v + n * len ) ) * att;
		
		len += ao_step;
		
		att *= 0.5;
	}
	
	return max( 1.0 - sum * ao_scale, 0.0 );
}


// get gradient in the world
float3 gradient( float3 v ) {
	const float3 dx = float3( grad_step, 0.0, 1.0 );
	const float3 dy = float3( 1.0, grad_step, 0.0 );
	const float3 dz = float3( 1.0, 0.0, grad_step );
	return normalize (
		float3(
			dist_field( v + dx ) - dist_field( v - dx ),
			dist_field( v + dy ) - dist_field( v - dy ),
			dist_field( v + dz ) - dist_field( v - dz )			
		)
	);
}

// ray marching
float ray_marching( float3 origin, float3 dir, float start, float end ) {
	float depth = start;
	for ( int i = 0; i < max_iterations; i++ ) {
		float dist = dist_field( origin + dir * depth );
		if ( dist < stop_threshold ) {
			return depth;
		}
		depth += dist;
		if ( depth >= end) {
			return end;
		}
	}
	return end;
}

// shadow
float shadow( float3 v, float3 light ) {
	float3 lv = v - light;
	float end = length( lv );
	lv /= end;
	
	float depth = ray_marching( light, lv, 0.0, end );
	
	return step( end - depth, 0.02 );
}

// phong shading
float3 shading( float3 v, float3 n, float3 eye ) {
	// ...add lights here...
	
	float3 final = 0.0 ;
	float3 ev = normalize( v - eye );
	float3 ref_ev = reflect( ev, n );
	
	// light 0
	{
		float3 light_pos   = ( 5.0 );
	
		float3 vl = normalize( light_pos - v );
	
		float diffuse  = 1.0;//max( 0.0, dot( vl, n ) );
		float specular = 1.0;//max( 0.0, dot( vl, ref_ev ) );
		specular = pow( specular, 12.0 );
		
		final += float3( 0.9, 0.9, 0.9 ) * ( diffuse * 0.4 + specular * 0.9 ) * shadow( v, light_pos ); 
	}
	
	// light 1
	{
		float3 light_pos   = ( -5.0 );
	
		float3 vl = normalize( light_pos - v );
	
		float diffuse  = 1.0;//max( 0.0, dot( vl, n ) );
		float specular = 1.0;//max( 0.0, dot( vl, ref_ev ) );
		specular = pow( specular, 64.0 );
		
		final += float3( 0.1, 0.1, 0.1 ) * ( diffuse * 0.4 + specular * 0.9 ); 
	}

	final += ao( v, n ) * float3( 0.15 , 0.15, 0.15);
	
	return final;
}

// pitch, yaw
float3x3 rot3xy( float2 angle ) {
	float2 c = cos( angle );
	float2 s = sin( angle );
	
	float3x3 mat3 = {
		c.y      ,  0.0, -s.y,
		s.y * s.x,  c.x,  c.y * s.x,
		s.y * c.x, -s.x,  c.y * c.x
	};

	return mat3;
}

// get ray direction
float3 ray_dir( float fov, float2 size, float2 pos ) {
	float2 xy = pos;

	float cot_half_fov = tan( ( 90.0 - fov * 0.5 ) * DEG_TO_RAD );	
	float z = size.y * 0.5 * cot_half_fov;
	
	return normalize( float3( xy, -z ) );
}


//////////////////////////////////////////////////////////////////////////////////////////////
/// DEFAULT
//////////////////////////////////////////////////////////////////////////////////////////////

fixed4 FRAGMENTSHADER (pixelPoints PIXELSPACE) : SV_Target
{
	float2 coordinate = PIXELSPACE.uv;
	float2 coordinateSprite = PIXELSPACE.uv2;
	
	float2 coordinateScale = (PIXELSPACE.uv * 2.0) - 0.5 ;
	
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
	
// default ray dir

	float2 uv = coordinateScale - float2(0.5, 0.5);
	float2 size = 2.0;

	float3 dir = ray_dir( 45.0, size, uv);
	
	// default ray origin
	float3 eye = float3( 0.0, 0.0, 4.4 );

	// rotate camera
	float3x3 rot = rot3xy( float2( -DEG_TO_RAD * 30.0, TIME * 0.5 ) );
	dir = mul(rot , dir);
	eye = mul(rot , eye);
	
	float4 fragColor;

	// ray marching
	float depth = ray_marching( eye, dir, 0.0, clip_far );
	if ( depth >= clip_far ) {
		fragColor = float4( 0.0, 0.0, 0.0, 1.0 );
    } else {
		// shading
		float3 pos = eye + dir * depth;
		float3 n = gradient( pos );
		fragColor =  float4(pos,1.0);//float4( shading( pos, n, eye ) * 2.0, 1.0 );
    }
	
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