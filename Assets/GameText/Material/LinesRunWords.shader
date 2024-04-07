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
            
            float hash(int x) { return frac(sin(float(x))*7.847); } 

            float dSegment(float2 a, float2 b, float2 c)
            {
                float2 ab = b-a;
                float2 ac = c-a;

                float h = clamp(dot(ab, ac)/dot(ab, ab), 0., 1.);
                float2 punto = a+ab*h;
                return length(c-punto);
            }

            fixed4 frag (v2f i) : SV_Target
            {
    
    
            float2 uv = (i.uv - 0.5) * 2.0;

            float3 color = (0.);


            for(int i=0; i < 100; ++i)
            {

                float2 a = float2(hash(i)*2.-1., hash(i+1)*2.-1.);
                float2 b = float2(hash(10*i+1)*2.-1., hash(11*i+2)*2.-1.);
                float3 lineColor = float3(hash(10+i) + 0.2, 0.16, 0.0);


                float speed = b.y*0.15;
                float size = (0.005 + 0.3*hash(5+i*i*2)) + (0.5+0.5*sin(a.y*5.+TIME*speed))*0.1;

                a += float2(sin(a.x*20.+TIME*speed), sin(a.y*15.+TIME*0.4*speed)*0.5);
                b += float2(b.x*5.+cos(TIME*speed), cos(b.y*10.+TIME*2.0*speed)*0.5);
                float dist = dSegment(a, b, uv);

                float soundWave = 1.0;
                
                color += lerp(lineColor, float3(0.0, 0.0, 0.0), smoothstep(0., 1.0, pow(dist/size, soundWave*(0.5+0.5*sin(TIME*2.+size+lineColor.x*140.))*0.20) ));

            }

	        return  float4(color,1.0);

            }
            ENDCG
        
        }
    }
}
