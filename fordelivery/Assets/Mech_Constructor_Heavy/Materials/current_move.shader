﻿Shader "Custom/outline_glow" {
Properties {  
    _Color ("Main Color", Color) = (1,1,1,1)  
    _SpecColor ("Specular Color", Color) = (0.5, 0.5, 0.5, 1)  
    _Shininess ("Shininess", Range (0.03, 1)) = 0.8
    _MainTex ("Base (RGB) Gloss (A)", 2D) = "black" {}  
    _BumpMap ("Normalmap", 2D) = "bump" {}  

    _RimColor ("Rim Color", Color) = (150,0,0,0.0)  
     
    _RimPower ("Rim Power", Range(0.5,8.0)) = 8.0  
}  
SubShader {   
    Tags { "RenderType"="Opaque" }  
    LOD 400  
  
CGPROGRAM  
#pragma surface surf BlinnPhong  
#pragma target 3.0  
  
sampler2D _MainTex;  
sampler2D _BumpMap;  
fixed4 _Color;  
half _Shininess;  
float4 _RimColor;  
float _RimPower;  
  
struct Input {  
    float2 uv_MainTex;  
    float2 uv_BumpMap;  
    float3 viewDir;  
};  
  
void surf (Input IN, inout SurfaceOutput o) {  
      
    fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);  
    o.Albedo = tex.rgb * _Color.rgb;  
    o.Gloss = tex.a;  
    o.Alpha = tex.a * _Color.a;  
    o.Specular = _Shininess;  
    o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_BumpMap));  
      
    half rim = 1 - saturate(dot (normalize(IN.viewDir), o.Normal));  
    o.Emission = _RimColor.rgb * pow (rim, _RimPower);  
}  
ENDCG  
      
}  
  
FallBack "Specular"  
}  

