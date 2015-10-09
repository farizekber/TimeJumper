Shader "Custom/BackgroundShader"
{
	Properties
	{
		[PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap("Pixel snap", Float) = 0

		_RedMultiplier("_RedMultiplier", Float) = 1.0
		_GreenMultiplier("_GreenMultiplier", Float) = 1.0
		_BlueMultiplier("_BlueMultiplier", Float) = 1.0

		[MaterialToggle] _RedInversed("_RedInversed", Float) = 1.0
		[MaterialToggle] _GreenInversed("_GreenInversed", Float) = 1.0
		[MaterialToggle] _BlueInversed("_BlueInversed", Float) = 1.0
	}

		SubShader
	{
		Tags
	{
		"Queue" = "Transparent"
		"IgnoreProjector" = "True"
		"RenderType" = "Transparent"
		"PreviewType" = "Plane"
		"CanUseSpriteAtlas" = "True"
	}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		CGPROGRAM
#pragma surface surf Lambert vertex:vert nofog keepalpha
#pragma multi_compile _ PIXELSNAP_ON

	sampler2D _MainTex;
	fixed4 _Color;
	float _RedMultiplier;
	float _GreenMultiplier;
	float _BlueMultiplier;
	float _RedInversed;
	float _GreenInversed;
	float _BlueInversed;

	struct Input
	{
		float2 uv_MainTex;
		fixed4 color;
	};

	void vert(inout appdata_full v, out Input o)
	{
#if defined(PIXELSNAP_ON)
		v.vertex = UnityPixelSnap(v.vertex);
#endif

		UNITY_INITIALIZE_OUTPUT(Input, o);
		o.color = v.color * _Color;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;

		c = fixed4((c.r * _RedMultiplier), (c.g * _GreenMultiplier), (c.b * _BlueMultiplier), c.a);

		if (_RedInversed > 0.5)
		{
			c.r = 1.0 - c.r;
		}

		if (_GreenInversed > 0.5)
		{
			c.g = 1.0 - c.g;
		}

		if (_BlueInversed > 0.5)
		{
			c.b = 1.0 - c.b;
		}

		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}
