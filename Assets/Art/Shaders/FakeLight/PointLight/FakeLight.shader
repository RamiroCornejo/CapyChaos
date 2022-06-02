// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Light/PointLight"
{
	Properties
	{
		[HDR]_ColorLight("ColorLight", Color) = (0,0,0,0)
		_LightIntensity("LightIntensity", Range( 0 , 1)) = 0
		_LightGradiant("LightGradiant", Float) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Transparent" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend One One
		AlphaToMask Off
		Cull Front
		ColorMask RGBA
		ZWrite Off
		ZTest GEqual
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _CameraDepthNormalsTexture1;
			UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
			uniform float4 _CameraDepthTexture_TexelSize;
			uniform float _LightGradiant;
			uniform float _LightIntensity;
			uniform float4 _ColorLight;
			float2 UnStereo( float2 UV )
			{
				#if UNITY_SINGLE_PASS_STEREO
				float4 scaleOffset = unity_StereoScaleOffset[ unity_StereoEyeIndex ];
				UV.xy = (UV.xy - scaleOffset.zw) / scaleOffset.xy;
				#endif
				return UV;
			}
			
			float3 InvertDepthDir72_g25( float3 In )
			{
				float3 result = In;
				#if !defined(ASE_SRP_VERSION) || ASE_SRP_VERSION <= 70301
				result *= float3(1,1,-1);
				#endif
				return result;
			}
			

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord1 = screenPos;
				
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);

				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				#endif
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 WorldPosition = i.worldPos;
				#endif
				float4 screenPos = i.ase_texcoord1;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float depthDecodedVal11_g24 = 0;
				float3 normalDecodedVal11_g24 = float3(0,0,0);
				DecodeDepthNormal( tex2D( _CameraDepthNormalsTexture1, ase_screenPosNorm.xy ), depthDecodedVal11_g24, normalDecodedVal11_g24 );
				float3 viewToWorldDir14_g24 = mul( UNITY_MATRIX_I_V, float4( normalDecodedVal11_g24, 0 ) ).xyz;
				float3 objToWorld7_g24 = mul( unity_ObjectToWorld, float4( float3(0,0,0), 1 ) ).xyz;
				float2 UV22_g26 = ase_screenPosNorm.xy;
				float2 localUnStereo22_g26 = UnStereo( UV22_g26 );
				float2 break64_g25 = localUnStereo22_g26;
				float clampDepth69_g25 = SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy );
				#ifdef UNITY_REVERSED_Z
				float staticSwitch38_g25 = ( 1.0 - clampDepth69_g25 );
				#else
				float staticSwitch38_g25 = clampDepth69_g25;
				#endif
				float3 appendResult39_g25 = (float3(break64_g25.x , break64_g25.y , staticSwitch38_g25));
				float4 appendResult42_g25 = (float4((appendResult39_g25*2.0 + -1.0) , 1.0));
				float4 temp_output_43_0_g25 = mul( unity_CameraInvProjection, appendResult42_g25 );
				float3 temp_output_46_0_g25 = ( (temp_output_43_0_g25).xyz / (temp_output_43_0_g25).w );
				float3 In72_g25 = temp_output_46_0_g25;
				float3 localInvertDepthDir72_g25 = InvertDepthDir72_g25( In72_g25 );
				float4 appendResult49_g25 = (float4(localInvertDepthDir72_g25 , 1.0));
				float4 temp_output_2_0_g24 = mul( unity_CameraToWorld, appendResult49_g25 );
				float3 normalizeResult12_g24 = normalize( ( objToWorld7_g24 - (temp_output_2_0_g24).xyz ) );
				float dotResult16_g24 = dot( viewToWorldDir14_g24 , normalizeResult12_g24 );
				float3 worldToObj5_g24 = mul( unity_WorldToObject, float4( temp_output_2_0_g24.xyz, 1 ) ).xyz;
				float3 temp_output_24_0_g24 = (worldToObj5_g24*_LightGradiant + 0.0);
				float dotResult13_g24 = dot( temp_output_24_0_g24 , temp_output_24_0_g24 );
				float temp_output_19_0_g24 = saturate( ( 1.0 - dotResult13_g24 ) );
				
				
				finalColor = ( ( saturate( step( dotResult16_g24 , 1.0 ) ) * ( temp_output_19_0_g24 * temp_output_19_0_g24 ) ) * _LightIntensity * _ColorLight );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;455;1145;364;322.4409;-189.9962;1.713397;True;False
Node;AmplifyShaderEditor.RangedFloatNode;35;447.6284,313.2491;Inherit;False;Property;_LightGradiant;LightGradiant;4;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;28;535.6743,404.4467;Inherit;False;Property;_LightIntensity;LightIntensity;3;0;Create;True;0;0;0;False;0;False;0;0.486;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;42;620.9688,315.6317;Inherit;False;LightMain;1;;24;01c75922bd03c7e4e9625ffac91b2717;0;1;27;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1;582.791,498.2628;Inherit;False;Property;_ColorLight;ColorLight;0;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;1,0,0.03137255,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;866.7753,339.9471;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1015.133,338.1749;Float;False;True;-1;2;ASEMaterialInspector;100;1;Custom/Light/PointLight;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;True;4;1;False;-1;1;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;True;True;1;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;True;2;False;-1;True;4;False;-1;True;False;0;False;-1;0;False;-1;True;1;RenderType=Transparent=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;42;27;35;0
WireConnection;21;0;42;0
WireConnection;21;1;28;0
WireConnection;21;2;1;0
WireConnection;0;0;21;0
ASEEND*/
//CHKSM=E4EA02B57A41DA56E57D324BD4214812384C197F