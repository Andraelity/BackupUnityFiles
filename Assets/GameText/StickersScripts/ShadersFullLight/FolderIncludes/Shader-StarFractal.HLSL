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

#define iterations 17
#define formuparam 0.53

#define volsteps 20
#define stepsize 0.1

#define zoom   0.800
#define tile   0.850
#define speed  0.000

#define brightness 0.0015
#define darkmatter 0.300
#define distfading 0.730
#define saturation 0.850

float2 computeNext(float2 current, float2 constant){
    float zr = (current.x * current.x) - (current.y * current.y);
    float zi = 2.0 * current.x * current.y;
   
    return float2(zr, zi) + constant;
}

float cheap_star(float2 uv, float anim)
{
    uv = abs(uv);
    float2 pos = min(uv.xy/uv.yx, anim);
    float p = (2.0 - pos.x - pos.y);
    return (2.0+p*(p*p-1.5)) / (uv.x+uv.y);      
}

float mod2(float2 z){
    return z.x * z.x + z.y * z.y;
}

float computeIterations(float2 z0, float2 constant, float max_iterations){
    float2 zn = z0;
    float iteration = 0.;
    while(length(zn) < 2. && iteration < max_iterations){
        zn = computeNext(zn, constant);
        iteration++;
    }
   
    float mod2 = length(zn);
    float smoothf = float(iteration) - log2(max(1., log2(mod2)));
    return smoothf;
}

float3 getColor(float it, float maxIt){
    return(float3(it/maxIt, it/maxIt, it/maxIt));
}
float4 mainVR(in float3 ro, in float3 rd )
{
//get coords and direction
float3 dir=rd;
float3 from=ro;

//volumetric rendering
float s=0.1,fade=1.;
float3 v = float3(0.0, 0.0, 0.0);
for (int r=0; r<volsteps; r++) 
{
float3 p=from+s*dir*.5;
p = abs(float3(tile, tile, tile)-fmod(p,float3(tile*2.0, tile*2.0, tile*2.0))); // tiling fold
float pa,a=pa=0.;
for (int i=0; i<iterations; i++) 
{
p=abs(p)/dot(p,p)-formuparam;
float2x2 mat2 = {cos(TIME*0.05),sin(TIME*0.05),-sin(TIME*0.05),cos(TIME*0.05)};// the magic formula

p.xy =  mul(mat2, p.xy);
a+=abs(length(p)-pa); // absolute sum of average change
pa=length(p);
}
float dm=max(0.,darkmatter-a*a*.001); //dark matter
a*=a*a; // add contrast
if (r>6) fade*=1.2-dm; // dark matter, don't render near
//v+=vec3(dm,dm*.5,0.);
v+=fade;
v+=float3(s,s*s,s*s*s*s)*a*brightness*fade; // coloring based on distance
fade*=distfading; // distance fading
s+=stepsize;
}
v=lerp(float3(length(v), length(v), length(v)),v,saturation); //color adjust
float4 fragColor = float4(v*.01,1.);


return fragColor;
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

float2 uv = coordinateScale;
// uv.y*=iResolution.y/iResolution.x;
float3 dir=float3(uv*zoom,1.);
float time=TIME*speed+.25;


    uv *= 1.01;
   
    float max_Iterations = 100.;
    float uTime = pow(sin(TIME * 0.2),3.)*.03 - 1.313;
    //float uTime = -1.313;
    uv *= sin(TIME*0.2) + 2.;

    float2x2 operationUV =  {cos(TIME*0.5),sin(TIME*0.5),-sin(TIME*0.5),cos(TIME*0.5)};
    uv = mul(operationUV, uv);
    float2 uConstant = (float2(-abs(-sin(uTime)), (cos(uTime))));
    float color = computeIterations(uv, uConstant, max_Iterations);
   
    float3 col = float3(getColor(color, max_Iterations));
    col = float3(pow(col.x, 1.7),pow(col.x, 1.7),pow(col.x, 1.7));
    col.z = 0.;
      uv *= 2.0 * ( cos(TIME * 2.0) -2.5);
   
    // anim between 0.9 - 1.1
    float anim = sin(TIME * 12.0) * 0.1 + 1.0;    

   
    col += float3(0, 0,.5);
    col*= float3(1.3,1,1);
   

float a1 = 1.0;
float a2 = 1.0;
float2x2 rot1 = {cos(a1),sin(a1),-sin(a1),cos(a1)};
float2x2 rot2 = {cos(a2),sin(a2),-sin(a2),cos(a2)};
dir.xz= mul(rot1, dir.xz);
dir.xy= mul(rot2, dir.xy);
float3 from = float3(1.,.5,0.5);
from+=float3(time*2.,time,-2.);
from.xz= mul(rot1, from.xz);
from.xy= mul(rot2, from.xy);

float4 fragColor = mainVR( from, dir);
fragColor+= float4(col,1.0);
fragColor*= float4(cheap_star(uv,anim) * float3(0.55,0.5,0.55)*0.6, 1.0);

float4 outputSmoothed = smoothstep(0.0, 1.0, fragColor);
    
fragColor = outputSmoothed;

///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
///////////////////////↓↓↓↓↓↓↓↓↓// THIS IS THE LAST STEP ON THE PROCESS
float4 colBackground = fragColor;
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