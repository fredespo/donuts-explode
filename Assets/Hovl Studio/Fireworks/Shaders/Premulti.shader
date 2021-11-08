Shader "ERB/Particles/Premulti"{
	Properties{
		[HDR]_TintColor("Tint Color", Color) = (1,1,1,1)
		_MainTex("Main Texture", 2D) = "white" {}
		_Opacity ("Opacity", Range(0, 1)) = 0.5
        _Emission ("Emission", Float ) = 2
	}
	Category{
		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
		Blend One OneMinusSrcAlpha
		Cull off
		ZWrite off
		SubShader{
			Pass{
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma multi_compile_particles
				//#pragma multi_compile_fog
				#pragma multi_compile_instancing
				#include "UnityCG.cginc"
				sampler2D _MainTex;
				float4 _MainTex_ST;
				float _Opacity;
				float _Emission;
				half4 _TintColor;

				struct appdata_t {
					float4 vertex : POSITION;
					float4 normal : NORMAL;
					half4 vertexColor : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
				};

				struct v2f {
					float4 vertex : SV_POSITION;
					half4 vertexColor : COLOR;
					float2 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(4)
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
				};

				v2f vert(appdata_t v) {
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.vertexColor = v.vertexColor;
					o.texcoord.xy = TRANSFORM_TEX(v.texcoord.xy, _MainTex);
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				half4 frag(v2f i) : SV_Target {
					UNITY_SETUP_INSTANCE_ID(i);
					half4 tex = tex2D(_MainTex, i.texcoord);
					half4 texCol = tex * _TintColor;
					float3 emissive = (texCol.rgb*i.vertexColor.rgb*_Emission*i.vertexColor.a);
					fixed4 finalRGBA = fixed4(emissive,(texCol.a*i.vertexColor.a*_Opacity));
					UNITY_APPLY_FOG_COLOR(i.fogCoord, texCol, finalRGBA);
					return finalRGBA;
				}
				ENDCG
			}
		}
	}
}
