using UnityEngine;

public class StartDeactivated : MonoBehaviour
{
    void Awake()
    {
        gameObject.SetActive(false);
    }
}
