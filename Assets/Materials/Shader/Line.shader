Shader "Custom/Line"
{
    Properties
    {
        // _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
                "IgnoreProjector"="True"
                "Quene"="Transparent"}
        LOD 200
        // Blend SrcAlpha OneMinusSrcAlpha

        // Pass
        // {


        Pass {
            CGPROGRAM
// Upgrade NOTE: excluded shader from DX11; has structs without semantics (struct v2f members uv)
#pragma exclude_renderers d3d11
            // #pragma surface surf  NoLight
            #pragma vertex vert //alpha noforwardadd
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct v2f {
           //     float3 pos;
                float2 uv;
            };

            v2f vert (flaot2 uv : TEXCOORD0) {
              //  v2f.pos = UnityObjectToClipPos(vertex);
                v2f.uv = uv;
            }

            fixed4 _Color;

            fixed4 frag (v2f v) : Color{
                _Color.x = 255;
                // color.z = 1.0f;
                // color.a = 1.0f;

                return _Color;
            }
            ENDCG
        }

        // float4 LightingNoLight(SurfaceOutput s, float3 lightDir, half3 vierDir, half atten) {
        //     float4 c;
        //     c.rgb = s.Albedo;
        //     c.a = s.Alpha;
        //     return c;           
        // }

        // sampler2D _MainTex;
        // fixed4 _SelfCol;

        // struct Input {
        //     float2 uv_MainTex;
        //     float4 vertColor;
        // };

        // void vert(inout appdata_full v, out Input o) {
        //     o.vertColor = v.color;
        //     o.uv_MainTex = v.texcoord;
        // }

        // void surf (Input IN, inout SurfaceOutput o) {
        //     half4 c = tex2D (_MainTex, IN.uv_MainTex);
        //     o.Alpha = c.a * IN.vertColor.a;
        //     o.Albedo = IN.vertColor.rgb;
        // }

        // ENDCG
        // }
    }
    FallBack "Diffuse"
}
