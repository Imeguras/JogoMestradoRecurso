Shader "Custom/Linhas"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineThickness ("Outline Thickness", Range(0, 1)) = 1
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
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

            struct appdata_t
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
            float _OutlineThickness;
            float4 _OutlineColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);

                // Calculate outline
                float2 offset = float2(_OutlineThickness, _OutlineThickness);
                float outline = tex2D(_MainTex, i.uv + offset).a;
                outline += tex2D(_MainTex, i.uv - offset).a;
                outline += tex2D(_MainTex, i.uv + float2(offset.x, -offset.y)).a;
                outline += tex2D(_MainTex, i.uv + float2(-offset.x, offset.y)).a;

                if (outline < 4.0)
                {
                    // If near the edge, use the outline color
                    return _OutlineColor;
                }

                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}