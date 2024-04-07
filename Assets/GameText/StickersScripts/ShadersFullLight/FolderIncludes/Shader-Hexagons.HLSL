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

#define AA 2

// { 2d cell id, distance to border, distnace to center )
float4 hexagon( float2 p ) 
{
	float2 q = float2( p.x*2.0*0.5773503, p.y + p.x*0.5773503 );
	
	float2 pi = floor(q);
	float2 pf = frac(q);

	float v = fmod(pi.x + pi.y, 3.0);

	float ca = step(1.0,v);
	float cb = step(2.0,v);
	float2  ma = step(pf.xy,pf.yx);
	
    // distance to borders
	float e = dot( ma, 1.0-pf.yx + ca*(pf.x+pf.y-1.0) + cb*(pf.yx-2.0*pf.xy) );

	// distance to center	
	p = float2( q.x + floor(0.5+p.y/1.5), 4.0*p.y/3.0 )*0.5 + 0.5;
	float f = length( (frac(p) - 0.5)*float2(1.0,0.85) );		
	
	return float4( pi + ca - cb*ma, e, f );
}

float hash1( float2  p ) { float n = dot(p,float2(127.1,311.7) ); return frac(sin(n)*43758.5453); }

float noise( in float3 x )
{

    float3 p = floor(x);
    float3 f = frac(x);
	f = f*f*(3.0-2.0*f);
	float2 uv = (p.xy+float2(37.0,17.0)*p.z) + f.xy;
	float2 rg = tex2D(_TextureChannel1, (uv+0.5)/256.0 ).yx;
	return lerp( rg.x, rg.y, f.z );

}




#define ENVIRONMENT 0
#define OFFSET 1.0
#define DEPTH 4
#define CHILDREN 3

float2 rotate(float2 v, float a) {
	return float2(cos(a)*v.x+sin(a)*v.y, -sin(a)*v.x+cos(a)*v.y);
}

float2 repeat_arc(float2 pos, int n) {
	float a = atan2(pos.y, pos.x);
	float s = 2.*PI/float(n);
	return rotate(float2(length(pos),0), fmod(a,s)-s/2.);
}

float cog_teeth (float a) {
	return 4.*clamp(2.*abs(fmod(a/2./PI, 1.)-.5), .25, .75)-2.;
}

bool cog (float2 pos, float r, float depth, int teeth) {
	return length(pos) < r - depth*cog_teeth(float(teeth)*atan2(pos.y, pos.x));
}

bool cogs(float2 pos, float theta, float r1, float r2, float r3, float cog_depth, int cog_teeth) {
	float rm = (r2+r3)/2.;
	float ri = (r2-r3)/2.;
	
	bool result = false;
	for(int i=0; i<DEPTH; i++) {
		float s_theta = (r2/rm)*(1.-r3/r2)*theta; // (r2/rm) is magic factor
		result = result || cog(pos, r1, cog_depth, cog_teeth);
		result = result &&!cog(pos, r2, cog_depth*ri/r1, int(float(cog_teeth)*r2/ri));
		result = result || cog(rotate(pos, theta), r3, cog_depth*ri/r1, int(float(cog_teeth)*r3/ri));
		pos = rotate(repeat_arc(rotate(pos, s_theta*(r3/r2)), CHILDREN)-float2(rm,0.), s_theta*(r3/ri))*r1/ri;
	}
	return result;
}

bool cogs(float2 pos, float theta) {
	return cogs(pos, theta, 1., .8, .4, .1, 9);
}

bool cog_array(float2 pos, float theta, float r, float depth, int teeth) {
	pos = fmod(pos,4.*r);
	if(pos.x < 2.*r != pos.y < 2.*r) {
		theta += PI/float(teeth);
	}
	pos = abs(pos-float2(2.*r, 2.*r));
	pos = rotate(pos-float2(r, r), theta);
	return cog(pos, r, depth, teeth);
}

float2 get_pos(float2 fragCoord) {
	return fragCoord;
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


	float3 tot = (0.0);
    
        float2 uv  = coordinate * 2.0; 
        float2 pos = coordinateScale * 2.0;

        // distort
        pos *= 1.2 + 0.15*length(pos);

        // gray
        float4 h = hexagon(-8.0*pos + 4.0*TIME);
        float n = noise( float3(0.3*h.xy+TIME* 10.0,TIME * 2.0) );
        float3 col = 0.15 + 0.15*hash1(h.xy+1.2)*float3(1.0, 1.0, 1.0);
        col *= smoothstep( 0.10, 0.11, h.z );
        col *= smoothstep( 0.10, 0.11, h.w );
        col *= 1.0 + 0.15*sin(40.0*h.z);
        col *= 0.75 + 0.5*h.z*n;

        // // shadow
        h = hexagon(6.0 * (pos + 0.1 * float2(-1.3,1.0)) + 0.6 * TIME);
        col *= 1.0-0.8*smoothstep(0.45,0.451,noise( float3(0.3*h.xy+TIME*0.1,0.5*TIME) ));

        // red
        
        h = hexagon(6.0*pos + 4.0*TIME);
        n = noise( float3(0.3*h.xy+TIME*0.1,1.0) );
        float3 colb = 0.9 + 0.8*sin( hash1(h.xy)*1.5 + 2.0 + float3(0.1,1.0,1.1) );
        colb *= smoothstep( 0.8, 0.11, h.z );
        colb *= 1.0 ;//+ 0.05*sin(40.0*h.z);

        col = lerp( col, colb, smoothstep(0.45,0.451,n) );
        
        col *= 2.5/(2.0+col);

        // col *= pow( 16.0*uv.x*(1.0-uv.x)*uv.y*(1.0-uv.y), 0.1 );

        tot += col;
        
	float4 fragColor = float4( tot, 1.0 );


	float2 fragCoord = coordinate * 2.0 - 1.0;

	float4 fragColor2 = cogs(rotate(get_pos(fragCoord)+float2(.4,0),-TIME), TIME) ? float4(1, 1, 1, 1) :
						cogs(rotate(get_pos(fragCoord)+float2(-.5,+.4)-OFFSET/3., -TIME)*3., TIME) ? float4(1.0, 1.0, 1.0, 1.0) : 
						cog_array(get_pos(fragCoord)-OFFSET, TIME, .1, .01, 9) ? float4(1, 1, 1, 1) : float4(1.0, 0.0, 0.0, 0.0);


	fragColor = smoothstep( fragColor, fragColor2, 0.2);


// return fragColor2;
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