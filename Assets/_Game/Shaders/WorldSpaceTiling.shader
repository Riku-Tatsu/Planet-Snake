Shader "Custom/WorldSpaceTilingWithColorAndShadows"
{
    Properties
    {
        _MainTex ("Primary Texture", 2D) = "white" {}
        _SecondaryTex ("Secondary Texture", 2D) = "white" {}
        _TilingPrimary ("Primary Tiling", Float) = 1.0
        _TilingSecondary ("Secondary Tiling", Float) = 1.0
        _Color ("Color Tint", Color) = (1,1,1,1)
        _Metallic ("Metallic", Range(0, 1)) = 0.0
        _Smoothness ("Smoothness", Range(0, 1)) = 0.5
        _Threshold ("Threshold", Range(0, 1)) = 0.1
        _OutlineColor ("Outline Color", Color) = (1,1,1,1)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard fullforwardshadows

        sampler2D _MainTex;
        sampler2D _SecondaryTex;
        float _TilingPrimary;
        float _TilingSecondary;
        fixed4 _Color;
        half _Metallic;
        half _Smoothness;
        float _Threshold;
        fixed4 _OutlineColor;

        struct Input
        {
            float3 worldPos;
            float3 worldNormal;
            float2 uv_MainTex;
        };

        void vert (inout appdata_full v, out Input o)
        {
            UNITY_INITIALIZE_OUTPUT(Input, o);
            o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
            o.worldNormal = mul((float3x3)unity_ObjectToWorld, v.normal);
            o.uv_MainTex = v.texcoord.xy;
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            float3 absWorldNormal = abs(IN.worldNormal);
            float3 blendWeights = normalize(max(absWorldNormal, 0.00001));

            float2 uvPrimary = IN.uv_MainTex * _TilingPrimary;
            float2 uvSecondary = IN.uv_MainTex * _TilingSecondary;

            fixed4 primaryTex = tex2D(_MainTex, uvPrimary) * _Color;
            primaryTex.a *= _Color.a;

            fixed4 secondaryTex = tex2D(_SecondaryTex, uvSecondary);

            // Perform color key operation to discard black parts in the secondary texture with a threshold
            float3 keyColor = float3(0, 0, 0);
            float secondaryAlpha = step(_Threshold, length(secondaryTex.rgb - keyColor));
            secondaryTex.a *= secondaryAlpha * _OutlineColor.a;

            // Apply outline color to the secondary texture
            secondaryTex.rgb = _OutlineColor.rgb;

            // Combine primary texture and secondary texture
            fixed4 finalColor = lerp(primaryTex, secondaryTex, secondaryTex.a);

            o.Albedo = finalColor.rgb;
            o.Alpha = finalColor.a;
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
