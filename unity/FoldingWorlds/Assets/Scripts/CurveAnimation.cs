using UnityEngine;
using UnityEngine.Animations;

public class CurveAnimation : MonoBehaviour
{
    public float duration = 1f;
    public float randomValueRange = 0.5f;

    private float timer = 1;
    private float currentValue;
    private float targetValue;
public AnimationCurve c;
float initialRotation;
    private void Start()
    {
        initialRotation = transform.localEulerAngles.y;

    }

    private void Update()
    {
        timer += Time.deltaTime;


        float timeRatio = timer / duration;
    float curveValue = c.Evaluate (timeRatio);
    float deltaRotation = curveValue * randomValueRange;

       
    
        
        transform.localRotation = Quaternion.Euler(0f, initialRotation + deltaRotation, 0f);
        
        }

   public void StartAnimation()
    {
        targetValue = Random.Range(-randomValueRange, randomValueRange);
        timer = 0;

    }
}