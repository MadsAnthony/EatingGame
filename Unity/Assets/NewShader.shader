Shader "Unlit/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _vec2 ("Offset", Vector) = (0,0,0,0) 
        _Amount ("Amount", float) = 0
    }
    SubShader
    {
	    Tags {"Queue"="Transparent" "IgnoreProjector"="True"}
	    ZWrite Off
	    Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float2 _vec2;
            float _Amount;
            
            v2f vert (appdata v)
            {
                v2f o;
                if (v.vertex.x<0) {
                	//v.vertex.x += _Amount;
                }
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = TRANSFORM_TEX(v.vertex, _MainTex);
                //o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}