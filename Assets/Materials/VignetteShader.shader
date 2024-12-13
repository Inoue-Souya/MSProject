Shader "Custom/VerticalGradientBlink"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _GlowColor ("Glow Color", Color) = (1, 0, 0, 1) // �����̐F
        _Intensity ("Max Intensity", Range(0, 1)) = 0.5 // �ő�G�t�F�N�g�̋����i�㕔�j
        _MinIntensity ("Min Intensity", Range(0, 1)) = 0.1 // �ŏ��G�t�F�N�g�̋����i�����j
        _Sharpness ("Gradient Sharpness", Range(1, 10)) = 5 // �O���f�[�V�����̉s��
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
            float _Sharpness;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // UV���W��y��������ɉs���O���f�[�V�������쐬
                float gradient = pow(saturate(i.uv.y), _Sharpness);
                gradient = lerp(_MinIntensity, _Intensity, gradient);

                // �e�N�X�`�����擾
                fixed4 color = tex2D(_MainTex, i.uv);

                // �����̓K�p
                color = lerp(color, _GlowColor, gradient);

                return color;
            }
            ENDCG
        }
    }
}
