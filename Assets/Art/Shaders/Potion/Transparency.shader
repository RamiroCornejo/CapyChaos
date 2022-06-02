// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Main/Transparency"
{
	Properties
	{
		_Opacity("Opacity", Range( 0 , 1)) = 0
		[NoScaleOffset]_TextureSample0("Texture Sample 0", 2D) = "white" {}
		[HDR]_BloomColor("BloomColor", Color) = (1,1,1,0)
		[NoScaleOffset]_Transparency("Transparency", 2D) = "white" {}
		_UseTexture("UseTexture", Range( 0 , 1)) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" }
		Cull Back
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Standard alpha:fade keepalpha noshadow 
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform sampler2D _TextureSample0;
		uniform half4 _BloomColor;
		uniform float _Opacity;
		uniform sampler2D _Transparency;
		uniform float _UseTexture;

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 uv_TextureSample09 = i.uv_texcoord;
			o.Albedo = ( tex2D( _TextureSample0, uv_TextureSample09 ) * _BloomColor ).rgb;
			float2 uv_Transparency15 = i.uv_texcoord;
			float lerpResult14 = lerp( i.vertexColor.g , tex2D( _Transparency, uv_Transparency15 ).r , _UseTexture);
			float lerpResult7 = lerp( _Opacity , 1.0 , lerpResult14);
			o.Alpha = lerpResult7;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;427;1150;392;532.7622;-1.475689;1.3;True;False
Node;AmplifyShaderEditor.VertexColorNode;1;-532.9718,191.4703;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;15;-596.82,435.1991;Inherit;True;Property;_Transparency;Transparency;3;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;c59e8f27bd6774f459553dc5ead849e6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;16;-468.9814,668.0672;Inherit;False;Property;_UseTexture;UseTexture;4;0;Create;True;0;0;0;False;0;False;0;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;11;-373.3615,-73.99949;Half;False;Property;_BloomColor;BloomColor;2;1;[HDR];Create;True;0;0;0;False;0;False;1,1,1,0;1,1,1,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;9;-304.5999,-314.6026;Inherit;True;Property;_TextureSample0;Texture Sample 0;1;1;[NoScaleOffset];Create;True;0;0;0;False;0;False;-1;None;d25d743a768626b40927afb26d1cc81a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-387.6513,147.0789;Inherit;False;Property;_Opacity;Opacity;0;0;Create;True;0;0;0;False;0;False;0;0.57;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;14;-186.6146,399.9229;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;100.815,-119.3476;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;7;21.47591,125.9269;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;24;509.8995,-5.8523;Float;False;True;-1;2;ASEMaterialInspector;0;0;Standard;Custom/Main/Transparency;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;False;0;False;Transparent;;Transparent;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;14;0;1;2
WireConnection;14;1;15;1
WireConnection;14;2;16;0
WireConnection;10;0;9;0
WireConnection;10;1;11;0
WireConnection;7;0;4;0
WireConnection;7;2;14;0
WireConnection;24;0;10;0
WireConnection;24;9;7;0
WireConnection;24;10;7;0
ASEEND*/
//CHKSM=454638FC3F49187016A0FB1153689085DAA60F9F