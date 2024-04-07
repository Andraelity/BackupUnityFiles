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

#define LINEAR_ROTATION

#define WEIGHT (3.0 / 1000.0)
static const float3 RED = float3(1.0,0.0,0.0);
static const float3 GREEN = float3(0.0,1.0,0.0);
static const float3 BLUE = float3(0.0,0.8,1.0);
static const float3 WHITE = float3(1.0,1.0,0.97);
static const float3 YELLOW = float3(1.0,1.0,0.0);

// rasterize functions
float lineFunction(float2 p, float2 p0, float2 p1, float w) {
    float2 d = p1 - p0;
    float t = clamp(dot(d,p-p0) / dot(d,d), 0.0,1.0);
    float2 proj = p0 + d * t;
    float dist = length(p - proj);
    dist = (1.0/dist)*WEIGHT*w;
    return min(dist*dist,1.0);
}
float circle(float2 p, float2 c, float r, float w) {
    float dist = abs(length(p - c)) + r;
    dist = (1.0/dist)* WEIGHT*w;
    return min(dist*dist,1.0);
}

// matrices
float4x4 getRotMatrix(float3 a) {
    float3 s = sin(a);
    float3 c = cos(a);    
    float4x4 ret = 
    {
    float4(c.y*c.z,c.y*s.z,-s.y,0.0),
    float4(s.x*s.y*c.z-c.x*s.z,s.x*s.y*s.z+c.x*c.z,s.x*c.y,0.0),
    float4(c.x*s.y*c.z+s.x*s.z, c.x*s.y*s.z-s.x*c.z,   c.x*c.y,0.0),
    float4(0.0,0.0,0.0,1.0)
    };

    return ret;
}
float4x4 getPosMatrix(float3 p) {   
    float4x4 ret = 
    {
    float4(1.0,0.0,0.0,p.x),
    float4(0.0,1.0,0.0,p.y),
    float4(0.0,0.0,1.0,p.z),  
    float4(0.0,0.0,0.0,1.0)
    };
    return ret;
}

// utils
float3 mix3(float3 a, float3 b, float3 c, float t) {
    if(t>0.5) return lerp(b,c,t*2.0-1.0);
    else return lerp(a,b,t*2.0);
}
float3 fragment(float3 p) {
    float t = abs(tan(p.x*0.8+TIME*0.5))*0.5+0.5;
    float fog = min(pow(p.z,3.0)*400.0,1.0);
    return mix3(RED,GREEN,BLUE,t) * fog;
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
	
	float2 uv = coordinate * 3.0 - float2(1.5, 1.7777);
    
    float line_width = 0.4;
    float time = TIME * 0.31415;
    //float3 c = float3(lerp(float3(0.19,0.13,0.1), float3(1.0, 1.0, 1.0), 0.5 * pow( length(uv)*0.5,2.0)));
    float3 c = 0.0;

    float4x4 cam = getPosMatrix(float3(0.0,0.0,30.0));
 	float4x4 rot = getRotMatrix(float3(-time, -time*0.86, -time * 0.473));


    // float p = 0.08;
    // float4x4 rot = getRotMatrix(float3(-time		+sin(-time*30.0)*p,
    //        	 	                       -time*0.860	+sin(-time*20.0)*p*1.24,
    //        	 	                       -time*0.473	+sin(-time*10.0)*p));

    
    float3 instances[18]=
    {
     float3( 0.0, 0.0,-1.0),
     float3(-1.0, 0.0,-1.0),
     float3( 1.0, 0.0,-1.0),
     float3( 0.0, 1.0,-1.0),
     float3( 0.0,-1.0,-1.0),
     float3(-1.0, 0.0, 0.0),
     float3( 1.0, 0.0, 0.0),
     float3( 0.0, 1.0, 0.0),
     float3( 0.0,-1.0, 0.0),
     float3(-1.0,-1.0, 0.0),
     float3( 1.0, 1.0, 0.0),
     float3(-1.0, 1.0, 0.0),
     float3( 1.0,-1.0, 0.0),
     float3( 0.0, 0.0, 1.0),
     float3(-1.0, 0.0, 1.0),
     float3( 1.0, 0.0, 1.0),
     float3( 0.0, 1.0, 1.0),
     float3( 0.0,-1.0, 1.0)
    };
    // box pipeline 
    for(int dip = 8; dip < 18; dip++) {
        
        // input assembly
        float3 vert[8] = 
        {
        float3(-1.0,-1.0, 1.0),
        float3(-1.0, 1.0, 1.0),    
        float3( 1.0, 1.0, 1.0),    
        float3( 1.0,-1.0, 1.0),
        float3(-1.0,-1.0,-1.0),
        float3(-1.0, 1.0,-1.0),    
        float3( 1.0, 1.0,-1.0),     
        float3( 1.0,-1.0,-1.0)
    	};
        // vertex processing        
        float4x4 pos = getPosMatrix(instances[dip] * 4.0);
        float4x4 mat = mul(pos, rot);


        for(int i = 0; i < 8; i++) {

            // transform
            vert[i] = mul( mat, float4(vert[i],1.0)).xyz;

            // perspective
            vert[i].z = 1.0 / vert[i].z;
            vert[i].xy *= vert[i].z;
        }    

        // primitive assembly and rasterize
        float valueI;
        valueI  = lineFunction(uv,vert[0].xy,vert[1].xy,line_width);
        valueI += lineFunction(uv,vert[1].xy,vert[2].xy,line_width);
        valueI += lineFunction(uv,vert[2].xy,vert[3].xy,line_width);
        valueI += lineFunction(uv,vert[3].xy,vert[0].xy,line_width);
        valueI += lineFunction(uv,vert[4].xy,vert[5].xy,line_width);
        valueI += lineFunction(uv,vert[5].xy,vert[6].xy,line_width);
        valueI += lineFunction(uv,vert[6].xy,vert[7].xy,line_width);
        valueI += lineFunction(uv,vert[7].xy,vert[4].xy,line_width);
        valueI += lineFunction(uv,vert[0].xy,vert[4].xy,line_width);
        valueI += lineFunction(uv,vert[1].xy,vert[5].xy,line_width);
        valueI += lineFunction(uv,vert[2].xy,vert[6].xy,line_width);
        valueI += lineFunction(uv,vert[3].xy,vert[7].xy,line_width);
        c += fragment(vert[0]) * min(valueI,1.0);
        // c +=  min(valueI,1.0);
    }
     
    instances[0] = float3(-1.0, 1.0,-1.0);
    instances[1] = float3( 1.0, 1.0,-1.0);
    instances[2] = float3(-1.0,-1.0,-1.0);
    instances[3] = float3( 1.0,-1.0,-1.0);
    instances[4] = float3(-1.0, 1.0, 1.0);
    instances[5] = float3( 1.0, 1.0, 1.0);
    instances[6] = float3(-1.0,-1.0, 1.0);
    instances[7] = float3( 1.0,-1.0, 1.0);
    
    // cicle pipeline
    for(int dip = 0; dip < 8; dip++) {
        
        // input assembly
        float3 vert = (0.0);

        // vertex processing
        float4x4 pos = getPosMatrix(instances[dip] * 4.0);
        // float4x4 mat = cam;
        float4x4 mat = mul(pos,cam);

        // transform
        vert = mul( mat, float4(vert + float3( 10* sin(TIME *3), 10 *cos(TIME * 3.0), -15.0),1.0) ).xyz;

        // // perspective
        vert.z = 1.0 / vert.z;
        vert.xy *= vert.z;

        // rasterize
        // c += fragment(vert) * circle(uv,vert.xy,-vert.z,line_width);
        c += circle(uv,vert.xy,-vert.z,line_width);
    }
    
    // fragment
	float4 fragColor = float4(c,1.0);



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