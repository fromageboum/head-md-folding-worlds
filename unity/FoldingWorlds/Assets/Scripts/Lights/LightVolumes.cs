using UnityEngine;

public class LightVolumes : MonoBehaviour
{
    [Header("Default to self")]
    public Transform lightVolumeRoot;

    private Camera mainCamera;

    private void Awake()
    {
        if (lightVolumeRoot == null) lightVolumeRoot = transform;
        mainCamera = Camera.main;
    }

    void LateUpdate()
    {
        // ensure all the light volume quads are camera-facing
        for (int i = 0; i < lightVolumeRoot.transform.childCount; i++)
        {
            lightVolumeRoot.transform.GetChild(i).rotation = Quaternion.LookRotation(
                (lightVolumeRoot.transform.GetChild(i).position - mainCamera.transform.position).normalized);
        }
    }
}
