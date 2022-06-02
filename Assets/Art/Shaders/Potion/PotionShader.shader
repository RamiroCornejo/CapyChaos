// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/VFX/PotionShader"
{
	Properties
	{
		_Cutoff( "Mask Clip Value", Float ) = 0.5
		[HDR]_OutsideColor("Outside Color", Color) = (0,0,0,0)
		_LiquidColor("Liquid Color", Color) = (0,0,0,0)
		_IntensityWave("Intensity Wave", Float) = 0.5
		_Height("Height", Float) = 1
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "TransparentCutout"  "Queue" = "AlphaTest+0" "IsEmissive" = "true"  }
		Cull Off
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha addshadow fullforwardshadows 
		struct Input
		{
			half ASEVFace : VFACE;
			float3 worldPos;
		};

		uniform float4 _OutsideColor;
		uniform float4 _LiquidColor;
		uniform float _IntensityWave;
		uniform float _Height;
		uniform float _Cutoff = 0.5;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 switchResult46 = (((i.ASEVFace>0)?(_OutsideColor):(_LiquidColor)));
			float4 transform29 = mul(unity_ObjectToWorld,float4( 0,0,0,1 ));
			float3 ase_worldPos = i.worldPos;
			float4 temp_output_33_0 = ( transform29 - float4( ase_worldPos , 0.0 ) );
			float4 temp_cast_2 = (( 1.0 - _Height )).xxxx;
			float4 temp_output_41_0 = ( ( ( ( temp_output_33_0 * _IntensityWave ) + (temp_output_33_0).y ) - temp_cast_2 ) / 0.1 );
			float grayscale42 = Luminance(temp_output_41_0.xyz);
			float4 lerpResult47 = lerp( switchResult46 , _LiquidColor , saturate( step( ( grayscale42 * grayscale42 ) , 0.93 ) ));
			o.Emission = lerpResult47.rgb;
			o.Alpha = 1;
			float4 temp_cast_6 = (( 1.0 - _Height )).xxxx;
			float4 clampResult25 = clamp( temp_output_41_0 , float4( 0,0,0,0 ) , float4( 1,1,1,1 ) );
			clip( ( clampResult25 * 1.0 ).x - _Cutoff );
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18900
0;413;1137;406;2341.456;429.0464;1.646589;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;28;-3029.274,367.8933;Inherit;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;29;-3014.146,201.4803;Inherit;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleSubtractOpNode;33;-2791.342,300.7534;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;31;-2530.858,325.2864;Inherit;False;Property;_IntensityWave;Intensity Wave;3;0;Create;True;0;0;0;False;0;False;0.5;0.2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-2354.042,187.9314;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.ComponentMaskNode;35;-2559.642,428.0533;Inherit;False;False;True;False;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-2265.612,366.6364;Inherit;False;Property;_Height;Height;5;0;Create;True;0;0;0;False;0;False;1;1.15;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;-2187.653,218.9954;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.OneMinusNode;38;-2097.3,370.2633;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;40;-1834.7,305.2943;Inherit;False;Constant;_FallOff;Fall Off;6;0;Create;True;0;0;0;False;0;False;0.1;0.08;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;39;-1987.158,173.3004;Inherit;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;41;-1648.726,197.5283;Inherit;True;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TFHCGrayscale;42;-1396.57,264.2864;Inherit;False;0;1;0;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;-1206.668,351.5693;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;48;-946.4894,241.6884;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0.93;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;43;-1545.23,-57.19167;Inherit;False;Property;_LiquidColor;Liquid Color;2;0;Create;True;0;0;0;False;0;False;0,0,0,0;0.6981132,0,0.5858676,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;44;-1555.085,-329.847;Inherit;False;Property;_OutsideColor;Outside Color;1;1;[HDR];Create;True;0;0;0;False;0;False;0,0,0,0;1,0,0.8873906,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SwitchByFaceNode;46;-1137.626,-253.917;Inherit;False;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;25;-431.6999,156.3513;Inherit;False;3;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0,0,0,0;False;2;FLOAT4;1,1,1,1;False;1;FLOAT4;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-519.6179,339.3004;Inherit;False;Constant;_Opacity;Opacity;7;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;45;-1028.027,1.86841;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;27;-2923.004,123.5454;Inherit;False;Property;_SpeedWave;Speed Wave;4;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;-217.1317,195.3553;Inherit;True;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleTimeNode;30;-2749.965,167.7203;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;47;-848.9663,-101.4066;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SinOpNode;32;-2563.49,167.4008;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;51;33.5035,-52.20913;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Custom/VFX/PotionShader;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Off;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Masked;0.5;True;True;0;False;TransparentCutout;;AlphaTest;All;14;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;33;0;29;0
WireConnection;33;1;28;0
WireConnection;36;0;33;0
WireConnection;36;1;31;0
WireConnection;35;0;33;0
WireConnection;37;0;36;0
WireConnection;37;1;35;0
WireConnection;38;0;34;0
WireConnection;39;0;37;0
WireConnection;39;1;38;0
WireConnection;41;0;39;0
WireConnection;41;1;40;0
WireConnection;42;0;41;0
WireConnection;49;0;42;0
WireConnection;49;1;42;0
WireConnection;48;0;49;0
WireConnection;46;0;44;0
WireConnection;46;1;43;0
WireConnection;25;0;41;0
WireConnection;45;0;48;0
WireConnection;26;0;25;0
WireConnection;26;1;24;0
WireConnection;30;0;27;0
WireConnection;47;0;46;0
WireConnection;47;1;43;0
WireConnection;47;2;45;0
WireConnection;32;0;30;0
WireConnection;51;2;47;0
WireConnection;51;10;26;0
ASEEND*/
//CHKSM=2CCE98C7FCCD0BE3825FA5A94F4CA2522D1413EF