// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Interact/Interactuable"
{
	Properties
	{
		[Header(Interact Parameters)]_InteractSwitch("InteractSwitch", Range( 0 , 1)) = 0
		_Desactivate("Desactivate", Color) = (0,0,0,0)
		_Activate("Activate", Color) = (0,0,0,0)
		[Header(Base Color)][NoScaleOffset]_BaseTexture("BaseTexture", 2D) = "white" {}
		_ShadowFacet("ShadowFacet", Color) = (0,0,0,0)
		[Toggle(_USETEXTURE_ON)] _UseTexture("UseTexture", Float) = 0
		[HDR][Header(Fresnel Parameters)]_FresnelColor("FresnelColor", Color) = (0,0,0,0)
		_SizeFresnel("SizeFresnel", Float) = 5
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		#pragma shader_feature_local _USETEXTURE_ON
		struct Input
		{
			float2 uv_texcoord;
			float3 worldPos;
			float3 worldNormal;
		};

		uniform float4 _Desactivate;
		uniform float4 _Activate;
		uniform float _InteractSwitch;
		uniform sampler2D _BaseTexture;
		uniform float4 _ShadowFacet;
		uniform float4 _FresnelColor;
		uniform float _SizeFresnel;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 lerpResult25 = lerp( _Desactivate , _Activate , _InteractSwitch);
			float2 uv_BaseTexture29 = i.uv_texcoord;
			float4 tex2DNode29 = tex2D( _BaseTexture, uv_BaseTexture29 );
			float3 ase_worldPos = i.worldPos;
			float3 temp_output_5_0_g1 = ase_worldPos;
			float3 normalizeResult4_g1 = normalize( cross( ddy( temp_output_5_0_g1 ) , ddx( temp_output_5_0_g1 ) ) );
			float grayscale6_g1 = Luminance(( normalizeResult4_g1 + 0.0 ));
			float4 lerpResult32 = lerp( tex2DNode29 , ( tex2DNode29 * _ShadowFacet ) , grayscale6_g1);
			#ifdef _USETEXTURE_ON
				float4 staticSwitch35 = ( lerpResult25 * lerpResult32 );
			#else
				float4 staticSwitch35 = lerpResult25;
			#endif
			float3 ase_worldViewDir = normalize( UnityWorldSpaceViewDir( ase_worldPos ) );
			float3 ase_worldNormal = i.worldNormal;
			float fresnelNdotV36 = dot( ase_worldNormal, ase_worldViewDir );
			float fresnelNode36 = ( 0.0 + 2.0 * pow( 1.0 - fresnelNdotV36, _SizeFresnel ) );
			float4 lerpResult37 = lerp( staticSwitch35 , _FresnelColor , saturate( fresnelNode36 ));
			o.Emission = lerpResult37.rgb;
			o.Alpha = 1;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				float3 worldNormal : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.worldNormal = worldNormal;
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.worldPos = worldPos;
				surfIN.worldNormal = IN.worldNormal;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;495;1217;324;910.3998;-662.8515;1;True;False
Node;AmplifyShaderEditor.ColorNode;34;-648.3196,402.5099;Inherit;False;Property;_ShadowFacet;ShadowFacet;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-806.032,186.87;Inherit;True;Property;_BaseTexture;BaseTexture;3;2;[Header];[NoScaleOffset];Create;True;1;Base Color;0;0;False;0;False;-1;None;4feccb6bab8e9fc4687ae6368c6d6187;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.WorldPosInputsNode;31;-631.8472,568.4814;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;26;-819.1597,116.5905;Inherit;False;Property;_InteractSwitch;InteractSwitch;0;1;[Header];Create;True;1;Interact Parameters;0;0;False;0;False;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;24;-753.5599,-224.6096;Inherit;False;Property;_Desactivate;Desactivate;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.462264,0.462264,0.462264,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;27;-788.1225,-36.25623;Inherit;False;Property;_Activate;Activate;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-464.3196,246.5479;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0.4811321,0.2007577,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;30;-471.2575,549.5006;Inherit;False;Facet;-1;;1;94c9d6932b26f8d43a7932235f202e92;0;2;9;FLOAT;0;False;5;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;25;-476.4663,-39.8404;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;32;-241.1807,187.0655;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;39;-477.2178,868.3927;Inherit;False;Property;_SizeFresnel;SizeFresnel;7;0;Create;True;0;0;0;False;0;False;5;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;28;-251.8508,-14.62286;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FresnelNode;36;-275.6825,747.2546;Inherit;True;Standard;TangentNormal;ViewDir;True;False;5;0;FLOAT3;0,0,1;False;4;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;2;False;3;FLOAT;1.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;35;-20.46954,-76.59296;Inherit;False;Property;_UseTexture;UseTexture;5;0;Create;True;0;0;0;False;0;False;0;0;1;True;;Toggle;2;Key0;Key1;Create;True;True;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;40;51.41779,730.3678;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;38;-96.70514,546.9368;Inherit;False;Property;_FresnelColor;FresnelColor;6;2;[HDR];[Header];Create;True;1;Fresnel Parameters;0;0;False;0;False;0,0,0,0;0,0.7490196,0.7490196,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.LerpOp;37;180.6682,586.9787;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;622.9052,-49.8651;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Custom/Interact/Interactuable;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;33;0;29;0
WireConnection;33;1;34;0
WireConnection;30;5;31;0
WireConnection;25;0;24;0
WireConnection;25;1;27;0
WireConnection;25;2;26;0
WireConnection;32;0;29;0
WireConnection;32;1;33;0
WireConnection;32;2;30;0
WireConnection;28;0;25;0
WireConnection;28;1;32;0
WireConnection;36;3;39;0
WireConnection;35;1;25;0
WireConnection;35;0;28;0
WireConnection;40;0;36;0
WireConnection;37;0;35;0
WireConnection;37;1;38;0
WireConnection;37;2;40;0
WireConnection;0;2;37;0
ASEEND*/
//CHKSM=297096699BD3B21388F820D2866875C475DB6B1C