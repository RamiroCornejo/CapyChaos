// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Enviroment/Water"
{
	Properties
	{
		_Shadow("Shadow", Color) = (0,0,0,0)
		_Light("Light", Color) = (0,0,0,0)
		_ShadowOffset("ShadowOffset", Float) = 0
		_Dir("Dir", Vector) = (0,0,0,0)
		_Intensity("Intensity", Float) = 0
		_Texture0("Texture 0", 2D) = "white" {}

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
			#define ASE_NEEDS_FRAG_WORLD_POSITION


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
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			uniform sampler2D _Texture0;
			uniform float3 _Dir;
			uniform float _Intensity;
			uniform float4 _Shadow;
			uniform float4 _Light;
			uniform float _ShadowOffset;

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				float2 texCoord3_g2 = v.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 panner4_g2 = ( 1.0 * _Time.y * float2( 0,-0.11 ) + texCoord3_g2);
				float3 normalizeResult8_g2 = normalize( _Dir );
				
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = ( tex2Dlod( _Texture0, float4( panner4_g2, 0, 0.0) ).r * normalizeResult8_g2 * v.ase_normal * _Intensity );
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
				float3 temp_output_5_0_g1 = WorldPosition;
				float3 normalizeResult4_g1 = normalize( cross( ddy( temp_output_5_0_g1 ) , ddx( temp_output_5_0_g1 ) ) );
				float grayscale6_g1 = Luminance(( normalizeResult4_g1 + 0.0 ));
				float4 lerpResult3 = lerp( _Shadow , _Light , saturate( ( grayscale6_g1 + _ShadowOffset ) ));
				
				
				finalColor = lerpResult3;
				return finalColor;
			}
			ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=18900
0;498;1208;321;1713.347;-304.6231;1.683071;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;2;-1105,212;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;15;-845.3423,277.6884;Inherit;False;Property;_ShadowOffset;ShadowOffset;2;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-947.1,178;Inherit;False;Facet;-1;;1;94c9d6932b26f8d43a7932235f202e92;0;2;9;FLOAT;0;False;5;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;14;-642.3423,185.6883;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TexturePropertyNode;20;-632.3052,295.8954;Inherit;True;Property;_Texture0;Texture 0;5;0;Create;True;0;0;0;False;0;False;None;46073085386271d4ca9d074396dd6391;False;white;Auto;Texture2D;-1;0;2;SAMPLER2D;0;SAMPLERSTATE;1
Node;AmplifyShaderEditor.ColorNode;5;-874.5313,-0.604162;Inherit;False;Property;_Light;Light;1;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0.8264365,1,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;4;-874.531,-167.0042;Inherit;False;Property;_Shadow;Shadow;0;0;Create;True;0;0;0;False;0;False;0,0,0,0;0,0.5314466,0.6132076,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;13;-506.4873,164.3228;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;8;-553.7277,479.0694;Inherit;False;Property;_Dir;Dir;3;0;Create;True;0;0;0;False;0;False;0,0,0;0,0,1;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;9;-535.2952,658.4272;Inherit;False;Property;_Intensity;Intensity;4;0;Create;True;0;0;0;False;0;False;0;0.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;3;-400.0311,59.1958;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;19;-358.1683,264.0071;Inherit;False;Wave;-1;;2;a2fb1b197c8cdf2488ede27e0a31bb6e;0;3;1;SAMPLER2D;;False;10;FLOAT3;0,0,0;False;11;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;18;0,0;Float;False;True;-1;2;ASEMaterialInspector;100;1;Custom/Enviroment/Water;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;False;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;False;False;False;False;False;False;False;False;False;True;0;False;-1;False;True;0;False;-1;False;True;True;True;True;True;0;False;-1;False;False;False;False;False;False;False;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;1;RenderType=Opaque=RenderType;True;2;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;False;0
WireConnection;1;5;2;0
WireConnection;14;0;1;0
WireConnection;14;1;15;0
WireConnection;13;0;14;0
WireConnection;3;0;4;0
WireConnection;3;1;5;0
WireConnection;3;2;13;0
WireConnection;19;1;20;0
WireConnection;19;10;8;0
WireConnection;19;11;9;0
WireConnection;18;0;3;0
WireConnection;18;1;19;0
ASEEND*/
//CHKSM=A0F2FC6CD370F97A103A32FB6BC2ADFA77FC78A1