Shader "Spell Shader"
{
    Properties
    {
        [NoScaleOffset]_MainTex ("Spell Texture", 2D) = "white" {}
[HideInInspector]_UnscaledTime ("UnScale Time", Float) = 0
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha One 
		Cull Off Lighting Off ZWrite Off

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
				half3 normal : NORMAL;
};

            struct v2f
            {
                float2 uv : TEXCOORD0;
				float2 uv1 : TEXCOORD1;
				float2 uv2 : TEXCOORD2;
				float fresnel : TEXCOORD3;
                float4 vertex : SV_POSITION;
        UNITY_FOG_COORDS(4)
};

            sampler2D _MainTex;
            float4 _MainTex_ST;
float _UnscaledTime;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                float2 ngu = TRANSFORM_TEX(v.uv, _MainTex);
				float3 i = normalize(ObjSpaceViewDir(v.vertex));
				o.fresnel = pow((1.0 - abs(dot(v.normal, i))), 10);
				o.uv = ngu;
    o.uv1 = float2(ngu.x + (_UnscaledTime * 0.3), ngu.y);
    o.uv2 = float2(ngu.x - (_UnscaledTime * 0.3), ngu.y);
    UNITY_TRANSFER_FOG(o, o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 a = tex2D(_MainTex, i.uv);
				fixed4 b = tex2D(_MainTex, i.uv1);
				fixed4 c = tex2D(_MainTex, i.uv2);
				fixed4 abc = fixed4(1,0.46,0,1)+fixed4(1,0.46,0,1);
				abc.a = (a.r+b.g+c.b)+i.fresnel;
    UNITY_APPLY_FOG(i.fogCoord, abc);
                return abc;
            }
            ENDCG
        }
    }
}
