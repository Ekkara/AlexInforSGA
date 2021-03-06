﻿Shader "Unlisted/Camera Distortion"
{
	Properties{
		[HideInInspector]_MainTex("Texture", 2D) = "white" {}
		_Noise("NoiseTex", 2D) = "white" {}
		_Frequency("Frequency", Float) = 1.0
		_Intensity("Intensity", Float) = 1.0
		_Speed("Speed", Float) = 1.0
	}
		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always Fog{ Mode off }

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
					float4 pos : SV_POSITION;
					float2 uv : TEXCOORD0;
				};

				v2f vert(appdata v)
				{
					v2f o;
					o.pos = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				sampler2D _MainTex;
				sampler2D _Noise;
				float _Frequency;
				float _Intensity;
				float _Speed;
				float4 _Noise_ST;

				fixed4 frag(v2f i) : SV_Target
				{
					float2 offset = float2((sin(tex2D(_Noise, float2(0,i.uv.y + _Time[1] * _Speed)*_Frequency).r) - 0.5)*_Intensity,0.0);
					fixed test = tex2D(_Noise, i.uv + half2(_Noise_ST.z, _Noise_ST.w+_Time[1]*_Speed)).r;

					if (i.uv.y > 0.5) {
						fixed4 col = tex2D(_MainTex, (i.uv + test));
						if (test > 0.5) col = 1 - col;
						return col;
					}
					fixed4 col = tex2D(_MainTex, i.uv);

					return col;
				}
				ENDCG
			}
		}
}
