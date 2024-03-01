﻿Shader "DynamicFog/Opaque/Standard Shader Solid Color" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard finalcolor:fogColor exclude_path:deferred  exclude_path:prepass fullforwardshadows
		#pragma target 3.0

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		struct Input {
			float3 worldPos;
		};

		#define SURFACE_STRUCT SurfaceOutputStandard
		#include "DFMSurfaceShaderCommon.cginc"

		void surf (Input IN, inout SurfaceOutputStandard o) {
	 		o.Albedo = _Color.rgb;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
		}

		ENDCG
	}
	FallBack "Diffuse"
}