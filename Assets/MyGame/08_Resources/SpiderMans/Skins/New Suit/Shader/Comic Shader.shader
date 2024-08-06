Shader "Comic Shader"
{
	Properties
	{
		_MainTex ("Base (RGB)", 2D) = "grey" {}
		[NoScaleOffset]_MatCap ("MatCap (RGB)", 2D) = "white" {}
	}
	
	Subshader
	{
		Tags { "RenderType"="Opaque" }
		
		Pass
		{
			CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_fog
				#include "UnityCG.cginc"
				
				struct v2f
				{
					float4 pos	: SV_POSITION;
					float2 uv 	: TEXCOORD0;
					float2 uv2 	: TEXCOORD1;
					float2 cap	: TEXCOORD2;
					float3 posi	: TEXCOORD3;
					UNITY_FOG_COORDS(4)
				};
				
				uniform float4 _MainTex_ST;
				
				v2f vert (appdata_base v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
					float dist = 50/(distance(_WorldSpaceCameraPos, mul(unity_ObjectToWorld, v.vertex)));
					o.uv2 =  UnityObjectToClipPos(v.vertex).xy*(dist+15);
					float3 worldNorm = normalize(unity_WorldToObject[0].xyz * v.normal.x + unity_WorldToObject[1].xyz * v.normal.y + unity_WorldToObject[2].xyz * v.normal.z);
					worldNorm = mul((float3x3)UNITY_MATRIX_V, worldNorm);
					o.posi = v.vertex;
					o.cap.xy = worldNorm.xy * 0.5 + 0.5;
					
					UNITY_TRANSFER_FOG(o, o.pos);

					return o;
				}
				
				uniform sampler2D _MainTex;
				uniform sampler2D _MatCap;
				
				fixed4 frag (v2f i) : COLOR
				{
					fixed4 tex = tex2D(_MainTex, i.uv);
					fixed4 tex2 = tex2D(_MatCap, i.uv2);
					fixed dot = step(1-(i.posi.g*0.5), tex2.a);
					fixed4 mc = tex2D(_MatCap, i.cap);
					fixed4 final = lerp(lerp(lerp(tex,tex*tex,1-step(mc.b, tex2.a)),tex+0.1,1-step(mc.g, tex2.a)),tex+0.5,1-step(mc.r, tex2.a));
					UNITY_APPLY_FOG(i.fogCoord, final);
					return final;

				}
			ENDCG
		}
	}
}
