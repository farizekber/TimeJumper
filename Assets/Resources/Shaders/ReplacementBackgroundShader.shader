Shader "Custom/ReplacementBackgroundShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "white" {}
		[MaterialToggle] _Replace("_Replace", Float) = 1.0
	}

		SubShader{
		Pass{

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
#pragma vertex vert
#pragma fragment frag nofog keepalpha
#include "UnityCG.cginc"

	float _Replace;
	uniform sampler2D _MainTex;

	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
		
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

	v2f vert(appdata_img v)
	{
		v2f o;
		o.pos = mul(UNITY_MATRIX_MVP, v.vertex);
		o.uv = v.texcoord;
		return o;
	}

	float4 frag(v2f i) : SV_Target
	{
		float2 offset = i.uv;
		float4 c = tex2D(_MainTex, offset);

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

		return c * c.a;
	}
		ENDCG

	}
	}

	Fallback "Transparent/VertexLit"

}
