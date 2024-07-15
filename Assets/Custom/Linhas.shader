Shader "Custom/Linhas"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineThickness ("Outline Thickness", Range(0, 0.1)) = 0.01
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
		_ForegroundColor ("Foreground Color", Color) = (1,1,1,1)

		_TimeScale ("Time Scale", Range(0, 1)) = 0.5
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
			float4 _ForegroundColor;
			float _TimeScale;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
			//fresnel
			float4 fresnel(float3 normal, float3 viewDir, float power)
			{
				return _ForegroundColor * pow(1 + dot(normal, viewDir), power);
			}

			
			fixed4 frag (v2f i) : SV_Target
            {
                fixed4 color = tex2D(_MainTex, i.uv);
				
				
				float3 normal = normalize(float3(i.uv.x - 0.5, i.uv.y - 0.5, 0));
				float3 viewDir = normalize(_WorldSpaceCameraPos - mul(unity_ObjectToWorld, i.vertex).xyz);
				color = lerp(color, fresnel(normal, viewDir, 2), _TimeScale);
				
				
			
                return color;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}