Shader"Electric Shader"
{
    Properties
    {
        [NoScaleOffset]_MainTexaaa ("Texture", 2D) = "white" {}
		[NoScaleOffset]_Mask ("Mask", 2D) = "white" {}
[HideInInspector]_UnscaledTime ("UnScale Time", Float) = 0
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
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
};

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float2 uv2 : TEXCOORD1;
    UNITY_FOG_COORDS(2)
           
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTexaaa;
            float4 _MainTexaaa_ST;
float _UnscaledTime;
			sampler2D _Mask;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTexaaa);
    o.uv2 = v.uv2.y + (_UnscaledTime * 0.5);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 a = tex2D(_MainTexaaa, i.uv);
				fixed4 b = tex2D(_Mask, i.uv2);
				fixed4 final = (1-a.a)*b.r*fixed4(0.0235849,0.5980856,1,1)+a;
    final.a = 1;
                UNITY_APPLY_FOG(i.fogCoord, final);
                return final;
            }
            ENDCG
        }
    }
}
