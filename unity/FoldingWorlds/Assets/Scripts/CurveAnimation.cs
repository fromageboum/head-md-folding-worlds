using UnityEngine;
using UnityEngine.Animations;

public class CurveAnimation : MonoBehaviour
{
    public float duration = 1f;
    public float randomValueRange = 0.5f;

    private float timer;
    private float currentValue;
    private float targetValue;
public AnimationCurve c;
float initialRotation;
    private void Start()
    {
        Debug.Log("hello");
        initialRotation = transform.eulerAngles.y;

    }

    private void Update()
    {
        timer += Time.deltaTime;


        float timeRatio = timer / duration;
    float curveValue = c.Evaluate (timeRatio);
    float deltaRotation = curveValue * randomValueRange;

       
    
        
        transform.rotation = Quaternion.Euler(0f, initialRotation + deltaRotation, 0f);
        
        }

   public void StartAnimation()
    {
        targetValue = Random.Range(-randomValueRange, randomValueRange);
        timer = 0;

    }
}