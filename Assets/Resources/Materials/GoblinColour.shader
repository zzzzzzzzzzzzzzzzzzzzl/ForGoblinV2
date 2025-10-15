Shader "Unlit/GoblinColour"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}

        _TargetSkinColour1("Targer A", Color) = (1.0, 1.0, 1.0, 1.0)
        _ReplaceSkinColour1("Replace A", Color) = (1.0, 1.0, 1.0, 1.0)

                _TargetSkinColour2("Target B", Color) = (1.0, 1.0, 1.0, 1.0)
        _ReplaceSkinColour2("Replace B", Color) = (1.0, 1.0, 1.0, 1.0)

                _TargetSkinColour3("Target C", Color) = (1.0, 1.0, 1.0, 1.0)
        _ReplaceSkinColour3("Replace C", Color) = (1.0, 1.0, 1.0, 1.0)

                _TargetSkinColour4("Target D", Color) = (1.0, 1.0, 1.0, 1.0)
        _ReplaceSkinColour4("Replace D", Color) = (1.0, 1.0, 1.0, 1.0)

                _TargetSkinColour5("Target E", Color) = (1.0, 1.0, 1.0, 1.0)
        _ReplaceSkinColour5("Replace E", Color) = (1.0, 1.0, 1.0, 1.0)

                _TargetSkinColour6("Target F", Color) = (1.0, 1.0, 1.0, 1.0)
        _ReplaceSkinColour6("Replace F", Color) = (1.0, 1.0, 1.0, 1.0)
        _SkinTolerance("Skin Tolerance", Float) = 0.1

        _Stencil("Stencil Reference", Float) = 0
        _StencilComp("Stencil Comparison", Float) = 8
        _StencilOp("Stencil Operation", Float) = 0
        _StencilReadMask("Stencil Read Mask", Float) = 255
        _StencilWriteMask("Stencil Write Mask", Float) = 255
        _ColorMask("Color Mask", Float) = 15
    }
    
    SubShader
    {
        
        Tags
        {
            "RenderType" = "Transparent"
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "PreviewType" = "Plane"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off
        Lighting Off

        Pass
        {

            Stencil {
                Ref[_Stencil]
                Comp[_StencilComp]
                Pass[_StencilOp]
                ReadMask[_StencilReadMask]
                WriteMask[_StencilWriteMask]
                }
        
                ColorMask[_ColorMask]
            Name "GoblinPass"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 2.0

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            Texture2D _MainTex;
            float4 _MainTex_ST;
            SamplerState point_clamp_sampler;
            SamplerState bilinear_clamp_sampler;

            float4 _TargetSkinColour1;
            float4 _ReplaceSkinColour1;
            float4 _TargetSkinColour2;
            float4 _ReplaceSkinColour2;
            float4 _TargetSkinColour3;
            float4 _ReplaceSkinColour3;
            float4 _TargetSkinColour4;
            float4 _ReplaceSkinColour4;
            float4 _TargetSkinColour5;
            float4 _ReplaceSkinColour5;
            float4 _TargetSkinColour6;
            float4 _ReplaceSkinColour6;

            float _SkinTolerance;

            float4 _TargetSkinColours[6];
            float4 _ReplaceSkinColours[6];
            

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float3 rgb2hsv(float3 c)
            {
                float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 p = lerp(float4(c.bg, K.wz), float4(c.gb, K.xy), step(c.b, c.g));
                float4 q = lerp(float4(p.xyw, c.r), float4(c.r, p.yzx), step(p.x, c.r));

                float d = q.x - min(q.w, q.y);
                float e = 1.0e-10;
                return float3(abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
            }
            float3 hsv2rgb(float3 c)
            {
                float4 K = float4(1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0);
                float3 p = abs(frac(c.xxx + K.xyz) * 6.0 - K.www);
                return c.z * lerp(K.xxx, clamp(p - K.xxx, 0.0, 1.0), c.y);
            }

            fixed4 frag(v2f i) : SV_Target
            {

                _TargetSkinColours[0] = _TargetSkinColour1;
                _TargetSkinColours[1] = _TargetSkinColour2;
                _TargetSkinColours[2] = _TargetSkinColour3;
                _TargetSkinColours[3] = _TargetSkinColour4;
                _TargetSkinColours[4] = _TargetSkinColour5;
                _TargetSkinColours[5] = _TargetSkinColour6;

                _ReplaceSkinColours[0] = _ReplaceSkinColour1;
                _ReplaceSkinColours[1] = _ReplaceSkinColour2;
                _ReplaceSkinColours[2] = _ReplaceSkinColour3;
                _ReplaceSkinColours[3] = _ReplaceSkinColour4;
                _ReplaceSkinColours[4] = _ReplaceSkinColour5;
                _ReplaceSkinColours[5] = _ReplaceSkinColour6;
                // sample the texture
                fixed4 rgb = _MainTex.Sample(bilinear_clamp_sampler, i.uv);
                float3 hsv = rgb2hsv(rgb);
                for(int idx=0;idx<6;idx++){

                float3 targetHsv = rgb2hsv(_TargetSkinColours[idx]);
                float3 replaceHsv = rgb2hsv(_ReplaceSkinColours[idx]);
                float resultV = targetHsv.z - hsv.z;
                if (abs(targetHsv.x - hsv.x) < _SkinTolerance && abs(targetHsv.y - hsv.y) < _SkinTolerance)
                {
                    hsv.x = replaceHsv.x;
                    hsv.y = replaceHsv.y;
                    hsv.z = replaceHsv.z - resultV;
                }

                }


                float3 final = hsv2rgb(hsv);

                return fixed4(final.xyz, rgb.a);
            }
            ENDCG
        }
    }
}

        /*GrabPass{"GoblinPass"}
        Pass
        {
            //BlendOp RevSub
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Cull Off
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 grabUv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            Texture2D GoblinPass;
            SamplerState trilinear_clamp_sampler;
            SamplerState point_clamp_sampler;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.grabUv = ComputeGrabScreenPos(o.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = GoblinPass.Sample(trilinear_clamp_sampler, i.grabUv);

                return fixed4(col.rgb, col.a);
            }
            ENDCG
        }
    }
}

            fixed4 frag(v2f i) : SV_Target
            {
                // sample the texture
                float4 rgb = tex2D(_MainTex, i.uv);
                float3 hsv = rgb2hsv(rgb);
                float3 upperHsv = rgb2hsv(_ReplaceColour);
                float value, minValue, overlay;

                if (hsv.z > 0.5)
                {
                    value = (1 - hsv.z) / 0.5;
                    minValue = hsv.z - (1 - hsv.z);
                    overlay = upperHsv.z * value + minValue;
                }
                else
                {
                    value = hsv.z / 0.5;
                    overlay = upperHsv.z * value;
                }

                return fixed4(overlay, overlay, overlay, rgb.a);
            }*/