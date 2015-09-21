//Shader "Custom/CombinedMovingBackgroundShaderHorizontal" {
//
//	Properties
//	{
//		_MainTex1("Base (RGB) Trans (A)", 2D) = "white" {}
//		_MainTex2("Base (RGB) Trans (A)", 2D) = "white" {}
//		_MainTex3("Base (RGB) Trans (A)", 2D) = "white" {}
//		_MainTex4("Base (RGB) Trans (A)", 2D) = "white" {}
//		_MainTex5("Base (RGB) Trans (A)", 2D) = "white" {}
//		_TickCount("Tick Count", float) = 0.0
//	}
//
//		SubShader
//		{
//
//			Tags{ "RenderQueue" = "Geometry" }
//			LOD 200
//			ZWrite Off
//
//			//PASS1
//			CGPROGRAM
//#pragma target 3.0
//#pragma surface surf Standard
//			float _TickCount;
//			sampler2D _MainTex5;
//			float _Horizonatl;
//
//			struct Input {
//				float2 uv_MainTex5;
//			};
//
//			void surf(Input IN, inout SurfaceOutputStandard o) {
//				o.Albedo = tex2D(_MainTex5, IN.uv_MainTex5);
//			}
//			ENDCG
//
//				//PASS3
//				CGPROGRAM
//#pragma target 3.0
//#pragma surface surf Standard vertex:vert
//				sampler2D _MainTex2;
//			float _TickCount;
//			float _Horizontal;
//
//			struct Input {
//				float2 uv_MainTex2;
//				float4 vertex;
//			};
//
//			void vert(inout appdata_full v, out Input o)
//			{
//				o.uv_MainTex2 = v.texcoord.xy;
//				o.vertex = v.vertex;
//			}
//
//			void surf(Input IN, inout SurfaceOutputStandard o) {
//				fixed2 scrollUV1 = IN.uv_MainTex2;
//
//				scrollUV1 += fixed2(_TickCount, 0.45);
//
//				fixed4 c1 = tex2D(_MainTex2, scrollUV1);
//
//				if (c1.a > 0.8 && IN.vertex.y > -0.134)
//				{
//					o.Albedo = c1.rgb;
//				}
//				else
//				{
//					o.Alpha = 0.0;
//				}
//			}
//			ENDCG
//
//				//PASS2
//				CGPROGRAM
//#pragma target 3.0
//#pragma surface surf Standard vertex:vert
//			sampler2D _MainTex1;
//			float _TickCount;
//			float _Horizontal;
//
//			struct Input {
//				float2 uv_MainTex1;
//				float4 vertex;
//			};
//
//			void vert(inout appdata_full v, out Input o)
//			{
//				o.uv_MainTex1 = v.texcoord.xy;
//				o.vertex = v.vertex;
//			}
//
//			void surf(Input IN, inout SurfaceOutputStandard o) {
//				fixed2 scrollUV1 = IN.uv_MainTex1;
//
//				scrollUV1 += fixed2(_TickCount, 0);
//
//				fixed4 c1 = tex2D(_MainTex1, scrollUV1);
//
//				if (c1.a > 0.8 && IN.vertex.y >= 0.17)
//				{
//					o.Albedo = c1.rgb;
//				}
//				else
//				{
//					o.Alpha = 0.0;
//				}
//			}
//			ENDCG
//
//				//PASS4
//				CGPROGRAM
//#pragma target 3.0
//#pragma surface surf Standard vertex:vert
//			sampler2D _MainTex3;
//			float _TickCount;
//			float _Horizontal;
//
//			struct Input {
//				float2 uv_MainTex3;
//				float4 vertex;
//			};
//
//			void vert(inout appdata_full v, out Input o)
//			{
//				o.uv_MainTex3 = v.texcoord.xy;
//				o.vertex = v.vertex;
//			}
//
//			void surf(Input IN, inout SurfaceOutputStandard o) {
//				fixed2 scrollUV1 = IN.uv_MainTex3;
//
//				scrollUV1 += fixed2(_TickCount, 0);
//
//				fixed4 c1 = tex2D(_MainTex3, scrollUV1);
//
//				if (c1.a > 0.8 && IN.vertex.y < 0.0)
//				{
//					o.Albedo = c1.rgb;
//				}
//				else
//				{
//					o.Alpha = 0.0;
//				}
//			}
//			ENDCG
//
//				//PASS5
//				CGPROGRAM
//#pragma target 3.0
//#pragma surface surf Standard vertex:vert
//				sampler2D _MainTex4;
//			float _TickCount;
//			float _Horizontal;
//
//			struct Input {
//				float2 uv_MainTex4;
//				float4 vertex;
//			};
//
//			void vert(inout appdata_full v, out Input o)
//			{
//				o.uv_MainTex4 = v.texcoord.xy;
//				o.vertex = v.vertex;
//			}
//
//			void surf(Input IN, inout SurfaceOutputStandard o) {
//				fixed2 scrollUV1 = IN.uv_MainTex4;
//
//				scrollUV1 += fixed2(_TickCount, 0);
//
//				fixed4 c1 = tex2D(_MainTex4, scrollUV1);
//
//				if (c1.a > 0.8 && IN.vertex.y <= 0)
//				{
//					o.Albedo = c1.rgb;
//				}
//				else
//				{
//					o.Alpha = 0.0;
//				}
//			}
//			ENDCG
//	}
//
//	FallBack "Unlit/Transparent"
//}
