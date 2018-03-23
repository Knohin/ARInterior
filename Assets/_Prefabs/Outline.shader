Shader "Custom/Outline_Fresnel" {
	Properties{
		_MainTex("Albedo", 2D) = "white" {}
	_BumpMap("BumpMap", 2D) = "bump" {}
	_OutlineColor("OutlineColor", Color) = (1,1,1,1)
		_Outline("Outline", Range(0.1, 0.4)) = 0.2
	}

		SubShader{
		Tags{ "RenderType" = "Opaque" }
		Cull back
		CGPROGRAM
#pragma surface surf Toon 

		fixed4 _Color;
	sampler2D _MainTex;
	sampler2D _BumpMap;
	struct Input {
		float2 uv_MainTex;
		float2 uv_BumpMap;
	};

	fixed4 _OutlineColor;
	fixed _Outline;

	void surf(Input In, inout SurfaceOutput o)
	{
		fixed4 c = tex2D(_MainTex, In.uv_MainTex);
		o.Albedo = c.rgb;
		o.Normal = UnpackNormal(tex2D(_BumpMap, In.uv_BumpMap));
		o.Alpha = c.a;
	}

	fixed4 LightingToon(SurfaceOutput s, fixed3 lightDir, fixed3 viewDir, fixed atten)
	{
		float halfLambert = dot(s.Normal, lightDir) * 0.5 + 0.5;
		if (halfLambert > 0.7) {
			halfLambert = 1;
		}
		else {
			halfLambert = 0.3;
		}

		fixed4 final;
		float rim = abs(dot(s.Normal, viewDir));
		if (rim > _Outline) {
			rim = 1;
			final.rgb = s.Albedo;
		}
		else {
			rim = -1;
			final.rgb = _OutlineColor.rgb * rim;
		}

		final.rgb = final.rgb * halfLambert *_LightColor0.rgb * rim;
		final.a = s.Alpha;
		return final;
	}
	ENDCG
	}
		FallBack "Diffuse"
}