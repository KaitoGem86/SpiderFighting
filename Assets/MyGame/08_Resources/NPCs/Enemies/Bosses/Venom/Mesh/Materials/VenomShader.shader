Shader "Kim/Venom"
{
    Properties
    {
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _VipMap ("Venom Map", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Standard

        sampler2D _MainTex;
		sampler2D _VipMap;

        struct Input
        {
            float2 uv_MainTex;
			float2 uv2_MainTex;
			float3 viewDir;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            fixed4 dcm = tex2D (_MainTex, IN.uv_MainTex);
			float2 newuv = float2(IN.uv2_MainTex.x,IN.uv2_MainTex.y + (0.1*_Time.y));
			fixed4 dkm = tex2D (_MainTex, newuv);
			fixed4 vcl = tex2D (_VipMap, IN.uv_MainTex);
			half4 vkl = tex2D (_VipMap, newuv);
			float3 normal01 = UnpackNormal(fixed4(0,vcl.r,1,vcl.g));
			float3 normal02 = UnpackNormal(fixed4(0,dkm.a,0,vkl.a));
			o.Albedo = tex2D (_MainTex, IN.uv_MainTex).rgb;
			o.Normal =  lerp(float3(normal01.xy + normal02.xy, normal01.z*normal02.z),normal01,1-vcl.b);
			o.Emission = (pow((1.0 - saturate(dot (normalize(IN.viewDir), o.Normal))),3)*half4(1,0,0,1));
			o.Smoothness = lerp(0.6,0,1-vcl.b);
        }
        ENDCG
    }
}
