using System.Collections;
using UnityEngine;

public class SceneLightDimmer : MonoBehaviour
{
    [Header("Dimms the Passthrough and directional light intensity on enable")]
    public Light sceneLight;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnEnable()
    {
        StartCoroutine(FadeLighting(new Color(0, 0, 0, 0.95f), 0.0f, 0.25f));
    }

    public void OnDisable()
    {
        StopAllCoroutines();

        mainCamera.backgroundColor = Color.clear;
        if (sceneLight) sceneLight.intensity = 1.0f;
        
        // No Fade On Disable
        // StartCoroutine(FadeLighting(Color.clear, 1.0f, 0.25f));
    }

    IEnumerator FadeLighting(Color newColor, float sceneLightIntensity, float fadeTime)
    {
        float timer = 0.0f;
        Color currentColor = Camera.main.backgroundColor;
        float currentLight = sceneLight ? sceneLight.intensity : 0;
        bool hasSceneLight = sceneLight;
        while (timer <= fadeTime)
        {
            timer += Time.deltaTime;
            float normTimer = Mathf.Clamp01(timer / fadeTime);
            mainCamera.backgroundColor = Color.Lerp(currentColor, newColor, normTimer);
            if (hasSceneLight) sceneLight.intensity = Mathf.Lerp(currentLight, sceneLightIntensity, normTimer);
            yield return null;
        }

        if (hasSceneLight)
        {
            if (sceneLight.intensity < 0.0001f) sceneLight.enabled = false;
        }
    }
}
