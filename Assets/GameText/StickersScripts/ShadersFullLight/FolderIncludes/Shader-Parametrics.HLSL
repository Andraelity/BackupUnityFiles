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



#define time TIME 
#define pi 3.14159265

//Included a few types of parametric curves
// 1= Hypotrochoid | 2 = Epitrochoid | 3 = Rose curve | 4 = Lissajous | 5 =  Hypocycloid
// #define TYPE 3

//Coloring scheme
// 1 = Default | 2 = Vintage | 3 = Argon
#define COLOR_TYPE 3

//might need to be adjusted depending on the curve
#define STRIDE 0.035

// #if TYPE == 5
// #undef STRIDE
// #define STRIDE 0.13
// #endif

//the main number of iterations to find the global minimum region (simple curves need much less)
#define ITR 100

//for animations
float sfloor(in float x, in float w)
{
    float fx = floor(x);
    return fx+smoothstep(fx+w,fx+1.-w,x);
}

//parametric equation
float2 f(in float t, in int type)
{   
    float x = 0;
    float y = 0;

    float TYPE = type;
    //Hypotrochoid
    if (TYPE == 1)
    {
	    
	    float cam= sfloor(time*0.6,.2)*pi;
	    float R = fmod(floor(time*0.3),9.)+3.;
	    const float r = 1.;
	    //float d = sin(time*1.)*2.+3.5;
	    float d= R;
	    x = ((R-r)*cos(t-cam)+d*cos((R-r)/r*t))/(R+.5)*3.;
	    y = ((R-r)*sin(t)-d*sin((R-r)/r*t))/(R+.5)*3.;

    }
    //Epitrochoid
    if( TYPE == 2)
    {

    	float cam= floor(time*0.4) * pi* 2;//,.1)*pi*2.;
    	const float R = 2.8;
    	const float r = .4;
    	float d = sin(time*0.21)*2.+2.6;
    	x = (R+r)*cos(t-cam)-d*cos((R+r)/r*t);
    	y = (R+r)*sin(t)-d*sin((R+r)/r*t);

    }
    
    //Rose Curve
   	if(TYPE == 3)
   	{

    	float cam= sfloor(time*0.4,.1)*pi;
    	float k = fmod(floor(time*0.4),6.)+2.;
    	x = cos(k*t-cam)*cos(t+cam)*5.5;
    	y = cos(k*t)*sin(t)*5.5;
   	
   	}
    
    //Lissajous
   	if (TYPE == 4)
   	{
    	
    	float ft = fmod((time*0.25),3.);
    	float a = 3.+ft;
    	float b = 2.+ft;
    	y = abs(sin(a*t+pi/2.+time*1.5)*5.);
    	x = abs(cos((b*t)*5.));

   	}
    
    //Hypocycloid (just a special case of hypotrochoids)
    if( TYPE == 5)
    {

    	const float a = 6.;
    	float ft = fmod(floor(time*0.25),5.)+2.25;
    	float b = 6./ft;
    	float cam= sfloor(time*0.5,.2)*pi;
    	x = ((a-b)*cos(t-cam) + b*cos(t*(a/b-1.)));
    	y = ((a-b)*sin(t) - b*sin(t*(a/b-1.)));

    }
    
    if( TYPE == 6)
    {
    	
    	x=  (abs(frac(t*1.+time*0.2)-0.5)-0.25)*20.;
    	y= ((abs(frac(t*.5+.5*abs(frac(t*.5+.5)-0.5))-0.5))-0.25)*20.;

    }

    return float2(x,y);
}

//squared distance from point (pixel) to curve
float fd(in float2 p, in float t, in int type)
{
    p = p+f(t , type);
    return dot(p,p);
}


//root finding on the derivative
float bisect(in float2 p, in float near, in float far, in int type)
{
    float mid = 0.;
    float mdrv = 0.;
    float ep = 0.02;
    for (int i = 0; i <= 5; i++)
    {
        mid = (near + far)*.5;

        mdrv = fd(p, mid+ep, type)-fd(p, mid-ep, type);
        
        if (abs(mdrv) < 0.001)break;
        
        if(mdrv > 0.) 
       	{
        	far=mid; 
       	}
		else
		{
			
			near=mid;

		}	
    }    
    //once the root is found. return the actual length (not squared)
    return length(p+ f((near + far)*.5, type));
}


// this function can most definitely be optimized
float intersect(in float2 p, in float near, in float far, in int type)
{
    float t = near;
    float told = near;
    float nr = 0., fr = 1.;
    float mn = 10000.;
    
    for (int i = 0; i <= 120; i++)
    {
        float d = fd(p, t, type);
        if (d < mn)
        {
            mn = d;
            nr = told;
            fr = t+.05;
        }
        if (t > far)break;
        told = t;
        //t += log(d1+1.15)*.03;
        t += log(d+1.15)*STRIDE;
    }
    
   	return bisect(p, nr,fr, type);
}


//Reinhard based tone mapping
float3 tone(float3 color, float gamma)
{
	float white = 2.;
	float luma = dot(color, float3(0.2126, 0.7152, 0.0722));
	float toneMappedLuma = luma * (1. + luma / (white*white)) / (1. + luma);
	color *= toneMappedLuma / luma;
	color = pow(color, float3(1. / gamma, 1. / gamma, 1. / gamma));
	return color;
}



// various noise functions
float Hash3d(float3 uv)
{
    float f = uv.x + uv.y * 37.0 + uv.z * 521.0;
    return frac(cos(f*3.333)*100003.9);
}
float mixP(float f0, float f1, float a)
{
    return lerp(f0, f1, a*a*(3.0-2.0*a));
}

static const float2 zeroOne = float2(0.0, 1.0);

float noise(float3 uv)
{
    float3 fr = frac(uv.xyz);
    float3 fl = floor(uv.xyz);
    float h000 = Hash3d(fl);
    float h100 = Hash3d(fl + zeroOne.yxx);
    float h010 = Hash3d(fl + zeroOne.xyx);
    float h110 = Hash3d(fl + zeroOne.yyx);
    float h001 = Hash3d(fl + zeroOne.xxy);
    float h101 = Hash3d(fl + zeroOne.yxy);
    float h011 = Hash3d(fl + zeroOne.xyy);
    float h111 = Hash3d(fl + zeroOne.yyy);
    return mixP(
        mixP(mixP(h000, h100, fr.x), mixP(h010, h110, fr.x), fr.y),
        mixP(mixP(h001, h101, fr.x), mixP(h011, h111, fr.x), fr.y)
        , fr.z);
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

	float2 uv = coordinate - 0.5 ;
	float2 p = coordinate  - 0.5;
    float2 bp = p+0.5;
    p *= 20.;
    float rz = intersect(p,0.,30., 2);
    
    p += 10.0;
    float rx = intersect(p, 0.0, 30.0, 3);

    p = uv * 10.0 - 5.0;
    float rn = intersect(p, 0.0, 30.0, 4);

    p = uv * 20.0 + float2(10.,-10);
    float rm = intersect(p, 0.0, 30.0, 5);

    p = uv * 20.0 + float2(-10.,10);
    float rt = intersect(p, 0.0, 30.0, 6);


    // #if COLOR_TYPE == 1
    // rz = pow(rz*2.,0.5);
    // float3 col = float3(2.,1.5,.1)*1.-log(rz+1.8);
    // col = clamp(col,0.5,10.);
    // // col *= (tex2D(_TextureChannel2,p*0.00025*1.0+frac(time)*float2(51,100)).r-0.5)*.24+1.;
    // // col *= float3(Hash3d(uv.xyx), Hash3d(uv.xyx), Hash3d(uv.xyx) );
    // col *= float3(noise(uv.xyx), noise(uv.xyx), noise(uv.xyx) );
    // col = lerp(col,tone(col,1.7),0.5);
    
    // #elif COLOR_TYPE == 2
    // vec3 col = vec3(5.,1.5,.5)*log(rz+1.05);
    // col = tone(col,8.5);
    // col *= (texture(iChannel0,p*0.00017*iResolution.x+fract(time)*vec2(51,100)).r-0.5)*.12+1.;
    
    rz = pow(rz*50.,.7);
    float3 col = float3(.6,.2,1.)/(rz+1.5)*9.;
    col *= float3(noise(uv.xyx), noise(uv.xyx), noise(uv.xyx) );
    col = tone(col,1.6);
    

    rx = pow(rx*50.,.7);
    float3 col2 = float3(.6,.2,1.)/(rx+1.5)*9.;
    col2 *= float3(noise(uv.xyx), noise(uv.xyx), noise(uv.xyx) );
    col2 = tone(col2,1.6);


    rn = pow(rn*50.,.7);
    float3 col3 = float3(.6,.2,1.)/(rn+1.5)*9.;
    col3 *= float3(noise(uv.xyx), noise(uv.xyx), noise(uv.xyx) );
    col3 = tone(col3,1.6);


    rm = pow(rm*50.,.7);
    float3 col4 = float3(.6,.2,1.)/(rm+1.5)*9.;
    col4 *= float3(noise(uv.xyx), noise(uv.xyx), noise(uv.xyx) );
    col4 = tone(col4,1.6);


    rt = pow(rt*50.,.7);
    float3 col5 = float3(.6,.2,1.)/(rt+1.5)*9.;
    col5 *= float3(noise(uv.xyx), noise(uv.xyx), noise(uv.xyx) );
    col5 = tone(col5,1.6);


    col3 = lerp(col2, col3, 0.5) * 1.5;
    col5 = lerp(col4, col5, 0.5) * 1.5;
    // col5 = lerp(col3, col5, 0.5) * 2.0;


    col5 = smoothstep(1.0 , col5, col );
    col5 = smoothstep(0.0 , col5, col3 );

    // col5 = smoothstep(1.0 , col5, 0.5 );
    // col5 = tone(col5,1.5);


	// col *= pow(16.0*bp.x*bp.y*(1.0-bp.x)*(1.0-bp.y),.45);
    
	float4 fragColor = float4(col5,1.0);

	// fragColor= smoothstep(0.0, 1.0, fragColor);

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