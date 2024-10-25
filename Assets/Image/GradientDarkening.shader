Shader "Custom/GradientDarkeningShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DarkColor ("Dark Color", Color) = (0, 0, 0, 1)
        _Center ("Center", Vector) = (0.5, 0.5, 0)
        _Radius ("Radius", Float) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

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
            float4 _DarkColor;
            float2 _Center;
            float _Radius;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            half4 frag (v2f i) : SV_Target
            {
                float2 diff = i.uv - _Center;
                float dist = length(diff);

                // グラデーションで暗くする
                float factor = smoothstep(_Radius, 0.0, dist);

                half4 texColor = tex2D(_MainTex, i.uv);
                half4 finalColor = lerp(texColor, _DarkColor, factor);
                return finalColor;
            }
            ENDCG
        }
    }
}
