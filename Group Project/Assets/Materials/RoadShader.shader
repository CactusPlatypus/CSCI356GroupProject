Shader "Custom/RoadShader"
{
    Properties
    {
        _InnerColor("Inner Color", Color) = (0.0,0.0,0.0,1)
        _OuterColor("Outer Color", Color) = (1,0.8,0.20,1)
        _Glossiness("Smoothness", Range(0,1)) = 0.25
        _Metallic("Metallic", Range(0,1)) = 0.2
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        struct Input
        {
            float2 texcoord; // Set in vertex shader
            float3 worldPos; // Set automatically
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _InnerColor;
        fixed4 _OuterColor;

        // Required to pass texture coordinates to surface shader
        void vert(inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.texcoord = v.texcoord;
        }

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
            // Create grid of circles using mod 1 distance to 0.5
            float circleGrid = length(frac(IN.worldPos.xz) - 0.5);
            float circleMix = lerp(2.0 - noise2(IN.worldPos), -12.0, circleGrid);

            // Mix circles with inner color
            fixed4 inner = _InnerColor + saturate(circleMix) * 0.07;

            // Create outer border using distance to 0.5
            float distToCenter = abs(IN.texcoord.x - 0.5);
            float wobblyEdges = noise(IN.worldPos) * 0.5;
            float outerMix = distToCenter * 32 - 12 + wobblyEdges;

            // Create more circles for fun
            circleGrid = length(frac(IN.worldPos.xz * 1.5) - 0.5);
            circleMix = lerp(2.0 + noise2(IN.worldPos * 3.0), -12.0, circleGrid);

            // Mix inner color with outer color
            fixed4 outer = lerp(inner, _OuterColor, saturate(outerMix)) - saturate(circleMix) * 0.05;

            o.Albedo = outer.rgb;
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = outer.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}