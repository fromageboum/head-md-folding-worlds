using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    public float rotationSpeed = 1f; // Adjust this to control the speed of rotation
    public float maxRotationAngle = 45f; // Adjust this to control the maximum rotation angle in degrees

    private Quaternion initialRotation;
    private Quaternion targetRotation;

    private void Start()
    {
        initialRotation = transform.localRotation;
        GenerateRandomRotation();
    }

    private void Update()
    {
        // Rotate towards the target rotation over time
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if the target rotation has been reached, then generate a new random rotation
        if (Quaternion.Angle(transform.localRotation, targetRotation) < 0.1f)
        {
            GenerateRandomRotation();
        }
    }

    private void GenerateRandomRotation()
    {
        // Generate a random rotation within the specified angle range
        float randomAngle = Random.Range(-maxRotationAngle, maxRotationAngle);
        targetRotation = initialRotation * Quaternion.Euler(0f, randomAngle, 0f);
    }
}
