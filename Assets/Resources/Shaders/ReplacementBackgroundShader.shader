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
	
	float CompareFloatValues(float original, float check)
	{
		if(abs(check - original) < 0.01)
			return 1.0;
		else
			return 0.0;
	}

	float IsColor(fixed4 color, float r, float g, float b)
	{
		if (CompareFloatValues(color.r, r) > 0.5 && CompareFloatValues(color.g, g) > 0.5 && CompareFloatValues(color.b, b) > 0.5)
			return 1.0;
		else
			return 0.0;
	}

	void surf(Input IN, inout SurfaceOutput o)
	{
		float4 c = tex2D(_MainTex, IN.uv_MainTex) * IN.color;

		if (_Replace > 0.5)
		{
			if (IsColor(c, 95.0 / 255.0, 57.0 / 255.0, 22.0 / 255.0))//1
			{
				c = float4(183.0 / 255.0, 225.0 / 255.0, 230.0 / 255.0, c.a);
			}
			else if (IsColor(c, 117.0 / 255.0, 77.0 / 255.0, 41.0 / 255.0))//2
			{
				c = float4(55.0 / 255.0, 176.0 / 255.0, 182.0 / 255.0, c.a);
			}
			else if (IsColor(c, 47.0 / 255.0, 26.0 / 255.0, 16.0 / 255.0))//3
			{
				c = float4(58.0 / 255.0, 128.0 / 255.0, 135.0 / 255.0, c.a);
			}
			else if (IsColor(c, 74.0 / 255.0, 39.0 / 255.0, 16.0 / 255.0))//4
			{
				c = float4(16.0 / 255.0, 68.0 / 255.0, 73.0 / 255.0, c.a);
			}
			else if (IsColor(c, 195.0 / 255.0, 154.0 / 255.0, 107.0 / 255.0))//5
			{
				c = float4(107.0 / 255.0, 187.0 / 255.0, 193.0 / 255.0, c.a);
			}
			else if (IsColor(c, 214.0 / 255.0, 205.0 / 255.0, 191.0 / 255.0))//6
			{
				c = float4(190.0 / 255.0, 210.0 / 255.0, 211.0 / 255.0, c.a);
			}
			else if (IsColor(c, 169.0 / 255.0, 127.0 / 255.0, 83.0 / 255.0))//7
			{
				c = float4(84.0 / 255.0, 160.0 / 255.0, 167.0 / 255.0, c.a);
			}
			else if (IsColor(c, 71.0 / 255.0, 44.0 / 255.0, 28.0 / 255.0))//8
			{
				c = float4(38.0 / 255.0, 116.0 / 255.0, 124.0 / 255.0, c.a);
			}
			else if (IsColor(c, 59.0 / 255.0, 35.0 / 255.0, 20.0 / 255.0))//9
			{
				c = float4(25.0 / 255.0, 77.0 / 255.0, 86.0 / 255.0, c.a);
			}
			else if (IsColor(c, 96.0 / 255.0, 56.0 / 255.0, 19.0 / 255.0))//10
			{
				c = float4(183.0 / 255.0, 255.0 / 255.0, 230.0 / 255.0, c.a);
			}
			else if (IsColor(c, 117.0 / 255.0, 77.0 / 255.0, 41.0 / 255.0))//11
			{
				c = float4(108.0 / 255.0, 183.0 / 255.0, 193.0 / 255.0, c.a);
			}
			else if (IsColor(c, 116.0 / 255.0, 76.0 / 255.0, 40.0 / 255.0))//12
			{
				c = float4(83.0 / 255.0, 158.0 / 255.0, 169.0 / 255.0, c.a);
			}
			else if (IsColor(c, 32.0 / 255.0, 32.0 / 255.0, 32.0 / 255.0))
			{
				c = float4(190.0 / 255.0, 210.0 / 255.0, 211.0 / 255.0, c.a);
			}
			else
			{
				c = float4(190.0 / 255.0, 210.0 / 255.0, 211.0 / 255.0, c.a);
			}
		}

		o.Albedo = c.rgb * c.a;
		o.Alpha = c.a;
	}
	ENDCG
	}
		Fallback "Transparent/VertexLit"
}
