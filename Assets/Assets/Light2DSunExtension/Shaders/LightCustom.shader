/*

Light shader with custom path tracking steps (defined in _SAMPLES).
Code contained in LightBase.cginc, only path tracking samples count is defined here.

*/

Shader "Light2D/Light Custom" {
Properties {
	_MainTex ("Light texture", 2D) = "white" {}
	_ObstacleMul ("Obstacle Mul", Float) = 2000
	_EmissionColorMul ("Emission color mul", Float) = 1
	_SAMPLES("Path Tracking Samples", Int) = 360
}
SubShader {	
	Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}

	LOD 100
	Blend OneMinusDstColor One
	Cull Off
	ZWrite Off
	Lighting Off

	Pass {  
		CGPROGRAM
			uniform half _SAMPLES;
			#define PATH_TRACKING_SAMPLES _SAMPLES // count of path tracking steps
			#pragma target 3.0
			#pragma multi_compile ORTHOGRAPHIC_CAMERA PERSPECTIVE_CAMERA
			
			#include "UnityCG.cginc"
			#include "Assets/Light2DSunExtension/Shaders/LightBaseCustom.cginc" // all code is here
			
			#pragma vertex light2d_fixed_vert
			#pragma fragment light2_fixed_frag
		ENDCG
	}
}

Fallback "Light2D/Light 40 Points"

}