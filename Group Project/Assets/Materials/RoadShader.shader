Shader "Custom/RoadShader"
{
    Properties
    {
        _MainTex("Base (RGB)", 2D) = "white" {}
        _InnerColor("Inner Color", Color) = (0.12,0.12,0.12,1)
        _OuterColor("Outer Color", Color) = (1,0.8,0.20,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.5
        _Metallic("Metallic", Range(0,1)) = 0.0
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _InnerColor;
        fixed4 _OuterColor;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        float noise(float3 pos)
        {
            return cos(pos.z / 3) + sin(pos.x / 5) + sin(pos.z / 7) + cos(pos.x / 9);
        }

        float noise2(float3 pos)
        {
            return sin(pos.x / 3) + cos(pos.z / 3) + sin(pos.x / 5) + sin(pos.z / 5);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float distToCenter = abs(0.5 - IN.uv_MainTex.x);
            float jaggedEdges = noise(IN.worldPos) * 0.5;
            float mult = distToCenter * 32 - 12 + jaggedEdges;

            float circleDist = distance(fixed2(0.5, 0.5), frac(IN.worldPos.xz));
            float circle = saturate(lerp(4.0 + noise2(IN.worldPos), -14.0, circleDist));

            fixed4 c = lerp(_InnerColor + circle * 0.1, _OuterColor, clamp(mult, 0, 1));
            o.Albedo = c.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}