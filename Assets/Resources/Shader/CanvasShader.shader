Shader "Koala/CanvasShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}

	}
	SubShader 
	{
		Tags {
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent"
		}


		Cull Off
		Lighting Off
		ZWrite Off
		Fog { Mode Off }
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			SetTexture [_MainTex]
		}
	}
}
