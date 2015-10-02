Shader "Custom/ReplacementBackgroundShader"
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

	fixed3 NormalizeRGB(float r, float g, float b)
	{
		return fixed3(r / 255.0, g/255.0, b/255.0);
	}
	
	float CompareFloatValues(float original, float check)
	{
		if (original - 0.001 < check && original + 0.001 > check)
			return 1.0;
		else
			return 0.0;
	}

	float IsColor(fixed4 color, float r, float g, float b)
	{
		fixed3 color2 = NormalizeRGB(r, g, b);
		if (CompareFloatValues(color.r, color2.r) > 0.5 && CompareFloatValues(color.g, color2.g) > 0.5 && CompareFloatValues(color.b, color2.b) > 0.5)
			return 1.0;
		else
			return 0.0;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;

		if (IsColor(c, 254, 254, 254))
		{
			c = fixed4(NormalizeRGB(255, 255, 255), c.a);
		}

		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}

		Fallback "Transparent/VertexLit"
}
