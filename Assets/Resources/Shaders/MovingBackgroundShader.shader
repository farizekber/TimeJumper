Shader "Custom/MovingBackgroundShader" {
	Properties{
		_Color("Main Color", Color) = (1,1,1,1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
		_Cutoff("Alpha cutoff", Range(0,1)) = 0.5
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_TickCount("Tick Count", float) = 0.0
		_Horizontal("Horizontal", float) = 10
	}

		SubShader{
		Tags{ "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
		LOD 200

		CGPROGRAM
#pragma surface surf Standard alphatest:_Cutoff fullforwardshadows

		sampler2D _MainTex;
		fixed4 _Color;
		float _TickCount;
		float _Horizontal;

	struct Input {
		float2 uv_MainTex;
	};

	half _Glossiness;
	half _Metallic;

	void surf(Input IN, inout SurfaceOutputStandard o) {
		fixed2 scrollUV = IN.uv_MainTex;

		if (_Horizontal > 0)
			scrollUV += fixed2(_TickCount, 0);
		else
			scrollUV += fixed2(0, -1 * _TickCount);

		fixed4 c = tex2D(_MainTex, scrollUV) * _Color;
		o.Albedo = c.rgb;
		o.Alpha = c.a;
		o.Metallic = _Metallic;
		o.Smoothness = _Glossiness;
	}
	ENDCG
	}

		Fallback "Legacy Shaders/Transparent/Cutout/VertexLit"
}
