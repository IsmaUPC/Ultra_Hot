Shader "Custom/Dissolve"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _FlowMap ("Flow Map", 2D) = "black" {}
        _DissolveTexture("Dissolve Map", 2D) = "black" {}
        _Expand("Expand", float) = 1
        _Weight("Weight", Range(0,1)) = 0
        _Direction("Direction", Vector) = (0, 0, 0, 0)
        _R("Radius", float) = .1
    }

    CGINCLUDE

    #include "UnityCG.cginc"
    #include "Lighting.cginc"

    sampler2D _MainTex;
    float4 _MainTex_ST;
    sampler2D _FlowMap;
    sampler2D _DissolveTexture;
    float4 _FlowMap_ST;
    float _Expand;
    float _Weight;
    float4 _Direction;
    float _R;


    struct appdata {
        float4 vertex : POSITION;
        float3 normal : NORMAL;
        float2 uv : TEXCOORD0;
    };

    struct v2g {
        float4 position : SV_POSITION;
        float2 uv : TEXCOORD0;
        float3 normal : NORMAL;
        float3 worldPos : TEXCOORD1;
    };

    struct g2f {
        float4 worldPos : SV_POSITION;
        float2 uv : TEXCOORD0;
        fixed4 color : COLOR;
        float3 normal : NORMAL;
    };

    v2g vert(appdata v) {
        v2g o;
        o.position = v.vertex;
        o.uv = v.uv;
        o.normal = UnityObjectToWorldNormal(v.normal);
        o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
        return o;
    }

    float random(float2 uv) {
        return frac(sin(dot(uv, float2(12.9898, 78.233))) * 43758.5453123);
    }

    float remap(float value, float from1, float to1, float from2, float to2) {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    //float randomMapped(float2 uv, float from, float to) {
    //    return remap(random(uv), 0, 1, from, to);
    //}

    float4 remapFlowTexture(float4 tex) {
        return float4(
            remap(tex.x, 0, 1, -1, 1),
            remap(tex.y, 0, 1, -1, 1),
            0,
            remap(tex.w, 0, 1, -1, 1)
            );
    }

    [maxvertexcount(3)]
    void geom(triangle v2g IN[3], inout TriangleStream<g2f> triStream)
    {
        float3 avgPos = (IN[0].position + IN[1].position + IN[2].position) / 3;
        float2 avgUV = (IN[0].uv + IN[1].uv + IN[2].uv) / 3;
        float3 avgNormal = (IN[0].normal + IN[1].normal + IN[2].normal) / 3;


        float2 flowUV = TRANSFORM_TEX(mul(unity_ObjectToWorld, avgPos).xz, _FlowMap);
        float4 flowVector = remapFlowTexture(tex2Dlod(_FlowMap, float4(flowUV, 0, 0)));

        for (int i = 0; i < 3; ++i)
        {
            float3 targetPos = IN[i].position + _Direction;
            targetPos += flowVector.xyz * _Expand;

            float3 p = IN[i].position;
            p = lerp(IN[i].position, targetPos, _Weight);

            g2f v;
            v.worldPos = UnityObjectToClipPos(p);
            //v.uv = MultiplyUV(UNITY_MATRIX_TEXTURE0, float2(1.0f, 1.0f));
            v.uv = avgUV;
            v.color = float4(1, 1, 1, 1);
            v.normal = avgNormal;
            triStream.Append(v);
        }

    }

    ENDCG

    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
    
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma geometry geom
            // make fog work
            #pragma multi_compile_fog
    
            #include "UnityCG.cginc"

            fixed4 frag (g2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                return col;
            }
            ENDCG
        }
    }
}
