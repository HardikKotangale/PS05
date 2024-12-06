using UnityEngine;
using UnityEngine.UI;


namespace Lab13_hkotanga{
public class LightController : MonoBehaviour
{
    public GameObject lightObject;
    public Slider ambientSlider; // Reference to the UI slider

    public Slider shininessSlider; // Reference to the UI slider
    public Slider diffuseSlider; // Reference to the UI slider
    private void Start()
    {
        // Set an initial value for the global ambient intensity
        float initialIntensity = 0.2f;
        Shader.SetGlobalFloat("_AmbientIntensity", initialIntensity);

        // Sync slider value with the initial intensity
        if (ambientSlider != null)
        {
            ambientSlider.value = initialIntensity;
            ambientSlider.onValueChanged.AddListener(UpdateAmbientIntensity);
        }
        // Sync slider value with the initial intensity
        if (shininessSlider != null)
        {
            shininessSlider.value = 0;
            shininessSlider.onValueChanged.AddListener(UpdateShininessIntensity);
        }
        // Sync slider value with the initial intensity
        if (diffuseSlider != null)
        {
            diffuseSlider.value = 0;
            diffuseSlider.onValueChanged.AddListener(UpdateDiffuseIntensity);
        }
    }

    public void TurnLightOn()
    {
        Shader.SetGlobalFloat("_AmbientIntensity", ambientSlider.value); // Use current slider value
        lightObject.SetActive(true);
    }

    public void TurnLightOff()
    {
        Shader.SetGlobalFloat("_AmbientIntensity", 0f); // Set to zero for no ambient light
        Shader.SetGlobalFloat("_DiffuseIntensity", 0f); // Set to zero for no ambient light
        Shader.SetGlobalFloat("_Shininess", 0f); // Set to zero for no ambient light
        lightObject.SetActive(false);
    }

    public void UpdateAmbientIntensity(float intensity)
    {
        // Update the ambient intensity in the shader
        Shader.SetGlobalFloat("_AmbientIntensity", intensity);
    }
    public void UpdateDiffuseIntensity(float intensity)
    {
        // Update the ambient intensity in the shader
        Shader.SetGlobalFloat("_DiffuseIntensity", intensity);
    }

    public void UpdateShininessIntensity(float intensity)
    {
        // Update the ambient intensity in the shader
        Shader.SetGlobalFloat("_Shininess", intensity);
    }
}
}