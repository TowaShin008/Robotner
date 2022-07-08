Shader "Custom/CameraShaderBraun"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_FrameRate("FrameRate", Range(0.1,30)) = 15
		_Frequency("Frequency", Range(0,1)) = 0.1

	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
		
			sampler2D _MainTex;
			float _FrameRate;
			float _Frequency;
			float2 barrel(float2 uv) {
				float s1 = .99, s2 = .125;
				float2 centre = 2. * uv - 1.;
				float barrel = min(1. - length(centre) * s1, 1.0) * s2;
				return uv - centre * barrel;
			}

			float2 CRT(float2 uv)
			{
				float2 nu = uv * 2. - 1.;
				float2 offset = abs(nu.yx) / float2(6., 4.);
				nu += nu * offset * offset;
				return nu;
			}

			float Scanline(float2 uv)
			{
				float scanline = clamp(0.95 + 0.05 * cos(3.14 * (uv.y + 0.008 * floor(_Time.y * 15.) / 15.) * 240.0 * 1.0), 0.0, 1.0);
				float grille = 0.85 + 0.15 * clamp(1.5 * cos(3.14 * uv.x * 640.0 * 1.0), 0.0, 1.0);
				return scanline * grille * 1.2;
			}
			//ランダムな値を返す
			float rand(float2 co) //引数はシード値と呼ばれる　同じ値を渡せば同じものを返す
			{
				return frac(sin(dot(co.xy, float2(12.9898, 78.233))) * 43758.5453);
			}

			//パーリンノイズ
			float perlinNoise(fixed2 st)
			{
				fixed2 p = floor(st);
				fixed2 f = frac(st);
				fixed2 u = f * f * (3.0 - 2.0 * f);

				float v00 = rand(p + fixed2(0, 0));
				float v10 = rand(p + fixed2(1, 0));
				float v01 = rand(p + fixed2(0, 1));
				float v11 = rand(p + fixed2(1, 1));

				return lerp(lerp(dot(v00, f - fixed2(0, 0)), dot(v10, f - fixed2(1, 0)), u.x),
					lerp(dot(v01, f - fixed2(0, 1)), dot(v11, f - fixed2(1, 1)), u.x),
					u.y) + 0.5f;
			}

			fixed4 frag(v2f_img i) : SV_Target
			{


				// barrel distortion
				float2 p = barrel(i.uv);

				//グリッチ
				float2 uv = p;
				//ポスタライズ 
				float posterize1 = floor(frac(perlinNoise(_SinTime) * 10) / (1 / _FrameRate)) * (1 / _FrameRate);
				float posterize2 = floor(frac(perlinNoise(_SinTime) * 5) / (1 / _FrameRate)) * (1 / _FrameRate);
				//uv.x方向のノイズ計算 -0.1 < noiseX < 0.1
				float noiseX = (2.0 * rand(posterize1) - 0.5) * 0.1;
				//step(t,x) はxがtより大きい場合1を返す
				float frequency = step(rand(posterize2), _Frequency);
				noiseX *= frequency;
				//uv.y方向のノイズ計算 -1 < noiseY < 1
				float noiseY = 2.0 * rand(posterize1) - 0.5;
				//グリッチの高さの補間値計算 どの高さに出現するかは時間変化でランダム
				float glitchLine1 = step(uv.y - noiseY, rand(noiseY));
				float glitchLine2 = step(uv.y + noiseY, noiseY);
				float glitch = saturate(glitchLine1 - glitchLine2);
				//速度調整
				uv.x = lerp(uv.x, uv.x + noiseX, glitch);


				fixed4 col = tex2D(_MainTex, uv);
				
				// color grading
				col.rgb *= float3(1.25, 0.95, 0.7);
				col.rgb = clamp(col.rgb, 0.0, 1.0);
				col.rgb = col.rgb * col.rgb * (3.0 - 2.0 * col.rgb);
				//col.rgb = 0.5 + 0.5 * col.rgb;
				
				// scanline
				col.rgb *= Scanline(i.uv);
				
				// crt monitor
				float2 crt = CRT(i.uv);
				crt = abs(crt);
				crt = pow(crt, 15.);
				col.rgb = lerp(col.rgb, (.0).xxx, crt.x + crt.y);

				// gammma correction
				col.rgb = pow(col.rgb, (.4545).xxx);
				
				return col;
			}
			ENDCG
		}
	}}
