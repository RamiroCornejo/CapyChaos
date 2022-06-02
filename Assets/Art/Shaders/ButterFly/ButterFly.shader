// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Enviroment/ButterFly"
{
	Properties
	{
		_Intensity("Intensity", Float) = 3
		_Vector0("Vector 0", Vector) = (0,0,1,0)
		_Color0("Color 0", Color) = (0,0,0,0)
		_Float0("Float 0", Float) = 0
		_Color1("Color 1", Color) = (0,0,0,0)
		_Noise("Noise", 2D) = "white" {}

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Opaque" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		AlphaToMask Off
		Cull Back
		ColorMask RGBA
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		
		
		
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
			#define ASE_NEEDS_VERT_COLOR
			#define ASE_NEEDS_FRAG_COLOR


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				float4 ase_texcoord : TEXCOORD0;
				float3 ase_normal : NORMAL;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				#ifdef ASE_NEEDS_FRAG_WORLD_POSITION
				float3 worldPos : TEXCOORD0;
				#endif
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _Noise;
			uniform float3 _Vector0;
			uniform float _Intensity;
			uniform float _Float0;
			uniform float4 _Color0;
			uniform float4 _Color1;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float2 texCoord3_g2 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner4_g2 = ( 1.0 * _Time.y * float2( 0,-0.11 ) + texCoord3_g2);
				float3 normalizeResult8_g2 = normalize( _Vector0 );
				
				o.ase_texcoord1 = v.vertex;
				o.ase_color = v.color;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( ( tex2Dlod( _Noise, float4( panner4_g2, 0, 0.0) ).r * normalizeResult8_g2 * v.ase_normal * _Intensity ) * ( 1.0 - v.color.g ) );
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
				float3 temp_output_5_0_g1 = i.ase_texcoord1.xyz;
				float3 normalizeResult4_g1 = normalize( cross( ddy( temp_output_5_0_g1 ) , ddx( temp_output_5_0_g1 ) ) );
				float grayscale6_g1 = Luminance(( normalizeResult4_g1 + _Float0 ));
				
				
				finalColor = ( ( grayscale6_g1 * _Color0 * i.ase_color.r ) + ( ( 1.0 - i.ase_color.r ) * _Color1 ) );
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;498;1208;321;1538.474;541.2237;1.711189;True;False
Node;AmplifyShaderEditor.PosVertexDataNode;20;-936.4196,-579.1821;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;23;-876.8292,-683.043;Inherit;False;Property;_Float0;Float 0;3;0;Create;True;0;0;0;False;0;False;0;0.39;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;1;-965.3608,-17.52253;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;16;-781.7039,157.8614;Inherit;False;Property;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;0,0,1;0,0,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;15;-702,303;Inherit;False;Property;_Intensity;Intensity;0;0;Create;True;0;0;0;False;0;False;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;19;-721.4577,-637.8224;Inherit;False;Facet;-1;;1;94c9d6932b26f8d43a7932235f202e92;0;2;9;FLOAT;0;False;5;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;29;-857.8456,348.5042;Inherit;True;Property;_Noise;Noise;5;0;Create;True;0;0;0;False;0;False;None;46073085386271d4ca9d074396dd6391;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.ColorNode;27;-677.3116,-152.7555;Inherit;False;Property;_Color1;Color 1;4;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.4339622,0.2345013,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;24;-548.6669,-302.7489;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;22;-787.7808,-497.7245;Inherit;False;Property;_Color0;Color 0;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.06568063,1,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;21;-444.964,-517.4496;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;5;-765.7332,82.602;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;28;-431.892,267.9335;Inherit;False;Wave;-1;;2;a2fb1b197c8cdf2488ede27e0a31bb6e;0;3;1;SAMPLER2D;;False;10;FLOAT3;0,0,0;False;11;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-347.1708,-227.4281;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-179.2487,-270.4089;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;30;-11.6245,224.52;Inherit;False;2;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;100;1;Custom/Enviroment/ButterFly;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;19;9;23;0
WireConnection;19;5;20;0
WireConnection;24;0;1;1
WireConnection;21;0;19;0
WireConnection;21;1;22;0
WireConnection;21;2;1;1
WireConnection;5;0;1;2
WireConnection;28;1;29;0
WireConnection;28;10;16;0
WireConnection;28;11;15;0
WireConnection;26;0;24;0
WireConnection;26;1;27;0
WireConnection;25;0;21;0
WireConnection;25;1;26;0
WireConnection;30;0;28;0
WireConnection;30;1;5;0
WireConnection;0;0;25;0
WireConnection;0;1;30;0
ASEEND*/
//CHKSM=9FD2F076F0532BDBB74AFF5497527C736F701370