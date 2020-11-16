// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "SimpleOverlay/PostProcess" {
    Properties{
      _MainTex("Texture", 2D) = "white" {}
      _PaperTex("PaperTex", 2D) = "white" {}
      _ShadowTex("Shadow", 2D) = "white" {}
      _VRadius("Vignette Radius", Range(0.0,1.0)) = 1.0
      _VSoft("Vignette Softness", Range(0.0,1.0)) = 0.5
    }

        SubShader{
          Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc" // required for v2f_img


            //
            struct VertIn{
                float4 vertex : POSITION;
                float2 uv: TEXCOORD0;
                float4 ray : TEXCOORD1;
            };

            struct vertOut
            {
                float4 position : SV_POSITION;
                float2 uv : TEXCOORD0;
                float2 uv_depth : TEXCOORD1;
                float3 interpolatedRay: TEXCOORD2;
            };

            vertOut vert(VertIn vertIn)
            {
                vertOut o;
                o.uv = vertIn.uv.xy;
                o.position = mul(unity_ObjectToWorld, vertIn.vertex);
                o.uv_depth = vertIn.uv.xy;

                o.interpolatedRay = vertIn.ray;

                return o;
            }

            // Properties
            sampler2D _MainTex;
            sampler2D _PaperTex;
            sampler2D _ShadowTex;
            float4 _CameraWS;
            float _VRadius;
            float _VSoft;
            //float4 _WorldSpaceCameraPos;

            // Main problem is image effects do not have world position. We need to provide it
            float4 frag(vertOut fragIn) : COLOR
            {
                // Base Color
                float4 col = tex2D(_MainTex,fragIn.uv);

                // Paper Texture sample offset by camera position
                float2 camOffset = float2(_WorldSpaceCameraPos.x / 20, _WorldSpaceCameraPos.y/13);
                float4  paperCol = tex2D(_PaperTex, camOffset + fragIn.uv);

                // Vignette Color. As distance grows, darken the image;
                float distFromCenter = distance(fragIn.uv.xy, float2(0.5, 0.5));
                float vignette = smoothstep(_VRadius, _VRadius - _VSoft, distFromCenter);

                // Shadow Color sample
                float4 shadowCol = tex2D(_ShadowTex, fragIn.uv);

                // Combination
                return col * paperCol * vignette;
            }

            //
                /*
            float4 frag(v2f_img input) : COLOR{
            float distFromCenter = distance(input.uv.xy,float2(0.5,0.5));
            float vingette = smoothstep(_VRadius, _VRadius -_VSoft, distFromCenter);
                // sample texture for color
            float4 base = tex2D(_MainTex, input.uv);
            base = saturate(vingette * base);
            base = saturate(base * tex2D(_ShadowTex, input.uv) * tex2D(_PaperTex, input.uv));
            return base; */
           //}
              ENDCG
        } }}