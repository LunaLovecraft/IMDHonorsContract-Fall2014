Shader "Custom/TilingTexture_NoLight" 
{
	Properties 
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader 
	{
		Pass{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"
			
			// Vertex Output Structure
			struct VertexToFragment
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			// Texture sampler and tiling information
			sampler2D _MainTex;
			float4 _MainTex_ST;	

			// Vertex Shader
			// Passes basic information through the pipeline
			VertexToFragment vert(appdata_base v)
			{
				VertexToFragment o;
				o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord.xy, _MainTex);

				return o;
			}

			// Fragment Shader
			// Returns the sampled color value
			half4 frag(VertexToFragment input) : COLOR
			{
				half4 litColor = tex2D(_MainTex, input.uv);
				
				return litColor;
			}

			ENDCG
		}
	} 
	Fallback Off
}
