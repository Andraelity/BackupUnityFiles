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

static const float tau = 6.28318530717958647692;
#define GAMMA (9.0)
static float3 viewSpaceRay;


float3 ToGamma( in float3 col )
{
	// convert back into colour values, so the correct light will come out of the monitor
	return pow( col, float3(1.0/GAMMA,1.0/GAMMA,1.0/GAMMA) );
}

void CamPolar( out float3 pos, out float3 ray, in float3 origin, in float2 rotation, in float distance, in float zoom, in float2 fragCoord )
{
	// get rotation coefficients
	float2 c = float2(cos(rotation.x),cos(rotation.y));
	float4 s;
	s.xy = float2(sin(rotation.x),sin(rotation.y)); // worth testing if this is faster as sin or sqrt(1.0-cos);
	s.zw = -s.xy;

	// ray in view space
	ray.xy = fragCoord * 200.0 - 50;
	ray.z = 200.0*zoom;
	ray = normalize(ray);
	
	viewSpaceRay = ray;
	
	// rotate ray
	ray.yz = ray.yz*c.xx + ray.zy*s.zx;
	ray.xz = ray.xz*c.yy + ray.zx*s.yw;
	
	// position camera
	pos = origin - distance*float3(c.x*s.y,s.z,c.x*c.y);
}



float2 Noise( in float3 x )
{
   
	x = frac(x * float3(.1031, .1030, .0973));
    x += dot(x, x.yzx+33.33);
    return frac((x.xx+x.yz)*x.zy); 
}


float DistanceField( float3 pos )
{

// rotational symmettry
const float slice = tau/12.0;
float a = abs(frac(atan2(pos.x,pos.z)/slice+TIME*2.5)-.5)*slice;
pos.xz = length(pos.xz)*float2(sin(a),cos(a));

// symettry in y
pos.y = abs(pos.y);

return dot(pos,normalize(float3(1,1,1))) - 1.0;
}


float3 Normal( float3 pos, float rad )
{
	float2 delta = float2(0,rad);
	float3 grad;
	grad.x = DistanceField( pos+delta.yxx )-DistanceField( pos-delta.yxx );
	grad.y = DistanceField( pos+delta.xyx )-DistanceField( pos-delta.xyx );
	grad.z = DistanceField( pos+delta.xxy )-DistanceField( pos-delta.xxy );
	return normalize(grad);
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
	
	float zoom = 1.5;
	float3 pos, ray;
	CamPolar( pos, ray, .04 * float3(Noise(float3(3.0*TIME,0,0)).xy,0), float2(.22,0)+float2(.7,tau)*-40.0, 6.0, zoom, coordinateScale/2.0  );

	// radius of cone to trace, at 1m distance;
	float coneRad = .7071/(200.0 * zoom);
	
	float coverage = -1.0;
	float3 coverDir = 0; // this could be a single angle, or a 2D vector, since it's perp to the ray
	
	float aperture = .05;
	float focus = 5.0;
	
	float3 col = (0);
	float t = .0;
	for ( int i=0; i < 100; i++ )
	{
		float rad = t*coneRad + aperture*abs(t-focus);

		float3 p = pos + t*ray;
		float h = DistanceField( p );
		
		if ( h < rad )
		{
			// shading
			float3 normal = Normal(p, rad);
			
			float3 albedo = (.2);
			
			// lighting
			float3 ambient = float3(0.1, 0.1, 0.1)*smoothstep(.7,2.0,length(p.xz)+abs(p.y));
			float3 directional = 3.0*float3(1,.1,.13)*max(dot(normal,normalize(float3(-2,-2,-1))),.0);
			directional *= smoothstep(.5,1.5,dot(p,normalize(float3(1,1,-1))));

			float fresnel = pow( 1.0-abs(dot( normal, ray )), 5.0 );
			fresnel = lerp( .03, 1.0, fresnel );
			
			float3 reflection = (1.0);//Sky( reflect(ray,normal) );
			
			float3 sampleCol = lerp( albedo*(ambient+directional), reflection, float3(fresnel, fresnel, fresnel) );
			
			// compute new coverage
			float newCoverage = -h/rad;
			float3 newCoverDir = normalize(normal-dot(normal,ray)*ray);

			// allow for coverage at different angles
			// very dubious mathematics!
			// basically, coverage adds to old coverage if the angles mean they don't overlap
			newCoverage += (1.0+coverage)*(.5-.5*dot(newCoverDir,coverDir));
			newCoverage = min(newCoverage,1.0);

			// S-curve, to imitate coverage of circle
			newCoverage = sin(newCoverage*tau/4.0);//smoothstep(-1.0,1.0,newCoverage)*2.0-1.0;

			if ( newCoverage > coverage )
			{
				
				// combine colour
				col += sampleCol*(newCoverage-coverage)*.5;
				
				coverDir = normalize(lerp(newCoverDir,coverDir,(coverage+1.0)/(newCoverage+1.0)));
				coverage = newCoverage;
			}
		}
		
		t += max( h, rad*.5 ); // use smaller values if there are echoey artefacts
		
		if ( h < -rad || coverage > 1.0 )
			break;
	}
	
	col += 0.0;//(1.0-coverage)*.5*Sky(ray);

	// grain
	float3 grainPos = float3(coordinate*.8,TIME*30.0);
	grainPos.xy = grainPos.xy*cos(.75)+grainPos.yx*float2(-1,1)*sin(.75);
	grainPos.yz = grainPos.yz*cos(.5)+grainPos.zy*float2(-1,1)*sin(.5);
	float2 filmNoise = Noise(grainPos*.5);
	col *= lerp( float3(1,1,1), lerp(float3(1,.5,0),float3(0,.5,1),filmNoise.x), .1*pow(filmNoise.y,1.0) );

	// dust
	float2 uv = 1.0; // fragCoord.xy/iResolution.y;
	float T = floor( TIME * 60.0 );
	float2 scratchSpace = lerp( Noise(float3(uv*8.0,T)).xy, uv.yx+T, .8 )*1.0;
	float scratches = 1.0; // texture( iChannel1, scratchSpace ).r;
	
	col *= float3(1.0, 1.0, 1.0 )-.5* float3(.3,.5,.7)*pow(1.0-smoothstep( .0, .1, scratches ),2.0);
		

	float4 fragColor;
	fragColor.rgb = ToGamma(col);
	fragColor.w = (fragColor.x + fragColor.y + fragColor.z )/3.0;

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