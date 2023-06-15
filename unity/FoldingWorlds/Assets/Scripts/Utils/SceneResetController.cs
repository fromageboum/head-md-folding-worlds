using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneResetController : MonoBehaviour
{
    public InputActionReference resetButtonRef;
    private float lastClickTime = 0;
    private float catchTime = 0.2f; // time window for double click, adjust as needed

    private void OnEnable()
    {
        resetButtonRef.action.started += CheckDoubleClick;
    }

    private void OnDisable()
    {
        resetButtonRef.action.started -= CheckDoubleClick;
    }

    private void CheckDoubleClick(InputAction.CallbackContext callbackContext)
    {
        if (Time.time - lastClickTime < catchTime)
        {
            // Double-click happened, reload the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        lastClickTime = Time.time;
    }
}