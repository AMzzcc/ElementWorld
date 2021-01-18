Shader "MyShaders/Noise/45_Dissolve" {

    Properties {
        _BurnAmount ("Burn Amount", Range(0.0, 1.0)) = 0.0 //控制消融程度
        _LineWidth ("Burn Line Width", Range(0.0, 1.0)) = 0.1 //灼烧宽度
        _MainTex ("Base (RGB)", 2D) = "white" {} //物体本身的漫反射纹理
        _BumpMap ("Normal Map", 2D) = "bump" {} //物体本身的法线纹理
        _BurnFirstColor ("Burn First Color", Color) = (1, 0, 0, 1) //火焰边缘颜色1
        _BurnSecondColor ("Burn Second Color", Color) = (1, 0, 0, 1) //火焰边缘颜色2
        _BurnMap ("Burn Map", 2D) = "white" {} //噪声纹理
    }

    SubShader {
        Tags {
			"Queue" = "Transparent"
			"IgnoreProjector" = "True"
			"RenderType" = "Transparent"
			"PreviewType" = "Plane"
			"CanUseSpriteAtlas" = "True"
        }

			Cull Off
			Lighting Off
			ZWrite Off
			Blend One OneMinusSrcAlpha

        Pass {
            /*Tags {
                "LightMode" = "ForwardBase"
            }
            Cull Off*/

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fwdbase

            #include "Lighting.cginc"
            #include "AutoLight.cginc"

            float _BurnAmount;
            float _LineWidth;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            sampler2D _BumpMap;
            float4 _BumpMap_ST;
            fixed4 _BurnFirstColor;
            fixed4 _BurnSecondColor;
            sampler2D _BurnMap;
            float4 _BurnMap_ST;

            struct a2v {
                float4 vertex : POSITION;
                float4 texcoord : TEXCOORD0;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
            };

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uvMainTex : TEXCOORD0;
                float2 uvBumpMap : TEXCOORD1;
                float2 uvBurnMap : TEXCOORD2;
                float3 lightDir : TEXCOORD3;
                float3 worldPos : TEXCOORD4;
                SHADOW_COORDS(5)
            };

            v2f vert(a2v v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uvMainTex = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.uvBumpMap = TRANSFORM_TEX(v.texcoord, _BumpMap);
                o.uvBurnMap = TRANSFORM_TEX(v.texcoord, _BurnMap);

                TANGENT_SPACE_ROTATION;
                o.lightDir = mul(rotation, ObjSpaceLightDir(v.vertex)).xyz;
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                TRANSFER_SHADOW(o);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target {
                fixed3 burn = tex2D(_BurnMap, i.uvBurnMap).rgb;

                // _BurnAmount = _Time.x * 3;
                clip(burn.r - _BurnAmount);
				clip(tex2D(_MainTex, i.uvMainTex).a - 0.01);

                float3 tangentLightDir = normalize(i.lightDir);
                fixed3 tangentNormal = UnpackNormal(tex2D(_BumpMap, i.uvBumpMap));

                fixed3 albedo = tex2D(_MainTex, i.uvMainTex).rgb;
                //fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;

                //fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(tangentNormal, tangentLightDir));
				fixed3 diffuse = albedo;

                fixed t = 1 - smoothstep(0.0, _LineWidth, burn.r - _BurnAmount);
                fixed3 burnColor = lerp(_BurnFirstColor, _BurnSecondColor, t);
                // burnColor = pow(burnColor, 5);

                //UNITY_LIGHT_ATTENUATION(atten, i, i.worldPos);
                //fixed3 finalColor = lerp(ambient + diffuse * atten, burnColor, t * step(0.0001, _BurnAmount));
				fixed3 finalColor = lerp(albedo, burnColor, t * step(0.0001, _BurnAmount));

				return fixed4(finalColor, tex2D(_MainTex, i.uvMainTex).a);
                //return fixed4(finalColor, 1);
            }

            ENDCG
        }

        //Pass {
        //    Tags {
        //        "LightMode" = "ShadowCaster"
        //    }

        //    CGPROGRAM
        //    #pragma vertex vertShadow
        //    #pragma fragment fragShadow
        //    #pragma multi_compile_shadowcaster
        //    #include "UnityCG.cginc"

        //    fixed _BurnAmount;
        //    sampler2D _BurnMap;
        //    float4 _BurnMap_ST;

        //    struct v2fShadow {
        //        V2F_SHADOW_CASTER;
        //        float2 uvBurnMap : TEXCOORD1;
        //    };

        //    v2fShadow vertShadow(appdata_base v) {
        //        v2fShadow o;
        //        TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
        //        o.uvBurnMap = TRANSFORM_TEX(v.texcoord, _BurnMap);
        //        return o;
        //    }

        //    fixed4 fragShadow(v2fShadow i) : SV_Target {
        //        fixed3 burn = tex2D(_BurnMap, i.uvBurnMap).rgb;
        //        // _BurnAmount = _Time.x * 3;
        //        clip(burn.r - _BurnAmount);
        //        SHADOW_CASTER_FRAGMENT(i)
        //    }
        //    ENDCG
        //}
    }

    FallBack "Diffuse"
}