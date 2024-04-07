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


#define t (TIME)

#define DETAIL 8

float hashThisShader(float v) { return frac(sin(v)*45841.117); }
float noise(float v) {
	float F = floor(v), f = frac(v);
	f = f * f * (3. - 2. *f);
	return lerp(hashThisShader(F), hashThisShader(F+1.), f);
}
float noise(float2 v) {
	float2 F = floor(v), f = frac(v);
	float V = F.x + F.y * 117.;
	f = f * f * (3. - 2. *f);
	return lerp(lerp(hashThisShader(V), hashThisShader(V+1.), f.x),
			    lerp(hashThisShader(V+117.), hashThisShader(V+118.), f.x), f.y);
}
float fnoise(float2 v) {
	return .5 * noise(v) + .25*noise(v*1.98) + .125 * noise(v * 4.12);
}

float isect_tri(in float3 O, in float3 D, in float3 v0, in float3 v1, in float3 v2, out float3 n, out float3 p, out float3 c) {
	// MÃ¶llerâ€“Trumbore
	float3 e1 = v1 - v0;
	float3 e2 = v2 - v0;
	float3 P, Q, T;
	float det, inv_det, u, v, tt;
	P = cross(D, e2);
	det = dot(e1, P);
	if (abs(det) < 1e-4) return -1.;
	inv_det = 1. / det;
	T = O - v0;
	u = dot(T, P) * inv_det;
	if (u < 0. || u > 1.) return -1.;
	Q = cross(T, e1);
	v = dot(D, Q) * inv_det;
	if (v < 0. || (v+u) > 1.) return -1.;
	tt = dot(e2, Q) * inv_det;
	if (tt <= 0.) return -1.;
	n = normalize(cross(e1, e2));
	p = O + D * tt;
	c = float3(u, v, 1. - u - v);
	return tt;
}

float3 lookat(float3 pos, float3 at, float3 rdir) {
	float3 f = normalize(at - pos);
	float3 r = cross(f, float3(0., 1., 0.));
	float3 u = cross(r, f);

	float3x3 mat3 = {r, u, -f};
	return mul(mat3, rdir);
}

struct material_t {
	float3 diffuse;
	float3 specular;
	float specular_power;
};

float3 light_dir(float3 at, float3 normal, float3 l_eye, material_t m, float3 l_color, float3 l_dir) {
	float3 color = m.diffuse * l_color * max(0.,dot(normal,l_dir));
	
	if (m.specular_power > 0.) {
		float3 h = normalize(l_dir + l_eye);
		color += l_color * m.specular * pow(max(0.,dot(normal,h)), m.specular_power) * (m.specular_power + 8.) / 25.;
	}
	return color;
}

float3 v_i(in float2 v) {
	float phi = v.x * 6.2831;
	float theta = v.y * 3.1416;
	float r = sin(theta);
	float R = 1.5 + .6*sin(phi*2.+ t * sin(theta*3.+t/30.0));//noise(sin(10.*fract(v)+t*1.4));
	return R * float3(r*cos(phi), cos(theta), r*sin(phi));
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
	
	float2 uv = coordinateScale - float2(0.0, 0.25);
	
	
	float ta = 3.5 * cos(75.5*.14);
	float3 at = .3*(float3(noise(75.5+.3), noise(ta), noise(75.5+7.6)) - .5);
	float3 O = 5. * float3(cos(ta), cos(75.5*.3), sin(ta));
	float3 D = lookat(O, at, normalize(float3(uv, -2.)));
	
	float3 color = float3(0.,.2,.1);
	float3 N, P, C;
	float minlen = 10000.;
	
	material_t m;
	m.diffuse = (.8);
	m.specular = 10. * float3(1., .2, 1.);
	m.specular_power = 30.;
	
	for (int i = 0; i < DETAIL*DETAIL; ++i) {
		float2 vi = float2(fmod(float(i),float(DETAIL)), float(i/DETAIL)) / float(DETAIL);
		float3 v0 = v_i(vi);
		float3 v1 = v_i(vi + float2(1./float(DETAIL), 0.));
		float3 v2 = v_i(vi + float2(.0, 1./float(DETAIL)));
		float3 v3 = v_i(vi + float2(1./float(DETAIL), 1./float(DETAIL)));
		float3 n, p, c;
		float len = isect_tri(O, D, v0, v3, v1, n, p, c);
		if (len >= 0. && len < minlen && dot(n,D) >= 0.) {
			N = n;
			P = p;
			C = c;
			minlen = len;
		}
		len = isect_tri(O, D, v0, v2, v3, n, p, c);
		if (len >= 0. && len < minlen && dot(n,D) >= 0.) {
			N = n;
			P = p;
			C = c;
			minlen = len;
		}
	}
	
	if (minlen < 10000.) {
		C = float3(smoothstep(.3,.8,pow(min(C.x, min(C.y, C.z)), .1)), smoothstep(.3,.8,pow(min(C.x, min(C.y, C.z)), .1)), smoothstep(.3,.8,pow(min(C.x, min(C.y, C.z)), .1)));
		float3 eyedir = normalize(at-O);
		color = C * m.diffuse * .03;
		color += C * light_dir(P, N, eyedir, m, float3(1.,.2,.1), normalize(float3(1.,0.,1.)));
		color += C * light_dir(P, N, eyedir, m, float3(.0,.2,.9), normalize(float3(0.,1.,-1.)));
	} else {
		float2 np = uv*20. + 2.7*at.xy;
		np.x += .2 * cos(fnoise(np) * 6.2831);
		np.y += .2 * sin(fnoise(np) * 6.2831);
		color *= 0.0;
	}
		
	float4 fragColor = float4(pow(color, float3(1./2.2, 1./2.2, 1./2.2)),color.x + color.y + color.z /3.0); 
   // fragColor = float4(uv, 0.0 ,1.0);
	
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