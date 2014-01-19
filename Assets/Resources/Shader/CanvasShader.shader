Shader "Koala/CanvasShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader 
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		ZWrite Off 
		Blend SrcAlpha OneMinusSrcAlpha 
		Cull Off 
		Fog { Mode Off }
		LOD 100

		Pass 
		{
			Lighting Off
			SetTexture [_MainTex]
		}
	}
}
