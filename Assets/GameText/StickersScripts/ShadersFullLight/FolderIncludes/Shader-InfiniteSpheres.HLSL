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






static float3 lp=float3(10.0,10.0,10.0);
static float4 lc=(0.5);
static float4 la=(0.5);
static float4 mc=(0.0);

float s;
float t;
void srand(float2 p){
	s=sin(dot(p,float2(423.62431,321.54323)));
}
float rand(){
	s=frac(s*32322.65432+0.12333);
	return abs(frac(s));
}
float3x3 rot_x(float a){
	float c=cos(a);
	float s=sin(a);
	float3x3 matRot = {1.0,0.0,0.0,0.0,c,-s,0.0,s,c};

	return matRot;

}


float3x3 rot_y(float a){
	float c=cos(a);
	float s=sin(a);
	float3x3 matRot = {c,0.0,s,0.0,1.0,0.0,-s,0.0,c};
	return matRot;

}


float3x3 rot_z(float a){
	float c=cos(a);
	float s=sin(a);
	float3x3 matRot = {c,-s,0.0,s,c,0.0,0.0,0.0,1.0};

	return matRot;
}

float3x3 rot(float3 z,float a){
	float c=cos(a);
	float s=sin(a);
	float ic=1.0-c;
	float3x3 matRot = {
		ic*z.x*z.x+c,ic*z.x*z.y-z.z*s,ic*z.z*z.x+z.y*s,
		ic*z.x*z.y+z.z*s,ic*z.y*z.y+c,ic*z.y*z.z-z.x*s,
		ic*z.z*z.x-z.y*s,ic*z.y*z.z+z.x*s,ic*z.z*z.z+c
	};
	return matRot;
}

float plane(float3 p,float4 n){
	float d=dot(p,n.xyz)+n.w;
	if(d<0.0)mc=(0.0);
	return d;
}
float sphere(float3 p,float r){
	float4 c=(1.0);
	float3 tmp;
	float2 rp=float2(floor(p.x),floor(p.y));
	srand(rp);
	c.r=rand()*0.2+0.2*sin(t/4.0+PI/4.0)+0.2;
	c.g=rand()*0.2+0.4*sin(t/3.0+PI/3.0)+0.2;
	c.b=rand()*0.2+0.2*sin(t/2.0)+0.3;
	float x=(frac(p.x)-0.5);
	float y=(frac(p.y)-0.5);
	float tmp2=rand();
	float z=rand()*5.0*(sin(t/2.0+tmp2*PI*2.0)+1.0);
	tmp=float3(x,y,p.z-z);
	float d=length(tmp)-r;
	if(d<0.0)mc=c;
	return min(d,0.1);
}

float dist(float3 p){
	float d=100.0;
	d=min(d,plane(p,float4(0.0,0.0,1.0,0.5)));
	d=min(d,sphere(p,0.5));
	return d;
}

float3 normal(float3 p){
	float d=dist(p);
	float3 n=float3(0.01,0.0,0.0);
	return normalize(float3(dist(p+n.xyy)-d,dist(p+n.yxy)-d,dist(p+n.yyx)-d));
}

float trace_light(float3 p,float3 dv){
	float d;
	float a=1.0;
	float l=0.1;
	p+=dv*l;
	for(int i=0;i<32;i++){
		d=dist(p);
		if(d<0.01){
			return 0.0;
		}
		a=min(a,2.0*d/l);
		l+=d;
		p+=dv*d;
	}
	return a;
}

float4 trace(float3 p,float3 dv){
	float d;
	float dt=0.0;
	float3 lv;
	float4 c;
	mc=(0.0);
	for(int i=0;i<256;i++){
		d=dist(p);
		dt+=d;
		if(d<0.0)break;
		d=max(d,0.01);
		p+=dv*d;
	}
	c=mc;
	float3 n=normal(p);
	lv=normalize(lp-p);
	float4 df=clamp(c*lc*dot(n,lv),0.0,1.0);
	float4 ab=c*la;	
	float sd=trace_light(p-0.01*dv,lv);
	float sp=max(pow(dot(lv,reflect(dv,n)),length(lp-p)),0.0);
	return min(df,float4(1.0, 1.0, 1.0, 1.0)*sd)+ab+min(sd,sp);
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

	t=TIME;
	float h=2.0;
	float fov=0.75;
	float2 sp= coordinateScale;
	
	float3 la = float3(5.0*sin(t/2.0+PI),5.0*cos(t/3.0+PI),sin(t/4.0)+2.0);
	float3 ep = float3(2.0*sin(t),2.0*cos(t),sin(t)*2.0+6.0);
	
	srand(sp);
	float3 rv;
	float dof=0.2;
	rv.x=(rand()-0.5)*dof;
	rv.y=(rand()-0.5)*dof;
	rv.z=(rand()-0.5)*dof;
	ep+=rv;
	
	
	/*
	vec3 la=vec3(0.0,0.0,h);
	vec3 ep=vec3(0.0,-3.0,0.0);
	ep*=rot_x(PI*m.y/2.0+PI/8.0-PI/4.0);
	ep*=rot_z(2.0*PI*m.x);
	ep.z+=h;
	*/
	
	float3 uv = float3(0.0,0.0,1.0);
	float3 dv = normalize(la-ep);
	float3 hv = normalize(cross(dv,uv));
	float3 vv = normalize(cross(hv,dv));
	dv = mul(rot(vv,fov*sp.x), dv);
	dv = mul(rot(hv,fov*sp.y), dv);
	float4 fragColor = trace(ep,dv) + 0.5;


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