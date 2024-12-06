using UnityEngine;

namespace Lab13_hkotanga{

public class TetrahedronGenerator : MonoBehaviour
{
    void Start()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        // Define vertices
        Vector3[] vertices = {
            new Vector3(0, 1, 0),
            new Vector3(-1, -1, -1),
            new Vector3(1, -1, -1),
            new Vector3(0, -1, 1)
        };

        // Define triangles
        int[] triangles = {
            0, 1, 2,
            0, 2, 3,
            0, 3, 1,
            1, 3, 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals(); // For proper shading

        meshFilter.mesh = mesh;
        Material lightingMaterial = Resources.Load<Material>("Light");
        meshRenderer.material = lightingMaterial;
    }
}
}