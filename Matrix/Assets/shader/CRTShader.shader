// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/CRTShader"
{	
	    
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
	}

		SubShader{

		Pass{
		ZTest Always Cull Off ZWrite Off Fog{ Mode off }
		
		CGPROGRAM
	
#pragma vertex vert
#pragma fragment frag
#pragma fragmentoption ARB_precision_hint_fastest
#include "UnityCG.cginc"
#pragma target 3.0
		
		struct v2f
	{
		

		float4 pos      : POSITION;
		float2 uv       : TEXCOORD0;
		float4 scr_pos	: TEXCOORD1;
	};

	uniform sampler2D _MainTex;
	 
	v2f vert(appdata_img v)
	{
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, v.texcoord);
		o.scr_pos = ComputeScreenPos(o.pos);
		return o;
	}

	half4 frag(v2f i) : COLOR
	{
		
		half4 color = tex2D(_MainTex, i.uv);
		float2 ps = i.scr_pos.xy *_ScreenParams.xy / i.scr_pos.w;
		int pp = (int)ps.y % 3; // остаток от деления на 3
		float4 outcolor = float4(color.r/1,color.g/1, color.b/1, color.a/1);
		if (pp == 1) outcolor = float4(color.r / 1.8, color.g / 1.8, color.b / 1.8, color.a / 1.8); else if (pp == 2) outcolor= float4(color.r / 2.3, color.g / 2.3, color.b / 2.3, color.a / 2.3); else outcolor= float4(color.r /1, color.g /1, color.b /1, color.a /1);;
		return outcolor;
	}

		ENDCG
	}
	}
		FallBack "Diffuse"
}