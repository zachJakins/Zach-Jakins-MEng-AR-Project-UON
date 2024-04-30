// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Cutaway Shader" 
{
        Properties 
        {
        _CutDir("Cutaway Direction", Range(-1,1)) = 1.0
        _CutPlanePos("Cut Plane Normal Coordinate", Vector) = (0, 0, 0)
        _CutPlaneCentre("Centre of Model", Vector) = (0, 0, 0)
        _Color("Color", Color) = (0.25, 0.5, 0.5, 1.0)
        }
   SubShader 
   {
       Tags { "RenderType" = "Opaque" }

      Pass 
        {
         Cull Off
         Tags {"LightMode"="ForwardBase"}

         CGPROGRAM 
         #pragma vertex vert  
         #pragma fragment frag 
         #include "UnityCG.cginc" 
         #include "Lighting.cginc"
 
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float3 normal : normal; // pass normals along
            float4 worldPos : TEXCOORD0;

            float2 uv : UV;
            fixed4 diff : COLOR0;

         };

         half _CutPos;
         half _CutDir;
         float3 _CutPlaneCentre;
         float3 _CutPlanePos;  
         uniform float4 _Color; 


         bool evaluatePlane(float3 plane_xyz, float3 vertex_xyz, float cutDirection,float3 sphereCentre)
         {
             float x0 = sphereCentre.x;
             float y0 = sphereCentre.y;
             float z0 = sphereCentre.z;

             float a = plane_xyz.x;
             float b = plane_xyz.y;
             float c = plane_xyz.z;

             float3 d = plane_xyz;//normal vector

             float3 P = d + sphereCentre; //find world space location of plane
             
             float x = vertex_xyz.x;
             float y = vertex_xyz.y;
             float z = vertex_xyz.z;

             float f = d.x * (x-P.x) + d.y * (y-P.y) + d.z * (z-P.z);

             if(cutDirection < 0) f = -f;
            
             if(f > 0)
             {
                 return true;
             }
             else
             {
                 return false;
             }
         
         }
         
         vertexOutput vert(appdata_base input) 
         {
            vertexOutput output;

            output.pos = UnityObjectToClipPos(input.vertex);
            output.uv = input.texcoord;
            output.worldPos = mul(unity_ObjectToWorld, input.vertex);
            output.normal = input.normal;

            half3 worldNormal = UnityObjectToWorldNormal(input.normal);
            half nl = max(0, dot(worldNormal, _WorldSpaceLightPos0.xyz));
            output.diff = nl * _LightColor0;
            output.diff.rgb += ShadeSH9(half4(worldNormal,1));
            
            return output;
         }

         
         float4 frag(vertexOutput input) : COLOR 
         {
            bool boolCut = evaluatePlane(_CutPlanePos, input.worldPos.xyz, _CutDir, _CutPlaneCentre);
            if (boolCut)
            {
               discard; // drop the fragment if y coordinate > 0
            }
            return _Color * input.diff;
            
         }

         ENDCG  
      }
      Pass
        {
            Tags {"LightMode"="ShadowCaster"}

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_shadowcaster
            #include "UnityCG.cginc"

            struct v2f { 
                V2F_SHADOW_CASTER;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }

    }

   Fallback "Diffuse"
 }


