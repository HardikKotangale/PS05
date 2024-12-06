using UnityEngine;
namespace Lab13_hkotanga{
public class WaveSurfaceGenerator : MonoBehaviour
{
    public int width = 50;
    public int height = 50;
    public float waveHeight = 1f;
    public float waveFrequency = 2f;

    void Start()
    {
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[(width + 1) * (height + 1)];
        int[] triangles = new int[width * height * 6];

        for (int z = 0; z <= height; z++)
        {
            for (int x = 0; x <= width; x++)
            {
                float y = Mathf.Sin(x * waveFrequency) * waveHeight;
                vertices[z * (width + 1) + x] = new Vector3(x, y, z);
            }
        }

        int index = 0;
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                int start = z * (width + 1) + x;
                triangles[index++] = start;
                triangles[index++] = start + width + 1;
                triangles[index++] = start + 1;

                triangles[index++] = start + 1;
                triangles[index++] = start + width + 1;
                triangles[index++] = start + width + 2;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;

        mesh.RecalculateNormals();

        meshFilter.mesh = mesh;
        meshRenderer.material = new Material(Shader.Find("Standard"));
        //Material lightingMaterial = Resources.Load<Material>("Light");
        //meshRenderer.material = lightingMaterial;

    }
}
}
