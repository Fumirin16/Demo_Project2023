Shader "Unlit/StageModelShader"
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
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;

                // ���C�e�B���O
                float4 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;

                // ���C�e�B���O�i�@���j
                half3 normalWorld : TEXCOORD1;

                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);

                // ���C�e�B���O
                float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                //o.viewDir = normalize(UnityWorldSpaceViewDir(worldPos));

                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);

                // ���C�e�B���O
                // ���������x�N�g���𐳋K��
                half3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

                // ���C�g�̐ݒ�l
                half3 ambient = 0.1f;
                half4 lightColor0 = float4(0.9f, 0.9f, 0.9f, 1.0f);

                // ���C�g�̔��f
                float NdotL = dot(i.normalWorld, lightDir); // �m�[�}���}�b�v
                float diffusePower = max(ambient, NdotL);
                half4 diffuse = diffusePower * col * lightColor0;

                fixed4 output = diffuse;

                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, output);
                return col;
            }
            ENDCG
        }
    }
}
