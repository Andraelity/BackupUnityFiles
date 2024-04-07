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


static const int maxSteps = 64;
static const float hitThreshold = 0.01;
static const float minStep = 0.01;


static const float3 translucentColor = float3(0.8, 0.2, 0.1)*3.0;
//const vec3 translucentColor = vec3(0.2, 0.05, 0.5)*2.0;

float difference(float a, float b)
{
    return max(a, -b);
}

float noise( in float3 x )
{
    float3 p = floor(x);
    float3 f = frac(x);
	f = f*f*(3.0-2.0*f);
	
	float2 uv = (p.xy+float2(37.0,17.0)*p.z) + f.xy;
	float2 rg = tex2D( _TextureChannel0, (uv+ 0.5)/256.0).yx;
	return lerp( rg.x, rg.y, f.z )*2.0-1.0;
}

static const float3x3 m = { 0.00,  0.80,  0.60,
 						   -0.80,  0.36, -0.48,
 						   -0.60, -0.48,  0.64 };

float fbm( float3 p )
{
    float f;
    f  = 0.5000*noise( p ); 
    p = mul(m*2.02, p); //p = p*2.02;
    f += 0.2500*noise( p ); 
    p = mul(m*2.03, p); //p = p*2.02;
    f += 0.1250*noise( p ); 
    p = mul(m*2.01, p); //p = p*2.02;
    f += 0.0625*noise( p ); 	
    return f;
}


// transforms
float3 rotateX(float3 p, float a)
{
    float sa = sin(a);
    float ca = cos(a);
    return float3(p.x, ca*p.y - sa*p.z, sa*p.y + ca*p.z);
}

float3 rotateY(float3 p, float a)
{
    float sa = sin(a);
    float ca = cos(a);
    return float3(ca*p.x + sa*p.z, p.y, -sa*p.x + ca*p.z);
}

float sphere(float3 p, float r)
{
    return length(p) - r;
}

float box( float3 p, float3 b )
{
  float3 d = abs(p) - b;
  return min(max(d.x,max(d.y,d.z)),0.0) + length(max(d,0.0));
}

// distance to scene
float scene(float3 p)
{          
    float d;

	
	//d = p.y;
	d = sphere(p, 1.0);
	//d = box(p, vec3(1.0));
	d = difference(box(p, float3(1.1, 1.1, 1.1)), d);
	d = min(d, sphere(p, 0.5));
	
	float3 np = p;
	//vec3 np = p + vec3(0.0, -iTime*0.2, 0.0);
	d += fbm(np)*0.2;
	
	return d;
}

// calculate scene normal
float3 sceneNormal(in float3 pos )
{
    float eps = 0.05;
    float3 n;
    float d = scene(pos);
    n.x = scene( float3(pos.x+eps, pos.y, pos.z) ) - d;
    n.y = scene( float3(pos.x, pos.y+eps, pos.z) ) - d;
    n.z = scene( float3(pos.x, pos.y, pos.z+eps) ) - d;
    return normalize(n);
}


// trace ray using regular sphere tracing
// returns position of closest surface
float3 trace(float3 ro, float3 rd, out bool hit)
{
    hit = false;
    float3 pos = ro;
    for(int i=0; i<maxSteps; i++)
    {
		float d = scene(pos);
		if (abs(d) < hitThreshold) {
			hit = true;
		}
		pos += d*rd;
    }
    return pos;
}

// trace all the way through the scene,
// keeping track of distance traveled inside volume
float3 traceInside(float3 ro, float3 rd, out bool hit, out float insideDist)
{
    hit = false;
	insideDist = 0.0;	
    float3 pos = ro;
	float3 hitPos = pos;
    for(int i=0; i<maxSteps; i++)
    {
		float d = scene(pos);
		d = max(abs(d), minStep) * sign(d); // enforce minimum step size
		
		if (d < hitThreshold && !hit) {
			// save first hit
			hitPos = pos;
			hit = true;
		}
		
		if (d < 0.0) {
			// sum up distance inside
			insideDist += -d;
		}
		pos += abs(d)*rd;
    }
    return hitPos;
}

float3 background(float3 rd)
{
     return (1.0);
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



   	float2 pixel = coordinateScale;

    // compute ray origin and direction
    float asp = 1.0;
    float3 rd = normalize(float3(asp*pixel.x, pixel.y, -1.5));
    float3 ro = float3(0.0, 0.0, 2.5);

     float2 mouse = 0.0;
     float roty = -TIME*0.2;
     float rotx = 0.0;
     
    rd = rotateX(rd, rotx);
    ro = rotateX(ro, rotx);
          
    rd = rotateY(rd, roty);
    ro = rotateY(ro, roty);
          
    // trace ray
    bool hit;
	float dist;
	//vec3 hitPos = trace(ro, rd, hit);
	float3 hitPos = traceInside(ro, rd, hit, dist);

    float3 rgb = (0.0);
    if(hit) {
		float3 n = sceneNormal(hitPos);
		//rgb = n*0.5+0.5;
		//rgb = vec3(dist*0.2);
		
		// exponential fall-off:
		rgb = exp(-dist*dist*translucentColor);
		
		// cubemap reflection
		float3 i = normalize(hitPos - ro);
		float3 r = reflect(i, n);
		float fresnel = 0.1 + 0.9*pow(1.0 - clamp(dot(-i, n), 0.0, 1.0), 2.0);
		rgb += tex2D(_TextureChannel1, r.xy).rgb * fresnel;
		//rgb += vec3(fresnel);

     } else {
        rgb = background(rd);
     }
     
    float4 fragColor=float4(rgb, 1.0);    
    // Output to screen

    // float4 fragColor = float4(colTexture, valueOutput.x + valueOutput.y + valueOutput.z / 3.0);
    

///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);
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