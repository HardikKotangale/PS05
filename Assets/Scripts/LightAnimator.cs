using UnityEngine;
namespace Lab13_hkotanga{
public class LightAnimator : MonoBehaviour
{
    public Transform lightObject; // The light's position placeholder
    public float radius = 5f;
    public float speed = 1f;

    private Vector3 center = Vector3.zero; // The center of the animation circle
    public float angle = 20f; // Initial angle

    private bool isAnimating = true; // Control animation state

    void Update()
    {
        if (isAnimating)
        {
            angle += speed * Time.deltaTime;
            float x = center.x + radius * Mathf.Cos(angle);
            float z = center.z + radius * Mathf.Sin(angle);
            float y = center.y + Mathf.Sin(angle * 0.5f) * 2f; // Oscillating height
            lightObject.position = new Vector3(x, y, z);
        }

        // Update light position in the shader
        Shader.SetGlobalVector("_LightPosition", lightObject.position);
    }

    // Public method to start animation
    public void StartAnimation()
    {
        isAnimating = true;
    }

    // Public method to stop animation
    public void StopAnimation()
    {
        isAnimating = false;
    }
}
}