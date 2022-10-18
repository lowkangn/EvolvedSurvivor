using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TeamOne.EvolvedSurvivor
{
    public class RadarChartUI : MonoBehaviour
    {
        [SerializeField] private CanvasRenderer radarMeshCanvasRenderer;
        [SerializeField] private Texture2D radarTexture2D;
        [SerializeField] private Material radarMaterial;

        public void UpdateVisual(TraitChart traitChart)
        {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[6];
            Vector2[] uv = new Vector2[6];
            int[] triangles = new int[3 * 5]; // 3 vertices for 5 triangles

            float angleIncrement = 360f / 5;
            float radarChartSize = 120f; // Length from origin point to a vertex

            // Set vertex according to their angle, size of the background image, and the value ratio
            Vector3 damageVertex = Quaternion.Euler(0, 0, -angleIncrement * 0) * Vector3.up * radarChartSize * traitChart.DamageRatio;
            Vector3 uptimeVertex = Quaternion.Euler(0, 0, -angleIncrement * 1) * Vector3.up * radarChartSize * traitChart.UptimeRatio;
            Vector3 aoeVertex = Quaternion.Euler(0, 0, -angleIncrement * 2) * Vector3.up * radarChartSize * traitChart.AoeRatio;
            Vector3 quantityVertex = Quaternion.Euler(0, 0, -angleIncrement * 3) * Vector3.up * radarChartSize * traitChart.QuantityRatio;
            Vector3 utilityVertex = Quaternion.Euler(0, 0, -angleIncrement * 4) * Vector3.up * radarChartSize * traitChart.UtilityRatio;

            int damageVertexIndex = 1;
            int uptimeVertexIndex = 2;
            int aoeVertexIndex = 3;
            int quantityVertexIndex = 4;
            int utilityVertexIndex = 5;

            // Vertices for each trait
            vertices[0] = Vector3.zero;
            vertices[damageVertexIndex] = damageVertex;
            vertices[uptimeVertexIndex] = uptimeVertex;
            vertices[aoeVertexIndex] = aoeVertex;
            vertices[quantityVertexIndex] = quantityVertex;
            vertices[utilityVertexIndex] = utilityVertex;

            // Triangle between Damage and Uptime
            triangles[0] = 0;
            triangles[1] = damageVertexIndex;
            triangles[2] = uptimeVertexIndex;

            // Triangle between Uptime and Aoe
            triangles[3] = 0; 
            triangles[4] = uptimeVertexIndex; 
            triangles[5] = aoeVertexIndex; 

            // Triangle between Aoe and Quantity
            triangles[6] = 0; 
            triangles[7] = aoeVertexIndex; 
            triangles[8] = quantityVertexIndex; 

            // Triangle between Quantity and Utility
            triangles[9] = 0; 
            triangles[10] = quantityVertexIndex; 
            triangles[11] = utilityVertexIndex; 

            // Triangle between Utility and Damage
            triangles[12] = 0; 
            triangles[13] = utilityVertexIndex; 
            triangles[14] = damageVertexIndex; 

            // For mesh texture 
            uv[0] = Vector2.zero;
            uv[damageVertexIndex] = Vector2.one;
            uv[uptimeVertexIndex] = Vector2.one;
            uv[aoeVertexIndex] = Vector2.one;
            uv[quantityVertexIndex] = Vector2.one;
            uv[utilityVertexIndex] = Vector2.one;         

            // Set mesh
            mesh.vertices = vertices;
            mesh.uv = uv;
            mesh.triangles = triangles;

            radarMeshCanvasRenderer.SetMesh(mesh);
            radarMeshCanvasRenderer.SetMaterial(radarMaterial, radarTexture2D);
        }

        public void ClearVisual() 
        {
            // This method should remove the mesh from the radarChart 
            radarMeshCanvasRenderer.SetMesh(null);
        }
    }
}
