Shader "Custom/TilingTexture_SurfaceLit" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1, 1, 1, 1)
		_MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}
		_BumpMap ("Normal Map", 2D) = "bump" {}
	}
	SubShader 
	{
		Tags { "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		sampler2D _BumpMap;
		fixed4 _Color;
		
		struct Input
		{
			float2 uv_MainTex;
			float2 uv_BumpMap;
		};

		void surf(Input input, inout SurfaceOutput output)
		{
			fixed4 c = tex2D(_MainTex, input.uv_MainTex);
			output.Albedo = c.rgb;
			output.Alpha = c.a;
			output.Normal = UnpackNormal(tex2D(_BumpMap, input.uv_BumpMap));
		}

		ENDCG 
	}
		
	FallBack "Diffuse2D"
}
