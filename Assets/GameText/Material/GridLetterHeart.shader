Shader "Unlit/Grid"
{
    Properties
    {

        _MainTex ("Texture", 2D) = "white" {}

    }
    SubShader
    {

        Tags { "RenderType"="Opaque" }
        
        LOD 100

        Pass
        {
        
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;  
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            #define TIME _Time.y
            
#define POINT_COUNT 8

static float2 points[POINT_COUNT];
static const float speed = -0.5;
static const float len = 0.25;
static const float scale = 0.012;
static float intensity = 1.3;
static float radius = 0.015;

//https://www.shadertoy.com/view/MlKcDD
//Signed distance to a quadratic bezier
float sdBezier(float2 pos, float2 A, float2 B, float2 C){    
    float2 a = B - A;
    float2 b = A - 2.0*B + C;
    float2 c = a * 2.0;
    float2 d = A - pos;

    float kk = 1.0 / dot(b,b);
    float kx = kk * dot(a,b);
    float ky = kk * (2.0*dot(a,a)+dot(d,b)) / 3.0;
    float kz = kk * dot(d,a);      

    float res = 0.0;

    float p = ky - kx*kx;
    float p3 = p*p*p;
    float q = kx*(2.0*kx*kx - 3.0*ky) + kz;
    float h = q*q + 4.0*p3;

    if(h >= 0.0){ 
        h = sqrt(h);
        float2 x = (float2(h, -h) - q) / 2.0;
        float2 uv = sign(x)*pow(abs(x), float2(1.0/3.0, 1.0/3.0));
        float t = uv.x + uv.y - kx;
        t = clamp( t, 0.0, 1.0 );

        // 1 root
        float2 qos = d + (c + b*t)*t;
        res = length(qos);
    }else{
        float z = sqrt(-p);
        float v = acos( q/(p*z*2.0) ) / 3.0;
        float m = cos(v);
        float n = sin(v)*1.732050808;
        float3 t = float3(m + m, -n - m, n - m) * z - kx;
        t = clamp( t, 0.0, 1.0 );

        // 3 roots
        float2 qos = d + (c + b*t.x)*t.x;
        float dis = dot(qos,qos);
        
        res = dis;

        qos = d + (c + b*t.y)*t.y;
        dis = dot(qos,qos);
        res = min(res,dis);

        qos = d + (c + b*t.z)*t.z;
        dis = dot(qos,qos);
        res = min(res,dis);

        res = sqrt( res );
    }
    
    return res;
}


//http://mathworld.wolfram.com/HeartCurve.html
float2 getHeartPosition(float t){
    return float2(16.0 * sin(t) * sin(t) * sin(t),
                -(13.0 * cos(t) - 5.0 * cos(2.0*t)
                - 2.0 * cos(3.0*t) - cos(4.0*t)));
}

//https://www.shadertoy.com/view/3s3GDn
float getGlow(float dist, float radius, float intensity){
    return pow(radius/dist, intensity);
}

float getSegment(float t, float2 pos, float offset){
	
    for(int i = 0; i < POINT_COUNT; i++){
        points[i] = getHeartPosition(offset + float(i)*len + frac(speed * t) * 6.28);
    }
    
    float2 c = (points[0] + points[1]) / 2.0;
    float2 c_prev;
	float dist = 10000.0;
    
    for(int i = 0; i < POINT_COUNT-1; i++){
        //https://tinyurl.com/y2htbwkm
        c_prev = c;
        c = (points[i] + points[i+1]) / 2.0;
        dist = min(dist, sdBezier(pos, scale * c_prev, scale * points[i], scale * c));
    }

    return max(0.0, dist);

}


            fixed4 frag (v2f i) : SV_Target
            {
    
                float2 centre = float2(0.5, 0.5);
                float2 pos = (centre - i.uv) /2.0;

                //Shift upwards to centre heart
                pos.y += 0.03;

                float t = TIME;

                //Get first segment
                float dist = getSegment(t, pos, 0.0);
                float glow = getGlow(dist, radius, intensity);

                float3 col = (0.0);

                //White core
                col += 10.0 * (smoothstep(0.006, 0.003, dist));
                //Pink glow
                col += glow * float3(1.0,0.16,0.0);

                //Get second segment
                dist = getSegment(t, pos, 3.4);
                glow = getGlow(dist, radius, intensity);

                //White core
                col += 10.0 * (smoothstep(0.006, 0.003, dist));
                //Blue glow
                col += glow * float3(1.0,0.16,0.0);

                //Tone mapping
                col = 1.0 - exp(-col);

                //Gamma
                col = pow(col, float3(0.4545, 0.4545, 0.4545));

                //Output to screen
                // return float4(i, 0.0,1.0);
                return float4(col,1.0);

            }
            
            ENDCG
        
        }
    }
}
