Shader "Custom/ColliderOutlineShader"
{
    Properties
    {
        _Color ("Outline Color", Color) = (1,0,0,1)
        _MainTex ("Base (RGB)", 2D) = "white" { }
    }

    SubShader
    {
        Tags {"Queue"="Overlay" }
        Pass
        {
            Cull Front
            ZWrite On
            ZTest LEqual
            ColorMask RGB
            Blend SrcAlpha OneMinusSrcAlpha

            CGPROGRAM
            #pragma vertex vert
            #include "UnityCG.cginc"
            struct appdata
            {
                float4 vertex : POSITION;
            };
            struct v2f
            {
                float4 pos : POSITION;
            };
            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }
            ENDCG
        }
    }
}