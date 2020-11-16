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

            // Properties
            sampler2D _MainTex;
            sampler2D _PaperTex;
            sampler2D _ShadowTex;
            float _VRadius;
            float _VSoft;

            float4 frag(v2f_img input) : COLOR{
            float distFromCenter = distance(input.uv.xy,float2(0.5,0.5));
            float vingette = smoothstep(_VRadius, _VRadius -_VSoft, distFromCenter);
                // sample texture for color
            float4 base = tex2D(_MainTex, input.uv);
            base = saturate(vingette * base);
            base = saturate(base * tex2D(_ShadowTex, input.uv) * tex2D(_PaperTex, input.uv));
            return base;
           }
              ENDCG
        } }}