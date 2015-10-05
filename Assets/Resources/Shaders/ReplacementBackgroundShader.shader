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

		[MaterialToggle] _Replace("_Replace", Float) = 1.0
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
	float _Replace;

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
		return float3(r / 255.0, g/255.0, b/255.0);
	}
	
	float CompareFloatValues(float original, float check)
	{
		if (original - 0.055 <= check && original + 0.055 >= check)
			return 1.0;
		else
			return 0.0;
	}

	float IsColor(fixed4 color, float r, float g, float b)
	{
		float3 color2 = NormalizeRGB(r, g, b);
		if (CompareFloatValues(color.r, color2.r) > 0.5 && CompareFloatValues(color.g, color2.g) > 0.5 && CompareFloatValues(color.b, color2.b) > 0.5)
			return 1.0;
		else
			return 0.0;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;

		if (_Replace > 0.5)
		{
			if (IsColor(c, 95, 57, 22))//1
			{
				c = float4(NormalizeRGB(183, 225, 230), c.a);
			}
			else if (IsColor(c, 117, 77, 41))//2
			{
				c = float4(NormalizeRGB(55, 176, 182), c.a);
			}
			else if (IsColor(c, 47, 26, 16))//3
			{
				c = float4(NormalizeRGB(58, 128, 135), c.a);
			}
			else if (IsColor(c, 74, 39, 16))//4
			{
				c = float4(NormalizeRGB(16, 68, 73), c.a);
			}
			else if (IsColor(c, 195, 154, 107))//5
			{
				c = float4(NormalizeRGB(107, 187, 193), c.a);
			}
			else if (IsColor(c, 214, 205, 191))//6
			{
				c = float4(NormalizeRGB(190, 210, 211), c.a);
			}
			else if (IsColor(c, 169, 127, 83))//7
			{
				c = float4(NormalizeRGB(84, 160, 167), c.a);
			}
			else if (IsColor(c, 71, 44, 28))//8
			{
				c = float4(NormalizeRGB(38, 116, 124), c.a);
			}
			else if (IsColor(c, 59, 35, 20))//9
			{
				c = float4(NormalizeRGB(25, 77, 86), c.a);
			}
			else if (IsColor(c, 96, 56, 19))//10
			{
				c = float4(NormalizeRGB(183, 255, 230), c.a);
			}
			else if (IsColor(c, 117, 77, 41))//11
			{
				c = float4(NormalizeRGB(108, 183, 193), c.a);
			}
			else if (IsColor(c, 116, 76, 40))//12
			{
				c = float4(NormalizeRGB(83, 158, 169), c.a);
			}
			else if (IsColor(c, 32, 32, 32))
			{
				c = float4(NormalizeRGB(190, 210, 211), c.a);
			}
			else
			{
				c = float4(NormalizeRGB(190, 210, 211), c.a);
			}
		}

		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}
		Fallback "Transparent/VertexLit"
}
