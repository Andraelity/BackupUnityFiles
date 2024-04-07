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
            
            float grid(float2 p) 
            {
            
                float2 orient = normalize(float2(1.0,3.0));
                float2 perp = float2(orient.y, -orient.x);
                float g = fmod(floor(1. * dot(p, orient)) + floor(1. * dot(p, perp)), 2.);
                return g;
            
            }


            fixed4 frag (v2f i) : SV_Target
            {

                float cellSize = 0.03;
                // colors
                float4 col0 = float4(0.5, 0.5, 0.5, 1.0);
                float4 col1 = float4(0.7, 0.7, 0.7, 1.0);
            

	            const float pairSize = cellSize * 2.0;

                bool a = fmod(i.uv.x, pairSize) < cellSize;
                bool b = fmod(i.uv.y, pairSize) < cellSize;
    
                float4 col = ((a && !b) || (!a && b)) ? col0 : col1;;
                
               	float2 p = ((i.uv - 0.5) * 2.0 ) / 10.0;
               	float2 q = (((i.uv - 0.5) * 0.5 ) *float2(30.0, 5.0) ) - float2(-TIME/5.0, TIME/5.0) + float2( 50.0, 0.0);

                // return float4(i.uv.xy, 0.0, 1.0);
                // return float4(coordinateScale, 0.0, 1.0);
                
                float samp = 30.0;
                
                
                //fragColor = vec4(p, 0.0, 1.0);
                 return float4(grid(q), grid(q)/6.0, 0.0, 1.0);


            }
            ENDCG
        
        }
    }
}
