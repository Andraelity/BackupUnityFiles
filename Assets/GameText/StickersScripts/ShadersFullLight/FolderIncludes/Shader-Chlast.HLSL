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

float mapWall( float2 uv )
{
	float sp = 0.0
		- max( 0.0, ( sin( TIME * 3.0 + uv.y / 3.14 ) + 1.0 ) * 0.4 );
	sp *= max( 4.0 - abs( uv.x ), 0.0 ) * .5;
	
	float zshift = -TIME * 4.0;
	return min( 
		( 1.0 - abs( fmod( uv.x, 2.0 ) - 1.0 ) ), ( 1.0 - abs( fmod( uv.y + zshift, 2.0 ) - 1.0 ) ) ) *
		( 1.0 + sp * 2.0 );
}

float3 calcLighting( float3 pos, float3 norm, float3 light, float3 amb )
{
	float3 dirL = normalize( light - pos );
	float dotL = abs( dot( dirL, norm ) );
	float3 R = reflect( dirL, norm );
	float spec = pow( abs( dot( R, normalize( -pos ) ) ), 10.0 );
	return float3( dotL * 0.2 , dotL * 0.2 , dotL * 0.2 ) + float3( spec * .3,  spec * .3,  spec * .3 ) + amb + abs( pos * 0.05 );
}

float3 getPlaneVPos( float3 pt, float pos )
{
	return pt + float3( sign( pos ) * mapWall( pt.yz ), 0.0, 0.0 );
}
float3 getPlaneHPos( float3 pt, float pos )
{
	return pt + float3( 0.0, sign( pos ) * mapWall( pt.xz ), 0.0 );
}

float3 planeVColor( float t, float3 ro, float3 rd, float pos, float3 light, float3 amb )
{
	float3 pt = ro + t * rd;
	
	float3 norm = normalize( cross( 
		getPlaneVPos( pt + float3( 0.0, 0.0, 0.1 ), pos ) - getPlaneVPos( pt, pos ), 
		getPlaneVPos( pt + float3( 0.0, sign(pos) * 0.1, 0.0 ), pos ) - getPlaneVPos( pt, pos ) ) );
	
	return calcLighting( pt, norm, light, amb );
}

float3 planeHColor( float t, float3 ro, float3 rd, float pos, float3 light, float3 amb )
{
	float3 pt = ro + t * rd;
	
	float3 norm = normalize( cross( 
		getPlaneHPos( pt + float3( 0.0, 0.0, 0.1 ), pos ) - getPlaneHPos( pt, pos ), 
		getPlaneHPos( pt + float3( sign(pos) * 0.1, 0.0, 0.0 ), pos ) - getPlaneHPos( pt, pos ) ) );
	
	return calcLighting( pt, norm, light, amb );
}

float planeV( float3 ro, float3 rd, float pos ) // vertical
{
	float t = 0.0;
	float h = 0.0;
	for( int i = 0; i < 50; i++ )
	{
		float3 pt = ro + t * rd;
		h = abs( pos - getPlaneVPos( pt, pos ).x );
		if ( h <  0.1 )
		{
			break;
		}
		t += h * 0.32;
	}
	return t;
}
float planeH( float3 ro, float3 rd, float pos ) // horizontal
{
	float t = 0.0;
	float h = 0.0;
	for( int i = 0; i < 50; i++ )
	{
		float3 pt = ro + t * rd;
		h = abs( pos - getPlaneHPos( pt, pos ).y );
		if ( h <  0.1 )
		{
			break;
		}
		t += h * 0.32;
	}
	return t;
}

float3 colorize( float2 uv )
{
	float3 ro = float3( 0.0, 0.0, 0.0 );
	float3 rd = normalize( float3( uv.x - 0.5, uv.y - 0.5, 1.0 ) );
	rd.x *= 1.5;
	
	float3 c1, c2, c3, c4;
	
	float3 light = float3( 0.0, 0.0, 10.0 );
	float3 amb = 0.2 ;
	
	float t1 = planeV( ro, rd, -6.0 );
	float t2 = planeV( ro, rd,  6.0 );
	float t3 = planeH( ro, rd, -6.0 );
	float t4 = planeH( ro, rd,  6.0 );
	
	float t = 0.0;
	float3 c = float3( 0.0, 0.0, 0.0 );

	if ( t1 < t2 && t1 < t3 && t1 < t4 )
	{
		t = t1;
		c = planeVColor( t, ro, rd, -6.0, light, amb );
	}
	else if ( t2 < t3 && t2 < t4 )
	{
		t = t2;
		c = planeVColor( t, ro, rd,  6.0, light, amb );
	}
	else if ( t3 < t4 )
	{
		t = t3;
		c = planeHColor( t, ro, rd, -6.0, light, amb );
	}
	else
	{
		t = t4;
		c = planeHColor( t, ro, rd,  6.0, light, amb );
	}

	float mult = clamp( 4.0 - t * 0.05 , 0.0, 4.0 ) * 0.25;
	return c * mult + float3( 1.0 - mult , 1.0 - mult , 1.0 - mult );
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
	

	float2 uv = coordinate;

	float3 color = colorize( uv );	
	
	float4 fragColor = float4(color,1.0);

///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);
// float4 outputSmoothed = fragColor;
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