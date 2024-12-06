Shader "Custom/VerticalGradientBlink"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 0, 0, 1) // 発光の色
        _Intensity ("Max Intensity", Range(0, 1)) = 0.5 // 最大エフェクトの強さ（上部）
        _MinIntensity ("Min Intensity", Range(0, 1)) = 0.1 // 最小エフェクトの強さ（下部）
    }
    SubShader
    {
        Tags { "Queue"="Overlay" "RenderType"="Opaque" }
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
            float4 _GlowColor;
            float _Intensity;
            float _MinIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // UV座標のy成分を基準に強度を計算
                float gradient = lerp(_MinIntensity, _Intensity, i.uv.y);

                // テクスチャを取得
                fixed4 color = tex2D(_MainTex, i.uv);

                // 発光の適用
                color = lerp(color, _GlowColor, gradient);

                return color;
            }
            ENDCG
        }
    }
}
