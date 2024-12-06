Shader "Custom/LightingShader"
{
    Properties
    {
        _MainColor ("Main Color", Color) = (255,1,1,255)
        _SpecularColor ("Specular Color", Color) = (255,255,0,255)

    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
                float3 worldPos : TEXCOORD0;
                float3 normal : TEXCOORD1;
            };

            float4 _LightPosition;  // Position of the point light
            float4 _MainColor;      // Object base color
            float4 _SpecularColor;  // Specular color
            float _Shininess;       // Specular shininess factor
            float _AmbientIntensity;
            float _DiffuseIntensity;

            v2f vert (appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
                o.normal = normalize(mul((float3x3)unity_ObjectToWorld, v.normal));
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                // Light direction
                float3 lightDir = normalize(_LightPosition.xyz - i.worldPos);

                // Ambient component
                float3 ambient = _AmbientIntensity * _MainColor.rgb;

                // Diffuse component
                float3 diffuse = max(0, dot(i.normal, lightDir));
                diffuse = _DiffuseIntensity *diffuse * _MainColor.rgb;

                // Specular component
                float3 viewDir = normalize(-i.worldPos); // Assuming camera at origin
                float3 reflectDir = reflect(-lightDir, i.normal);
                float spec = pow(max(0, dot(viewDir, reflectDir)), _Shininess);
                float3 specular = spec * _SpecularColor.rgb;

                // Combine components
                float3 finalColor = ambient + diffuse + specular;
                return float4(finalColor, 1.0);
            }
            ENDCG
        }
    }
}
