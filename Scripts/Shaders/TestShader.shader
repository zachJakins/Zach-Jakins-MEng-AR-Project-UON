// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Test Cutaway Shader" 
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
         CGPROGRAM 
          
         #pragma surface surf Standard vertex:vert
 
         struct vertexInput {
            float4 vertex : POSITION;
            float3 normal : NORMAL; // include normal info

         };
         struct vertexOutput {
            float4 pos : SV_POSITION;
            float4 posInObjectCoords : TEXCOORD0;
            float3 normal : TEXCOORD1; // pass normals along
            float4 worldPos : TEXCOORD2;

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
         
         void vert (inout appdata_full v, out vertexOutput output)    
         {
            

            output.pos = UnityObjectToClipPos(v.vertex);
            output.posInObjectCoords = v.vertex; 
            // Calculate the world position coordinates to pass to the fragment shader
            output.worldPos = mul(unity_ObjectToWorld, v.vertex);

            output.normal = v.normal;
         }

           void surf (vertexOutput input, inout SurfaceOutputStandard o) 
          {
            bool boolCut = evaluatePlane(_CutPlanePos, input.worldPos.xyz, _CutDir, _CutPlaneCentre);
             if (boolCut)
            {
               //discard; // drop the fragment if y coordinate > 0
            }
              o.Albedo *= _Color;
          }

         ENDCG  
      }
   Fallback "Diffuse"
 }


