using UnityEngine;

public class PositionView : MonoBehaviour
{
    public float position = 0.0f;
    public float width = 1.0f;

    private void Update()
    {
        transform.localPosition = new Vector3(3.4f * (position + width / 2) - 1.7f, transform.localPosition.y, transform.localPosition.z);
        transform.localScale = new Vector3(3.4f * width, transform.localScale.y, transform.localScale.z);
    }
}
