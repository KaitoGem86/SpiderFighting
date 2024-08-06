Shader "Custom/NiorSpider"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_Color ("Color Holo", Color) = (1,1,1,1)
		_Trans ("Fade", Range(0,6)) = 0
		
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
				float ngu : TEXCOORD2;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			float _Trans;
			float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				float scale = length(float3(unity_ObjectToWorld[0].y, unity_ObjectToWorld[1].y, unity_ObjectToWorld[2].y));
				o.ngu = v.vertex.y*20*scale;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 base = fixed4(col.rgb,1);
				float remap1 = -4.3 + (_Trans - 0) * (4.3 +4.3) / (1 - 0);
				float remap2 = -3.3 + (_Trans - 0) * (3.3 +3.3) / (1 - 0);
				float alpha1 = saturate(-i.ngu+remap1+1.5)*(1-col.a);
				fixed4 holo = fixed4(_Color.rgb,alpha1*3);
				//col.a = saturate(-i.ngu+remap2);
				fixed4 final = lerp(holo,base,saturate(-i.ngu+remap2));
                UNITY_APPLY_FOG(i.fogCoord, final);
                return final;
            }
            ENDCG
        }
    }
}
