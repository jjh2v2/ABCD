/*

Light shader that ignores obstacles.

*/

Shader "Light2D/Light Custom Ignore Obstacles" {
Properties {
	_MainTex ("Light texture", 2D) = "white" {}
	_EmissionColorMul ("Emission color mul", Float) = 1
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
			#pragma target 3.0
			#pragma multi_compile ORTHOGRAPHIC_CAMERA PERSPECTIVE_CAMERA
			
			#include "UnityCG.cginc"
			#include "Assets/Light2DSunExtension/Shaders/LightBaseCustom.cginc" // all code is here
			
			#pragma vertex light2d_fixed_vert
			#pragma fragment light2_fixed_frag
		ENDCG
	}
}

}